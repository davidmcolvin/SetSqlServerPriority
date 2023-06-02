using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace SetSqlServerPriority
{
  public class LogHelper
  {
    public static log4net.ILog GetLogger([CallerFilePath] string filename = "")
    {
      return log4net.LogManager.GetLogger(filename);
    }
  }
}
