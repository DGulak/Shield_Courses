using Ascon.Plm.Loodsman.PluginSDK;
using DataProvider;
using Loodsman;
using RGiesecke.DllExport;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shield_Courses
{
    [LoodsmanPlugin]
    public class PluginClass : ILoodsmanNetPlugin
    {
        Barrier barrier = new Barrier(2);
        public PluginClass()
        {
            System.Diagnostics.Debugger.Launch();
        }

        public void BindMenu(IMenuDefinition menu)
        {
            menu.AddMenuItem("Новая команда", Action, CheckNewCommand);
        }

        #region
        public void OnCloseDb()
        {

        }

        public void OnConnectToDb(INetPluginCall call)
        {
            Task.Run(() => Action(call));
        }

        public void PluginLoad()
        {

        }

        public void PluginUnload()
        {
            barrier.SignalAndWait();
        }

        #endregion

        public void Action(INetPluginCall call)
        {
            MessageBox.Show("Функция вызвана", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);












            barrier.SignalAndWait();
        }

        private bool CheckNewCommand(INetPluginCall call)
        {
            return true;
        }
    }
}
