using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kooboo.Job;
using Kooboo.HealthMonitoring;
using System.Web;
using Kooboo.CMS.Common.Runtime.Dependency;

namespace ToolkitDemo.Services
{
    /// <summary>
    /// Sample Job Development
    /// add record in Web.config
    /// <add name="SampleJobModule" type="ToolkitDemo.Services.SampleJobModule,ToolkitDemo"/>
    /// </summary>
    [Dependency(typeof(IJob), Order = 100)]
    public class SampleJobService : IJob
    {
        public SampleJobService()
        {
        }

        public void Error(Exception e)
        {
            Log.LogException(e);
        }
        public void Execute(object executionState)
        {
            var message = new Exception(String.Format("Job Starts in {0}", DateTime.Now));
            message.Source = "This is a Sample Job in ToolkitDemo.Services.SampleJobService";
            Log.Logger(message);
        }
    }

    public class SampleJobModule : IHttpModule
    {
        public void Dispose()
        {
            Log.Logger(new Exception(String.Format("SampleJob Dispose in {0}", DateTime.Now)));
        }
        private static bool running = false;

        public void Init(HttpApplication context)
        {
            var intervalConfig = System.Configuration.ConfigurationManager.AppSettings[ConfigurationKeys.SampleJobInterval] ?? "0";
            var enabledConfig = System.Configuration.ConfigurationManager.AppSettings[ConfigurationKeys.SampleJobEnabled] ?? "false";
            var interval = 0;
            bool enabled = false;
            int.TryParse(intervalConfig, out interval);
            bool.TryParse(enabledConfig, out enabled);
            if (enabled && interval > 0)
            {
                Jobs.Instance.AttachJob("SampleJobModule", new SampleJobService(), interval, running);
            }
        }
    }
}
