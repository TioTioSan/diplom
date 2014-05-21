using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;

namespace Diplom.Forms
{
    public partial class MakeVertexForm : Form
    {
        public Vector3 result;

        public MakeVertexForm()
        {
            InitializeComponent();
        }

        private void MakeVertexForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            result = new Vector3((float)nudX.Value, (float)nudY.Value, (float)nudZ.Value);
        }
    }
}
