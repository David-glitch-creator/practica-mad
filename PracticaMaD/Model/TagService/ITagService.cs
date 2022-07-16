using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.TagService
{
    public interface ITagService
    {
        ITagDao TagDao { set; }

        List<Tag> GetByPopularity();

        /// <exception cref="InstanceNotFoundException"/>
        void AddTagToImage(string tagName, long imageId);

        /// <exception cref="InstanceNotFoundException"/>
        void RemoveTagFromImage(string tagName, long imageId);

        /// <exception cref="InstanceNotFoundException"/>
        List<TagDto> GetTagsFromImage(long imageId);
    }
}