using Ascon.Plm.Loodsman.PluginSDK;
using System.Windows.Forms;

namespace Shield_Courses
{
    [LoodsmanPlugin]
    public class PluginClass : ILoodsmanNetPlugin
    {
        public void BindMenu(IMenuDefinition menu)
        {
            menu.AddMenuItem("Новая команда", NewCommandFunction, CheckNewCommand);
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

        private void NewCommandFunction(INetPluginCall call)
        {
            MessageBox.Show("Функция вызвана", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool CheckNewCommand(INetPluginCall call)
        {
            return true;
        }
    }
}
