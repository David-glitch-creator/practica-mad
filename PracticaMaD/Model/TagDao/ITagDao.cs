using Es.Udc.DotNet.ModelUtil.Dao;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.TagDao
{
    public interface ITagDao : IGenericDao<Tag, Int64>
    {
        Tag FindByName(string tagName);

        List<Tag> FindAllOrderByPopularity();

        void TagImage(Tag tag, ImageEntity image);

        void UntagImage(Tag tag, ImageEntity image);
    }
}