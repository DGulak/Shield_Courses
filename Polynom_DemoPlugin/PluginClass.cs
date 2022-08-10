using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using Ascon.Polynom.GUI.PluginInterfaces;
using Ascon.Polynom.Api;

namespace Polynom_DemoPlugin
{
    [Export(typeof(IClientPlugin))]
    [ExportMetadata("Id", "8FB19533-ECAC-4102-AAB6-7F3F5E9EA22C")]
    [ExportMetadata("DisplayName", "Демонстрационное расширение")]
    [ExportMetadata("Description", "Демонстрирует базовою структуру расширения")]
    [ExportMetadata("Version", "1.0.0 demo")]
    public class PluginClass : IClientPlugin
    {
        private ISession _session;
        private PolymonMenuEntry _menuEntry;

        public PluginClass()
        {
            System.Diagnostics.Debugger.Launch();
        }
        public void Close()
        {
            
        }

        public void Initialize(IClient client)
        {
            _session = client.GetSession(); // получаем сессию для работы в ней с основным API ПОЛИНОМ:MDM
            _menuEntry = new PolymonMenuEntry(_session, client); // создаем команду для контекстного меню
            client.RegisterContextMenuItem(_menuEntry); // регистрируем нашу команду в контекстном меню клиента ПОЛИНОМ:MDM
            client.RegisterActionsMenuItem(_menuEntry); // регистрируем нашу команду в контекстном меню клиента ПОЛИНОМ:MDM
        }
    }
}
