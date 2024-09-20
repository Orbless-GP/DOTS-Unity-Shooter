using UnityEngine;
using Unity.Entities;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Burst;

public partial struct EnemySpawnerSystem : ISystem
{
    private EntityManager _entityManager;

    private Entity _enemySpawnerEntity;
    private EnemySpawnerComponent _enemySpawnerComponent;

    private Entity _playerEntity;

    private Unity.Mathematics.Random _random;

    public void OnCreate(ref SystemState state)
    {
        _random = Unity.Mathematics.Random.CreateFromIndex((uint)_enemySpawnerComponent.GetHashCode());
    }
    public void OnUpdate(ref SystemState state)
    {
        _entityManager = state.EntityManager;

        _enemySpawnerEntity = SystemAPI.GetSingletonEntity<EnemySpawnerComponent>();
        _enemySpawnerComponent = _entityManager.GetComponentData<EnemySpawnerComponent>(_enemySpawnerEntity);

        _playerEntity = SystemAPI.GetSingletonEntity<PlayerComponent>();

        SpawnEnemies(ref state);
    }

    [BurstCompile]
    private void SpawnEnemies(ref SystemState state)
    {
        //decrement timer
        _enemySpawnerComponent.CurrentTimeBeforeNextSpawn -= SystemAPI.Time.DeltaTime;
        if (_enemySpawnerComponent.CurrentTimeBeforeNextSpawn <= 0f)
        {
            for (int i = 0; i < _enemySpawnerComponent.NumOfEnemiesToSpawnPerSecond; i++)
            {
                EntityCommandBuffer ECB = new EntityCommandBuffer(Allocator.Temp);
                Entity enemyEntity = _entityManager.Instantiate(_enemySpawnerComponent.EnemyPrefabToSpawn);

                LocalTransform enemyTransform = _entityManager.GetComponentData<LocalTransform>(enemyEntity);
                LocalTransform playerTransform = _entityManager.GetComponentData<LocalTransform>(_playerEntity);


                //random spawn point
                float minDistanceSquared = _enemySpawnerComponent.MinimumDistanceFromPlayer * _enemySpawnerComponent.MinimumDistanceFromPlayer;
                float2 randomOffset = _random.NextFloat2Direction() * _random.NextFloat(_enemySpawnerComponent.MinimumDistanceFromPlayer, _enemySpawnerComponent.EnemySpawnRadius);
                float2 playerPosition = new float2(playerTransform.Position.x, playerTransform.Position.y);
                float2 spawnPosition = playerPosition + randomOffset;
                float distanceSquared = math.lengthsq(spawnPosition - playerPosition);

                if (distanceSquared < minDistanceSquared)
                {
                    spawnPosition = playerPosition + math.normalize(randomOffset) * math.sqrt(minDistanceSquared);
                }
                enemyTransform.Position = new float3(spawnPosition.x, spawnPosition.y, 0f);

                //spawn look direction
                float3 direction = math.normalize(playerTransform.Position - enemyTransform.Position);
                float angle = math.atan2(direction.y, direction.x);
                quaternion lookRot = quaternion.AxisAngle(new float3(0, 0, 1), angle);
                enemyTransform.Rotation = lookRot;

                ECB.SetComponent(enemyEntity, enemyTransform);

                ECB.AddComponent(enemyEntity, new EnemyComponent
                {
                    CurrentHealth = 100f,
                    EnemySpeed = 1.25f
                });

                ECB.Playback(_entityManager);
                ECB.Dispose();
            }
            //increment the number of enemies that sapwn in each wave
            int desiredEnemiesPerWave = _enemySpawnerComponent.NumOfEnemiesToSpawnPerSecond + _enemySpawnerComponent.NumOfEnemiesToSpawnIncrementAmount;
            int enemiesPerWave = math.min(desiredEnemiesPerWave, _enemySpawnerComponent.MaxNumberOfEnemiesToSpawnPerSecond);
            _enemySpawnerComponent.NumOfEnemiesToSpawnPerSecond = enemiesPerWave;


            _enemySpawnerComponent.CurrentTimeBeforeNextSpawn = _enemySpawnerComponent.TimeBeforeNextSpawn;
        }

        _entityManager.SetComponentData(_enemySpawnerEntity, _enemySpawnerComponent);
    }
}