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

using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Codific.Mvc567.Cli.Commands
{
    internal abstract class CommandFactory
    {
        private CliConfig cliConfig;
        protected string cliConfigDirectory;

        protected Dictionary<string, object> sessionDictionary;
        internal abstract void Execute(Dictionary<string, string> parameters);

        protected abstract void InitSessionDictionary(params object[] args);

        protected CliConfig CliConfig
        {
            get
            {
                return this.cliConfig;
            }
        }

        protected void LoadCliConfig()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            var files = Directory.GetFiles(currentDirectory, "cli-config.json", SearchOption.AllDirectories);
            if (files != null && files.Length > 0)
            {
                this.cliConfig = JsonConvert.DeserializeObject<CliConfig>(File.ReadAllText(files[0]));
                this.cliConfigDirectory = (new FileInfo(files[0])).Directory.FullName;
            }
        }
    }

    internal class CliConfig
    {
        public string ProjectName { get; set; }

        public string EntityDirectory { get; set; }

        public string DtoEntityDirectory { get; set; }

        public string EntityNamespace { get; set; }

        public string DtoEntityNamespace { get; set; }
    }
}
