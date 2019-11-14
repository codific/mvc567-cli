// This file is part of the mvc567 CLI distribution (https://github.com/intellisoft567/mvc567-cli).
// Copyright (C) 2019 Codific Ltd.
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
            if (directorySeparator == @"\")
            {
                directorySeparator = @"\\";
            }

            this.sessionDictionary = new Dictionary<string, object>
            {
                { "ProjectName", args[0] },
                { "ProjectGuid", Guid.NewGuid().ToString().ToUpper() },
                { "MainProjectGuid", Guid.NewGuid().ToString().ToUpper() },
                { "SolutionGuid", Guid.NewGuid().ToString().ToUpper() },
                { "DirectorySeparator", directorySeparator }
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
