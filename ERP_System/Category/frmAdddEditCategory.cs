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
    public partial class frmAdddEditCategory : Form
    {
        private enum _enMode { eAddNew = 1, eUpdate = 2 }
        _enMode _Mode = _enMode.eAddNew;
        private int _CategoryID = -1;
        public frmAdddEditCategory()
        {
            InitializeComponent();
            _Mode = _enMode.eAddNew;
        }

        public frmAdddEditCategory(int CategoryID)
        {
            InitializeComponent();
            _CategoryID = CategoryID;
            _Mode = _enMode.eUpdate;
        }

        private void frmAdddEditCategory_Load(object sender, EventArgs e)
        {

        }
    }
}
