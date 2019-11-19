using System;
using Newtonsoft.Json;

namespace Codific.Mvc567.Cli.Commands.Options.Models
{
    internal class GeneratorOptions
    {
        public bool IsValid => !string.IsNullOrEmpty(this.JavaPath) && !string.IsNullOrEmpty(this.JarCommand) && !string.IsNullOrEmpty(this.JarPath) && !string.IsNullOrEmpty(this.ApplicationJsonOption);

        [JsonProperty("javaPath")]
        internal string JavaPath { get; set; }

        [JsonProperty("jarCommand")]
        internal string JarCommand { get; set; }

        [JsonProperty("jarPath")]
        internal string JarPath { get; set; }

        [JsonProperty("applicationJsonOption")]
        internal string ApplicationJsonOption { get; set; }
    }
}