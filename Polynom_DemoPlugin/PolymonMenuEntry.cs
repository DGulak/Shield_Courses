using Ascon.Polynom.Api;
using Ascon.Polynom.GUI.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polynom_DemoPlugin
{
    internal class PolymonMenuEntry : IMenuItem
    {
        private readonly IClient _client;
        private readonly ISession _session;

        public PolymonMenuEntry(ISession session, IClient client)
        {
            _client = client;
            _session = session;
        }

        public string DisplayName => "Демонстрационная команда расширения"; // название команды

        public string Icon => char.ConvertFromUtf32(0xE29F); // символ иконки команды из шрифта AsconComplex.otf, в данном случае взят по коду символа

        public Action<ISelection> Command => DoCommand;

        public bool GetEnability(ISelection selection)
        {
            return true;
        }

        public bool GetVisibility(ISelection selection)
        {
            return true;
        }

        private void DoCommand(ISelection selection) // метод нашей команды
        {
            System.Diagnostics.Debugger.Launch();
        }
    }
}
