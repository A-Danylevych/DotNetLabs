using BLL.Abstracts.IMapper;
using DAL.Entities;
using Models.Base;

namespace BLL.Impl.Mappers
{
    public class StatusMapper:IMapper<Status, StatusModel>
    {
        public StatusModel Map(Status entity)
        {
            return new StatusModel()
            {
                Id = entity.Id,
                Name = entity.Name,
            };
        }
    }
}