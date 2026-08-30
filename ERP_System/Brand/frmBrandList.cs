using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP_System.Brand
{
    public partial class frmBrandList : Form
    {
        public frmBrandList()
        {
            InitializeComponent();
        }

        private void btnNewBrand_Click(object sender, EventArgs e)
        {
            frmAddEditBrand AddNewBrand = new frmAddEditBrand();
            AddNewBrand.ShowDialog();
        }
    }
}
