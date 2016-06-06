﻿using System.IO;
using Ghpr.Core.EmbeddedResources;
using Ghpr.Core.Enums;
using Ghpr.Core.HtmlPages;

namespace Ghpr.Core
{
    public static class Reporter
    {
        public static string OutputPath => Properties.Settings.Default.outputPath;
        public const string Src = "src";

        public static void ExtractReportBase()
        {
            var re = new ResourceExtractor(Path.Combine(OutputPath, Src));

            var repornMainPage = new ReportMainPage(Src);
            repornMainPage.SavePage(OutputPath, "index.html");

            re.Extract(Resource.All);
        }
    }
}
