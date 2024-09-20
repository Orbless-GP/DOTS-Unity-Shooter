using Unity.Entities;
using UnityEngine;

public class PlayerAuthoring : MonoBehaviour
{
    public float MoveSpeed = 5f;
    public GameObject BulletPrefab;
    public int NumOfBulletsToSpawn = 50;
    [Range(0f, 10f)] public float BulletSpread = 5f;

    public class PlayerBaker : Baker<PlayerAuthoring>
    {

        public override void Bake(PlayerAuthoring authoring)
        {
            Entity playerEntity = GetEntity(TransformUsageFlags.None);

            AddComponent(playerEntity, new PlayerComponent
            {
                MoveSpeed = authoring.MoveSpeed,
                BulletPrefab = GetEntity(authoring.BulletPrefab, TransformUsageFlags.None),
                NumOfBulletsToSpawn = authoring.NumOfBulletsToSpawn,
                BulletSpread = authoring.BulletSpread
            });
        }
    }
}
