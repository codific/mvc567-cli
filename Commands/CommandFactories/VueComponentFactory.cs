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

using Mvc567.Cli.Templates;
using Mvc567.Cli.Templates.VueComponent;
using Mvc567.Common.Utilities;
using System;
using System.Collections.Generic;
using System.IO;

namespace Mvc567.Cli.Commands.CommandFactories
{
    internal class VueComponentFactory : CommandFactory
    {
        private Dictionary<string, object> sessionDictionary;
        private string templateNamespace = "Mvc567.Cli.Templates.VueComponent";

        internal override void Execute(Dictionary<string, string> parameters)
        {
            if (parameters.ContainsKey(CommandParameters.ComponentName) && !string.IsNullOrWhiteSpace(parameters[CommandParameters.ComponentName]))
            {
                string modifiedComponentName = parameters[CommandParameters.ComponentName].Replace("-", string.Empty).Replace(" ", string.Empty);
                InitSessionDictionary(modifiedComponentName);
                CreateComponent(modifiedComponentName);
            }
            else
            {
                Console.WriteLine("Vue component requires name parameter.");
            }
        }

        private void CreateComponent(string name)
        {
            string scriptsFolder = "Scripts";
            string vueComponentsFolder = "VueComponents";
            string targetDirectory = Path.Combine(scriptsFolder, vueComponentsFolder);

            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }

            string vueComponentContent = TemplateRenderer.RenderTemplate(typeof(VueComponentTemplate), this.sessionDictionary);

            string vueComponentFilePath = Path.Combine(targetDirectory, $"{name}.js");

            if (!File.Exists(vueComponentFilePath))
            {
                File.WriteAllText(vueComponentFilePath, vueComponentContent);
                Console.WriteLine("Vue component has been successfully created.");
            }
            else
            {
                Console.WriteLine("Vue component with same name exists in the folder.");
            }
        }

        private void InitSessionDictionary(string componentName)
        {
            string componentNameKebapCase = StringFunctions.SplitWordsByCapitalLetters(componentName)
                                               .Replace(" ", "-")
                                               .ToLower();

            this.sessionDictionary = new Dictionary<string, object>
            {
                { "ComponentName", componentName },
                { "ComponentNameKebapCase", componentNameKebapCase }
            };
        }
    }
}
