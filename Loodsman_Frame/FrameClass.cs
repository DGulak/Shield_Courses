using Loodsman;
using LoodsmanObjects;
using Microsoft.Win32;
using PDMObjects;
using PDMTree;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Loodsman_Frame
{
    [ProgId("Loodsman_Course_Frame")]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    [Guid("c1ccdc41-90ef-4f3c-806c-6b5b061a76a1")]
    [ComVisible(true)]
    public class FrameClass : TableLayoutPanel, IFrameInfo, ILoodsmanFrame, IContent
    {
        public FrameClass()
        {
            System.Diagnostics.Debugger.Launch();
            Init();
        }

        private void Init()
        {
            this.BackColor = System.Drawing.SystemColors.Control;

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
            if(_selectedObj != null)
            {
                IPDMObject2 obj = _selectedObj.PDMObject;
                MessageBox.Show($"{obj.Name}\n{obj.TypeName}\n{obj.Version}", "Объект", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #region IFrameInfo
        public string GetInTypes()
        {
            return "1";
        }

        public int GetOutType()
        {
            return 0;
        }

        public string GetFrameName()
        {
            return "Loodsman_Course_Frame";
        }

        public string GetFrameDescription()
        {
            return "Демонстрационный фрейм";
        }

        public bool IsRootFrame()
        {
            return false;
        }
        #endregion

        #region ILoodsmanFrame

        /// <summary>
        /// Интерфейс контекста MDI окна, в котором находится фрейм
        /// </summary>
        private IDBContext _context;
        /// <summary>
        /// Указатель на интерфейс контейнера содержащего фрейм.
        /// </summary>
        private CoFrameContainer _container;
        /// <summary>
        /// Указатель на основной интерфейс ЛОЦМАН Клиента ILoodsmanApplication
        /// </summary>
        private ILoodsmanApplication _loodsmanApp;
        public void OnFrameCreate(IDBContext Context, CoFrameContainer Container, object OwnerApplication)
        {
            _context = Context;
            _container = Container;
            _loodsmanApp = (ILoodsmanApplication)OwnerApplication;
        }

        public void OnFrameDestroy()
        {

        }

        public void OnFrameActivate()
        {

        }

        public void OnFrameDeactivate()
        {

        }

        ILoodsmanTreeNode _selectedObj;
        public void OnStartRefresh()
        {
            object selContent = _container?.ParentFrame?.Content?.Selected;

            if (selContent.GetType() == typeof(DBNull))
                return;
            
            int contentType = _container.ParentFrame.Content.ContentType;
            var count = _container.ParentFrame.Content.SelectedCount;
            switch (contentType)
            {
                case 1:
                    {
                        
                        _selectedObj = (ILoodsmanTreeNode)selContent;
                        break;
                    }
            }
        }

        public void OnFrameClear()
        {

        }

        public dynamic OnCustomEvent(int EventCode, object EventData)
        {
            return null;
        }

        public void OnLoadOptions(CoOptions AFrameOptions)
        {

        }

        public void OnSaveOptions(CoOptions AFrameOptions)
        {

        }

        #endregion

        #region IContent
        public dynamic SelectedByIndex(int AIndex)
        {
            return null;
        }

        public dynamic ItemByIndex(int AIndex)
        {
            return null;
        }

        public dynamic Selected { get { return 0;}}

        new public dynamic Focused { get { return null; } }

        public int SelectedCount { get { return 0;}}

        public int ContentType { get { return 0;}}

        public int ItemCount { get { return 0; } }


        #endregion

        static Guid categoryGuid = Guid.Parse("A975F281-B99C-466E-926E-BC2F08B45365");

        [ComRegisterFunctionAttribute]
        public static void RegisterFunction(Type t)
        {
            Registry.ClassesRoot.CreateSubKey("CLSID\\" + t.GUID.ToString("B") +
            "\\Implemented Categories\\" + categoryGuid.ToString("B"));
        }

        [ComUnregisterFunctionAttribute]
        public static void UnregisterFunction(Type t)
        {
            Registry.ClassesRoot.DeleteSubKey("CLSID\\" + t.GUID.ToString("B") +
            "\\Implemented Categories\\" + categoryGuid.ToString("B"), false);
        }
    }
}
