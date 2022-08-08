using System.ComponentModel;

namespace Loodsman_Frame
{
    [RunInstaller(true)]
    public partial class Installer : System.Configuration.Install.Installer
    {
        public Installer()
        {
            InitializeComponent();


            this.AfterInstall += Installer_AfterInstall;
            this.AfterUninstall += Installer_AfterUninstall;
            
        }

        private void Installer_AfterUninstall(object sender, System.Configuration.Install.InstallEventArgs e)
        {
            FrameClass.UnregisterFunction(typeof(FrameClass));
        }

        private void Installer_AfterInstall(object sender, System.Configuration.Install.InstallEventArgs e)
        {
            FrameClass.RegisterFunction(typeof(FrameClass));
        }
    }
}
