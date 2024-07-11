using UnityEngine;

public class SpawnerCubes : Spawner<Cube>
{    
    [Space, Header("Additional params")]
    [SerializeField] private SpawnerBombs _spawnerBombs;
    [SerializeField, Min(0.1f)] private float _delay = 1f;
    [SerializeField, Min(0.1f)] private float _radiusSpawn;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnCube), 0, _delay);
    }

    private void SpawnCube()
    {
        Pool.Get();
    }

    protected override void ActionOnGet(Cube cube)
    {
        base.ActionOnGet(cube);
        cube.transform.position = GetStartPoint();
    }

    protected override void ActionOnRelease(Cube cube)
    {
        base.ActionOnRelease(cube);
        _spawnerBombs.Spawn(cube.transform.position);
    }

    private Vector3 GetStartPoint()
    {
        float x = transform.position.x + GetRandomCorrection();
        float y = transform.position.y;
        float z = transform.position.z + GetRandomCorrection();

        return new Vector3(x, y, z);
    }

    private float GetRandomCorrection()
    {
        return Random.Range(-_radiusSpawn, _radiusSpawn);
    }
}
