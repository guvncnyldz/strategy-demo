public class MoveCommand : ICommand
{
    public readonly IGridNode Target;

    public MoveCommand(IGridNode target)
    {
        Target = target;
    }
}