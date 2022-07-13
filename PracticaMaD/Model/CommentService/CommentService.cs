using Es.Udc.DotNet.PracticaMaD.Model.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.UserProfileDao;
using Ninject;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService
{
    public class CommentService : ICommentService
    {
        [Inject]
        public ICommentDao CommentDao { private get; set; }
        [Inject]
        public IUserProfileDao UserProfileDao { private get; set; }

        public long CommentImage(long userId, long imageId, string commentText)
        {
            Comment comment = new Comment();

            comment.author = userId;
            comment.imageId = imageId;
            comment.commentText = commentText;
            comment.postedDate = DateTime.Now;

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

        public List<CommentDto> GetCommentsByImage(long imageId)
        {
            List<Comment> comments = CommentDao.FindByImage(imageId);

            List<CommentDto> commentDtos = new List<CommentDto>();

            foreach (Comment comment in comments)
            {
                String loginName = UserProfileDao.Find(comment.author).loginName;

                commentDtos.Add(new CommentDto(comment.commentId, comment.author, loginName,
                    comment.imageId, comment.commentText, comment.postedDate));
            }

            return commentDtos;
        }

        public List<CommentDto> GetCommentsByAuthor(long userId)
        {
            List<Comment> comments = CommentDao.FindByAuthor(userId);

            List<CommentDto> commentDtos = new List<CommentDto>();

            foreach (Comment comment in comments)
            {
                String loginName = UserProfileDao.Find(comment.author).loginName;

                commentDtos.Add(new CommentDto(comment.commentId, comment.author, loginName,
                    comment.imageId, comment.commentText, comment.postedDate));
            }

            return commentDtos;
        }

        public bool HasComments(long imageId)
        {
            List<Comment> result = CommentDao.FindByImage(imageId);
            return result.Count > 0;
        }
    }
}