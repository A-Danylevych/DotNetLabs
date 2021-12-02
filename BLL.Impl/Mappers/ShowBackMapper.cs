using BLL.Abstracts.IMapper;
using DAL.Entities;
using Models.Base;

namespace BLL.Impl.Mappers
{
    public class ShowBackMapper: IBackMapper<Show, ShowModel>
    {
        public Show MapBack(ShowModel model)
        {
            return new Show
            {
                Id = model.Id,
                AuthorId = model.AuthorId,
                GenreId = model.GenreId,
                Date = model.Date,
                Name = model.Name,
            };
        }
    }
}