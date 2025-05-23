using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyChanNuoi.QuanLyNhanVien
{
    public partial class FrmThemNhanVien : Form
    {
        public FrmThemNhanVien()
        {
            InitializeComponent();
        }
        public string FormTitle
        {
            get => this.Text;
            set
            {
                this.Text = value;          
                label1.Text = value;      
            }
        }
        private void FrmThemNhanVien_Load(object sender, EventArgs e)
        {
            // Fix for CS0120: Use the 'Text' property of the current instance of the form.  
            label1.Text = this.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
