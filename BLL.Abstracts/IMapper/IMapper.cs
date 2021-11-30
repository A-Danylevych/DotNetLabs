namespace BLL.Abstracts.IMapper
{
    public interface IMapper<in TEntity, out TModel>
    {
        public TModel Map(TEntity entity);

    }
}