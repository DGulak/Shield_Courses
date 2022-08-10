using Ascon.Plm.Loodsman.PluginSDK;
using Loodsman;
using RGiesecke.DllExport;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Shield_Courses
{
    [LoodsmanPlugin]
    public class PluginClass : ILoodsmanNetPlugin
    {
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

        }

        public void PluginLoad()
        {

        }

        public void PluginUnload()
        {

        }

        #endregion

        public void Action(INetPluginCall call)
        {
            MessageBox.Show("Функция вызвана", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool CheckNewCommand(INetPluginCall call)
        {
            return true;
        }
    }
}
