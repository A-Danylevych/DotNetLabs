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
            return new ShowModel
            {
                Id = entity.Id,
                AuthorId = entity.AuthorId,
                Date = entity.Date,
                GenreId = entity.GenreId,
                Name = entity.Name,
            };
        }
    }
}