using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.CommentService
{
    public class CommentDto
    {
        #region Properties Region

        public long CommentId { get; private set; }
        public long AuthorId { get; private set; }
        public String AuthorLogin { get; private set; }
        public long ImageId { get; private set; }
        public String CommentText { get; private set; }
        public System.DateTime PostedDate { get; private set; }

        #endregion

        public CommentDto(long commentId, long authorId, String authorLogin, long imageId, String commentText,
            System.DateTime postedDate)
        {
            this.CommentId = commentId;
            this.AuthorId = authorId;
            this.AuthorLogin = authorLogin;
            this.ImageId = imageId;
            this.CommentText = commentText;
            this.PostedDate = postedDate;
        }

        public override bool Equals(object obj)
        {
            CommentDto target = (CommentDto)obj;

            return this.CommentId == target.CommentId
                && this.AuthorId == target.AuthorId
                && this.AuthorLogin == target.AuthorLogin
                && this.ImageId == target.ImageId
                && this.CommentText == target.CommentText
                && this.PostedDate == target.PostedDate;
        }

        public override int GetHashCode()
        {
            return this.CommentText.GetHashCode();
        }

        public override String ToString()
        {
            String strCommentDetails;

            strCommentDetails =
                "[ commentId = " + CommentId + " ]";


            return strCommentDetails;
        }
    }
}
