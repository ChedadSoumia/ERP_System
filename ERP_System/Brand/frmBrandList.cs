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
    public partial class frmBrandList : Form
    {
        private DataTable _dtAllBrand;
        public frmBrandList()
        {
            InitializeComponent();
        }

        private void btnNewBrand_Click(object sender, EventArgs e)
        {
            frmAddEditBrand AddNewBrand = new frmAddEditBrand();
            AddNewBrand.ShowDialog();
            frmBrandList_Load(null, null);
        }

        private void frmBrandList_Load(object sender, EventArgs e)
        {
            cbFilter.Text = "Aucun";
            txtFilter.Visible = false;

            _dtAllBrand = clsBrand.GetAllBrands();
            dgvBrandList.DataSource = _dtAllBrand;

            lblCountRecord.Text = dgvBrandList.Rows.Count.ToString();


            if (dgvBrandList.Rows.Count > 0)
            {
                dgvBrandList.Columns[0].HeaderText = "ID de la marque";

                dgvBrandList.Columns[1].HeaderText = "Nom de la marque";

            }

        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.Text == "ID de la marque")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.Text == "Aucun")
            {
                txtFilter.Visible = false;
            }
            else
            {
                txtFilter.Visible = true;
                txtFilter.Text = "";
                txtFilter.Focus();
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbFilter.Text)
            {
                case "ID de la marque":
                    FilterColumn = "brand_id";
                    break;
                case "Nom de la marque":
                    FilterColumn = "brand_name";
                    break;
                
                default:
                    FilterColumn = "Aucun";
                    break;
            }

            if (FilterColumn == "Aucun" || txtFilter.Text == "")
            {
                _dtAllBrand.DefaultView.RowFilter = "";
                lblCountRecord.Text = dgvBrandList.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "brand_id")
            {
                _dtAllBrand.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilter.Text.Trim());
            }
            else
            {
                _dtAllBrand.DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", FilterColumn, txtFilter.Text.Trim());
            }


            lblCountRecord.Text = dgvBrandList.Rows.Count.ToString();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Êtes-vous sûr de vouloir supprimer la marque [" + dgvBrandList.CurrentRow.Cells[0].Value + "]", "Confirmer la suppression", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)

            {

                //Perform Delele and refresh
                if (clsBrand.DeleteBrand((int)dgvBrandList.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("marque supprimée avec succès.", "Réussi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmBrandList_Load(null, null);
                }

                else
                    MessageBox.Show("La marque n’a pas été supprimée car des données y sont liées.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditBrand AddNewBrand = new frmAddEditBrand((int)dgvBrandList.CurrentRow.Cells[0].Value);
            AddNewBrand.ShowDialog();
            frmBrandList_Load(null, null);
        }
    }
}
