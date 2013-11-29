using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kooboo.Job;
using Kooboo.HealthMonitoring;
using System.Web;

namespace ToolkitDemo.Services
{
    /// <summary>
    /// Sample Job Development
    /// add record in Web.config
    /// <add name="SampleJobModule" type="ToolkitDemo.Services.SampleJobModule,ToolkitDemo"/>
    /// </summary>
    public class SampleJobService : IJob
    {
        public void Error(Exception e)
        {
            Log.LogException(e);
        }
        public void Execute(object executionState)
        {
            if (null == HttpContext.Current.Application[ConfigurationKeys.SampleJobStarted] || !(bool)HttpContext.Current.Application[ConfigurationKeys.SampleJobStarted])
            {
                HttpContext.Current.Application[ConfigurationKeys.SampleJobStarted] = true;
                var message = new Exception(String.Format("Job Starts in {0}", DateTime.Now));
                message.Source = "This is a Sample Job in ToolkitDemo.Services.SampleJobService";
                Log.Logger(message);
            }
        }
    }

    public class SampleJobModule : IHttpModule
    {
        public void Dispose()
        {
            Log.Logger(new Exception(String.Format("SampleJob Dispose in {0}", DateTime.Now)));
        }

        public void Init(HttpApplication context)
        {
            var intervalConfig = System.Configuration.ConfigurationManager.AppSettings[ConfigurationKeys.SampleJobInterval] ?? "60000";
            var enabledConfig = System.Configuration.ConfigurationManager.AppSettings[ConfigurationKeys.SampleJobEnabled] ?? "false";
            var interval = 60000;
            bool enabled = false;
            int.TryParse(intervalConfig, out interval);
            bool.TryParse(enabledConfig, out enabled);
            if (enabled && interval > 0)
            {
                context.Application[ConfigurationKeys.SampleJobStarted] = true;
                Jobs.Instance.AttachJob("SampleJobModule", new SampleJobService(), interval, null);
            }
        }
    }
}
