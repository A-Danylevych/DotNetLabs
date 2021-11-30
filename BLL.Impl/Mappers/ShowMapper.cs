using System.Collections.Generic;
using System.Linq;
using BLL.Abstracts.IMapper;
using DAL.Entities;
using Models.Base;

namespace BLL.Impl.Mappers
{
    public class ShowMapper : IMapper<Show, ShowModel>
    {
        public ShowModel Map(Show entity)
        {
            List<int> genresIds = entity.Genres.Select(genre => genre.Id).ToList();
            return new ShowModel
            {
                AuthorId = entity.AuthorId,
                Date = entity.Date,
                GenresIds = genresIds,
                Name = entity.Name,
            };
        }
    }
}