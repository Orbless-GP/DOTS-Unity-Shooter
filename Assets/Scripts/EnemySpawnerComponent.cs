using Unity.Entities;

public struct EnemySpawnerComponent : IComponentData
{
    public Entity EnemyPrefabToSpawn;

    public int NumOfEnemiesToSpawnPerSecond;
    public int NumOfEnemiesToSpawnIncrementAmount;
    public int MaxNumberOfEnemiesToSpawnPerSecond;
    public float EnemySpawnRadius;
    public float MinimumDistanceFromPlayer;

    public float TimeBeforeNextSpawn;
    public float CurrentTimeBeforeNextSpawn;
}