public class AttackCommand : ICommand
{
    public readonly IHittable Hittable;

    public AttackCommand(IHittable hittable)
    {
        Hittable = hittable;
    }
}
