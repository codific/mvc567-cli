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

using Codific.Mvc567.Cli.Commands.Options.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Codific.Mvc567.Cli.Commands.CommandFactories
{
    internal class PrototizerFactory : CommandFactory
    {
        private Process process;
        private const string DefaultJsonPath = "generator.json";

        internal override void Execute(Dictionary<string, string> parameters)
        {
            string jsonPath = parameters.ContainsKey(CommandParameters.GeneratorJsonPath) ? parameters[CommandParameters.GeneratorJsonPath] : null;
            var options = GetGeneratorOptions(jsonPath);
            if (options != null && options.IsValid)
            {
                GenerateProject(options);
                return;
            }
            Console.WriteLine("Missing or incomplete configuration.");
            Console.WriteLine("Generation failed.");
        }

        protected override void InitSessionDictionary(params object[] args)
        {

        }

        private GeneratorOptions GetGeneratorOptions(string consoleJsonPath)
        {
            GeneratorOptions options = null;
            string jsonPath = (!string.IsNullOrEmpty(consoleJsonPath)) ? consoleJsonPath : DefaultJsonPath;
            if (File.Exists(jsonPath))
            {
                var jsonContent = File.ReadAllText(jsonPath);
                options = JsonConvert.DeserializeObject<GeneratorOptions>(jsonContent);
            }
            return options;
        }

        private void GenerateProject(GeneratorOptions options)
        {
            this.process = new Process();
            this.process.StartInfo.FileName = options.JavaPath;
            this.process.StartInfo.Arguments = $"{options.JarCommand} {options.JarPath} {options.ApplicationJsonOption}";
            this.process.StartInfo.UseShellExecute = false;
            this.process.StartInfo.RedirectStandardOutput = true;
            this.process.Start();
            this.process.BeginOutputReadLine();
            this.process.OutputDataReceived += (sender, e) =>
            {
                Console.WriteLine(e.Data);
            };
            this.process.WaitForExit();
            Console.WriteLine("Project successfully generated.");
        }
    }
}
