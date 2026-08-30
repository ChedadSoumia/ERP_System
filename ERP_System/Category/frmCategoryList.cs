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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
            cbFilter.Text = "Aucun";
            txtFilter.Visible = false;

            _dtCategoryList = clsCategory.GetAllCategories();

            dgvCategoryList.DataSource = _dtCategoryList;
            lblCountRecord.Text = dgvCategoryList.Rows.Count.ToString();

            if (dgvCategoryList.Rows.Count > 0)
            {
                dgvCategoryList.Columns[0].HeaderText = "ID de la catégorie";
                dgvCategoryList.Columns[0].Width = 80;

                dgvCategoryList.Columns[1].HeaderText = "Nom de la catégorie";
                dgvCategoryList.Columns[1].Width = 80;


                dgvCategoryList.Columns[2].HeaderText = "Catégorie parente";
                dgvCategoryList.Columns[2].Width = 70;

                dgvCategoryList.Columns[3].HeaderText = "Nombre de sous-catégories";
                dgvCategoryList.Columns[3].Width = 90;


            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAdddEditCategory addEditCategory = new frmAdddEditCategory();
            addEditCategory.ShowDialog();
            frmCategoryList_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAdddEditCategory addEditCategory = new frmAdddEditCategory((int)dgvCategoryList.CurrentRow.Cells[0].Value);
            addEditCategory.ShowDialog();
            frmCategoryList_Load(null, null);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Êtes-vous sûr de vouloir supprimer la catégorie [" + dgvCategoryList.CurrentRow.Cells[0].Value + "]", "Confirmer la suppression", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)

            {

                //Perform Delele and refresh
                if (clsCategory.DeleteCategory((int)dgvCategoryList.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Catégorie supprimée avec succès.", "Réussi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmCategoryList_Load(null, null);
                }

                else
                    MessageBox.Show("La catégorie n’a pas été supprimée car des données y sont liées.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.SelectedIndex == 0) {
                txtFilter.Visible = false;
            }
            else{
                txtFilter.Visible = true;
                txtFilter.Text = "";
                txtFilter.Focus();
            }
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilter.Text == "ID de la catégorie" )
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbFilter.Text)
            {
                case "ID de la catégorie":
                    FilterColumn = "Category_id";
                    break;
                case "Nom de la catégorie":
                    FilterColumn = "Category_name";
                    break;
                case "Catégorie parente":
                    FilterColumn = "ParentCategory";
                    break;
                default:
                    FilterColumn = "Aucun";
                    break;
            }

            if(FilterColumn == "Aucun" || txtFilter.Text == "")
            {
                _dtCategoryList.DefaultView.RowFilter = "";
                lblCountRecord.Text = dgvCategoryList.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "Category_id")
            {
                _dtCategoryList.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilter.Text.Trim());
            }
            else
            {
                _dtCategoryList.DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", FilterColumn, txtFilter.Text.Trim());
            }


            lblCountRecord.Text = dgvCategoryList.Rows.Count.ToString();
        }
    }
}
