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
    internal static class CommandsNames
    {
        internal static string ProjectInit = "init";
        internal static string VueComponent = "vue-component";
        internal static string EntityDto = "entity-dto";
        internal static string Prototizer = "generate";

        internal static readonly string[] CommandList = new string[]
        {
            ProjectInit,
            VueComponent,
            EntityDto,
            Prototizer
        };
    }
}
