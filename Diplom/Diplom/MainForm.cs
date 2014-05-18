#region File Description
//-----------------------------------------------------------------------------
// MainForm.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

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

        private void ExitMenuClicked(object sender, EventArgs e)
        {
            Close();
        }

        private void OpenMenuClicked(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            // Default to the directory which contains our content files.
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string relativePath = Path.Combine(assemblyLocation, "../../../../Content");
            string contentPath = Path.GetFullPath(relativePath);

            fileDialog.InitialDirectory = contentPath;

            fileDialog.Title = "Load Model";

            fileDialog.Filter = "Model Files (*.fbx;*.x)|*.fbx;*.x|" +
                                "FBX Files (*.fbx)|*.fbx|" +
                                "X Files (*.x)|*.x|" +
                                "All Files (*.*)|*.*";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {

            }
        }

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
