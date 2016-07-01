namespace HP.ClearingCenter.Infrastructure.Contracts
{
    public interface ICommandHandler<TParameter> where TParameter : class, ICommand
    {
        void Execute(ICommandContext<TParameter> context);
    }
}
