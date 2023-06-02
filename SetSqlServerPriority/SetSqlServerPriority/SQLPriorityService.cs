using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace SetSqlServerPriority
{
  partial class SQLPriorityService : ServiceBase
  {
    log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //LogHelper.GetLogger();
    private System.Timers.Timer timer;
    
    public SQLPriorityService()
    {
      log.Debug("SQLPriorityService starting...");
      InitializeComponent();

      timer = new System.Timers.Timer();
      timer.Interval = 60 * 1000; // 60 seconds
      timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
    }


    protected override void OnStart(string[] args)
    {
      log.Debug("OnStart");
      timer.Start();
      CheckAndSetPriority();

    }

    protected override void OnStop()
    {
      log.Debug("OnStop");
      timer.Stop();
    }

    // This will be called every minute
    public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
    {
      log.Debug("OnTimer");
      CheckAndSetPriority();
    }

    private void CheckAndSetPriority()
    {
      log.Debug("CheckAndSetPriority");
      // Replace "sqlservr" with the actual name of your SQL Server process
      var processes = Process.GetProcessesByName("sqlservr");
      foreach (var process in processes)
      {
        log.Debug("found a process");
        // Make sure the process is running
        if (process != null)
        {
          log.Debug("process is not null");
          try
          {
            log.Debug(process.PriorityClass.ToString());
            // Set priority to high
            process.PriorityClass = ProcessPriorityClass.High;
            log.Debug(process.ToString());
            log.Debug(process.PriorityClass.ToString());
            timer.Stop();
          }
          catch (Exception ex)
          {
            // Log error, if any
            // You might want to use a logging framework like log4net or NLog for this
            log.Error(ex.ToString());
          }
        }
      }
    }
  }
}

