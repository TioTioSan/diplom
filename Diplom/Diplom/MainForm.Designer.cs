namespace Diplom
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ExportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AttachToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.MakeVertexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MakeEdgeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MakeTriangleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AddToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PlaneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CubeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CylinderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SphereToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbSubObject = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbTransform = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbDraw = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.modelViewerControl = new Diplom.ModelViewerControl();
            this.nudX = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.nudY = new System.Windows.Forms.NumericUpDown();
            this.labelZ = new System.Windows.Forms.Label();
            this.nudZ = new System.Windows.Forms.NumericUpDown();
            this.btnLookAtSelection = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudZ)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.EditToolStripMenuItem,
            this.AddToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(792, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.toolStripSeparator2,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator3,
            this.ExportToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.NewToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(143, 6);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenMenuClicked);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveAsToolStripMenuItem.Text = "Save as";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAsToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(143, 6);
            // 
            // ExportToolStripMenuItem
            // 
            this.ExportToolStripMenuItem.Name = "ExportToolStripMenuItem";
            this.ExportToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.ExportToolStripMenuItem.Text = "Export";
            this.ExportToolStripMenuItem.Click += new System.EventHandler(this.ExportToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(143, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitMenuClicked);
            // 
            // EditToolStripMenuItem
            // 
            this.EditToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AttachToolStripMenuItem,
            this.toolStripSeparator4,
            this.MakeVertexToolStripMenuItem,
            this.MakeEdgeToolStripMenuItem,
            this.MakeTriangleToolStripMenuItem});
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            this.EditToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.EditToolStripMenuItem.Text = "Edit";
            // 
            // AttachToolStripMenuItem
            // 
            this.AttachToolStripMenuItem.Enabled = false;
            this.AttachToolStripMenuItem.Name = "AttachToolStripMenuItem";
            this.AttachToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
            this.AttachToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.AttachToolStripMenuItem.Text = "Attach";
            this.AttachToolStripMenuItem.Click += new System.EventHandler(this.AttachToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(180, 6);
            // 
            // MakeVertexToolStripMenuItem
            // 
            this.MakeVertexToolStripMenuItem.Enabled = false;
            this.MakeVertexToolStripMenuItem.Name = "MakeVertexToolStripMenuItem";
            this.MakeVertexToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.V)));
            this.MakeVertexToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.MakeVertexToolStripMenuItem.Text = "Make vertex";
            this.MakeVertexToolStripMenuItem.Click += new System.EventHandler(this.MakeVertexToolStripMenuItem_Click);
            // 
            // MakeEdgeToolStripMenuItem
            // 
            this.MakeEdgeToolStripMenuItem.Enabled = false;
            this.MakeEdgeToolStripMenuItem.Name = "MakeEdgeToolStripMenuItem";
            this.MakeEdgeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.E)));
            this.MakeEdgeToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.MakeEdgeToolStripMenuItem.Text = "Make edge";
            this.MakeEdgeToolStripMenuItem.Click += new System.EventHandler(this.MakeEdgeToolStripMenuItem_Click);
            // 
            // MakeTriangleToolStripMenuItem
            // 
            this.MakeTriangleToolStripMenuItem.Enabled = false;
            this.MakeTriangleToolStripMenuItem.Name = "MakeTriangleToolStripMenuItem";
            this.MakeTriangleToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.T)));
            this.MakeTriangleToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.MakeTriangleToolStripMenuItem.Text = "Make triangle";
            this.MakeTriangleToolStripMenuItem.Click += new System.EventHandler(this.MakeTriangleToolStripMenuItem_Click);
            // 
            // AddToolStripMenuItem
            // 
            this.AddToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PlaneToolStripMenuItem,
            this.CubeToolStripMenuItem,
            this.CylinderToolStripMenuItem,
            this.SphereToolStripMenuItem});
            this.AddToolStripMenuItem.Name = "AddToolStripMenuItem";
            this.AddToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.AddToolStripMenuItem.Text = "Add";
            // 
            // PlaneToolStripMenuItem
            // 
            this.PlaneToolStripMenuItem.Name = "PlaneToolStripMenuItem";
            this.PlaneToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.PlaneToolStripMenuItem.Text = "Plane";
            this.PlaneToolStripMenuItem.Click += new System.EventHandler(this.PlaneToolStripMenuItem_Click);
            // 
            // CubeToolStripMenuItem
            // 
            this.CubeToolStripMenuItem.Name = "CubeToolStripMenuItem";
            this.CubeToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.CubeToolStripMenuItem.Text = "Cube";
            this.CubeToolStripMenuItem.Click += new System.EventHandler(this.CubeToolStripMenuItem_Click);
            // 
            // CylinderToolStripMenuItem
            // 
            this.CylinderToolStripMenuItem.Name = "CylinderToolStripMenuItem";
            this.CylinderToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.CylinderToolStripMenuItem.Text = "Cylinder";
            this.CylinderToolStripMenuItem.Click += new System.EventHandler(this.CylinderToolStripMenuItem_Click);
            // 
            // SphereToolStripMenuItem
            // 
            this.SphereToolStripMenuItem.Name = "SphereToolStripMenuItem";
            this.SphereToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.SphereToolStripMenuItem.Text = "Sphere";
            this.SphereToolStripMenuItem.Click += new System.EventHandler(this.SphereToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Sub object:";
            // 
            // cmbSubObject
            // 
            this.cmbSubObject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSubObject.Enabled = false;
            this.cmbSubObject.FormattingEnabled = true;
            this.cmbSubObject.Items.AddRange(new object[] {
            "None",
            "Vertex",
            "Edge",
            "Triangle"});
            this.cmbSubObject.Location = new System.Drawing.Point(79, 27);
            this.cmbSubObject.Name = "cmbSubObject";
            this.cmbSubObject.Size = new System.Drawing.Size(146, 21);
            this.cmbSubObject.TabIndex = 1;
            this.cmbSubObject.SelectedIndexChanged += new System.EventHandler(this.comboBoxes_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(231, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Transform:";
            // 
            // cmbTransform
            // 
            this.cmbTransform.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTransform.FormattingEnabled = true;
            this.cmbTransform.Items.AddRange(new object[] {
            "Translate",
            "Rotate",
            "Scale"});
            this.cmbTransform.Location = new System.Drawing.Point(294, 27);
            this.cmbTransform.Name = "cmbTransform";
            this.cmbTransform.Size = new System.Drawing.Size(121, 21);
            this.cmbTransform.TabIndex = 2;
            this.cmbTransform.SelectedIndexChanged += new System.EventHandler(this.comboBoxes_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(421, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Draw:";
            // 
            // cmbDraw
            // 
            this.cmbDraw.DisplayMember = "ved";
            this.cmbDraw.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDraw.FormattingEnabled = true;
            this.cmbDraw.Items.AddRange(new object[] {
            "Object",
            "Egde"});
            this.cmbDraw.Location = new System.Drawing.Point(462, 27);
            this.cmbDraw.Name = "cmbDraw";
            this.cmbDraw.Size = new System.Drawing.Size(121, 21);
            this.cmbDraw.TabIndex = 3;
            this.cmbDraw.SelectedIndexChanged += new System.EventHandler(this.comboBoxes_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.modelViewerControl);
            this.panel1.Location = new System.Drawing.Point(12, 54);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(768, 380);
            this.panel1.TabIndex = 4;
            // 
            // modelViewerControl
            // 
            this.modelViewerControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.modelViewerControl.Location = new System.Drawing.Point(0, 0);
            this.modelViewerControl.Name = "modelViewerControl";
            this.modelViewerControl.Size = new System.Drawing.Size(764, 376);
            this.modelViewerControl.TabIndex = 0;
            this.modelViewerControl.Text = "modelViewerControl";
            this.modelViewerControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.modelViewerControl_KeyDown);
            this.modelViewerControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.modelViewerControl_MouseDown);
            this.modelViewerControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.modelViewerControl_MouseMove);
            this.modelViewerControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.modelViewerControl_MouseUp);
            // 
            // nudX
            // 
            this.nudX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudX.DecimalPlaces = 6;
            this.nudX.Enabled = false;
            this.nudX.Location = new System.Drawing.Point(32, 440);
            this.nudX.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
            this.nudX.Minimum = new decimal(new int[] {
            -727379969,
            232,
            0,
            -2147483648});
            this.nudX.Name = "nudX";
            this.nudX.Size = new System.Drawing.Size(106, 20);
            this.nudX.TabIndex = 4;
            this.nudX.ValueChanged += new System.EventHandler(this.numericUpDowns_ValueChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 442);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "X:";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(144, 442);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Y:";
            // 
            // nudY
            // 
            this.nudY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudY.DecimalPlaces = 6;
            this.nudY.Enabled = false;
            this.nudY.Location = new System.Drawing.Point(167, 440);
            this.nudY.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
            this.nudY.Minimum = new decimal(new int[] {
            -727379969,
            232,
            0,
            -2147483648});
            this.nudY.Name = "nudY";
            this.nudY.Size = new System.Drawing.Size(106, 20);
            this.nudY.TabIndex = 5;
            this.nudY.ValueChanged += new System.EventHandler(this.numericUpDowns_ValueChanged);
            // 
            // labelZ
            // 
            this.labelZ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelZ.AutoSize = true;
            this.labelZ.Location = new System.Drawing.Point(279, 442);
            this.labelZ.Name = "labelZ";
            this.labelZ.Size = new System.Drawing.Size(17, 13);
            this.labelZ.TabIndex = 2;
            this.labelZ.Text = "Z:";
            // 
            // nudZ
            // 
            this.nudZ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudZ.DecimalPlaces = 6;
            this.nudZ.Enabled = false;
            this.nudZ.Location = new System.Drawing.Point(302, 440);
            this.nudZ.Maximum = new decimal(new int[] {
            -727379969,
            232,
            0,
            0});
            this.nudZ.Minimum = new decimal(new int[] {
            -727379969,
            232,
            0,
            -2147483648});
            this.nudZ.Name = "nudZ";
            this.nudZ.Size = new System.Drawing.Size(106, 20);
            this.nudZ.TabIndex = 6;
            this.nudZ.ValueChanged += new System.EventHandler(this.numericUpDowns_ValueChanged);
            // 
            // btnLookAtSelection
            // 
            this.btnLookAtSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLookAtSelection.Enabled = false;
            this.btnLookAtSelection.Location = new System.Drawing.Point(668, 440);
            this.btnLookAtSelection.Name = "btnLookAtSelection";
            this.btnLookAtSelection.Size = new System.Drawing.Size(112, 20);
            this.btnLookAtSelection.TabIndex = 7;
            this.btnLookAtSelection.Text = "Look at selection";
            this.btnLookAtSelection.UseVisualStyleBackColor = true;
            this.btnLookAtSelection.Click += new System.EventHandler(this.btnCameraLookAt_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 472);
            this.Controls.Add(this.btnLookAtSelection);
            this.Controls.Add(this.nudZ);
            this.Controls.Add(this.nudY);
            this.Controls.Add(this.nudX);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cmbDraw);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbTransform);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelZ);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbSubObject);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Diplom";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudZ)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private ModelViewerControl modelViewerControl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbSubObject;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbTransform;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbDraw;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown nudX;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudY;
        private System.Windows.Forms.Label labelZ;
        private System.Windows.Forms.NumericUpDown nudZ;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem ExportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MakeVertexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MakeEdgeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MakeTriangleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AddToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PlaneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CubeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CylinderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SphereToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AttachToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.Button btnLookAtSelection;

    }
}

