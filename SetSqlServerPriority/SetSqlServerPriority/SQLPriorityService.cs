using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SetSqlServerPriority
{
  partial class SQLPriorityService : ServiceBase
  {
    private System.Timers.Timer timer;
    
    public SQLPriorityService()
    {
      InitializeComponent();

      timer = new System.Timers.Timer();
      timer.Interval = 60 * 1000; // 60 seconds
      timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
    }


    protected override void OnStart(string[] args)
    {
      timer.Start();
      CheckAndSetPriority();

    }

    protected override void OnStop()
    {
      timer.Stop();
    }

    // This will be called every minute
    public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
    {
      CheckAndSetPriority();
    }

    private void CheckAndSetPriority()
    {
      // Replace "sqlservr" with the actual name of your SQL Server process
      var processes = Process.GetProcessesByName("sqlservr");
      foreach (var process in processes)
      {
        // Make sure the process is running
        if (process != null)
        {
          try
          {
            // Set priority to high
            process.PriorityClass = ProcessPriorityClass.High;
          }
          catch (Exception ex)
          {
            // Log error, if any
            // You might want to use a logging framework like log4net or NLog for this
            Console.WriteLine(ex.ToString());
          }
        }
      }
    }
  }
}

