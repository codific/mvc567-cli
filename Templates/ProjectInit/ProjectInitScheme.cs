﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Codific.Mvc567.Cli.Templates.ProjectInit {
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    
    public partial class ProjectInitScheme : ProjectInitSchemeBase {
        
        public virtual string TransformText() {
            this.GenerationEnvironment = null;
            this.Write("{\n  \"scheme\": [\n    {\n      \"type\": \"folder\",\n      \"name\": \"{{ProjectName}}\",\n  " +
                    "    \"content\": [\n        {\n          \"type\": \"file\",\n          \"name\": \"cli-conf" +
                    "ig.json\",\n          \"templateName\": \"CliConfigTemplate\"\n        },\n        {\n   " +
                    "       \"type\": \"file\",\n          \"name\": \"stylecop.json\",\n          \"templateNam" +
                    "e\": \"StyleCopJsonTemplate\"\n        },\n        {\n          \"type\": \"file\",\n      " +
                    "    \"name\": \"Codific.ruleset\",\n          \"templateName\": \"CodificRulesetTemplate" +
                    "\"\n        },\n        {\n          \"type\": \"file\",\n          \"name\": \"generator.js" +
                    "on\",\n          \"templateName\": \"GeneratorConfigTemplate\"\n        },\n        {\n  " +
                    "        \"type\": \"file\",\n          \"name\": \".gitignore\",\n          \"templateName\"" +
                    ": \"GitIgnoreTemplate\"\n        },\n        {\n          \"type\": \"file\",\n          \"" +
                    "name\": \"{{ProjectName}}.sln\",\n          \"templateName\": \"ProjectSolutionTemplate" +
                    "\"\n        },\n        {\n          \"type\": \"folder\",\n          \"name\": \"src\",\n    " +
                    "      \"content\": [\n            {\n              \"type\": \"folder\",\n              \"" +
                    "name\": \"{{ProjectName}}.Entities\",\n              \"content\": [\n                {\n" +
                    "                  \"type\": \"file\",\n                  \"name\": \"{{ProjectName}}.Ent" +
                    "ities.csproj\",\n                  \"templateName\": \"EntityProjectTemplate\"\n       " +
                    "         }\n              ]\n            },\n            {\n              \"type\": \"f" +
                    "older\",\n              \"name\": \"{{ProjectName}}.DataTransferObjects\",\n           " +
                    "   \"content\": [\n                {\n                  \"type\": \"file\",\n            " +
                    "      \"name\": \"{{ProjectName}}.DataTransferObjects.csproj\",\n                  \"t" +
                    "emplateName\": \"DtoProjectTemplate\"\n                }\n              ]\n           " +
                    " },\n            {\n              \"type\": \"folder\",\n              \"name\": \"{{Proje" +
                    "ctName}}.Services\",\n              \"content\": [\n                {\n               " +
                    "   \"type\": \"file\",\n                  \"name\": \"{{ProjectName}}.Services.csproj\",\n" +
                    "                  \"templateName\": \"ServiceProjectTemplate\"\n                }\n   " +
                    "           ]\n            },\n            {\n              \"type\": \"folder\",\n      " +
                    "        \"name\": \"{{ProjectName}}.Shared\",\n              \"content\": [\n           " +
                    "     {\n                  \"type\": \"file\",\n                  \"name\": \"{{ProjectNam" +
                    "e}}.Shared.csproj\",\n                  \"templateName\": \"SharedProjectTemplate\"\n  " +
                    "              }\n              ]\n            },\n            {\n              \"type" +
                    "\": \"folder\",\n              \"name\": \"{{ProjectName}}.Tests\",\n              \"conte" +
                    "nt\": [\n                {\n                  \"type\": \"file\",\n                  \"na" +
                    "me\": \"{{ProjectName}}.Tests.csproj\",\n                  \"templateName\": \"TestsPro" +
                    "jectTemplate\"\n                }\n              ]\n            },\n            {\n   " +
                    "           \"type\": \"folder\",\n              \"name\": \"{{ProjectName}}\",\n          " +
                    "    \"content\": [\n                {\n                  \"type\": \"folder\",\n         " +
                    "         \"name\": \"Controllers\",\n                  \"content\": [\n                 " +
                    "   {\n                      \"type\": \"folder\",\n                      \"name\": \"API\"" +
                    "\n                    },\n                    {\n                      \"type\": \"fol" +
                    "der\",\n                      \"name\": \"MVC\",\n                      \"content\": [\n  " +
                    "                      {\n                          \"type\": \"folder\",\n            " +
                    "              \"name\": \"Admin\",\n                          \"content\": [\n          " +
                    "                  {\n                              \"type\": \"file\",\n              " +
                    "                \"name\": \"AdminDashboardController.cs\",\n                         " +
                    "     \"templateName\": \"AdminDashboardControllerTemplate\"\n                        " +
                    "    }\n                          ]\n                        },\n                   " +
                    "     {\n                          \"type\": \"file\",\n                          \"name" +
                    "\": \"HomeController.cs\",\n                          \"templateName\": \"HomeControlle" +
                    "rTemplate\"\n                        }\n                      ]\n                   " +
                    " }\n                  ]\n                },\n                {\n                  \"t" +
                    "ype\": \"folder\",\n                  \"name\": \"DataAccess\",\n                  \"conte" +
                    "nt\": [\n                    {\n                      \"type\": \"file\",\n             " +
                    "         \"name\": \"EntityContext.cs\",\n                      \"templateName\": \"Enti" +
                    "tyContextTemplate\"\n                    },\n                    {\n                " +
                    "      \"type\": \"file\",\n                      \"name\": \"StandardRepository.cs\",\n   " +
                    "                   \"templateName\": \"StandardRepositoryTemplate\"\n                " +
                    "    }\n                  ]\n                },\n                {\n                 " +
                    " \"type\": \"folder\",\n                  \"name\": \"Migrations\",\n                  \"co" +
                    "ntent\": [\n\n                  ]\n                },\n                {\n            " +
                    "      \"type\": \"folder\",\n                  \"name\": \"privateroot\",\n               " +
                    "   \"content\": [\n                    {\n                      \"type\": \"folder\",\n  " +
                    "                    \"name\": \"uploads\",\n                      \"content\": [\n      " +
                    "                  {\n                          \"type\": \"folder\",\n                " +
                    "          \"name\": \"global\"\n                        },\n                        {\n" +
                    "                          \"type\": \"folder\",\n                          \"name\": \"t" +
                    "emp\"\n                        },\n                        {\n                      " +
                    "    \"type\": \"folder\",\n                          \"name\": \"users\"\n                " +
                    "        }\n                      ]\n                    },\n                    {\n " +
                    "                     \"type\": \"file\",\n                      \"name\": \"config.json\"" +
                    ",\n                      \"templateName\": \"PrivateConfigTemplate\"\n                " +
                    "    }\n                  ]\n                },\n                {\n                 " +
                    " \"type\": \"folder\",\n                  \"name\": \"Properties\",\n                  \"co" +
                    "ntent\": [\n                    {\n                      \"type\": \"file\",\n          " +
                    "            \"name\": \"launchSettings.json\",\n                      \"templateName\":" +
                    " \"LaunchSettingsTemplate\"\n                    }\n                  ]\n            " +
                    "    },\n                {\n                  \"type\": \"folder\",\n                  \"" +
                    "name\": \"Scripts\",\n                  \"content\": [\n                  ]\n           " +
                    "     },\n                {\n                  \"type\": \"folder\",\n                  " +
                    "\"name\": \"Styles\",\n                  \"content\": [\n                    {\n         " +
                    "             \"type\": \"folder\",\n                      \"name\": \"css\",\n            " +
                    "          \"content\": [\n\n                      ]\n                    },\n         " +
                    "           {\n                      \"type\": \"folder\",\n                      \"name" +
                    "\": \"scss\",\n                      \"content\": [\n                        {\n        " +
                    "                  \"type\": \"file\",\n                          \"name\": \"_misc.scss\"" +
                    ",\n                          \"templateName\": \"ScssMiscTemplate\"\n                 " +
                    "       },\n                        {\n                          \"type\": \"file\",\n  " +
                    "                        \"name\": \"_variables.scss\",\n                          \"te" +
                    "mplateName\": \"ScssVariablesTemplate\"\n                        },\n                " +
                    "        {\n                          \"type\": \"file\",\n                          \"n" +
                    "ame\": \"style.scss\",\n                          \"templateName\": \"ScssStyleTemplate" +
                    "\"\n                        }\n                      ]\n                    }\n      " +
                    "            ]\n                },\n                {\n                  \"type\": \"fo" +
                    "lder\",\n                  \"name\": \"Views\",\n                  \"content\": [\n       " +
                    "             {\n                      \"type\": \"folder\",\n                      \"na" +
                    "me\": \"AreasViews\",\n                      \"content\": [\n                        {\n" +
                    "                          \"type\": \"folder\",\n                          \"name\": \"A" +
                    "dmin\",\n                          \"content\": [\n                            {\n    " +
                    "                          \"type\": \"folder\",\n                              \"name\"" +
                    ": \"AdminDashboard\",\n                              \"content\": [\n                 " +
                    "               {\n                                  \"type\": \"file\",\n             " +
                    "                     \"name\": \"Index.cshtml\",\n                                  \"" +
                    "templateName\": \"AdminDashboardIndexViewTemplate\"\n                               " +
                    " }\n                              ]\n                            },\n              " +
                    "              {\n                              \"type\": \"file\",\n                  " +
                    "            \"name\": \"_ViewImports.cshtml\",\n                              \"templa" +
                    "teName\": \"AdminAreaViewImportsTemplate\"\n                            },\n         " +
                    "                   {\n                              \"type\": \"file\",\n             " +
                    "                 \"name\": \"_ViewStart.cshtml\",\n                              \"tem" +
                    "plateName\": \"AdminAreaViewStartTemplate\"\n                            }\n         " +
                    "                 ]\n                        }\n                      ]\n           " +
                    "         },\n                    {\n                      \"type\": \"folder\",\n      " +
                    "                \"name\": \"Components\",\n                      \"content\": [\n       " +
                    "               ]\n                    },\n                    {\n                  " +
                    "    \"type\": \"folder\",\n                      \"name\": \"ControllersViews\",\n        " +
                    "              \"content\": [\n                        {\n                          \"" +
                    "type\": \"folder\",\n                          \"name\": \"Home\",\n                     " +
                    "     \"content\": [\n                            {\n                              \"t" +
                    "ype\": \"file\",\n                              \"name\": \"Index.cshtml\",\n            " +
                    "                  \"templateName\": \"HomeIndexViewTemplate\"\n                      " +
                    "      }\n                          ]\n                        },\n                 " +
                    "       {\n                          \"type\": \"folder\",\n                          \"" +
                    "name\": \"Shared\",\n                          \"content\": [\n                        " +
                    "    {\n                              \"type\": \"file\",\n                            " +
                    "  \"name\": \"_BaseLayout.cshtml\",\n                              \"templateName\": \"V" +
                    "iewLayoutTemplate\"\n                            }\n                          ]\n   " +
                    "                     },\n                        {\n                          \"typ" +
                    "e\": \"file\",\n                          \"name\": \"_ViewImports.cshtml\",\n           " +
                    "               \"templateName\": \"ViewImportsTemplate\"\n                        },\n" +
                    "                        {\n                          \"type\": \"file\",\n            " +
                    "              \"name\": \"_ViewStart.cshtml\",\n                          \"templateNa" +
                    "me\": \"ViewStartTemplate\"\n                        }\n                      ]\n     " +
                    "               },\n                    {\n                      \"type\": \"folder\",\n" +
                    "                      \"name\": \"EmailViews\"\n                    }\n               " +
                    "   ]\n                },\n                {\n                  \"type\": \"folder\",\n  " +
                    "                \"name\": \"wwwroot\",\n                  \"content\": [\n              " +
                    "      {\n                      \"type\": \"folder\",\n                      \"name\": \"a" +
                    "ssets\",\n                      \"content\": [\n                        {\n           " +
                    "               \"type\": \"folder\",\n                          \"name\": \"css\"\n       " +
                    "                 },\n                        {\n                          \"type\": " +
                    "\"folder\",\n                          \"name\": \"images\",\n                          " +
                    "\"content\": [\n\n                          ]\n                        }\n            " +
                    "          ]\n                    },\n                    {\n                      \"" +
                    "type\": \"folder\",\n                      \"name\": \"content\",\n                      " +
                    "\"content\": [\n                        {\n                          \"type\": \"folder" +
                    "\",\n                          \"name\": \"global\"\n                        },\n       " +
                    "                 {\n                          \"type\": \"folder\",\n                 " +
                    "         \"name\": \"users\"\n                        }\n                      ]\n     " +
                    "               },\n                    {\n                      \"type\": \"folder\",\n" +
                    "                      \"name\": \"locales\",\n                      \"content\": [\n    " +
                    "                    {\n                          \"type\": \"file\",\n                " +
                    "          \"name\": \"en.json\",\n                          \"templateName\": \"EmptyJso" +
                    "nTemplate\"\n                        }\n                      ]\n                   " +
                    " }\n                  ]\n                },\n                {\n                  \"t" +
                    "ype\": \"file\",\n                  \"name\": \"{{ProjectName}}.csproj\",\n              " +
                    "    \"templateName\": \"ProjectFileTemplate\"\n                },\n                {\n " +
                    "                 \"type\": \"file\",\n                  \"name\": \"appsettings.json\",\n " +
                    "                 \"templateName\": \"AppSettingsTemplate\"\n                },\n      " +
                    "          {\n                  \"type\": \"file\",\n                  \"name\": \"appsett" +
                    "ings.Development.json\",\n                  \"templateName\": \"AppSettingsTemplate\"\n" +
                    "                },\n                {\n                  \"type\": \"file\",\n         " +
                    "         \"name\": \"appsettings.Sample.json\",\n                  \"templateName\": \"A" +
                    "ppSettingsTemplate\"\n                },\n                {\n                  \"type" +
                    "\": \"file\",\n                  \"name\": \"adminmenus.json\",\n                  \"templ" +
                    "ateName\": \"AdminMenusTemplate\"\n                },\n                {\n            " +
                    "      \"type\": \"file\",\n                  \"name\": \"package.json\",\n                " +
                    "  \"templateName\": \"PackageJsonTemplate\"\n                },\n                {\n   " +
                    "               \"type\": \"file\",\n                  \"name\": \"gulpfile.js\",\n        " +
                    "          \"templateName\": \"GulpFileTemplate\"\n                },\n                " +
                    "{\n                  \"type\": \"file\",\n                  \"name\": \"Program.cs\",\n    " +
                    "              \"templateName\": \"ProgramTemplate\"\n                },\n             " +
                    "   {\n                  \"type\": \"file\",\n                  \"name\": \"Startup.cs\",\n " +
                    "                 \"templateName\": \"StartupTemplate\"\n                }\n           " +
                    "   ]\n            }\n          ]\n        }\n      ]\n    }\n  ]\n}\n");
            return this.GenerationEnvironment.ToString();
        }
        
        public virtual void Initialize() {
        }
    }
    
    public class ProjectInitSchemeBase {
        
        private global::System.Text.StringBuilder builder;
        
        private global::System.Collections.Generic.IDictionary<string, object> session;
        
        private global::System.CodeDom.Compiler.CompilerErrorCollection errors;
        
        private string currentIndent = string.Empty;
        
        private global::System.Collections.Generic.Stack<int> indents;
        
        private ToStringInstanceHelper _toStringHelper = new ToStringInstanceHelper();
        
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session {
            get {
                return this.session;
            }
            set {
                this.session = value;
            }
        }
        
        public global::System.Text.StringBuilder GenerationEnvironment {
            get {
                if ((this.builder == null)) {
                    this.builder = new global::System.Text.StringBuilder();
                }
                return this.builder;
            }
            set {
                this.builder = value;
            }
        }
        
        protected global::System.CodeDom.Compiler.CompilerErrorCollection Errors {
            get {
                if ((this.errors == null)) {
                    this.errors = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errors;
            }
        }
        
        public string CurrentIndent {
            get {
                return this.currentIndent;
            }
        }
        
        private global::System.Collections.Generic.Stack<int> Indents {
            get {
                if ((this.indents == null)) {
                    this.indents = new global::System.Collections.Generic.Stack<int>();
                }
                return this.indents;
            }
        }
        
        public ToStringInstanceHelper ToStringHelper {
            get {
                return this._toStringHelper;
            }
        }
        
        public void Error(string message) {
            this.Errors.Add(new global::System.CodeDom.Compiler.CompilerError(null, -1, -1, null, message));
        }
        
        public void Warning(string message) {
            global::System.CodeDom.Compiler.CompilerError val = new global::System.CodeDom.Compiler.CompilerError(null, -1, -1, null, message);
            val.IsWarning = true;
            this.Errors.Add(val);
        }
        
        public string PopIndent() {
            if ((this.Indents.Count == 0)) {
                return string.Empty;
            }
            int lastPos = (this.currentIndent.Length - this.Indents.Pop());
            string last = this.currentIndent.Substring(lastPos);
            this.currentIndent = this.currentIndent.Substring(0, lastPos);
            return last;
        }
        
        public void PushIndent(string indent) {
            this.Indents.Push(indent.Length);
            this.currentIndent = (this.currentIndent + indent);
        }
        
        public void ClearIndent() {
            this.currentIndent = string.Empty;
            this.Indents.Clear();
        }
        
        public void Write(string textToAppend) {
            this.GenerationEnvironment.Append(textToAppend);
        }
        
        public void Write(string format, params object[] args) {
            this.GenerationEnvironment.AppendFormat(format, args);
        }
        
        public void WriteLine(string textToAppend) {
            this.GenerationEnvironment.Append(this.currentIndent);
            this.GenerationEnvironment.AppendLine(textToAppend);
        }
        
        public void WriteLine(string format, params object[] args) {
            this.GenerationEnvironment.Append(this.currentIndent);
            this.GenerationEnvironment.AppendFormat(format, args);
            this.GenerationEnvironment.AppendLine();
        }
        
        public class ToStringInstanceHelper {
            
            private global::System.IFormatProvider formatProvider = global::System.Globalization.CultureInfo.InvariantCulture;
            
            public global::System.IFormatProvider FormatProvider {
                get {
                    return this.formatProvider;
                }
                set {
                    if ((value != null)) {
                        this.formatProvider = value;
                    }
                }
            }
            
            public string ToStringWithCulture(object objectToConvert) {
                if ((objectToConvert == null)) {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                global::System.Type type = objectToConvert.GetType();
                global::System.Type iConvertibleType = typeof(global::System.IConvertible);
                if (iConvertibleType.IsAssignableFrom(type)) {
                    return ((global::System.IConvertible)(objectToConvert)).ToString(this.formatProvider);
                }
                global::System.Reflection.MethodInfo methInfo = type.GetMethod("ToString", new global::System.Type[] {
                            iConvertibleType});
                if ((methInfo != null)) {
                    return ((string)(methInfo.Invoke(objectToConvert, new object[] {
                                this.formatProvider})));
                }
                return objectToConvert.ToString();
            }
        }
    }
}
