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
using Codific.Mvc567.Cli.Templates.ProjectInit;
using Codific.Mvc567.Cli.Templates.Scaffolding;
using System;
using System.Collections.Generic;
using System.IO;

namespace Codific.Mvc567.Cli.Commands.CommandFactories
{
    internal class ProjectInitFactory : CommandFactory
    {
        private string templateNamespace = "Codific.Mvc567.Cli.Templates.ProjectInit";

        internal override void Execute(Dictionary<string, string> parameters)
        {
            if (parameters.ContainsKey(CommandParameters.ProjectName) && !string.IsNullOrWhiteSpace(parameters[CommandParameters.ProjectName]))
            {
                string modifiedProjectName = parameters[CommandParameters.ProjectName].Replace("-", "_").Replace(" ", "_");
                InitSessionDictionary(modifiedProjectName);
                ScaffoldProject(modifiedProjectName);
            }
            else
            {
                Console.WriteLine("Project initialization requires project name parameter.");
            }
        }

        protected override void InitSessionDictionary(params object[] args)
        {
            string directorySeparator = Path.DirectorySeparatorChar.ToString();
            string projectName = args[0].ToString().Substring(0, 1).ToUpper() + args[0].ToString().Substring(1);
            if (directorySeparator == @"\")
            {
                directorySeparator = @"\\";
            }
            this.sessionDictionary = new Dictionary<string, object>
            {
                { "ProjectName", projectName },
                { "ProjectGuid", Guid.NewGuid().ToString().ToUpper() },
                { "MainProjectGuid", Guid.NewGuid().ToString().ToUpper() },
                { "SolutionGuid", Guid.NewGuid().ToString().ToUpper() },
                { "DirectorySeparator", directorySeparator },
                { "DtoProjectGuid", Guid.NewGuid().ToString().ToUpper() },
                { "EntitiesProjectGuid", Guid.NewGuid().ToString().ToUpper() },
                { "ServicesProjectGuid", Guid.NewGuid().ToString().ToUpper() },
                { "SharedProjectGuid", Guid.NewGuid().ToString().ToUpper() },
                { "TestsProjectGuid", Guid.NewGuid().ToString().ToUpper() },
            };
        }

        private void ScaffoldProject(string projectName)
        {
            string projectSchemeJson = (new ProjectInitScheme()).TransformText();
            Scheme projectScheme = Newtonsoft.Json.JsonConvert.DeserializeObject<Scheme>(projectSchemeJson);
            Processor.CreateRootPathsRecursively(Directory.GetCurrentDirectory(), null, projectScheme.Items, projectName, this.templateNamespace, this.sessionDictionary);
            Console.WriteLine("Project structure has been successfully created.");
        }
    }
}
