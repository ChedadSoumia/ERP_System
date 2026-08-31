using ERP_System_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_System_Buisness
{
    public  class clsBrand
    {
        public enum enMode { eAddNew = 1, eUpdate = 2 }
        public enMode Mode = enMode.eAddNew;
        public int BrandID { get; set; }
        public string BrandName { get; set; }

        public clsBrand()
        {
            BrandID = -1;
            BrandName = "";
            Mode = enMode.eAddNew;
        }
        private clsBrand(int brandID, string brandName)
        {
            BrandID = brandID;
            BrandName = brandName;
            Mode = enMode.eUpdate;
        }

        private bool _AddNewBrand()
        {
            this.BrandID = clsBrandData.AddNewbrand(this.BrandName);
            return (this.BrandID > -1);
        }
        private bool _UpdateBrand()
        {
            return clsBrandData.UpdateBrand(this.BrandID, this.BrandName);
        }

        public static clsBrand Find(int BrandID)
        {
            string brandName = "";
            bool isFound = clsBrandData.GetBrandByID(BrandID, ref brandName);
            if (isFound)
            {
                return new clsBrand(BrandID, brandName);
            }
            else
            {
                return null;
            }
        }
        public static clsBrand Find(string BrandName)
        {
            int brandID = -1;
            bool isFound = clsBrandData.GetBrandByName(ref brandID, BrandName);
            if (isFound)
            {
                return new clsBrand(brandID, BrandName);
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetAllBrands()
        {
            return clsBrandData.GetAllBrands();
        }
        public static bool DeleteBrand(int BrandID)
        {
            return clsBrandData.DeleteBrand(BrandID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.eAddNew:
                    if (_AddNewBrand())
                    {
                        Mode = enMode.eUpdate;
                        return true;

                    }
                    else
                    {
                        return false;
                    }
                case enMode.eUpdate:
                    return _UpdateBrand();

            }
            return false;
        }
    }
}
