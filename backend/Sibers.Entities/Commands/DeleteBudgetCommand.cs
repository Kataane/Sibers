namespace Sibers.Entities.Commands;

public record DeleteBudgetCommand(Guid Id) : ICommand<Result>;