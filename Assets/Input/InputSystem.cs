using Unity.Entities;
using UnityEngine;

public partial class InputSystem : SystemBase
{
    private PlayerMotion _controls;

    protected override void OnCreate()
    {
        if (!SystemAPI.TryGetSingleton(out InputComponent input))
        {
            EntityManager.CreateEntity(typeof(InputComponent));
        }

        _controls = new PlayerMotion();
        _controls.Enable();
    }

    protected override void OnUpdate()
    {
        Vector2 moveVector = _controls.Player.Move.ReadValue<Vector2>();
        Vector2 mousePosition = _controls.Player.MousePosition.ReadValue<Vector2>();
        bool shoot = _controls.Player.Shoot.IsPressed();

        SystemAPI.SetSingleton(new InputComponent
        {
            MousePosition = mousePosition,
            Move = moveVector,
            Shoot = shoot
        });
    }
}
