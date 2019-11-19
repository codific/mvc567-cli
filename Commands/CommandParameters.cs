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

namespace Codific.Mvc567.Cli.Commands
{
    internal static class CommandParameters
    {
        private static string projectName = "-n";
        private static string saveDirectory = "-d";
        private static string componentName = "-n";
        private static string entity = "-e";
        private static string generatorJsonPath = "-j";
        private static string allEntities = "-a";

        public static string ProjectName => CommandParameters.projectName;

        public static string SaveDirectory => CommandParameters.saveDirectory;

        public static string ComponentName => CommandParameters.componentName;

        public static string Entity => CommandParameters.entity;

        public static string GeneratorJsonPath => CommandParameters.generatorJsonPath;

        public static string AllEntities => CommandParameters.allEntities;
    }
}