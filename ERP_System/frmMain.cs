using ERP_System.Category;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP_System
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void categoryListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCategoryList categoryList = new frmCategoryList();
            categoryList.ShowDialog();        }
    }
}
