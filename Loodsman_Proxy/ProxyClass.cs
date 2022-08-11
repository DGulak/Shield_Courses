using System.Runtime.InteropServices;
using LoodsmanIntegrator;
using RGiesecke.DllExport;

namespace Loodsman_Proxy
{
    public class ProxyClass
    {
        [DllExport("fInitialize", CallingConvention.StdCall)]
        public static uint fInitialize(out string ErrMessage)
        {
            System.Diagnostics.Debugger.Launch();
            ErrMessage = (string)null;
            return 0;
        }

        [DllExport("fCreate", CallingConvention.StdCall)]
        public static int fCreate(ProxyCall ProxyCall, CommonModel.Model cmQuery, out string ErrMessage)
        {
            System.Diagnostics.Debugger.Launch();
            ErrMessage = "";
            return 0;
        }

        [DllExport("fOpen", CallingConvention.StdCall)]
        public static int fOpen(ProxyCall ProxyCall, CommonModel.Model cmQuery, UnmanagedType RunTool, out string ErrMessage)
        {
            System.Diagnostics.Debugger.Launch();
            ErrMessage = "";
            return 0;
        }


        [DllExport("fRead", CallingConvention.StdCall)]
        public static int fRead(IProxyCall ProxyCall, CommonModel.Model inModel, UnmanagedType inLoadLinkFiles, out CommonModel.Model outModel, out string ErrMessage)
        {

            System.Diagnostics.Debugger.Launch();
            outModel = inModel;
            ErrMessage = "";
            return 0;
        }

        [DllExport("fTerminate", CallingConvention.StdCall)]
        public static uint fTerminate(out string ErrMessage)
        {
            System.Diagnostics.Debugger.Launch();
            ErrMessage = (string)null;
            return 0;
        }
    }
}
