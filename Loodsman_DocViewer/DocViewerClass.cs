using Loodsman;
using Microsoft.Win32;
using PDMObjects;
using SecondaryView;
using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Loodsman_DocViewer
{
    [ProgId("Loodsman_DocViewer.DocViewerClass")]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [Guid("054975fd-1bcb-48b2-930e-60d5ad4d08d0")]
    [ComVisible(true)]
    public class DocViewerClass : TableLayoutPanel, IDocumentViewer2
    {
        CPDMDocument _document;
        ILoodsmanApplication _application;
        #region IDocumentViewer2

        public DocViewerClass()
        {
            System.Diagnostics.Debugger.Launch();
        }
        public void Init(CDBWindow DBWindow, LooApplication LoodsmanApp)
        {
            _application = LoodsmanApp;
            BackColor = System.Drawing.SystemColors.Control;
            this.RowStyles.Clear();
            this.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));

            this.ColumnStyles.Clear();
            this.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));

            this.RowCount = 1;
            this.ColumnCount = 1;

            this.Margin = new Padding(0);
            this.Padding = new Padding(0);
            this.Dock = DockStyle.Fill;
            this.Anchor = AnchorStyles.Top & AnchorStyles.Left;

            Button button = new Button()
            {
                Text = "Click Me",
                Anchor = AnchorStyles.Top & AnchorStyles.Left,
                Size = new System.Drawing.Size(100, 25)
            };

            button.Click += Button_Click;

            this.Controls.Add(button);
        }

        private void Button_Click(object sender, EventArgs e)
        {

        }

        public void Finalize()
        {
            
        }

        public void Clear()
        {
            
        }

        public void Refresh(CPDMDocument PDMDocument)
        {
            _document = PDMDocument;
        }

        public void Command(SecondaryView.DocumentViewerCommands DocumentCommand, int MainWindow)
        {
            
        }

        public CPDMDocument Document { get { return _document; } }

        public int Features { get { return (int)cmFeature.ALL ; } }

        #endregion
        [Flags]
        enum cmFeature
        {
            COMMAND_NONE = 0,
            COMMAND_ANNOTATE = 1,
            COMMAND_CUSTOMIZE = 2,
            COMMAND_ABOUT = 4,
            COMMAND_DELETE = 8,
            COMMAND_HELP = 16,
            ALL = COMMAND_ANNOTATE |
                COMMAND_CUSTOMIZE | 
                COMMAND_ABOUT |
                COMMAND_DELETE |
                COMMAND_HELP 
        }

        [ComRegisterFunctionAttribute]
        public static void RegisterFunction(Type t)
        {           
            // And create the 'Control' key - this allows it to show up in 
            // the ActiveX control container 
            Registry.ClassesRoot.CreateSubKey("CLSID\\" + t.GUID.ToString("B") +
            "\\Control");

            // Open the CLSID\{guid} key for write access
            RegistryKey k = Registry.ClassesRoot.OpenSubKey("CLSID\\" + t.GUID.ToString("B"), true);

            // Next create the CodeBase entry - needed if not string named and GACced.
            RegistryKey inprocServer32 = k.OpenSubKey("InprocServer32", true);
            inprocServer32.SetValue("CodeBase", Assembly.GetExecutingAssembly().CodeBase);
            inprocServer32.Close();

            // Finally close the main key
            k.Close();

        }

        [ComUnregisterFunctionAttribute]
        public static void UnregisterFunction(Type t)
        {
            // Open HKCR\CLSID\{guid} for write access
            RegistryKey k = Registry.ClassesRoot.OpenSubKey("CLSID\\" + t.GUID.ToString("B"), true);

            // Delete the 'Control' key, but don't throw an exception if it does not exist
            k.DeleteSubKey("Control", false);

            // Next open up InprocServer32
            RegistryKey inprocServer32 = k.OpenSubKey("InprocServer32", true);

            // And delete the CodeBase key, again not throwing if missing 
            k.DeleteSubKey("CodeBase", false);

            // Finally close the main key 
            k.Close();

        }
    }
}
