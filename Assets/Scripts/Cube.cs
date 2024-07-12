using Assets.Scripts;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer))]
public class Cube : MonoBehaviour, ISpawnedObject<Cube>
{
    [SerializeField] private int _minReleaseTime;
    [SerializeField] private int _maxReleaseTime;

    private MeshRenderer _meshRenderer;
    private Color _defaulColor;

    private bool _isNotCollisionPlatform = true;

    public event System.Action<Cube> ReadyOnReleasing;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _defaulColor = _meshRenderer.material.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isNotCollisionPlatform)
        {
            if (collision.transform.TryGetComponent(out Platform platform))
            {
                _isNotCollisionPlatform = true;
                _meshRenderer.material.color = new Color(Random.value, Random.value, Random.value);
                Invoke(nameof(Release), Random.Range(_minReleaseTime, _maxReleaseTime));
            }
        }               
    }        

    private void Release()
    {
        _isNotCollisionPlatform = true;
        _meshRenderer.material.color = _defaulColor;

        if (ReadyOnReleasing != null)
            ReadyOnReleasing.Invoke(this);
        else
            Destroy(gameObject);
    }
}