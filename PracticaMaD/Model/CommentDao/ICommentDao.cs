using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao
{
    public interface ICommentDao : IGenericDao<Comment, Int64>
    {
        List<Comment> FindByImage(long imageId);

        List<Comment> FindByAuthor(long userId);
    }
}
