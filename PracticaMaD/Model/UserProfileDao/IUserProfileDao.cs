﻿using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao
{
    public interface IUserProfileDao : IGenericDao<UserProfile, Int64>
    {
        /// <summary>
        /// Finds a UserProfile by login
        /// </summary>
        /// <param name="login">loginName</param>
        /// <returns>The UserProfile</returns>
        /// <exception cref="InstanceNotFoundException"/>
        UserProfile FindByLoginName(String loginName);

        /// <summary>
        /// Finds a UserProfile by email
        /// </summary>
        /// <param name="email">loginName</param>
        /// <returns>The UserProfile</returns>
        /// <exception cref="InstanceNotFoundException"/>
        UserProfile FindByEmail(String email);
    }
}