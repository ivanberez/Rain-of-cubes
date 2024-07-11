using Assets.Scripts;
using Assets.Scripts.Spawner;
using System;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour, IStatisticSpawner where T : MonoBehaviour, ISpawnedObject<T> 
{
    [Header("Base params spawner")]
    [SerializeField] private T _prefab;
    [SerializeField] private bool _isChekCollection = true;
    [SerializeField, Min(1)] private int _capacity;
    [SerializeField, Min(1)] private int _maxSize;
    
    public event Action StatChanged;

    public int CountActive => Pool.CountActive;
    public int CountAll { get; protected set; }


    protected ObjectPool<T> Pool;    

    protected virtual void Awake()
    {
        Pool = new ObjectPool<T>(
           createFunc: () => CreateFunc(),
           actionOnGet: (obj) => ActionOnGet(obj),
           actionOnRelease: (obj) => ActionOnRelease(obj),
           actionOnDestroy: (obj) => ActionOnDestroy(obj),
           collectionCheck: _isChekCollection,
           defaultCapacity: _capacity,
           maxSize: _maxSize
           );
    }

    protected virtual void ActionOnRelease(T obj)
    {        
        obj.gameObject.SetActive(false);
        StatChanged?.Invoke();
    }

    protected virtual T CreateFunc()
    {
        T created = Instantiate(_prefab);
        created.ReadyOnReleasing += Pool.Release;
        return created;
    }    

    protected virtual void ActionOnGet(T obj)
    {
        obj.gameObject.SetActive(true);
        CountAll++; 
        StatChanged?.Invoke();
    }

    protected virtual void ActionOnDestroy(T obj) 
    {
        obj.ReadyOnReleasing -= Pool.Release;
        Destroy(obj.gameObject);
        StatChanged?.Invoke();
    }
}