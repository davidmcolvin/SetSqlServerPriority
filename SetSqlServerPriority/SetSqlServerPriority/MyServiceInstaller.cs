using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

[RunInstaller(true)]
public class MyServiceInstaller : Installer
{
  public MyServiceInstaller()
  {
    var serviceProcessInstaller = new ServiceProcessInstaller();
    var serviceInstaller = new ServiceInstaller();

    // Configure the service process installer
    serviceProcessInstaller.Account = ServiceAccount.LocalSystem;

    // Configure the service installer
    serviceInstaller.ServiceName = "SQLPriorityService";
    serviceInstaller.DisplayName = "SQL Priority Service";
    serviceInstaller.Description = "Sets Priority of SQL Server";
    serviceInstaller.StartType = ServiceStartMode.Automatic;

    // Add the installers to the installer collection
    Installers.Add(serviceProcessInstaller);
    Installers.Add(serviceInstaller);
  }
}
