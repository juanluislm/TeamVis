using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
//EMGU
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;

//System
using System.Threading;
using System.Diagnostics;

namespace VideoCapture
{
    public partial class Form1 : Form
    {


        /// <summary>
        /// Initialises controls on Form1
        /// </summary>
        public Form1()
        {
            InitializeComponent();           
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            Record rec = new Record();
            rec.MdiParent = this;
            rec.Show();
            this.button1.Visible.Equals(false);
            button2.Visible.Equals(false);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Debrief de = new Debrief();
            de.MdiParent = this;
            de.Show();
            button1.Visible = false;
            button2.Visible = false;
        }

    }
}
