// This file is part of the codific567 CLI distribution (https://codific.com).
// Copyright (C) 2019 Codific
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Codific.Mvc567.Cli.Commands.Utils;
using Codific.Mvc567.Cli.Templates;
using Codific.Mvc567.Cli.Templates.EntityDto;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Codific.Mvc567.Cli.Commands.CommandFactories
{
    internal class EntityDtoFactory : CommandFactory
    {
        private static readonly string[] ExcludedFiles = { "AbstractEntityContext", "IReferenceModel" };

        internal override void Execute(Dictionary<string, string> parameters)
        {
            if (parameters.ContainsKey(CommandParameters.Entity) && !string.IsNullOrWhiteSpace(parameters[CommandParameters.Entity]))
            {
                this.LoadCliConfig();
                if (this.CliConfig == null)
                {
                    Console.WriteLine("This command requires cli-config.json");
                    return;
                }

                string entityName = parameters[CommandParameters.Entity];
                string entityClassString = this.GetEntityClassString(entityName, entityDirectory: this.CliConfig.EntityDirectory);

                if (string.IsNullOrEmpty(entityClassString))
                {
                    Console.WriteLine("Selected entity does not exist.");
                    return;
                }

                this.SaveDtoFile($"{entityName}Dto.cs", this.GetDtoFileContents(entityName, entityClassString));
            }
            else if (parameters.ContainsKey(CommandParameters.AllEntities) && !string.IsNullOrWhiteSpace(parameters[CommandParameters.AllEntities]))
            {
                this.LoadCliConfig();
                if (this.CliConfig == null)
                {
                    Console.WriteLine("This command requires cli-config.json");
                    return;
                }

                string[] files = Directory.GetFiles(this.CliConfig.EntityDirectory, "*.cs", SearchOption.TopDirectoryOnly);
                foreach (string fileName in files)
                {
                    string entityName = Path.GetFileName(fileName).Replace(".cs", string.Empty);
                    if (!ExcludedFiles.Contains(entityName))
                    {
                        string entityClassString = this.GetEntityClassString(entityName, this.CliConfig.EntityDirectory);
                        this.SaveDtoFile($"{entityName}Dto.cs", this.GetDtoFileContents(entityName, entityClassString), true);
                    }
                }

                Console.WriteLine("All DTOs are created successfully!");
            }
            else
            {
                Console.WriteLine("Entity class name is required.");
            }
        }

        protected override void InitSessionDictionary(params object[] args)
        {
            this.SessionDictionary = new Dictionary<string, object>
            {
                { "EntityName", args[0] },
                { "EntityNamespace", args[1] },
                { "DtoNamespace", args[2] },
                { "Properties", args[3] },
                { "EnumNamespace", args[1] + ".Enums" },
            };
        }

        private string GetDtoFileContents(string entityName, string entityClassString)
        {
            var classProperties = this.GetClassProperties(entityClassString);
            var classDtoProperties = this.MapEntitiesToEntitiesDtos(classProperties);

            this.InitSessionDictionary(entityName, this.CliConfig.EntityNamespace, this.CliConfig.DtoEntityNamespace, classDtoProperties);

            string dtoContent = TemplateRenderer.RenderTemplate(typeof(EntityDtoTemplate), this.SessionDictionary);
            string[] dtoContentArray = dtoContent.Split(Environment.NewLine);
            List<string> dtoContentList = new List<string>();
            for (int i = 0; i < dtoContentArray.Length; i++)
            {
                if (i > 0 && string.IsNullOrWhiteSpace(dtoContentArray[i]) && string.IsNullOrWhiteSpace(dtoContentArray[i - 1]))
                {
                    continue;
                }

                dtoContentList.Add(dtoContentArray[i]);
            }

            return string.Join(Environment.NewLine, dtoContentList);
        }

        private void SaveDtoFile(string name, string content, bool suppressExistingFileMessage = false)
        {
            string directory = Path.Combine(this.CliConfigDirectory, this.CliConfig.DtoEntityDirectory);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string filePath = Path.Combine(directory, name);

            if (!suppressExistingFileMessage)
            {
                if (File.Exists(filePath))
                {
                    Console.WriteLine("The DTO you try to create already exists. The process has been terminated.");
                    return;
                }
                else
                {
                    File.WriteAllText(filePath, content);
                    Console.WriteLine("DTO has been created successfully!");
                }
            }
            else if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, content);
            }
        }

        private string GetEntityClassString(string entityName, string entityDirectory)
        {
            string filePath = Path.Combine("Entities", $"{entityName}.cs");
            if (!string.IsNullOrWhiteSpace(entityDirectory))
            {
                filePath = Path.Combine(this.CliConfigDirectory, entityDirectory, $"{entityName}.cs");
            }

            string entityClassString = null;
            if (File.Exists(filePath))
            {
                entityClassString = File.ReadAllText(filePath);
            }

            return entityClassString;
        }

        private List<EntityClassProperty> GetClassProperties(string classString)
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(classString);
            CompilationUnitSyntax root = tree.GetCompilationUnitRoot();

            SyntaxList<MemberDeclarationSyntax> propertyDeclarations = this.ExtractPropertiesDeclarations(root);

            List<EntityClassProperty> propertiesResult = new List<EntityClassProperty>();

            if (propertyDeclarations.Count > 0)
            {
                foreach (var propertyDeclaration in propertyDeclarations)
                {
                    EntityClassProperty propertyItem;
                    try
                    {
                        propertyItem = this.ParseEntityClassProperty((PropertyDeclarationSyntax)propertyDeclaration);
                    }
                    catch (Exception)
                    {
                        propertyItem = null;
                    }

                    if (propertyItem != null)
                    {
                        propertiesResult.Add(propertyItem);
                    }
                }
            }

            return propertiesResult;
        }

        private SyntaxList<MemberDeclarationSyntax> ExtractPropertiesDeclarations(CompilationUnitSyntax root)
        {
            SyntaxList<MemberDeclarationSyntax> propertyDeclarations = default(SyntaxList<MemberDeclarationSyntax>);
            MemberDeclarationSyntax targetMember = root.Members.FirstOrDefault();
            while (targetMember != null)
            {
                var currentDeclarationType = targetMember.GetType();
                if (currentDeclarationType == typeof(ClassDeclarationSyntax))
                {
                    propertyDeclarations = ((ClassDeclarationSyntax)targetMember).Members;
                    break;
                }

                if (currentDeclarationType == typeof(NamespaceDeclarationSyntax))
                {
                    targetMember = ((NamespaceDeclarationSyntax)targetMember).Members.FirstOrDefault();
                    continue;
                }

                break;
            }

            return propertyDeclarations;
        }

        private EntityClassProperty ParseEntityClassProperty(PropertyDeclarationSyntax propertyDeclaration)
        {
            try
            {
                var propertyModifiers = propertyDeclaration.Modifiers;
                bool hasVirtualModifier = false;
                foreach (var modifier in propertyModifiers)
                {
                    hasVirtualModifier = modifier.ValueText.ToLower().Contains("virtual");
                }

                var propertyAttributes = propertyDeclaration.AttributeLists;
                bool hasForeignKey = false;
                bool hasRequiredModifier = false;
                foreach (var attribute in propertyAttributes)
                {
                    hasForeignKey = attribute.ToString().Contains("ForeignKey");
                    hasRequiredModifier = attribute.ToString().Contains("Required");
                }

                bool isCustomType = false;
                string castedPropertyDeclarationTypeValue = this.GetPropertyDeclarationTypeValue(propertyDeclaration, out isCustomType);

                var propertyItem = new EntityClassProperty
                {
                    Name = propertyDeclaration.Identifier.ValueText,
                    Type = castedPropertyDeclarationTypeValue,
                    IsForeignKey = castedPropertyDeclarationTypeValue.Contains("Guid"),
                    IsEnum = !hasVirtualModifier && !hasForeignKey && isCustomType,
                    IsRequired = hasRequiredModifier,
                    IsCustomType = isCustomType,
                };

                return propertyItem;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private string GetPropertyDeclarationTypeValue(PropertyDeclarationSyntax propertyDeclaration, out bool isCustomType)
        {
            isCustomType = false;
            string propertyDeclarationTypeValue = null;
            if (propertyDeclaration.Type.GetType() == typeof(PredefinedTypeSyntax))
            {
                propertyDeclarationTypeValue = ((PredefinedTypeSyntax)propertyDeclaration.Type).Keyword.ValueText;
            }
            else if (propertyDeclaration.Type.GetType() == typeof(NullableTypeSyntax))
            {
                var elementType = ((NullableTypeSyntax)propertyDeclaration.Type).ElementType;
                if (elementType.GetType() == typeof(IdentifierNameSyntax))
                {
                    propertyDeclarationTypeValue = ((IdentifierNameSyntax)elementType).Identifier.ValueText + "?";
                }
                else if (elementType.GetType() == typeof(PredefinedTypeSyntax))
                {
                    propertyDeclarationTypeValue = ((PredefinedTypeSyntax)elementType).Keyword.ValueText + "?";
                }
            }
            else if (propertyDeclaration.Type.GetType() == typeof(IdentifierNameSyntax))
            {
                propertyDeclarationTypeValue = ((IdentifierNameSyntax)propertyDeclaration.Type).Identifier.ValueText;
                string[] defaultTypes = new string[] { "Guid", "DateTime", "TimeSpan" };
                if (!defaultTypes.Contains(propertyDeclarationTypeValue))
                {
                    isCustomType = true;
                }
            }

            return propertyDeclarationTypeValue;
        }

        private List<EntityDtoClassProperty> MapEntitiesToEntitiesDtos(List<EntityClassProperty> properties)
        {
            var resultList = new List<EntityDtoClassProperty>();
            int index = 1;
            foreach (var property in properties)
            {
                var entityProperty = new EntityDtoClassProperty(property, index);
                if (property.IsForeignKey)
                {
                    entityProperty.RelatedProperty = properties.Where(x => property.Name.Contains(x.Name) && !x.Name.EndsWith("Id")).FirstOrDefault();
                }

                resultList.Add(entityProperty);
                index++;
            }

            return resultList;
        }
    }
}