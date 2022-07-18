using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService
{
    public interface ICommentService
    {
        ICommentDao CommentDao { set; }

        long CommentImage(long userId, long imageId, string commentText);

        /// <exception cref="InstanceNotFoundException"/>
        long UpdateComment(long commentId, string newText);

        /// <exception cref="InstanceNotFoundException"/>
        void DeleteComment(long commentId);

        /// <exception cref="InstanceNotFoundException"/>
        CommentDto GetCommentById(long commentId);

        List<CommentDto> GetCommentsByImage(long imageId);

        List<CommentDto> GetCommentsByAuthor(long userId);

        bool HasComments(long imageId);
    }
}