using System.Threading.Tasks;
using BLL.Abstracts.IMapper;
using BLL.Abstracts.IService;
using DAL.Abstracts;
using DAL.Entities;
using Models.Base;
using Models.Error;

namespace BLL.Impl.Services
{
    public class AuthorService: IAuthorService
    {
        private readonly IBackMapper<Author, AuthorModel> _backMapper;
        private readonly AbstractUnitOfWork _unit;

        public AuthorService(IBackMapper<Author, AuthorModel> backMapper, AbstractUnitOfWork unit)
        {
            _backMapper = backMapper;
            _unit = unit;
        }

        public async Task Create(AuthorModel authorModel)
        {
            
            var entity = _backMapper.MapBack(authorModel);
            
            try
            {
                await _unit.Authors.Create(entity);
                await _unit.Save();
            }
            catch 
            {
                throw new CreationException(typeof(Author));
            }
           
        }

        public async Task<int> GetId(AuthorModel authorModel)
        {
            var entity = await _unit.Authors.FindAuthor(authorModel.FirstName, authorModel.LastName);
            if (entity == null)
            {
                throw new NotFoundException(typeof(Author));
            }

            return entity.Id;
        }
    }
}