using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.CategoryService
{
    public class CategoryDto
    {
        #region Properties Region

        public long CategoryId { get; private set; }
        public String Name { get; private set; }

        #endregion

        public CategoryDto(long categoryId, string name)
        {
            this.CategoryId = categoryId;
            this.Name = name;
        }

        public override bool Equals(object obj)
        {

            CategoryDto target = (CategoryDto)obj;

            return (this.CategoryId == target.CategoryId)
                  && (this.Name == target.Name);
        }

        public override int GetHashCode()
        {
            return this.CategoryId.GetHashCode() * 31 * this.Name.GetHashCode();
        }

        public override String ToString()
        {
            String strCategoryDetails;

            strCategoryDetails =
                "[ categoryId = " + CategoryId + ", name = " + Name + " ]";


            return strCategoryDetails;
        }
    }
}
