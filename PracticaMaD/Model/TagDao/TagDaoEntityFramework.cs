using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Es.Udc.DotNet.PracticaMaD.Model.TagDao
{
    public class TagDaoEntityFramework :
        GenericDaoEntityFramework<Tag, Int64>, ITagDao
    {
        /// <exception cref="InstanceNotFoundException"/>
        public Tag FindByName(string tagName)
        {
            Tag tag = null;

            DbSet<Tag> tags = Context.Set<Tag>();

            var result =
                (from t in tags
                 where t.tagName == tagName
                 select t);

            tag = result.FirstOrDefault();

            if (tag == null)
                throw new InstanceNotFoundException(tagName,
                    typeof(Category).FullName);

            return tag;
        }

        public List<Tag> FindAllOrderByPopularity()
        {
            DbSet<Tag> tags = Context.Set<Tag>();

            var result =
                (from t in tags
                 select t).OrderByDescending(t => t.taggedImagesNumber).ToList();

            return result;
        }

        public void TagImage(Tag tag, ImageEntity image)
        {
            if (!tag.ImageEntity.Contains(image))
            {
                tag.ImageEntity.Add(image);
                tag.taggedImagesNumber++;
            }

            Update(tag);
        }

        public void UntagImage(Tag tag, ImageEntity image)
        {
            if (image.Tag.Contains(tag))
            {
                image.Tag.Remove(tag);
                tag.taggedImagesNumber--;
            }

            Update(tag);
        }

        public List<Tag> GetTagsOfImage(ImageEntity image)
        {
            return image.Tag.ToList();
        }
    }
}