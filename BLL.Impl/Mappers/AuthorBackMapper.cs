using BLL.Abstracts.IMapper;
using DAL.Entities;
using Models.Base;

namespace BLL.Impl.Mappers
{
    public class AuthorBackMapper: IBackMapper<Author, AuthorModel>
    {
        public Author MapBack(AuthorModel model)
        {
            return new Author
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
            };
        }
    }
}