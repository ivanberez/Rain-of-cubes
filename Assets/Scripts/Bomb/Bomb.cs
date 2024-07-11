using Assets.Scripts;
using Assets.Scripts.Bomb;
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(MeshRenderer), typeof(TimerBomb))]
public class Bomb : MonoBehaviour, ISpawnedObject<Bomb>
{
    [SerializeField] private float _radiusExplosion;
    [SerializeField] private float _forceExplosion;

    private TimerBomb _timer;
    private VisualizingBomb _visualizing;    

    public event Action<Bomb> ReadyOnReleasing;

    private void Awake()
    {
        _timer = GetComponent<TimerBomb>();
        _visualizing = new VisualizingBomb(GetComponent<MeshRenderer>());
    }

    private void OnEnable()
    {
        _timer.Run(_visualizing.ChangeAlpha, Blow);
    }

    private void OnDisable()
    {
        _visualizing.Standardize();
    }

    private void Blow()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _radiusExplosion);

        foreach (Collider hit in hits)
            hit.attachedRigidbody?.AddExplosionForce(_forceExplosion, transform.position, _radiusExplosion, 1f, ForceMode.Impulse);

        if (ReadyOnReleasing == null)        
            Destroy(gameObject);
        else
            ReadyOnReleasing.Invoke(this);
    }
}