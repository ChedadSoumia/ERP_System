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

        private enum _enMode { eAddNew = 1, eUpdate = 2 }
        _enMode _Mode = _enMode.eAddNew;

        private int _BrandID = -1;
        private clsBrand _Brand;
        public frmAddEditBrand()
        {
            InitializeComponent();
            _Mode = _enMode.eAddNew;
        }
        public frmAddEditBrand(int BrandID)
        {
            InitializeComponent();
            _BrandID = BrandID;
            _Mode = _enMode.eUpdate;
        }


        private void frmAddEditBrand_Load(object sender, EventArgs e)
        {
            _ResetDefaultValue();
            if (_Mode == _enMode.eUpdate)
                _LoadDate();
        }

        private void _LoadDate()
        {
            _Brand = clsBrand.Find(_BrandID);
            if (_Brand == null)
            {
                MessageBox.Show("Aucune catégorie avec l’ID = " + _BrandID, " marque introuvable", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }
            lblBrandID.Text = _Brand.BrandID.ToString();
            txtBrandName.Text = _Brand.BrandName;
        }

        private void _ResetDefaultValue()
        {

            if (_Mode == _enMode.eAddNew)
            {
                this.Text = "Ajouter une brand";
                lblMainTitle.Text = "Ajouter une brand";
                _Brand = new clsBrand();
            }
            else
            {
                this.Text = "Modifier une brand";
                lblMainTitle.Text = "Modifier une brand";
            }
            txtBrandName.Text = "";
        }

        private void txtBrandName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtBrandName.Text.Trim().Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtBrandName, "Ce champ est obligatoire !");
                return;
            }
            else
            {
                errorProvider1.SetError(txtBrandName, null);
            }

            if (clsBrand.IsBrandExist(txtBrandName.Text.Trim()) && txtBrandName.Text.Trim() != _Brand.BrandName.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtBrandName, "Ce nom de marque existe déjà.");
                return;
            }
            else
            {
                errorProvider1.SetError(txtBrandName, null);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Certains champs ne sont pas valides ! Placez la souris sur la ou les icônes rouges pour voir l’erreur.", "Erreur de validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

             _Brand.BrandName = txtBrandName.Text.Trim();

            if (_Brand.Save())
            {
                lblBrandID.Text = _Brand.BrandID.ToString();
                //change form mode to update.
                _Mode = _enMode.eUpdate;

                this.Text = "Modifier une marque";
                lblMainTitle.Text = "Modifier une marque";

                MessageBox.Show("Données enregistrées avec succès.", "Enregistré", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Erreur : les données ne sont pas enregistrées.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
