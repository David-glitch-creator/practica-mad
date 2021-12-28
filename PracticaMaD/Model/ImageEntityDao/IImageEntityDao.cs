using Es.Udc.DotNet.ModelUtil.Dao;
using Es.Udc.DotNet.ModelUtil.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.ImageEntityDao
{
    public interface IImageEntityDao : IGenericDao<ImageEntity, Int64>
    {
        List<ImageEntity> FindByAuthor(long userId);

        List<ImageEntity> FindByCategory(long categoryId);

        List<ImageEntity> FindAll();

        List<ImageEntity> FindByKeywords(string keywords);

        List<ImageEntity> FindByCategoryKeywords(long categoryId, string keywords);

        long Like(UserProfile user, ImageEntity image);

        long Dislike(UserProfile user, ImageEntity image);

        int GetNumberOfLikes(ImageEntity image);

    }
}
