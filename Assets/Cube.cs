using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer))]
public class Cube : MonoBehaviour
{    
    [SerializeField] private int _minReleaseTime;
    [SerializeField] private int _maxReleaseTime;

    private MeshRenderer _meshRenderer;
    private Color _defaulColor;

    private SpawnerCubes _spawner;
    private Platform _collisionPlatform;    
   
    private void Awake()
    {        
        _meshRenderer = GetComponent<MeshRenderer>();
        _defaulColor = _meshRenderer.material.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Platform platform))
        {
            if (_collisionPlatform == null)
                DetectFirstCollisionPlatform(platform);
            else if (_collisionPlatform != platform)
                ChangeCollisionPlatform(platform);
        }
    }

    public void Init(SpawnerCubes spawner) => _spawner = spawner;

    private void DetectFirstCollisionPlatform(Platform platform)
    {
        Invoke(nameof(Release), Random.Range(_minReleaseTime, _maxReleaseTime));
        ChangeCollisionPlatform(platform);
    }

    private void ChangeCollisionPlatform(Platform platform)
    {
        _collisionPlatform = platform;
        _meshRenderer.material.color = new Color(Random.value, Random.value, Random.value);
    }

    private void Release()
    {
        _collisionPlatform = null;
        _meshRenderer.material.color = _defaulColor;

        if (_spawner)
            _spawner.Realease(this);
        else
            Destroy(gameObject);
    }
}