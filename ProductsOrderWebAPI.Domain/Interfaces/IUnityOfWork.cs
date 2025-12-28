namespace ProductsOrderWebAPI.Domain.Interfaces
{
    public interface IUnityOfWork
    {
        Task<int> CommitChangesAsync();
    }
}
