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
        List<ImageEntity> FindByAuthor(long userId, int startIndex, int count);

        List<ImageEntity> FindByCategory(long categoryId, int startIndex, int count);

        List<ImageEntity> FindAll(int startIndex, int count);

        List<ImageEntity> FindByKeywords(string keywords, int startIndex, int count);

        List<ImageEntity> FindByCategoryKeywords(long categoryId, string keywords, int startIndex, int count);

        long Like(UserProfile user, ImageEntity image);

        long Dislike(UserProfile user, ImageEntity image);

        int GetNumberOfLikes(ImageEntity image);

    }
}
