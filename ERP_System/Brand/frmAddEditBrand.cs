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

namespace ERP_System.Brand
{
    public partial class frmAddEditBrand : Form
    {
        private int _BrandID = -1;
        private clsBrand _Brand;
        public frmAddEditBrand()
        {
            InitializeComponent();
        }
        public frmAddEditBrand(int BrandID)
        {
            InitializeComponent();
            _BrandID = BrandID;
        }
    }
}
