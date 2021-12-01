using BLL.Abstracts.IMapper;
using DAL.Entities;
using Models.Base;

namespace BLL.Impl.Mappers
{
    public class GenreMapper : IMapper<Genre,GenreModel>
    {
        public GenreModel Map(Genre entity)
        {
            return new GenreModel
            {
                Id = entity.Id,
                Name = entity.Name,
            };
        }
    }
}