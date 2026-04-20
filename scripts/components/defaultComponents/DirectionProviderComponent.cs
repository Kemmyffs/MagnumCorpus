using Godot;

public abstract partial class DirectionProvidingComponent : Component
{
    public abstract Vector2 ProvideDirection();
}