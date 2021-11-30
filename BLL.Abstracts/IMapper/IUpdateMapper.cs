namespace BLL.Abstracts.IMapper
{
    public interface IUpdateMapper<TEntity, in TModel>
    {
        public TEntity MapUpdate(TModel model, TEntity entity);
    }
}