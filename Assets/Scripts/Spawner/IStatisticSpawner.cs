using System;

namespace Assets.Scripts.Spawner
{
    public interface IStatisticSpawner
    {
        event Action StatChanged;

        int CountActive { get; }
        int CountAll { get; }
    }
}
