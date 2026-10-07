namespace Solutions.ArchitectureBlueprint.Application;

public interface ICommand;
public interface ICommand<out TResponse> : ICommand;