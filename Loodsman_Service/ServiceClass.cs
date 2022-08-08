using DataProvider;
using Loodsman;
using SUPR;
using Task = System.Threading.Tasks.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Threading;

namespace Loodsman_Service
{
    [ProgId("Loodsman_Course_Service")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [Guid("305c0dda-4e3e-478e-bd31-8a253559219f")]
    [ComVisible(true)]
    public class ServiceClass : ILoodsmanService, IServiceInfo
    {
        private ILoodsmanApplication _loodsmanApplication;
        private ISimpleAPI2 _simpleAPI2;
        private IWBSSystem _wbsSystem;
        private IDataBase _dataBase;

        private Task Routine;
        private Barrier _serviceStopBarrier = new Barrier(2);
        public ServiceClass()
        {
            System.Diagnostics.Debugger.Launch();
        }
        #region IServiceInfo
        public string GetServiceName()
        {
            return "Loodsman_Course_Service";
        }

        public string GetServiceDescription()
        {
            return "Демо-сервис";
        }
        #endregion

        #region ILoodsmanSrvice
        public void OnBindService(object OwnerApplication)
        {
            _loodsmanApplication = (ILoodsmanApplication)OwnerApplication;
        }

        public void OnUnbindService()
        {
            _loodsmanApplication = null;
        }

        public void OnOpenDatabase(object Connection, object WBSSystem, CDataBase DataBase)
        {
            _simpleAPI2 = (ISimpleAPI2)Connection;
            _wbsSystem = (IWBSSystem)WBSSystem;
            _dataBase = DataBase;

            Routine = Task.Run(() => DoAction());
        }

        public void OnCloseDatabase(CDataBase DataBase)
        {
            _simpleAPI2 = null;
            _wbsSystem = null;
            _dataBase = null;

            _serviceStopBarrier.SignalAndWait();
        }
        #endregion

        static Guid categoryGuid = Guid.Parse("A00C228B-183F-434B-A99B-9FFF6F267988");

        [ComRegisterFunctionAttribute]
        public static void RegisterFunction(Type t)
        {
            RegistryKey key = Registry.ClassesRoot.CreateSubKey(
          "CLSID\\" + t.GUID.ToString("B") +
          "\\Implemented Categories\\" + categoryGuid.ToString("B"));
        }

        [ComUnregisterFunctionAttribute]
        public static void UnregisterFunction(Type t)
        {
            Registry.ClassesRoot.DeleteSubKey("CLSID\\" + t.GUID.ToString("B") +
             "\\Implemented Categories\\" + categoryGuid.ToString("B"), false);
        }

        private Task DoAction()
        {
            MessageBox.Show("Сервис запущен", GetServiceName(), MessageBoxButtons.OK, MessageBoxIcon.Information);

            _loodsmanApplication.NotifyUser("Уведомление", "Это просто уведомление", 0, 0);

            _serviceStopBarrier.SignalAndWait();
            return Task.CompletedTask;
        }
    }
}
