using System;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using Newtonsoft.Json;

namespace racingfeed.Config
{
	/// <summary>
    /// Application level configuration variables storage.
    /// <para>
    /// To use the file place Config.json next to it if json is not present returns the empty Json by default.
	/// The Json file should contain a single object with the same fields as Config class.
    /// </para></summary>
    public class Config
    {
		private static Config EmptyConfig = new Config();

		public string TwitterApiKey { get; set; }
		public string TwitterConsumerKey { get; set; }
		public string TwitterConsumerSecret { get; set; }
		public string TwitterUserAccessToken { get; set; }
		public string TwitterUserAccessSercret { get; set; }

		public static Config Read()
		{
			var assembly = Assembly.GetAssembly(typeof(Config));
			Stream stream = assembly.GetManifestResourceStream(typeof(Config).Namespace + ".Config.json");

			if (stream == null)
			{
				Debug.WriteLine("Unable to locate Config.json make sure the file is present and marked as a EmbeddedResource file");
				return EmptyConfig;
			}
			using (var reader = new StreamReader(stream))
            {
				var text = reader.ReadToEnd();
				return JsonConvert.DeserializeObject<Config>(text);
            }
		}
    }
}
