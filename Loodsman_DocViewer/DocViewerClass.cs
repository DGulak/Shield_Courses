using Loodsman;
using Microsoft.Win32;
using PDMObjects;
using SecondaryView;
using System;
using System.Drawing;
using System.IO;
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
        PictureBox _pictureBox;
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
            BackColor = SystemColors.Control;
            this.RowStyles.Clear();
            this.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            this.ColumnStyles.Clear();
            this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            this.RowCount = 1;
            this.ColumnCount = 1;

            this.Margin = new Padding(0);
            this.Padding = new Padding(0);
            this.Dock = DockStyle.Fill;
            this.Anchor = AnchorStyles.Top & AnchorStyles.Left;

            _pictureBox = new PictureBox()
            {
                Dock = DockStyle.Fill,
                BackColor = SystemColors.Control,

            };

            this.Controls.Add(_pictureBox);
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

            var file = _document.View.Load();

            if (file.EndsWith(".ico"))
            {
                var bytes = ReadFile(file);
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    _pictureBox.Image = new Icon(ms).ToBitmap();
                }
            }
        }

        public static byte[] ReadFile(string filePath)
        {
            byte[] buffer;
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            try
            {
                int length = (int)fileStream.Length;  // get file length
                buffer = new byte[length];            // create buffer
                int count;                            // actual number of bytes read
                int sum = 0;                          // total number of bytes read

                // read until Read method returns 0 (end of the stream has been reached)
                while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                    sum += count;  // sum is a buffer offset for next reading
            }
            finally
            {
                fileStream.Close();
            }
            return buffer;
        }

        public void Command(SecondaryView.DocumentViewerCommands DocumentCommand, int MainWindow)
        {
            switch (DocumentCommand)
            {
                case SecondaryView.DocumentViewerCommands.cmAnnotate:
                    {
                        
                        break;
                    }
                case SecondaryView.DocumentViewerCommands.cmCustomize:
                    break;
                case SecondaryView.DocumentViewerCommands.cmAbout:
                    break;
                case SecondaryView.DocumentViewerCommands.cmDeleteView:
                    break;
                case SecondaryView.DocumentViewerCommands.cmHelp:
                    break;
                case SecondaryView.DocumentViewerCommands.cmEdit:
                    break;
            }
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
            ALL = COMMAND_ANNOTATE | COMMAND_CUSTOMIZE | COMMAND_ABOUT | COMMAND_DELETE | COMMAND_HELP 
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
