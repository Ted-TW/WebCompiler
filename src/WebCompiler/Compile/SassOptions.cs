﻿using Newtonsoft.Json;

namespace WebCompiler
{
    /// <summary>
    /// Give all options for the Sass compiler
    /// </summary>
    public class SassOptions : BaseOptions<SassOptions>
    {
        private const string trueStr = "true";
        private readonly char[] separators = new char[] { ';', ',', '/' };

        /// <summary> Creates a new instance of the class.</summary>
        public SassOptions()
        { }

        /// <summary>
        /// Loads the settings based on the config
        /// </summary>
        protected override void LoadSettings(Config config)
        {
            base.LoadSettings(config);

            string autoPrefix = GetValue(config, "autoPrefix");
            if (autoPrefix != null)
                AutoPrefix = autoPrefix;

            if (config.Options.ContainsKey("style"))
                Style = config.Options["style"].ToString();

            int precision = 5;
            if (int.TryParse(GetValue(config, "precision"), out precision))
                Precision = precision;

            string relativeUrls = GetValue(config, "relativeUrls");
            if (relativeUrls != null)
                RelativeUrls = relativeUrls.Trim().ToLowerInvariant() == trueStr;

            string loadPaths = GetValue(config, "loadPaths");
            if (loadPaths != null)
                LoadPaths = loadPaths.Split(separators, System.StringSplitOptions.RemoveEmptyEntries);

            string sourceMapRoot = GetValue(config, "sourceMapRoot");
            if (sourceMapRoot != null)
                SourceMapRoot = sourceMapRoot;
        }

        /// <summary>
        /// The file name should match the compiler name
        /// </summary>
        protected override string CompilerFileName
        {
            get { return "sass"; }
        }

        /// <summary>
        /// Autoprefixer will use the data based on current browser popularity and
        /// property support to apply prefixes for you.
        /// </summary>
        [JsonProperty("autoPrefix")]
        public string AutoPrefix { get; set; } = string.Empty;

        /// <summary>
        /// Path to look for imported files
        /// </summary>
        [JsonProperty("loadPath")]
        public string[] LoadPaths { get; set; } = new string[0];

        /// <summary>
        /// Type of output style
        /// </summary>
        [JsonProperty("style")]
        public string Style { get; set; } = "expanded"; //"compressed"

        /// <summary>
        /// Precision
        /// </summary>
        public int Precision { get; set; } = 5;

        /// <summary>
        /// This option allows you to re-write URL's in imported files so that the URL is always
        /// relative to the base imported file.
        /// </summary>
        [JsonProperty("relativeUrls")]
        public bool RelativeUrls { get; set; } = true;

        /// <summary>
        /// Base path, will be emitted in source-map as is
        /// </summary>
        [JsonProperty("sourceMapRoot")]
        public string SourceMapRoot { get; set; } = string.Empty;
    }
}
