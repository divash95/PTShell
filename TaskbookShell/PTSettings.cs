using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskbookShell
{
    //Контекст для сериализации Json
    public class PTSettings
    {

        public string Host { get; set; }

        public string HelpNamespace { get; set; }

        public string Lang { get; set; }

        public string PtDirectory { get; set; }

        public string WorkingDirectory { get; set; }

        public string ProjectPath { get; set; }

        public ProgressBar DownloadProgress { get; set; }

        public PTSettings(string host, string helpNamespace, string lang, string ptDirectory, string workingDirectory, string projectPath, ProgressBar downloadProgress)
        {
            Host = host;
            HelpNamespace = helpNamespace;
            Lang = lang;
            PtDirectory = ptDirectory;
            WorkingDirectory = workingDirectory;
            ProjectPath = projectPath;
            DownloadProgress = downloadProgress;
        }
    }
}
