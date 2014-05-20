#region Using Statements
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using Diplom.Primitives;
using Diplom.SceneHelpers;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using Diplom.Forms;
using System.Text;
#endregion

namespace Diplom
{
    public partial class MainForm : Form
    {
        private Point mouseOldPos = new Point();
        private bool isDrag = false;

        public bool IsEnabledSubObjCmbBox
        {
            get { return cmbSubObject.Enabled; }
            set { cmbSubObject.Enabled = value; }
        }

        public bool IsEnabledBntLookAtSelection
        {
            get { return btnLookAtSelection.Enabled; }
            set { btnLookAtSelection.Enabled = value; }
        }

        public int SubObjComboBoxSelectedIndex
        {
            get { return cmbSubObject.SelectedIndex; }
            set { cmbSubObject.SelectedIndex = value; }
        }

        public MainForm()
        {
            InitializeComponent();

            modelViewerControl.MouseWheel += new MouseEventHandler(modelViewerControl_MouseWheel);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Engine.MainForm = this;

            Engine.ContentLoader.LoadTexture("DottedLine.tga", "DottedLine");
            //Engine.ContentLoader.LoadFont("DiplomFont.spritefont", "DiplomFont");

            modelViewerControl.Load();

            cmbSubObject.SelectedIndex = (int)SubObjectMode.None;
            cmbTransform.SelectedIndex = (int)TransformationMode.Translate;
            cmbDraw.SelectedIndex = (int)DrawMode.EntityOnly;

            modelViewerControl.AddNewSceneEntity();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Engine.IsChangesUnsaved)
                if (!ShowUnsaveChangesDialog())
                    e.Cancel = true;
        }

        #region Menu clicks
        private void ExitMenuClicked(object sender, EventArgs e)
        {
            Close();
        }

        private void OpenMenuClicked(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string relativePath = Path.Combine(assemblyLocation, "../Scenes");
            string scenesPath = Path.GetFullPath(relativePath);

            ofd.InitialDirectory = scenesPath;
            ofd.Title = "Open scene";
            ofd.Filter = "Diplom Files (*.d)|*.d;";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (Engine.IsChangesUnsaved)
                    if (!ShowUnsaveChangesDialog())
                        return;

                Engine.Reset();
                Engine.AssociatedFile = ofd.FileName;
                LoadScene();
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Engine.IsChangesUnsaved) return;
            if (Engine.AssociatedFile == "")
                SaveAsToolStripMenuItem_Click(this, new EventArgs());
            else
                SaveScene();
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string relativePath = Path.Combine(assemblyLocation, "../Scenes");
            string scenesPath = Path.GetFullPath(relativePath);

            sfd.InitialDirectory = scenesPath;
            sfd.FileName = "DiplomScene.d";
            sfd.Title = "Save scene";
            sfd.Filter = "Diplom Files (*.d)|*.d;";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Engine.AssociatedFile = sfd.FileName;
                SaveScene();
            }
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Engine.IsChangesUnsaved)
                if (!ShowUnsaveChangesDialog())
                    return;

            Engine.Reset();
            Engine.AssociatedFile = "";
            Engine.IsChangesUnsaved = false;
        }

        private void ExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string relativePath = Path.Combine(assemblyLocation, "../Export");
            string scenesPath = Path.GetFullPath(relativePath);

            sfd.InitialDirectory = scenesPath;
            sfd.FileName = "DiplomScene.obj";
            sfd.Title = "Export scene";
            sfd.Filter = "Object Files (*.obj)|*.obj;";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                ExportScene(sfd.FileName);
            }
        }
        #endregion

        #region Mouse events
        private void modelViewerControl_MouseDown(object sender, MouseEventArgs e)
        {
            modelViewerControl.Focus();

            switch (e.Button)
            {
                case MouseButtons.Left:
                    mouseOldPos.X = e.X;
                    mouseOldPos.Y = e.Y;
                    modelViewerControl.MyLeftMouseDown(e.X, e.Y);
                    break;
            }
        }

        private void modelViewerControl_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    modelViewerControl.MyLeftMouseUp(e.X, e.Y);
                    break;
            }
        }

        private void modelViewerControl_MouseMove(object sender, MouseEventArgs e)
        {
            int deltaX = mouseOldPos.X - e.X;
            int deltaY = mouseOldPos.Y - e.Y;

            Engine.PreviousMouseRay = Engine.CurrentMouseRay;
            Engine.CurrentMouseRay = Utils.ConvertMouseToRay(e.X, e.Y);

            Engine.PreviousMousePos = Engine.CurrentMousePos;
            Engine.CurrentMousePos = new Microsoft.Xna.Framework.Vector2((float)e.X, (float)e.Y);

            if (e.Button == MouseButtons.None)
                modelViewerControl.MyMouseMove(e.X, e.Y);
            else
                modelViewerControl.MyMouseDrag(e.X, e.Y, deltaX, deltaY, e.Button);

            mouseOldPos.X = e.X;
            mouseOldPos.Y = e.Y;
        }

        private void modelViewerControl_MouseWheel(object sender, MouseEventArgs e)
        {
            modelViewerControl.MyMouseWheel(e.Delta);
        }
        #endregion

        private void btnCameraLookAt_Click(object sender, EventArgs e)
        {
            Engine.ActiveCamera.LookAtSelection();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            modelViewerControl.MyKeyDown(e);

            //temp
            if (e.KeyCode == Keys.A)
                modelViewerControl.AddNewSceneEntity();
        }

        private void modelViewerControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.None && IsEnabledSubObjCmbBox)
            {
                #region None
                switch (e.KeyCode)
                {
                    case Keys.D1:
                        cmbSubObject.SelectedIndex = (int)SubObjectMode.None;
                        break;
                    case Keys.D2:
                        cmbSubObject.SelectedIndex = (int)SubObjectMode.Vertex;
                        break;
                    case Keys.D3:
                        cmbSubObject.SelectedIndex = (int)SubObjectMode.Edge;
                        break;
                    case Keys.D4:
                        cmbSubObject.SelectedIndex = (int)SubObjectMode.Triangle;
                        break;
                }
                #endregion
            }

            if (e.Modifiers == Keys.Shift)
            {
                #region Shift
                switch (e.KeyCode)
                {
                    case Keys.D1:
                        cmbTransform.SelectedIndex = (int)TransformationMode.Translate;
                        break;
                    case Keys.D2:
                        cmbTransform.SelectedIndex = (int)TransformationMode.Rotate;
                        break;
                    case Keys.D3:
                        cmbTransform.SelectedIndex = (int)TransformationMode.Scale;
                        break;
                }
                #endregion
            }

            if (e.Modifiers == Keys.Control)
            {
                #region Control
                switch (e.KeyCode)
                {
                    case Keys.D1:
                        cmbDraw.SelectedIndex = (int)DrawMode.EntityOnly;
                        break;
                    case Keys.D2:
                        cmbDraw.SelectedIndex = (int)DrawMode.WithEdges;
                        break;
                }
                #endregion
            }
            e.Handled = true;
        }

        private void comboBoxes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmbSender = sender as ComboBox;
            switch (cmbSender.Name)
            {
                case "cmbSubObject":
                    Engine.ActiveSubObjectMode = (SubObjectMode)cmbSender.SelectedIndex;
                    break;
                case "cmbTransform":
                    Engine.ActiveTransformMode = (TransformationMode)cmbSender.SelectedIndex;
                    break;
                case "cmbDraw":
                    Engine.ActiveDrawMode = (DrawMode)cmbSender.SelectedIndex;
                    break;
                default:
                    break;
            }
        }

        private void numericUpDowns_ValueChanged(object sender, EventArgs e)
        {
            if (isDrag || Engine.EntitySelectionPool.Count == 0) return;

            NumericUpDown nudSender = sender as NumericUpDown;
            Microsoft.Xna.Framework.Vector3 value = new Microsoft.Xna.Framework.Vector3(0);

            switch (nudSender.Name)
            {
                case "nudX":
                    value.X = (float)nudSender.Value;
                    break;
                case "nudY":
                    value.Y = (float)nudSender.Value;
                    break;
                case "nudZ":
                    value.Z = (float)nudSender.Value;
                    break;
            }

            Engine.StartAction(ActionType.VertexData);
            switch (Engine.ActiveTransformMode)
            {
                case TransformationMode.Translate:
                    value.X = (float)nudX.Value;
                    value.Y = (float)nudY.Value;
                    value.Z = (float)nudZ.Value;
                    Transformer.TranslateByValue(value);
                    break;
                case TransformationMode.Rotate:
                    Transformer.RotateByValue(value);
                    ResetNumericUpDowns();
                    break;
                case TransformationMode.Scale:
                    Transformer.ScaleByValue(value);
                    ResetNumericUpDowns();
                    break;
            }
            Engine.EndAction();
        }


        private bool ShowUnsaveChangesDialog()
        {
            switch (MessageBox.Show("Do you want to save changes?", "Diplom", MessageBoxButtons.YesNoCancel))
            {
                case DialogResult.Cancel:
                    return false;
                case DialogResult.No:
                    return true;
                case DialogResult.Yes:
                    SaveToolStripMenuItem_Click(this, new EventArgs());
                    return true;
                default:
                    return false;
            }
        }

        private void SaveScene()
        {
            Stream stream = File.Open(Engine.AssociatedFile, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, Engine.SceneEntities);
            stream.Close();

            Engine.IsChangesUnsaved = false;
        }

        private void LoadScene()
        {
            Cursor.Current = Cursors.WaitCursor;
            using (PleaseWaitForm pwf = new PleaseWaitForm())
            {
                pwf.Location = new Point(this.Location.X + this.Width / 2 - pwf.Width / 2,
                                        this.Location.Y + this.Height / 2 - pwf.Height / 2);
                pwf.Show();
                pwf.Update();

                Stream stream = File.Open(Engine.AssociatedFile, FileMode.Open);
                BinaryFormatter bFormatter = new BinaryFormatter();
                Engine.SceneEntities = (List<SceneEntity>)bFormatter.Deserialize(stream);
                stream.Close();
                Engine.IsChangesUnsaved = false;
            }
            Cursor.Current = Cursors.Default;
        }

        private void ExportScene(string fileName)
        {
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            var backupCulture = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            List<Microsoft.Xna.Framework.Vector3> verts = new List<Microsoft.Xna.Framework.Vector3>();
            List<Microsoft.Xna.Framework.Vector3> norms = new List<Microsoft.Xna.Framework.Vector3>();

            StringBuilder sb = new StringBuilder();
            foreach (var entity in Engine.SceneEntities)
            {
                sb.AppendLine("o entity_" + entity.Id);

                foreach (var vert in entity.VertexPositions)
                {
                    if (!verts.Contains(vert))
                    {
                        verts.Add(vert);
                        sb.Append("v ").Append(Math.Round(vert.X, 6)).Append(" ").Append(Math.Round(vert.Y, 6)).Append(" ").Append(Math.Round(vert.Z, 6)).AppendLine();
                    }
                }

                foreach (var vert in entity.VertexData)
                {
                    if (!norms.Contains(vert.Normal))
                    {
                        norms.Add(vert.Normal);
                        sb.Append("vn ").Append(Math.Round(vert.Normal.X, 6)).Append(" ").Append(Math.Round(vert.Normal.Y, 6)).Append(" ").Append(Math.Round(vert.Normal.Z, 6)).AppendLine();
                    }
                }

                for (int i = 0; i < entity.VertexData.Length; i += 3)
                {
                    sb.Append("f ");
                    sb.Append(verts.IndexOf(entity.VertexData[i].Position) + 1).Append("//").Append(norms.IndexOf(entity.VertexData[i].Normal) + 1).Append(" ");
                    sb.Append(verts.IndexOf(entity.VertexData[i + 1].Position) + 1).Append("//").Append(norms.IndexOf(entity.VertexData[i + 1].Normal) + 1).Append(" ");
                    sb.Append(verts.IndexOf(entity.VertexData[i + 2].Position) + 1).Append("//").Append(norms.IndexOf(entity.VertexData[i + 2].Normal) + 1).AppendLine();
                }
            }
            File.WriteAllText(fileName, sb.ToString(), Encoding.ASCII);

            System.Threading.Thread.CurrentThread.CurrentCulture = backupCulture;
        }


        public void SetNumericUpDowns(Microsoft.Xna.Framework.Vector3 value)
        {
            isDrag = true;
            nudX.Value = (decimal)value.X;
            nudY.Value = (decimal)value.Y;
            nudZ.Value = (decimal)value.Z;
            isDrag = false;
        }

        public void ResetNumericUpDowns()
        {
            isDrag = true;
            nudX.Value = 0;
            nudY.Value = 0;
            nudZ.Value = 0;
            isDrag = false;
        }

        
    }
}
