using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Abstracts.IMapper;
using BLL.Abstracts.IService;
using DAL.Abstracts.IRepository;
using DAL.Entities;
using Models.Base;

namespace BLL.Impl.Services
{
    public class StatusService : IStatusService
    {
        private readonly IMapper<Status, StatusModel> _mapper;
        private readonly IStatusRepository _repository;

        public StatusService(IMapper<Status, StatusModel> mapper, IStatusRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<StatusModel> GetById(int id)
        {
            return _mapper.Map(await _repository.GetById(id));
        }

        public async Task<ICollection<StatusModel>> GetAll()
        {
            return (from status in await _repository.GetAll() select _mapper.Map(status)).ToList();
        }
    }
}