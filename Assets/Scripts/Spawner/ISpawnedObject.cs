using System;
using UnityEngine;

namespace Assets.Scripts
{
    public interface ISpawnedObject<T> where T : MonoBehaviour
    {
        event Action<T> ReadyOnReleasing;
    }
}
