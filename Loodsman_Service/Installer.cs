using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace Loodsman_Service
{
    [RunInstaller(true)]
    public partial class Installer : System.Configuration.Install.Installer
    {
        public Installer()
        {
            InitializeComponent();

            AfterInstall += Installer_AfterInstall;
            AfterUninstall += Installer_AfterUninstall;
        }

        private void Installer_AfterUninstall(object sender, InstallEventArgs e)
        {
            ServiceClass.UnregisterFunction(typeof(ServiceClass));
        }

        private void Installer_AfterInstall(object sender, InstallEventArgs e)
        {
            ServiceClass.RegisterFunction(typeof(ServiceClass));
        }
    }
}
