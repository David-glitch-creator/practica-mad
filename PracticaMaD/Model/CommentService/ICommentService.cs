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

        CommentDto GetCommentById(long commentId);

        List<CommentDto> GetCommentsByImage(long imageId);

        List<CommentDto> GetCommentsByAuthor(long userId);

        bool HasComments(long imageId);
    }
}