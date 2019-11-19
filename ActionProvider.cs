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

using System;
using System.Collections.Generic;
using System.Reflection;
using Codific.Mvc567.Cli.Commands;
using Codific.Mvc567.Cli.Commands.CommandFactories;

namespace Codific.Mvc567.Cli
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
                { CommandsNames.ProjectInit, new ProjectInitFactory() },
                { CommandsNames.VueComponent, new VueComponentFactory() },
                { CommandsNames.EntityDto, new EntityDtoFactory() },
                { CommandsNames.Prototizer, new PrototizerFactory() },
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
                    this.PrintHelpInformation();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Something unexpected happened. :(");
                Console.WriteLine("Type codific567 to check available commands.");
            }
        }

        private void PrintHelpInformation()
        {
            string versionString = (Assembly.GetEntryAssembly() ?? throw new InvalidOperationException())
                                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                                .InformationalVersion
                                .ToString();

            this.PrintCodific567Text();
            Console.WriteLine($"codific567 .Net Command-line Tools {versionString}");
            Console.WriteLine("Usage: codific567 [command] [parameters]");
            Console.WriteLine();
            Console.WriteLine("Commands:");
            Console.WriteLine("init -n projectName");
            Console.WriteLine("vue-component -n componentName");
            Console.WriteLine("entity-dto -e entityClassName");
            Console.WriteLine("generate -j [optional] configurationJson");
            Console.WriteLine();
        }

        private void PrintCodific567Text()
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
