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

using System;
using System.Collections.Generic;
using System.IO;

namespace Codific.Mvc567.Cli.Templates.Scaffolding
{
    internal static class Processor
    {
        internal static void CreateRootPathsRecursively(string contentRootPath, string rootFolder, List<SchemeItem> scaffoldingSchemeItems, string projectName, string templatesNamespace, Dictionary<string, object> sessionDictionary)
        {
            foreach (var item in scaffoldingSchemeItems)
            {
                if (item.Type == SchemeItemType.Folder)
                {
                    string folderPath = item.Name;
                    if (!string.IsNullOrEmpty(rootFolder))
                    {
                        folderPath = Path.Combine(rootFolder, item.Name);
                        CreateContentRootPath(contentRootPath, projectName, rootFolder, item.Name);
                    }
                    else
                    {
                        CreateContentRootPath(contentRootPath, projectName, item.Name);
                    }

                    CreateRootPathsRecursively(contentRootPath, folderPath, item.Content, projectName, templatesNamespace, sessionDictionary);
                }
                else if (item.Type == SchemeItemType.File)
                {
                    string filePath = item.Name;
                    if (!string.IsNullOrEmpty(rootFolder))
                    {
                        filePath = Path.Combine(rootFolder, item.Name);
                    }

                    string fileContent = TemplateRenderer.RenderTemplate(Type.GetType($"{templatesNamespace}.{item.TemplateName}"), sessionDictionary);

                    CreateFile(filePath, fileContent, projectName);
                }
            }
        }

        private static void CreateContentRootPath(string contentRootPath, string projectName, params string[] folders)
        {
            List<string> foldersList = new List<string>();
            foldersList.Add(contentRootPath);
            foldersList.AddRange(folders);
            string folderPath = Path.Combine(foldersList.ToArray());

            CreateDirectory(folderPath, projectName);
        }

        private static void CreateDirectory(string folderPath, string projectName)
        {
            string modifiedFolderPath = folderPath.Replace("{{ProjectName}}", projectName);

            if (!Directory.Exists(modifiedFolderPath))
            {
                Directory.CreateDirectory(modifiedFolderPath);
            }
        }

        private static void CreateFile(string filePath, string fileContent, string projectName)
        {
            string modifiedFilePath = filePath.Replace("{{ProjectName}}", projectName);

            if (!File.Exists(modifiedFilePath))
            {
                File.WriteAllText(modifiedFilePath, fileContent);
            }
        }
    }
}
