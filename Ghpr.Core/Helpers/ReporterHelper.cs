﻿using System;
using System.IO;
using Ghpr.Core.Common;
using Ghpr.Core.Enums;
using Ghpr.Core.Utils;
using Newtonsoft.Json;

namespace Ghpr.Core.Helpers
{
    internal static class ReporterHelper
    {
        public static ReporterSettings GetSettingsFromFile(string fileName = "")
        {
            var uri = new Uri(typeof(ReporterSettings).Assembly.CodeBase);
            var settingsPath = Path.Combine(Path.GetDirectoryName(uri.LocalPath) ?? "",
                fileName.Equals("") ? Paths.Files.CoreSettings : fileName);
            var settings = JsonConvert.DeserializeObject<ReporterSettings>(File.ReadAllText(settingsPath));
            return settings;
        }

        public static string GetSettingsFileName(TestingFramework framework)
        {
            switch (framework)
            {
                case TestingFramework.MSTest:
                    return Paths.Files.MSTestSettings;
                case TestingFramework.NUnit:
                    return Paths.Files.NUnitSettings;
                case TestingFramework.SpecFlow:
                    return Paths.Files.SpecFlowSettings;
                default:
                    return Paths.Files.CoreSettings;
            }
        }

        public static void Save(this ReportSettings reportSettings, string path, string filename)
        {
            var serializer = new JsonSerializer();
            Paths.Create(path);
            var fullPath = Path.Combine(path, filename);
            if (!File.Exists(fullPath))
            {
                using (var file = File.CreateText(fullPath))
                {
                    serializer.Serialize(file, reportSettings);
                }
            }
        }
    }
}