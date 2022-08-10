using Loodsman;
using RGiesecke.DllExport;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using WorkflowBusinessLogic;

namespace Loodsman_BL_library
{
    public class BLClass
    {
        // Папка, из которой загружена сборка.
        // Из этой папки будут загружаться сборки от которых зависит текущая
        public static string InstallPath {get;private set;}

        // Структура Variant для автоопераций Workflow
        [StructLayout(LayoutKind.Sequential)]
        public struct Variant
        {
            public ushort vt;
            public ushort wReserved1;
            public ushort wReserved2;
            public ushort wReserved3;
            public IntPtr data1;
            public IntPtr data2;

            public Variant(VarEnum type)
            {
                vt = (ushort)type;
                wReserved1 = 0;
                wReserved2 = 0;
                wReserved3 = 0;
                data1 = (IntPtr)0;
                data2 = (IntPtr)0;
            }

            public Variant(bool value)
            {
                vt = (ushort)VarEnum.VT_BOOL;
                wReserved1 = 0;
                wReserved2 = 0;
                wReserved3 = 0;
                data1 = (IntPtr)Convert.ToInt32(value);
                data2 = (IntPtr)0;
            }

            public Variant(int value)
            {
                vt = (ushort)VarEnum.VT_I4;
                wReserved1 = 0;
                wReserved2 = 0;
                wReserved3 = 0;
                data1 = (IntPtr)value;
                data2 = (IntPtr)0;
            }

            public Variant(string value)
            {
                vt = (ushort)VarEnum.VT_BSTR;
                wReserved1 = 0;
                wReserved2 = 0;
                wReserved3 = 0;
                data1 = (IntPtr)UnsafeNativeMethods.SysAllocString(value);
                data2 = (IntPtr)0;
            }
        }

        // Структура TPDMVersionData для автоопераций Workflow
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct PDMVersionData
        {
            [MarshalAs(UnmanagedType.I4)]
            public int routeId;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
            public string appServer;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
            public string db;
            [MarshalAs(UnmanagedType.I4)]
            public int id;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
            public string product;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
            public string type;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
            public string version;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 255)]
            public string state;
            [MarshalAs(UnmanagedType.I1)]
            public byte accessLevel;
            [MarshalAs(UnmanagedType.I1)]
            public byte lockLevel;
            [MarshalAs(UnmanagedType.I1)]
            public byte document;
            [MarshalAs(UnmanagedType.I1)]
            public byte revision;
        }

        private static void Initialize()
        {
            InstallPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.AssemblyResolve += currentDomain_AssemblyResolve;
        }

        // Загрузка используемых сборок из папки, в которой находится текущая сборка.
        // По умолчанию сборки загружаются из папки в которой находится exe-файл
        private static Assembly currentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string fileName = Path.Combine(InstallPath, args.Name.Substring(0, args.Name.IndexOf(",")) + ".dll");
            if (File.Exists(fileName))
            {
                return Assembly.LoadFile(fileName);
            }
            return null;
        }

        // Функция для использования в автооперации Workflow с помощью ExecPluginFunction 
        [DllExport("BL_Library_Command", CallingConvention.StdCall)]
        public static Variant RunIntegration(Variant wfo, IntPtr versionData, IntPtr userData)
        {
            System.Diagnostics.Debugger.Launch();

            Initialize();

            if (wfo.vt != (ushort)VarEnum.VT_DISPATCH)
            {
                throw new ArgumentException();
            }

            //получаем WFBL
            IWFBusinessLogic wf = (IWFBusinessLogic)Marshal.GetTypedObjectForIUnknown(wfo.data1, typeof(IWFBusinessLogic));
            //получаем данные БП
            PDMVersionData data = (PDMVersionData)Marshal.PtrToStructure(versionData, typeof(PDMVersionData));
            //Получаем массив переданных параметров
            var passedData = (object)Marshal.GetObjectForNativeVariant(userData);


            try
            {
                IPluginCall pc = wf.GetPluginCallInterface(0, 0);

            }
            catch(Exception ex)
            {

            }


            return new Variant();
        }
    }
}
