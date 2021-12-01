using BLL.Abstracts.IMapper;
using DAL.Entities;
using Models.Base;

namespace BLL.Impl.Mappers
{
    public class GenreBackMapper: IBackMapper<Genre, GenreModel>
    {
        public Genre MapBack(GenreModel model)
        {
            return new Genre
            {
                Id = model.Id,
                Name = model.Name,
            };
        }
    }
}