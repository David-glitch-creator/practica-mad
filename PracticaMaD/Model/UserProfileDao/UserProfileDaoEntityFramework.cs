using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Data;

using System.Linq;

using System.Data.Entity;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao
{
    public class UserProfileDaoEntityFramework : 
        GenericDaoEntityFramework<UserProfile, Int64>, IUserProfileDao
    {
        #region Public Constructors

        public UserProfileDaoEntityFramework()
        {
        }

        #endregion Public Constructors

        #region IUserProfileDao Members. Specific Operations

        /// <summary>
        /// Finds a UserProfile by his login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        public UserProfile FindByLoginName(string loginName)
        {
            UserProfile userProfile = null;

            DbSet<UserProfile> userProfiles = Context.Set<UserProfile>();

            var result =
                (from u in userProfiles
                 where u.loginName == loginName
                 select u);

            userProfile = result.FirstOrDefault();

            if (userProfile == null)
                throw new InstanceNotFoundException(loginName,
                    typeof(UserProfile).FullName);

            return userProfile;
        }

        /// <summary>Finds a UserProfile by his email</summary>
        /// <param name="email"></param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <exception cref="InstanceNotFoundException"></exception>
        public UserProfile FindByEmail(string email)
        {
            UserProfile userProfile = null;

            DbSet<UserProfile> userProfiles = Context.Set<UserProfile>();

            var result =
                (from u in userProfiles
                 where u.email == email
                 select u);

            userProfile = result.FirstOrDefault();

            if (userProfile == null)
                throw new InstanceNotFoundException(email,
                    typeof(UserProfile).FullName);

            return userProfile; ;
        }

        public List<UserProfile> FindAllUsers( int startIndex, int count)
        {

            var userProfiles = Context.Set<UserProfile>();

            var result =
                (from u in userProfiles
                 orderby u.loginName
                 select u).Skip(startIndex).Take(count).ToList();

            return result; ;
        }

        public int GetNumberOfUsers()
        {

            DbSet<UserProfile> userProfiles = Context.Set<UserProfile>();

            var result =
                (from u in userProfiles
                 select u).Count();

            return result; ;
        }



        public void Follow(UserProfile followedUser, UserProfile follower)
        {
            if (!IsFollow(followedUser, follower))
            {
                follower.UserProfile2.Add(followedUser);
            }

            Update(follower);
        }

        public void Unfollow(UserProfile followedUser, UserProfile follower)
        {
            if (IsFollow(followedUser, follower))
            {
                follower.UserProfile2.Remove(followedUser);
            }

            Update(follower);
        }

        public Boolean IsFollow(UserProfile followedUser, UserProfile follower) {
            return (follower.UserProfile2.Contains(followedUser));
        }

        public List<UserProfile> GetFollowed(UserProfile user)
        {
            return user.UserProfile2.ToList();
        }


        public List<UserProfile> GetFollowers(UserProfile user)
        {
            return user.UserProfile1.ToList();
        }

        #endregion IUserProfileDao Members
    }
}
