using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService
{
    public class UserInfo
    {
        #region Properties Region

        public long UserId { get; private set; }

        public String LoginName { get; private set; }

        public String FirstName { get; private set; }

        public String Lastname { get; private set; }

        public String Email { get; private set; }

        public string Language { get; private set; }

        public string Country { get; private set; }

        #endregion

        public UserInfo(long userId, string loginName, string firstName, string lastName,
            string email, string language, string country)
        {
            this.UserId = userId;
            this.LoginName = loginName;
            this.FirstName = firstName;
            this.Lastname = lastName;
            this.Email = email;
            this.Language = language;
            this.Country = country;
        }

        public override bool Equals(object obj)
        {

            UserInfo target = (UserInfo)obj;

            return (this.UserId == target.UserId)
                  && (this.LoginName == target.LoginName)
                  && (this.FirstName == target.FirstName)
                  && (this.Lastname == target.Lastname)
                  && (this.Email == target.Email)
                  && (this.Language == target.Language)
                  && (this.Country == target.Country);
        }

        public override int GetHashCode()
        {
            return this.FirstName.GetHashCode();
        }

        public override String ToString()
        {
            String strUserProfileDetails;

            strUserProfileDetails =
                "[ userId = " + UserId +
                "loginName = " + LoginName +
                "firstName = " + FirstName + " | " +
                "lastName = " + Lastname + " | " +
                "email = " + Email + " | " +
                "language = " + Language + " | " +
                "country = " + Country + " ]";


            return strUserProfileDetails;
        }
    }
}
