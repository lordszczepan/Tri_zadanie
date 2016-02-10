using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Tri_zadanie
{
    partial class Form1
    {

        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbPntCount;
        private System.Windows.Forms.Label lbTriCount;
        private System.Windows.Forms.Label lbMousePos;
        private System.Windows.Forms.Button btnRestart;

        private List<Triangulacja.Geometry.Point> Vertices;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lbPntCount = new System.Windows.Forms.Label();
            this.lbTriCount = new System.Windows.Forms.Label();
            this.lbMousePos = new System.Windows.Forms.Label();
            this.btnRestart = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(8, 32);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(446, 342);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseMove);
            // 
            // lbPntCount
            // 
            this.lbPntCount.Location = new System.Drawing.Point(8, 8);
            this.lbPntCount.Name = "lbPntCount";
            this.lbPntCount.Size = new System.Drawing.Size(120, 16);
            this.lbPntCount.TabIndex = 1;
            this.lbPntCount.Text = "# punktów";
            this.lbPntCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbTriCount
            // 
            this.lbTriCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTriCount.Location = new System.Drawing.Point(134, 8);
            this.lbTriCount.Name = "lbTriCount";
            this.lbTriCount.Size = new System.Drawing.Size(128, 16);
            this.lbTriCount.TabIndex = 2;
            this.lbTriCount.Text = "# trójkątów";
            this.lbTriCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbMousePos
            // 
            this.lbMousePos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbMousePos.Location = new System.Drawing.Point(16, 382);
            this.lbMousePos.Name = "lbMousePos";
            this.lbMousePos.Size = new System.Drawing.Size(438, 16);
            this.lbMousePos.TabIndex = 3;
            this.lbMousePos.Text = "X,Y";
            this.lbMousePos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnRestart
            // 
            this.btnRestart.BackColor = System.Drawing.SystemColors.Control;
            this.btnRestart.Location = new System.Drawing.Point(268, 5);
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.Size = new System.Drawing.Size(75, 23);
            this.btnRestart.TabIndex = 4;
            this.btnRestart.Text = "Restart";
            this.btnRestart.UseVisualStyleBackColor = false;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 403);
            this.Controls.Add(this.btnRestart);
            this.Controls.Add(this.lbMousePos);
            this.Controls.Add(this.lbTriCount);
            this.Controls.Add(this.lbPntCount);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Triangulacja Delauney";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        [STAThread]
        static void Main()
        {
            Application.Run(new Form1());
        }
        private void pictureBox2_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            lbMousePos.Text = e.X.ToString() + "," + e.Y.ToString();
        }

        private void pictureBox2_MouseDown(System.Object eventSender, System.Windows.Forms.MouseEventArgs eventArgs)
        {
            Image b = (Image)new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(b);
            g.CompositingMode = CompositingMode.SourceOver;
            g.SmoothingMode = SmoothingMode.None;
            Pen myPen = new Pen(Color.Blue, 1);

            Triangulacja.Geometry.Point pNew = new Triangulacja.Geometry.Point(eventArgs.X, eventArgs.Y);

            if (!Vertices.Exists(delegate (Triangulacja.Geometry.Point p) { return pNew.Equals2D(p); }))
                Vertices.Add(pNew);

            if (Vertices.Count > 2)
            {
                List<Triangulacja.Geometry.Triangle> tris = Triangulacja.Delauney.Triangulate(Vertices);

                foreach (Triangulacja.Geometry.Triangle t in tris)
                {
                    g.DrawLine(myPen, (float)Vertices[t.p1].X, (float)Vertices[t.p1].Y, (float)Vertices[t.p2].X, (float)Vertices[t.p2].Y);
                    g.DrawLine(myPen, (float)Vertices[t.p2].X, (float)Vertices[t.p2].Y, (float)Vertices[t.p3].X, (float)Vertices[t.p3].Y);
                    g.DrawLine(myPen, (float)Vertices[t.p1].X, (float)Vertices[t.p1].Y, (float)Vertices[t.p3].X, (float)Vertices[t.p3].Y);
                }
                lbTriCount.Text = "Trójkątów: " + tris.Count;
            }

            for (int i = 0; i < Vertices.Count; i++)
                g.DrawEllipse(Pens.Red, (float)Vertices[i].X - 2, (float)Vertices[i].Y - 2, 4, 4);


            lbPntCount.Text = "Punktów: " + Vertices.Count.ToString();

            g.Dispose();
            pictureBox1.Image = b;
        }

    }
}

