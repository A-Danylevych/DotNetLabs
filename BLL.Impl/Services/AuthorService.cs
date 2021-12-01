using System.Collections.Generic;
using System.Linq;
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
        private readonly IMapper<Author, AuthorModel> _mapper;
        private readonly AbstractUnitOfWork _unit;

        public AuthorService(IBackMapper<Author, AuthorModel> backMapper, AbstractUnitOfWork unit, IMapper<Author, 
            AuthorModel> mapper)
        {
            _backMapper = backMapper;
            _unit = unit;
            _mapper = mapper;
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

        public async Task<ICollection<AuthorModel>> GetAll()
        {
            var list = await _unit.Authors.GetAll();
            return (from author in list select _mapper.Map(author)).ToList();
        }

        public async Task Delete(int id)
        {
            var entity = await _unit.Authors.GetById(id);
            if (entity == null)
            {
                throw new NotFoundException(typeof(Author));
            }
            _unit.Authors.Delete(entity);
            await _unit.Save();
        }

        public async Task<AuthorModel> GetById(int id)
        {
            return _mapper.Map(await _unit.Authors.GetById(id));
        }
    }
}