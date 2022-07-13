using Es.Udc.DotNet.ModelUtil.Exceptions;
using Es.Udc.DotNet.PracticaMaD.Model.ImageEntityDao;
using Es.Udc.DotNet.PracticaMaD.Model.TagDao;
using Ninject;
using System;
using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.TagService
{
    public class TagService : ITagService
    {
        [Inject]
        public ITagDao TagDao { private get; set; }

        [Inject]
        public IImageEntityDao ImageEntityDao { private get; set; }

        public List<Tag> GetByPopularity()
        {
            return TagDao.FindAllOrderByPopularity();
        }

        public void AddTagToImage(string tagName, long imageId)
        {
            ImageEntity image = ImageEntityDao.Find(imageId);
            Tag tag = new Tag();
            try
            {
                tag = TagDao.FindByName(tagName);
                TagDao.TagImage(tag, image);
            }
            catch (InstanceNotFoundException)
            {
                tag.tagName = tagName;
                tag.taggedImagesNumber = 0;
                TagDao.Create(tag);

                TagDao.TagImage(tag, image);
            }
        }

        public void RemoveTagFromImage(string tagName, long imageId)
        {
            ImageEntity image = ImageEntityDao.Find(imageId);
            Tag tag = new Tag();
            try
            {
                tag = TagDao.FindByName(tagName);
                TagDao.UntagImage(tag, image);
            }
            catch (InstanceNotFoundException)
            {
                // No se puede quitar una etiqueta que no existe
            }
        }

        public List<TagDto> GetTagsFromImage(long imageId)
        {
            ImageEntity image = ImageEntityDao.Find(imageId);

            List<Tag> tags = TagDao.GetTagsOfImage(image);

            List<TagDto> tagDtos = new List<TagDto>();

            foreach (Tag tag in tags)
            {
                tagDtos.Add(new TagDto(tag.tagId, tag.tagName, tag.taggedImagesNumber));
            }

            return tagDtos;
        }
    }
}