﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 15.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Mvc567.Cli.Templates.ProjectInit
{
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\mvc567-cli\Mvc567.Cli\Templates\ProjectInit\GulpFileTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "15.0.0.0")]
    public partial class GulpFileTemplate : GulpFileTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("/// <binding BeforeBuild=\'vue:build\' />\r\nlet gulp = require(\"gulp\"),\r\n    uglify " +
                    "= require(\'gulp-uglify-es\').default,\r\n    inject = require(\'gulp-inject-string\')" +
                    ",\r\n    replace = require(\'gulp-replace\'),\r\n    rename = require(\'gulp-rename\'),\r" +
                    "\n    concat = require(\'gulp-concat\'),\r\n    jsObfuscator = require(\'gulp-javascri" +
                    "pt-obfuscator\'),\r\n    sass = require(\'gulp-sass\'),\r\n    cssmin = require(\'gulp-c" +
                    "ssmin\');\r\n\r\nconst componentsDirectory = \'Scripts/VueComponents/\';\r\nconst csHtmlR" +
                    "esultDirectory = \'Views/Components/Vue\';\r\n\r\ngulp.task(\'sass\', function () {\r\n   " +
                    " return gulp.src(\'Styles/scss/**/*.scss\')\r\n        .pipe(sass())\r\n        .pipe(" +
                    "gulp.dest(\'Styles/css\'));\r\n});\r\n\r\ngulp.task(\'min:css\', [\'sass\'], function () {\r\n" +
                    "    return gulp.src(\'Styles/css/style.css\')\r\n        .pipe(cssmin({ keepSpecialC" +
                    "omments: 0 }))\r\n        .pipe(concat(\'style.min.css\'))\r\n        .pipe(gulp.dest(" +
                    "\'wwwroot/assets/css\'));\r\n});\r\n\r\ngulp.task(\'vue:vendors\', function () {\r\n    retu" +
                    "rn gulp.src([\r\n        \'node_modules/vue/dist/vue.min.js\',\r\n        \'node_module" +
                    "s/vue-router/dist/vue-router.min.js\',\r\n        \'node_modules/axios/dist/axios.mi" +
                    "n.js\',\r\n        \'node_modules/vuelidate/dist/vuelidate.min.js\',\r\n        \'node_m" +
                    "odules/vuelidate/dist/validators.min.js\',\r\n        \'node_modules/vue-cookies/vue" +
                    "-cookies.js\',\r\n        \'node_modules/vue-i18n/dist/vue-i18n.min.js\'\r\n    ])\r\n   " +
                    "     .pipe(uglify())\r\n        .pipe(inject.prepend(\'<script type=\"text/javascrip" +
                    "t\">\'))\r\n        .pipe(inject.append(\'</script>\'))\r\n        .pipe(replace(\'@\', \'@" +
                    "@\'))\r\n        .pipe(concat(\'_VueVendors.cshtml\'))\r\n        .pipe(gulp.dest(csHtm" +
                    "lResultDirectory));\r\n});\r\n\r\ngulp.task(\'vue:components\', function () {\r\n    retur" +
                    "n gulp.src(componentPath(\'*\'))\r\n        .pipe(uglify())\r\n        .pipe(jsObfusca" +
                    "tor())\r\n        .pipe(inject.prepend(\'<script type=\"text/javascript\">(function()" +
                    "{\"use strict\";\'))\r\n        .pipe(inject.append(\'})();</script>\'))\r\n        .pipe" +
                    "(replace(\'@\', \'@@\'))\r\n        .pipe(rename({\r\n            prefix: \"_\",\r\n        " +
                    "    suffix: \".Vue\",\r\n            extname: \".cshtml\"\r\n        }))\r\n        .pipe(" +
                    "gulp.dest(csHtmlResultDirectory));\r\n});\r\n\r\ngulp.task(\'styles\', [\'min:css\']);\r\ngu" +
                    "lp.task(\'vue:build\', [\'vue:vendors\', \'vue:components\']);\r\n\r\nconst componentPath " +
                    "= function (name = \'*\') {\r\n    return componentsDirectory + name + \'.js\';\r\n};");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "15.0.0.0")]
    public class GulpFileTemplateBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
