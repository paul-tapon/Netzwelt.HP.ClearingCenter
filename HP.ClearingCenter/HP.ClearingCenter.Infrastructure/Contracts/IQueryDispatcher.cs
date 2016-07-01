namespace HP.ClearingCenter.Infrastructure.Contracts
{
    public interface IQueryDispatcher
    {
        TResult Dispatch<TResult>(IQuery<TResult> query);
    }
}
