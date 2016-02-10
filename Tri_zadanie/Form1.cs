using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Tri_zadanie
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Vertices = new List<Triangulacja.Geometry.Point>();
        }
        
        private void btnRestart_Click(object sender, EventArgs e)
        {
            Vertices.Clear();
            pictureBox1.Image = null;
            lbTriCount.Text = "Trójkątów: 0";
            lbPntCount.Text = "Punktów: 0";
        }
    }
}
