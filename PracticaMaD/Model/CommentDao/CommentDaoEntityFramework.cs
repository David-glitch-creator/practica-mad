using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Data;

using System.Linq;

using System.Data.Entity;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentDao
{
    public class CommentDaoEntityFramework : 
        GenericDaoEntityFramework<Comment, Int64>, ICommentDao
    {
        #region Public Constructors

        public CommentDaoEntityFramework()
        {
        }

        #endregion Public Constructors

        #region IUserProfileDao Members. Specific Operations

        public List<Comment> FindByImage(long imageId)
        {
            DbSet<Comment> comments = Context.Set<Comment>();

            var result =
                (from c in comments
                 where c.imageId == imageId
                 select c).OrderByDescending(c=>c.postedDate).ToList();

            return result;
        }

        public List<Comment> FindByAuthor(long userId)
        {
            DbSet<Comment> comments = Context.Set<Comment>();

            var result =
                (from c in comments
                 where c.author == userId
                 select c).OrderByDescending(c => c.postedDate).ToList();

            return result;
        }

        #endregion IUserProfileDao Members
    }
}
