using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Ninject;
using System;
using Es.Udc.DotNet.PracticaMaD.Model;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Util;
using Es.Udc.DotNet.PracticaMaD.Model.UserService.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.UserService;
using System.Collections.Generic;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserService
{
    public class UserService : IUserService
    {
        [Inject]
        public IUserProfileDao UserProfileDao { private get; set; }

        #region IUserService Members

        /// <exception cref="IncorrectPasswordException"/>
        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void ChangePassword(long userProfileId, string oldClearPassword,
            string newClearPassword)
        {
            UserProfile userProfile = UserProfileDao.Find(userProfileId);
            String storedPassword = userProfile.enPassword;

            if (!PasswordEncrypter.IsClearPasswordCorrect(oldClearPassword,
                 storedPassword))
            {
                throw new IncorrectPasswordException(userProfile.loginName);
            }

            userProfile.enPassword =
            PasswordEncrypter.Crypt(newClearPassword);

            UserProfileDao.Update(userProfile);
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public UserProfileDetails FindUserProfileDetails(long userProfileId)
        {
            UserProfile userProfile = UserProfileDao.Find(userProfileId);

            UserProfileDetails userProfileDetails =
                new UserProfileDetails(userProfile.firstName,
                    userProfile.lastName, userProfile.email,
                    userProfile.lang, userProfile.country);

            return userProfileDetails;
        }

        /// <exception cref="InstanceNotFoundException"/>
        /// <exception cref="IncorrectPasswordException"/>
        [Transactional]
        public LoginResult Login(string loginName, string password, bool passwordIsEncrypted)
        {
            UserProfile userProfile =
                UserProfileDao.FindByLoginName(loginName);

            String storedPassword = userProfile.enPassword;

            if (passwordIsEncrypted)
            {
                if (!password.Equals(storedPassword))
                {
                    throw new IncorrectPasswordException(loginName);
                }
            }
            else
            {
                if (!PasswordEncrypter.IsClearPasswordCorrect(password,
                        storedPassword))
                {
                    throw new IncorrectPasswordException(loginName);
                }
            }

            return new LoginResult(userProfile.userId, userProfile.firstName,
                storedPassword, userProfile.lang, userProfile.country);
        }

        /// <exception cref="DuplicateInstanceException"/>
        [Transactional]
        public long RegisterUser(string loginName, string clearPassword,
            UserProfileDetails userProfileDetails)
        {
            try
            {
                UserProfileDao.FindByLoginName(loginName);

                throw new DuplicateInstanceException(loginName,
                    typeof(UserProfile).FullName);
            }
            catch (InstanceNotFoundException)
            {
                String encryptedPassword = PasswordEncrypter.Crypt(clearPassword);

                UserProfile userProfile = new UserProfile();

                userProfile.loginName = loginName;
                userProfile.enPassword = encryptedPassword;
                userProfile.firstName = userProfileDetails.FirstName;
                userProfile.lastName = userProfileDetails.Lastname;
                userProfile.email = userProfileDetails.Email;
                userProfile.lang = userProfileDetails.Language;
                userProfile.country = userProfileDetails.Country;

                UserProfileDao.Create(userProfile);

                return userProfile.userId;
            }
        }

        /// <exception cref="InstanceNotFoundException"/>
        [Transactional]
        public void UpdateUserProfileDetails(long userProfileId,
            UserProfileDetails userProfileDetails)
        {
            UserProfile userProfile =
                UserProfileDao.Find(userProfileId);

            userProfile.firstName = userProfileDetails.FirstName;
            userProfile.lastName = userProfileDetails.Lastname;
            userProfile.email = userProfileDetails.Email;
            userProfile.lang = userProfileDetails.Language;
            userProfile.country = userProfileDetails.Country;
            UserProfileDao.Update(userProfile);
        }

        public bool UserExists(string loginName)
        {
            
            try
            {
                UserProfile userProfile = UserProfileDao.FindByLoginName(loginName);
            }
            catch (InstanceNotFoundException e)
            {
                return false;
            }

            return true;
        }

        public List<UserProfile> FindAllUsers(int startIndex, int count)
        {
            return UserProfileDao.FindAllUsers(startIndex, count);
        }

        public void FollowUser(long followedUserId, long followerId)
        {
            UserProfile followedUser = UserProfileDao.Find(followedUserId);
            UserProfile follower = UserProfileDao.Find(followerId);

            UserProfileDao.Follow(followedUser, follower);
        }

        public void UnfollowUser(long followedUserId, long followerId)
        {
            UserProfile followedUser = UserProfileDao.Find(followedUserId);
            UserProfile follower = UserProfileDao.Find(followerId);

            UserProfileDao.Unfollow(followedUser, follower);
        }
        public Boolean IsFollow(long followedUserId, long followerId)
        {
            UserProfile followedUser = UserProfileDao.Find(followedUserId);
            UserProfile follower = UserProfileDao.Find(followerId);

            return UserProfileDao.IsFollow(followedUser, follower);
        }

        public List<UserInfo> ViewFollowedUsers(long userId)
        {
            List<UserInfo> result = new List<UserInfo>();

            UserProfile userProfile = UserProfileDao.Find(userId);

            List<UserProfile> profiles = UserProfileDao.GetFollowed(userProfile);

            foreach (UserProfile profile in profiles)
            {
                result.Add(new UserInfo(profile.userId, profile.loginName, 
                    profile.firstName, profile.lastName,
                    profile.email, profile.lang, profile.country));
            }

            return result;
        }

        public List<UserInfo> GetFollowers(long userId)
        {
            List<UserInfo> result = new List<UserInfo>();

            UserProfile userProfile = UserProfileDao.Find(userId);

            List<UserProfile> profiles = UserProfileDao.GetFollowers(userProfile);

            foreach (UserProfile profile in profiles)
            {
                result.Add(new UserInfo(profile.userId, profile.loginName,
                    profile.firstName, profile.lastName,
                    profile.email, profile.lang, profile.country));
            }

            return result;
        }

        [Transactional]
        public UserInfo GetUserInfo(long userProfileId)
        {
            UserProfile userProfile = UserProfileDao.Find(userProfileId);

            UserInfo userInfo =
                new UserInfo(userProfile.userId, userProfile.loginName, userProfile.firstName,
                    userProfile.lastName, userProfile.email,
                    userProfile.lang, userProfile.country);

            return userInfo;
        }

        [Transactional]
        public UserInfo FindUserByLoginName(string loginName)
        {
            UserProfile userProfile = UserProfileDao.FindByLoginName(loginName);

            UserInfo userInfo =
                new UserInfo(userProfile.userId, userProfile.loginName, userProfile.firstName,
                    userProfile.lastName, userProfile.email,
                    userProfile.lang, userProfile.country);

            return userInfo;
        }

        #endregion IUserService Members
    }

}