using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.TagService
{
    public class TagDto
    {
        #region Properties Region

        public long TagId { get; private set; }
        public String TagName { get; private set; }
        public long TaggedImagesNumber { get; private set; }

        #endregion

        public TagDto(long tagId, String name, long taggedImagesNumber)
        {
            this.TagId = tagId;
            this.TagName = name;
            this.TaggedImagesNumber = taggedImagesNumber;
        }

        public override bool Equals(object obj)
        {
            TagDto target = (TagDto)obj;

            return this.TagId == target.TagId
                && this.TagName == target.TagName
                && this.TaggedImagesNumber == target.TaggedImagesNumber;
        }

        public override int GetHashCode()
        {
            return this.TaggedImagesNumber.GetHashCode() * this.TagName.GetHashCode();
        }

        public override String ToString()
        {
            String strCommentDetails;

            strCommentDetails =
                "[ tagId = " + TagId + " ]";


            return strCommentDetails;
        }
    }
}
