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
using System.Linq;

namespace Codific.Mvc567.Cli.Commands
{
    internal class ConsoleCommand
    {
        internal ConsoleCommand(string[] commandArgs)
        {
            this.Parameters = new Dictionary<string, string>();
            this.ParseCommand(commandArgs);
        }

        internal string Command { get; set; }

        internal Dictionary<string, string> Parameters { get; set; }

        internal bool IsValid { get; set; }

        private void ParseCommand(string[] commandArgs)
        {
            if (commandArgs != null && commandArgs.Length > 0 && CommandsNames.CommandList.Contains(commandArgs[0]))
            {
                this.Command = commandArgs[0];
                if (this.Command == CommandsNames.Prototizer && commandArgs.Length == 1)
                {
                    this.IsValid = true;
                }
                else if (this.Command == CommandsNames.EntityDto && commandArgs.Length == 2 && commandArgs[1] == CommandParameters.AllEntities)
                {
                    this.IsValid = true;
                    this.Parameters.Add(commandArgs[1], commandArgs[1]);
                }
                else if (commandArgs.Length > 2 && commandArgs.Length % 2 == 1)
                {
                    for (int i = 1; i < commandArgs.Length; i += 2)
                    {
                        this.Parameters[commandArgs[i]] = commandArgs[i + 1];
                    }

                    this.IsValid = true;
                }
            }
        }
    }
}