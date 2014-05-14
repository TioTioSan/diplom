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
#endregion

namespace Diplom
{
    /// <summary>
    /// Custom form provides the main user interface for the program.
    /// In this sample we used the designer to fill the entire form with a
    /// ModelViewerControl, except for the menu bar which provides the
    /// "File / Open..." option.
    /// </summary>
    public partial class MainForm : Form
    {
        Point mouseOldPos = new Point();

        public MainForm()
        {
            InitializeComponent();

            modelViewerControl.MouseWheel += new MouseEventHandler(modelViewerControl_MouseWheel);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Engine.ContentLoader.LoadTexture("DottedLine.tga", "DottedLine");

            modelViewerControl.Load();

            Engine.SceneEntities.Add(new SceneEntity(new Cube(), modelViewerControl.GraphicsDevice));
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

        private void modelViewerControl_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.A:
                    Engine.SceneEntities.Add(new SceneEntity(new Cube(), modelViewerControl.GraphicsDevice));
                    break;
                case Keys.D1:
                    Engine.ActiveSubObjectMode = SubObjectMode.None;
                    break;
                case Keys.D2:
                    Engine.ActiveSubObjectMode = SubObjectMode.Vertex;
                    break;
                case Keys.D3:
                    Engine.ActiveSubObjectMode = SubObjectMode.Edge;
                    break;
                case Keys.D4:
                    Engine.ActiveSubObjectMode = SubObjectMode.Triangle;
                    break;
            }
        }
    }
}
