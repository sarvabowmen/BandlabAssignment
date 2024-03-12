using Banlab.Social.Api.Data;

namespace Banlab.Social.Api.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task UpsertAsync(T item);

        Task<T> GetById(string id);
    }
}
