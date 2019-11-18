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

using Codific.Mvc567.Cli.Templates;
using Codific.Mvc567.Cli.Templates.VueComponent;
using Codific.Mvc567.Common.Utilities;
using System;
using System.Collections.Generic;
using System.IO;

namespace Codific.Mvc567.Cli.Commands.CommandFactories
{
    internal class VueComponentFactory : CommandFactory
    {
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

        protected override void InitSessionDictionary(params object[] args)
        {
            string componentNameKebapCase = StringFunctions.SplitWordsByCapitalLetters(args[0].ToString())
                                   .Replace(" ", "-")
                                   .ToLower();

            this.sessionDictionary = new Dictionary<string, object>
            {
                { "ComponentName", args[0] },
                { "ComponentNameKebapCase", componentNameKebapCase }
            };
        }

    }
}
