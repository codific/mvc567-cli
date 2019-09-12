// This file is part of the mvc567 CLI distribution (https://github.com/intellisoft567/mvc567-cli).
// Copyright (C) 2019 Georgi Karagogov
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

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Mvc567.Cli.Templates;
using Mvc567.Cli.Templates.EntityDto;
using Mvc567.Common.Enums;
using Mvc567.Common.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mvc567.Cli.Commands.CommandFactories
{
    internal class EntityDtoFactory : CommandFactory
    {
        internal override void Execute(Dictionary<string, string> parameters)
        {
            if (parameters.ContainsKey(CommandParameters.Entity) && !string.IsNullOrWhiteSpace(parameters[CommandParameters.Entity]))
            {
                LoadCliConfig();
                if (CliConfig == null)
                {
                    Console.WriteLine("This command requires cli-config.json");
                    return;
                }
                string entityName = parameters[CommandParameters.Entity];
                string entityClassString = GetEntityClassString(entityName, CliConfig.EntityDirectory);

                if (string.IsNullOrEmpty(entityClassString))
                {
                    Console.WriteLine("Selected entity does not exist.");
                    return;
                }
                var classProperties = GetClassProperties(entityClassString);
                var classDtoProperties = MapEntitiesToEntitiesDtos(classProperties);

                InitSessionDictionary(entityName, CliConfig.EntityNamespace, CliConfig.DtoEntityNamespace, classDtoProperties);

                string dtoContent = TemplateRenderer.RenderTemplate(typeof(EntityDtoTemplate), this.sessionDictionary);
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

                string resultDtoContent = string.Join(Environment.NewLine, dtoContentList);
                SaveDtoFile($"{entityName}Dto.cs", resultDtoContent);
            }
            else
            {
                Console.WriteLine("Entity class name is required.");
            }
        }

        private void SaveDtoFile(string name, string content)
        {
            string directory = Path.Combine(this.cliConfigDirectory, CliConfig.DtoEntityDirectory);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            string filePath = Path.Combine(directory, name);

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

        protected override void InitSessionDictionary(params object[] args)
        {
            this.sessionDictionary = new Dictionary<string, object>
            {
                { "EntityName", args[0] },
                { "EntityNamespace", args[1] },
                { "DtoNamespace", args[2] },
                { "Properties",  args[3] }
            };
        }

        private string GetEntityClassString(string entityName, string entityDirectory)
        {
            string filePath = Path.Combine("Entities", $"{entityName}.cs");
            if (!string.IsNullOrWhiteSpace(entityDirectory))
            {
                filePath = Path.Combine(this.cliConfigDirectory ,entityDirectory, $"{entityName}.cs");
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

            SyntaxList<MemberDeclarationSyntax> propertyDeclarations = ExtractPropertiesDeclarations(root);

            List<EntityClassProperty> propertiesResult = new List<EntityClassProperty>();

            if (propertyDeclarations != null && propertyDeclarations.Count > 0)
            {
                foreach (var propertyDeclaration in propertyDeclarations)
                {
                    var propertyItem = ParseEntityClassProperty((PropertyDeclarationSyntax)propertyDeclaration);

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
            SyntaxList<MemberDeclarationSyntax> propertyDeclarations = new SyntaxList<MemberDeclarationSyntax>();
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
                foreach (var attribute in propertyAttributes)
                {
                    hasForeignKey = attribute.ToString().Contains("ForeignKey");
                }

                bool isCustomType = false;
                string castedPropertyDeclarationTypeValue = GetPropertyDeclarationTypeValue(propertyDeclaration, out isCustomType);

                var propertyItem = new EntityClassProperty
                {
                    Name = propertyDeclaration.Identifier.ValueText,
                    Type = castedPropertyDeclarationTypeValue,
                    IsForeignKey = castedPropertyDeclarationTypeValue.Contains("Guid"),
                    IsEnum = !hasVirtualModifier && !hasForeignKey && isCustomType,
                    IsCustomType = isCustomType
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

    internal class EntityClassProperty
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public bool IsForeignKey { get; set; }

        public bool IsEnum { get; set; }

        public bool IsCustomType { get; set; }
    }

    internal class EntityDtoClassProperty
    {
        internal EntityDtoClassProperty(EntityClassProperty property, int index)
        {
            EntityProperty = property;
            Name = property.Name;
            Type = property.IsCustomType && !property.IsEnum ? $"{property.Type}Dto" : property.Type;
            Index = index;
        }

        internal EntityClassProperty RelatedProperty { get; set; }

        private EntityClassProperty EntityProperty { get; set; }

        internal bool ShowAttributes
        {
            get
            {
                return !string.IsNullOrEmpty(DetailsAttribute) && !string.IsNullOrEmpty(CreateEditAttribute) & !string.IsNullOrEmpty(TableViewAttribute);
            }
        }
        internal int Index { get; private set; }

        internal string Name { get; private set; }

        internal string Type { get; private set; }

        internal string DetailsAttribute
        {
            get
            {
                if (EntityProperty.IsCustomType && !EntityProperty.IsEnum)
                {
                    return string.Empty;
                }
                return $"[DetailsOrder({Index})]";
            }
        }

        internal string CreateEditAttribute
        {
            get
            {
                if (EntityProperty.IsCustomType && !EntityProperty.IsEnum)
                {
                    return string.Empty;
                }
                return $"[CreateEditEntityInput(\"{StringFunctions.SplitWordsByCapitalLetters(Name)}\", CreateEntityInputType.{GetCreateEntityInputTypeEnum()})]";
            }
        }

        internal string TableViewAttribute
        {
            get
            {
                if (EntityProperty.IsCustomType && !EntityProperty.IsEnum)
                {
                    return string.Empty;
                }
                return $"[TableCell({Index}, \"{StringFunctions.SplitWordsByCapitalLetters(Name)}\", TableCellType.{GetTableCellTypeEnum()})]";
            }
        }

        internal string DatabaseEnumAttribute
        {
            get
            {
                string attribute = string.Empty;
                if (EntityProperty.IsForeignKey && RelatedProperty != null)
                {
                    attribute = $"[DatabaseEnum(typeof({RelatedProperty.Type}), \"Id\")]";
                }

                return attribute;
            }
        }

        internal bool ShowDatabaseEnumAttribute
        {
            get
            {
                return !string.IsNullOrEmpty(DatabaseEnumAttribute);
            }
        }

        private string GetTableCellTypeEnum()
        {
            string defaultType = TableCellType.Text.ToString();
            string targetType = defaultType;
            string[] numericTypes = new string[] 
            {
                "byte", "sbyte", "short", "ushort", "int", "uint", "long", "ulong", "float", "double", "decimal",
                "byte?", "sbyte?", "short?", "ushort?", "int?", "uint?", "long?", "ulong?", "float?", "double?", "decimal?",
                "Int16", "Int32", "Int64", "Single", "Double", "Decimal"
            };
            string[] booleanTypes = new string[] { "bool", "bool?", "Boolean" };

            if (numericTypes.Contains(Type.ToLower()))
            {
                targetType = TableCellType.Number.ToString();
            }
            else if (booleanTypes.Contains(Type.ToLower()))
            {
                targetType = TableCellType.Flag.ToString();
            }
            else if (Type.Contains("DateTime"))
            {
                targetType = TableCellType.DateTime.ToString();
            }
            else if (Type.Contains("TimeSpan"))
            {
                targetType = TableCellType.Time.ToString();
            }

            return targetType;
        }

        private string GetCreateEntityInputTypeEnum()
        {
            string defaultType = CreateEntityInputType.Text.ToString();
            string targetType = defaultType;
            string[] integerTypes = new string[]
            {
                "byte", "sbyte", "short", "ushort", "int", "uint", "long", "ulong",
                "byte?", "sbyte?", "short?", "ushort?", "int?", "uint?", "long?", "ulong?",
                "Int16", "Int32", "Int64"
            };
            string[] floatingPointTypes = new string[]
            {
                "float", "double", "decimal",
                "float?", "double?", "decimal?",
                "Single", "Double", "Decimal"
            };
            string[] booleanTypes = new string[] { "bool", "bool?", "Boolean" };

            if (integerTypes.Contains(Type.ToLower()))
            {
                targetType = CreateEntityInputType.Integer.ToString();
            }
            else if (floatingPointTypes.Contains(Type.ToLower()))
            {
                targetType = CreateEntityInputType.Double.ToString();
            }
            else if (booleanTypes.Contains(Type.ToLower()))
            {
                targetType = CreateEntityInputType.BoolSelect.ToString();
            }
            else if (Type.Contains("DateTime"))
            {
                targetType = CreateEntityInputType.Date.ToString();
            }
            else if (Type.Contains("TimeSpan"))
            {
                targetType = CreateEntityInputType.Time.ToString();
            }
            else if (EntityProperty.IsEnum)
            {
                targetType = CreateEntityInputType.EnumSelect.ToString();
            }
            else if (EntityProperty.IsForeignKey)
            {
                targetType = CreateEntityInputType.DatabaseSelect.ToString();
            }

            return targetType;
        }
    }
}
