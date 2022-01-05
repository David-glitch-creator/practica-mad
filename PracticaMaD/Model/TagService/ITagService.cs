using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.TagService
{
    public interface ITagService
    {
        ITagDao TagDao { set; }

        List<Tag> GetByPopularity();

        void AddTagToImage(string tagName, long imageId);

        void RemoveTagFromImage(string tagName, long imageId);
    }
}