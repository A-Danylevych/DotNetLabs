using BLL.Abstracts.IMapper;
using DAL.Entities;
using Models.Base;

namespace BLL.Impl.Mappers
{
    public class AuthorMapper: IMapper<Author, AuthorModel>
    {
        public AuthorModel Map(Author entity)
        {
            return new AuthorModel
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
            };
        }
    }
}