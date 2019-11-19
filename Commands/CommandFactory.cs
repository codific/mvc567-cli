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

using System.Collections.Generic;
using System.IO;
using Codific.Mvc567.Cli.Commands.Utils;
using Newtonsoft.Json;

namespace Codific.Mvc567.Cli.Commands
{
    internal abstract class CommandFactory
    {
        private string cliConfigDirectory;

        private Dictionary<string, object> sessionDictionary;

        private CliConfig cliConfig;

        public string CliConfigDirectory
        {
            get => this.cliConfigDirectory;

            set => this.cliConfigDirectory = value;
        }

        public Dictionary<string, object> SessionDictionary
        {
            get => this.sessionDictionary;

            set => this.sessionDictionary = value;
        }

        protected CliConfig CliConfig
        {
            get
            {
                return this.cliConfig;
            }
        }

        internal abstract void Execute(Dictionary<string, string> parameters);

        protected abstract void InitSessionDictionary(params object[] args);

        protected void LoadCliConfig()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            var files = Directory.GetFiles(currentDirectory, "cli-config.json", SearchOption.AllDirectories);
            if (files != null && files.Length > 0)
            {
                this.cliConfig = JsonConvert.DeserializeObject<CliConfig>(File.ReadAllText(files[0]));
                this.CliConfigDirectory = new FileInfo(files[0]).Directory?.FullName;
            }
        }
    }
}