using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageService
{
    public class ImageDto
    {
        #region Properties Region

        public long ImageId { get; private set; }
        public String Title { get; private set; }
        public System.DateTime UploadDate { get; private set; }
        public String Aperture { get; private set; }
        public String ExposureTime { get; private set; }
        public String Iso { get; private set; }
        public String WhiteBalance { get; private set; }
        public long AuthorId { get; private set; }
        public String AuthorLogin { get; private set; }
        public long CategoryId { get; private set; }

        public String CategoryName { get; private set; }
        public byte[] ImageFile { get; private set; }
        public String ImageDescription { get; private set; }

        #endregion

        public ImageDto(long imageId, string title, DateTime uploadDate, string aperture,
            string exposureTime, string iso, string whiteBalance, long authorId, String authorLogin,
            long categoryId, String categoryName, byte[] imageFile, string imageDescription)
        {
            this.ImageId = imageId;
            this.Title = title;
            this.UploadDate = uploadDate;
            this.Aperture = aperture;
            this.ExposureTime = exposureTime;
            this.Iso = iso;
            this.WhiteBalance = whiteBalance;
            this.AuthorId = authorId;
            this.AuthorLogin = authorLogin;
            this.CategoryId = categoryId;
            this.CategoryName = categoryName;
            this.ImageFile = imageFile;
            this.ImageDescription = imageDescription;
        }

        public override bool Equals(object obj)
        {

            ImageDto target = (ImageDto)obj;

            return (this.ImageId == target.ImageId)
                  && (this.Title == target.Title)
                  && (this.UploadDate == target.UploadDate)
                  && (this.Aperture == target.Aperture)
                  && (this.ExposureTime == target.ExposureTime)
                  && (this.Iso == target.Iso)
                  && (this.WhiteBalance == target.WhiteBalance)
                  && (this.AuthorId == target.AuthorId)
                  && (this.AuthorLogin == target.AuthorLogin)
                  && (this.CategoryId == target.CategoryId)
                  && (this.CategoryName == target.CategoryName)
                  && (this.ImageFile == target.ImageFile)
                  && (this.ImageDescription == target.ImageDescription);
        }

        public override int GetHashCode()
        {
            return this.ImageDescription.GetHashCode();
        }

        public override String ToString()
        {
            String strImageDetails;

            strImageDetails =
                "[ imageId = " + ImageId + " ]";


            return strImageDetails;
        }
    }
}
