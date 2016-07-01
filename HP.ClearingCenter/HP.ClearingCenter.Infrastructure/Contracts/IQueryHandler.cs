namespace HP.ClearingCenter.Infrastructure.Contracts
{
    public interface IQueryHandler<TParameter, TResult> where TParameter : IQuery<TResult>
    {
        TResult Retrieve(TParameter query);
    }
}
