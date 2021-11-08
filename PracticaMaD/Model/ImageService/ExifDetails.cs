using System;


namespace Es.Udc.DotNet.PracticaMaD.Model.ImageService
{

    /// <summary>
    /// VO Class which contains the user details
    /// </summary>
    [Serializable()]
    public class ExifDetails
    {
        #region Properties Region

        public String Aperture { get; private set; }

        public String ExposureTime { get; private set; }

        public String Iso { get; private set; }

        public string WhiteBalance { get; private set; }

        #endregion

        public ExifDetails(String aperture, String exposureTime,
            String iso, String whiteBalance)
        {
            this.Aperture = aperture;
            this.ExposureTime = exposureTime;
            this.Iso = iso;
            this.WhiteBalance = whiteBalance;
        }

        public override bool Equals(object obj)
        {

            ExifDetails target = (ExifDetails)obj;

            return (this.Aperture == target.Aperture)
                  && (this.ExposureTime == target.ExposureTime)
                  && (this.Iso == target.Iso)
                  && (this.WhiteBalance == target.WhiteBalance);
        }

        // The GetHashCode method is used in hashing algorithms and data 
        // structures such as a hash table. In order to ensure that it works 
        // properly, we suppose that the Aperture does not change.        
        public override int GetHashCode()
        {            
            return this.Aperture.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the 
        /// current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current 
        /// <see cref="T:System.Object"></see>.
        /// </returns>
        public override String ToString()
        {
            String strExifDetails;

            strExifDetails =
                "[ aperture = " + Aperture + " | " +
                "exposureTime = " + ExposureTime + " | " +
                "iso = " + Iso + " | " +
                "whiteBalance = " + WhiteBalance + " ]";


            return strExifDetails;
        }
    }
}
