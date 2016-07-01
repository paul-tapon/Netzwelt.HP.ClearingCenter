namespace HP.ClearingCenter.Infrastructure.Contracts
{
    public interface ICommandDispatcher
    {
        CommandResult Dispatch<TParameter>(TParameter command) where TParameter : class, ICommand;
    }
}
