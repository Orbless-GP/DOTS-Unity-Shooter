using UnityEngine;
using Unity.Entities;

public class EnemySpawnerAuthoring : MonoBehaviour
{
    public GameObject EnemyPrefabToSpawn;

public int NumOfEnemiesToSpawnPerSecond = 50;
public int NumOfEnemiesToSpawnIncrementAmount = 2;
public int MaxNumberOfEnemiesToSpawnPerSecond = 200;
public float EnemySpawnRadius = 40f;
public float MinimumDistanceFromPlayer = 5f;

public float TimeBeforeNextSpawn = 2f;

public class EnemySpawnerBaker : Baker<EnemySpawnerAuthoring>
    {
        public override void Bake(EnemySpawnerAuthoring authoring)
        {
            Entity enemySpawnerEntity = GetEntity(TransformUsageFlags.None);

            AddComponent(enemySpawnerEntity, new EnemySpawnerComponent
            {
                EnemyPrefabToSpawn = GetEntity(authoring.EnemyPrefabToSpawn, TransformUsageFlags.None),
                NumOfEnemiesToSpawnPerSecond = authoring.NumOfEnemiesToSpawnPerSecond,
                NumOfEnemiesToSpawnIncrementAmount = authoring.NumOfEnemiesToSpawnIncrementAmount,
                MaxNumberOfEnemiesToSpawnPerSecond = authoring.MaxNumberOfEnemiesToSpawnPerSecond,
                EnemySpawnRadius = authoring.EnemySpawnRadius,
                MinimumDistanceFromPlayer = authoring.MinimumDistanceFromPlayer,
                TimeBeforeNextSpawn = authoring.TimeBeforeNextSpawn
            });
        }
    }
}