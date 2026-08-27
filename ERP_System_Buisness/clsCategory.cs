using ERP_System_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_System_Buisness
{
    public class clsCategory
    {

        public enum enMode { eAddNew =1 , eUpdate = 2}
        public enMode Mode = enMode.eAddNew;

        public int CategoryId {  get; set; }
        public string CategoryName { get; set; }
        public int? CategoryParent {  get; set; }

        public clsCategory()
        {
            CategoryId = -1;
            CategoryName = "";
            CategoryParent = null;

            Mode = enMode.eAddNew;
        }
        public clsCategory(int categoryId, string categoryName, int? categoryParent)
        {
            CategoryId=categoryId;
            CategoryName=categoryName;
            CategoryParent=categoryParent;

            Mode = enMode.eUpdate;
        }

        private bool _AddNewCategories()
        {
            this.CategoryId = clsCategoryData.AddNewCategories(this.CategoryName, this.CategoryParent);
            return (this.CategoryId != -1);
        }
        private bool _UpdateCategory()
        {
            return clsCategoryData.UpdateCategory(this.CategoryId,this.CategoryName, this.CategoryParent);
        }

        public static clsCategory Find(int CategoryID)
        {
            string categoryName = "";
            int? categoryParent = null;

            bool isFound = clsCategoryData.GetCategoryByID(CategoryID,ref categoryName, ref categoryParent);

            if (isFound)
            {
                return new clsCategory(CategoryID,categoryName,categoryParent);
            }
            else
            {
                return null;
            }

        }
        public static clsCategory Find(string categoryName)
        {
            int CategoryID = -1;
            int? categoryParent = null;

            bool isFound = clsCategoryData.GetCategoryByName(ref CategoryID, categoryName, ref categoryParent);

            if (isFound)
            {
                return new clsCategory(CategoryID,categoryName,categoryParent);
            }
            else
            {
                return null;
            }

        }


        public static DataTable GetAllCategories()
        {
            return clsCategoryData.GetAllCategories();
        }

        public static bool IsCategoryExist(string CategoryName)
        {
            return clsCategoryData.IsCategoryExist(CategoryName);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.eAddNew:
                    if (_AddNewCategories())
                    {

                        Mode = enMode.eUpdate;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.eUpdate:

                    return _UpdateCategory();

            }

            return false;
        }
    }
}
