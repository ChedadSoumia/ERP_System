using DVDL.Global_Classes;
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
using static ERP_System_Buisness.clsCategory;

namespace ERP_System.Category
{
    public partial class frmAdddEditCategory : Form
    {
        private enum _enMode { eAddNew = 1, eUpdate = 2 }
        _enMode _Mode = _enMode.eAddNew;
        private int _CategoryID = -1;
        clsCategory _Category;
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

        private void _FillCountriesInComoboBox()
        {
            DataTable dtCategoryList = clsCategory.GetAllCategories();

            cbCategoryParent.Items.Add("None");
            cbCategoryParent.SelectedIndex = 0;

            foreach (DataRow row in dtCategoryList.Rows)
            {
                cbCategoryParent.Items.Add(row["category_name"]);
            }
            
        }

        private void frmAdddEditCategory_Load(object sender, EventArgs e)
        {
            _ResetDefaultData();
            if(_Mode == _enMode.eUpdate)
            {
                _LoadData();
            }


        }

        private void _LoadData()
        {

            _Category = clsCategory.Find(_CategoryID);
            if (_Category == null)
            {
                MessageBox.Show("Aucune catégorie avec l’ID = " + _CategoryID, " Catégorie introuvable", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }
            lblCategoryID.Text = _Category.CategoryId.ToString();
            txtCategoryName.Text = _Category.CategoryName;
            cbCategoryParent.Text = _Category.CategoryParent.HasValue ? clsCategory.Find(_Category.CategoryParent.Value)?.CategoryName : "None";


        }

        private void _ResetDefaultData()
        {
            _FillCountriesInComoboBox();
            if (_Mode == _enMode.eAddNew)
            {
                this.Text = "Ajouter une catégorie";
                lblMainTitle.Text = "Ajouter une catégorie";
                _Category = new clsCategory();
            }
            else
            {
                this.Text = "Modifier une catégorie";
                lblMainTitle.Text = "Modifier une catégorie";
            }

            txtCategoryName.Text = "";

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Certains champs ne sont pas valides ! Placez la souris sur la ou les icônes rouges pour voir l’erreur.", "Erreur de validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            _Category.CategoryName = txtCategoryName.Text.Trim();
            _Category.CategoryParent = cbCategoryParent.SelectedIndex > 0 ? clsCategory.Find(cbCategoryParent.Text).CategoryId : (int?)null;


            if (_Category.Save())
            {
                lblCategoryID.Text = _Category.CategoryId.ToString();
                //change form mode to update.
                _Mode = _enMode.eUpdate;

                this.Text = "Modifier une catégorie";
                lblMainTitle.Text = "Modifier une catégorie";

                MessageBox.Show("Données enregistrées avec succès.", "Enregistré", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Erreur : les données ne sont pas enregistrées.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void txtCategoryName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCategoryName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCategoryName, "Ce champ est obligatoire !");
                return;
            }
            else
            {
                errorProvider1.SetError(txtCategoryName, null);
            }

            if (clsCategory.IsCategoryExist(txtCategoryName.Text) && txtCategoryName.Text != _Category.CategoryName)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCategoryName, "Ce nom de catégorie existe déjà.");
                return;
            }
            else
            {
                errorProvider1.SetError(txtCategoryName, null);
            }



        }
    }
}
