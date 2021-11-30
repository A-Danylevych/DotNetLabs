namespace BLL.Abstracts.IMapper
{
    public interface IBackMapper<out TEntity, in TModel>
    {
        public TEntity MapBack(TModel model);
    }
}