using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Configuration.Install;
using System.IO ;

namespace Alivever.Com.DevBasic.BasicLib.AppCtrl
{

	/// <summary>
	/// class InstallerBase: This installer is used for service 
	/// </summary>
	[RunInstallerAttribute(true)]
	public class InstallerBase: Installer
	{
		private ServiceInstaller serviceInstaller;
		private ServiceProcessInstaller processInstaller;

		public InstallerBase()
		{

			processInstaller = new ServiceProcessInstaller();
			serviceInstaller = new ServiceInstaller();

			// Service will run under system account
			processInstaller.Account = ServiceAccount.LocalSystem;

			// Service will have Start Type of Manual
			serviceInstaller.StartType = ServiceStartMode.Manual;

			serviceInstaller.ServiceName = "DirectoryServicesDeamon";

			Installers.Add(serviceInstaller);
			Installers.Add(processInstaller);
		}
	}//class InstallerBase

}//namespace
