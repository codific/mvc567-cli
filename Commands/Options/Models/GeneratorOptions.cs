using System;
using Newtonsoft.Json;

namespace Codific.Mvc567.Cli.Commands.Options.Models
{
    internal class GeneratorOptions
    {
        [JsonProperty("javaPath")]
        internal string JavaPath { get; set; }

        [JsonProperty("jarCommand")]
        internal string JarCommand { get; set; }

        [JsonProperty("jarPath")]
        internal string JarPath { get; set; }

        [JsonProperty("applicationJsonOption")]
        internal string ApplicationJsonOption { get; set; }

        public bool IsValid
        {
            get
            {
                return !string.IsNullOrEmpty(JavaPath) && !string.IsNullOrEmpty(JarCommand) && !string.IsNullOrEmpty(JarPath) && !string.IsNullOrEmpty(ApplicationJsonOption);
            }
        }
    }
}
