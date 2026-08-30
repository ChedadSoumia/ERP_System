using ERP_System_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP_System.Category
{
    public partial class frmCategoryList : Form
    {
        private DataTable _dtCategoryList;
        public frmCategoryList()
        {
            InitializeComponent();
        }

        private void frmCategoryList_Load(object sender, EventArgs e)
        {
            _dtCategoryList = clsCategory.GetAllCategories();

            dgvCategoryList.DataSource = _dtCategoryList;
            lblCountRecord.Text = dgvCategoryList.Rows.Count.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAdddEditCategory addEditCategory = new frmAdddEditCategory();
            addEditCategory.ShowDialog();
            frmCategoryList_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAdddEditCategory addEditCategory = new frmAdddEditCategory();
            addEditCategory.ShowDialog();
            frmCategoryList_Load(null, null);
        }
    }
}
