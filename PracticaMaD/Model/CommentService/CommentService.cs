using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService
{
    public class CommentService : ICommentService
    {
        public ICommentDao CommentDao { private get; set; }

        public long CommentImage(long userId, long imageId, string commentText)
        {
            Comment comment = new Comment();

            comment.author = userId;
            comment.imageId = imageId;
            comment.commentText = commentText;

            CommentDao.Create(comment);

            return comment.commentId;
        }

        public long UpdateComment(long commentId, string newText)
        {
            Comment comment = CommentDao.Find(commentId);

            comment.commentText = newText;

            CommentDao.Update(comment);

            return comment.commentId;
        }

        public void DeleteComment(long commentId)
        {
            CommentDao.Remove(commentId);
        }

        public List<Comment> GetCommentsByImage(long imageId)
        {
            return CommentDao.FindByImage(imageId);
        }

        public List<Comment> GetCommentsByAuthor(long userId)
        {
            return CommentDao.FindByAuthor(userId);
        }
    }
}