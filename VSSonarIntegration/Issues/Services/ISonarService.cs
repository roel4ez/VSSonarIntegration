using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using Microsoft.VisualStudio.Settings.Internal;

using Newtonsoft.Json;

namespace VSSonarIntegration.Issues.Services
{
    public interface ISonarService : IDisposable
    {

        IEnumerable<SonarIssue> GetIssues(string user);

    }

    public class SonarService : ISonarService
    {
        public IEnumerable<SonarIssue> GetIssues(string user)
        {
            List<SonarIssue> sonarIssues = new List<SonarIssue>();
            using (var wc = new WebClient())
            {
                var json = wc.DownloadString(new Uri(""));

                dynamic data = JsonConvert.DeserializeObject(json);

                foreach (dynamic issue in data.issues)
                {
                    var sonarIssue = new SonarIssue((string)issue.key, (string)issue.status, (string)issue.severity, (string)issue.message);
                   sonarIssues.Add(sonarIssue);
                }
            }
            return sonarIssues;
        }

        public void Dispose()
        {
           // throw new NotImplementedException();
        }
    }


    public class SonarIssue
    {
        public SonarIssue(string id, string status, string severity, string title)
        {
            Id = id;
            Status = status;
            Severity = severity;
            Title = title;
        }

        public string Id { get; set; }
        public string Status { get; set; }
        public string Severity { get; set; }
        public string Title { get; set; }
    }
}