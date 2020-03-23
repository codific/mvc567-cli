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

using System.Linq;
using Codific.Mvc567.Common.Enums;
using Codific.Mvc567.Common.Utilities;

namespace Codific.Mvc567.Cli.Commands.Utils
{
    internal class EntityDtoClassProperty
    {
        internal EntityDtoClassProperty(EntityClassProperty property, int index)
        {
            this.EntityProperty = property;
            this.Name = property.Name;
            this.Type = property.IsCustomType && !property.IsEnum ? $"{property.Type}Dto" : property.Type;
            this.Index = index;
        }

        internal EntityClassProperty RelatedProperty { get; set; }

        internal bool ShowAttributes
        {
            get
            {
                return !string.IsNullOrEmpty(this.DetailsAttribute) && !string.IsNullOrEmpty(this.CreateEditAttribute) & !string.IsNullOrEmpty(this.TableViewAttribute);
            }
        }

        internal int Index { get; private set; }

        internal string Name { get; private set; }

        internal string Type { get; private set; }

        internal string DetailsAttribute
        {
            get
            {
                if (this.EntityProperty.IsCustomType && !this.EntityProperty.IsEnum)
                {
                    return string.Empty;
                }

                return $"[DetailsOrder({this.Index}0)]";
            }
        }

        internal string CreateEditAttribute
        {
            get
            {
                if (this.EntityProperty.IsCustomType && !this.EntityProperty.IsEnum)
                {
                    return string.Empty;
                }

                return $"[CreateEditEntityInput(\"{StringFunctions.SplitWordsByCapitalLetters(this.Name)}\", CreateEntityInputType.{this.GetCreateEntityInputTypeEnum()})]";
            }
        }

        internal string TableViewAttribute
        {
            get
            {
                if (this.EntityProperty.IsCustomType && !this.EntityProperty.IsEnum)
                {
                    return string.Empty;
                }

                return $"[TableCell({this.Index}0, \"{StringFunctions.SplitWordsByCapitalLetters(this.Name)}\", TableCellType.{this.GetTableCellTypeEnum()})]";
            }
        }

        internal string RequiredAttribute
        {
            get
            {
                if (!this.EntityProperty.IsRequired)
                {
                    return string.Empty;
                }

                return $"\n        [Required]";
            }
        }

        internal string DatabaseEnumAttribute
        {
            get
            {
                string attribute = string.Empty;
                if (this.EntityProperty.IsForeignKey && this.RelatedProperty != null)
                {
                    attribute = $"[DatabaseEnum(typeof({this.RelatedProperty.Type}), \"Id\")]";
                }

                return attribute;
            }
        }

        internal bool ShowDatabaseEnumAttribute
        {
            get
            {
                return !string.IsNullOrEmpty(this.DatabaseEnumAttribute);
            }
        }

        private EntityClassProperty EntityProperty { get; set; }

        private string GetTableCellTypeEnum()
        {
            string defaultType = TableCellType.Text.ToString();
            string targetType = defaultType;
            string[] numericTypes =
            {
                "float", "double", "decimal",
                "float?", "double?", "decimal?",
                "Single", "Double", "Decimal",
            };
            string[] booleanTypes = { "bool", "bool?", "Boolean" };

            if (numericTypes.Contains(this.Type.ToLower()))
            {
                targetType = TableCellType.Number.ToString();
            }
            else if (booleanTypes.Contains(this.Type.ToLower()))
            {
                targetType = TableCellType.Flag.ToString();
            }
            else if (this.Type.Contains("DateTime"))
            {
                targetType = TableCellType.DateTime.ToString();
            }
            else if (this.Type.Contains("TimeSpan"))
            {
                targetType = TableCellType.Time.ToString();
            }

            return targetType;
        }

        private string GetCreateEntityInputTypeEnum()
        {
            string defaultType = CreateEntityInputType.Text.ToString();
            string targetType = defaultType;
            string[] integerTypes = new string[]
            {
                "byte", "sbyte", "short", "ushort", "int", "uint", "long", "ulong",
                "byte?", "sbyte?", "short?", "ushort?", "int?", "uint?", "long?", "ulong?",
                "Int16", "Int32", "Int64",
            };
            string[] floatingPointTypes = new string[]
            {
                "float", "double", "decimal",
                "float?", "double?", "decimal?",
                "Single", "Double", "Decimal",
            };
            string[] booleanTypes = new string[] { "bool", "bool?", "Boolean" };

            if (integerTypes.Contains(this.Type.ToLower()))
            {
                targetType = CreateEntityInputType.Integer.ToString();
            }
            else if (floatingPointTypes.Contains(this.Type.ToLower()))
            {
                targetType = CreateEntityInputType.Double.ToString();
            }
            else if (booleanTypes.Contains(this.Type.ToLower()))
            {
                targetType = CreateEntityInputType.BoolSelect.ToString();
            }
            else if (this.Type.Contains("DateTime"))
            {
                targetType = CreateEntityInputType.Date.ToString();
            }
            else if (this.Type.Contains("TimeSpan"))
            {
                targetType = CreateEntityInputType.Time.ToString();
            }
            else if (this.EntityProperty.IsEnum)
            {
                targetType = CreateEntityInputType.EnumSelect.ToString();
            }
            else if (this.EntityProperty.IsForeignKey)
            {
                targetType = CreateEntityInputType.DatabaseSelect.ToString();
            }

            return targetType;
        }
    }
}