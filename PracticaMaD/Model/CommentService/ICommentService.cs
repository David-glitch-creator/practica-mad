using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService
{
    public interface ICommentService
    {
        ICommentDao CommentDao { set; }

        long CommentImage(long userId, long imageId, string commentText);

        long UpdateComment(long commentId, string newText);

        void DeleteComment(long commentId);

        List<Comment> GetCommentsByImage(long imageId);

        List<Comment> GetCommentsByAuthor(long userId);
    }
}