using UnityEngine;
using UnityEngine.Pool;

public class SpawnerCubes : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField, Min(0.1f)] private float _delay = 1f;
    [SerializeField, Min(0.1f)] private float _radiusSpawn;
    [SerializeField, Min(1)] private int _capacity;
    [SerializeField, Min(1)] private int _maxSize;

    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
           createFunc: () => CreateFunc(),
           actionOnGet: (cube) => ActionOnGet(cube),
           actionOnRelease: (cube) => cube.gameObject.SetActive(false),
           actionOnDestroy: (cube) => Destroy(cube.gameObject),
           collectionCheck: true,
           defaultCapacity: _capacity,
           maxSize: _maxSize
           );
    }

    private void Start()
    {
        InvokeRepeating(nameof(SpawnCube), 0, _delay);
    }

    public void Realease(Cube cube)
    {
        _pool.Release(cube);
    }

    private void SpawnCube()
    {
        _pool.Get();
    }

    private Cube CreateFunc()
    {
        var cube = Instantiate(_cube);
        cube.Init(this);

        return cube;
    }

    private void ActionOnGet(Cube cube)
    {
        cube.transform.position = GetStartPoint();
        cube.gameObject.SetActive(true);
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
