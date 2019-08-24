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

using Mvc567.Cli.Commands;
using Mvc567.Cli.Commands.CommandFactories;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Mvc567.Cli
{
    internal class ActionProvider
    {
        private readonly string[] args;
        private readonly Dictionary<string, CommandFactory> commandFactories;

        internal ActionProvider(string[] args)
        {
            this.args = args;

            this.commandFactories = new Dictionary<string, CommandFactory>
            {
                { CommandsNames.ProjectInit, new ProjectInitFactory() }
            };
        }

        internal void Execute()
        {
            try
            {
                ConsoleCommand consoleCommand = new ConsoleCommand(this.args);
                if (consoleCommand.IsValid)
                {
                    this.commandFactories[consoleCommand.Command].Execute(consoleCommand.Parameters);
                }
                else
                {
                    PrintHelpInformation();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Something unexpected happened. :(");
                Console.WriteLine("Type mvc567 to check available commands.");
            }

        }

        private void PrintHelpInformation()
        {
            string versionString = Assembly.GetEntryAssembly()
                                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                                .InformationalVersion
                                .ToString();

            PrintMvc567Text();
            Console.WriteLine($"mvc567 .Net Command-line Tools {versionString}");
            Console.WriteLine("Usage: mvc567 [command] [parameters]");
            Console.WriteLine();
            Console.WriteLine("Commands:");
            Console.WriteLine("init -n projectName");
            Console.WriteLine();
        }

        private void PrintMvc567Text()
        {
            Console.WriteLine();
            Console.WriteLine("               ╔═══╦═══╦═══╗ ╔═══╦╗  ╔══╗   ");
            Console.WriteLine("               ║╔══╣╔══╣╔═╗║ ║╔═╗║║  ╚╣╠╝   ");
            Console.WriteLine("      ╔╗╔╦╗╔╦══╣╚══╣╚══╬╝╔╝║ ║║ ╚╣║   ║║    ");
            Console.WriteLine("      ║╚╝║╚╝║╔═╩══╗║╔═╗║ ║╔╩═╣║ ╔╣║ ╔╗║║    ");
            Console.WriteLine("      ║║║╠╗╔╣╚═╦══╝║╚═╝║ ║╠══╣╚═╝║╚═╝╠╣╠╗   ");
            Console.WriteLine("      ╚╩╩╝╚╝╚══╩═══╩═══╝ ╚╝  ╚═══╩═══╩══╝   ");
            Console.WriteLine();
        }
    }
}
