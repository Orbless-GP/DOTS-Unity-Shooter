using Unity.Entities;
using Unity.Mathematics;

public struct InputComponent : IComponentData
{
    public float2 Move;
    public float2 MousePosition;
    public bool Shoot;
}
