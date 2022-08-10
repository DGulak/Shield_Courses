using Ascon.Polynom.Api;
using Ascon.Polynom.Login;
using System;
using System.IO;
using System.Reflection;

namespace Polynom_DemoConsole
{
    internal class Program
    {
        private static string InstallPath = @"C:\Program Files (x86)\ASCON\Polynom\Bin";
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            Run();
        }

        private static void Run()
        {
            UserAccount user = new UserAccount();

            user.UserName = "SynchroService";
            user.Password = "SynchroService123";

            ISession session = LoginManager.OpenSession("Электрощит", user);
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var fileName = Path.Combine(InstallPath, args.Name.Substring(0, args.Name.IndexOf(",")) + ".dll");

            if (File.Exists(fileName))
            {
                return Assembly.LoadFile(fileName);
            }

            var modulePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            fileName = Path.Combine(modulePath, args.Name.Substring(0, args.Name.IndexOf(",")) + ".dll");

            if (File.Exists(fileName))
            {
                return Assembly.LoadFile(fileName);
            }

            return null;
        }
    }
}
