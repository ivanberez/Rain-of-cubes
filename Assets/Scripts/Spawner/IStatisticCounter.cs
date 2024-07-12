using System;

namespace Assets.Scripts.Spawner
{
    public interface IStatisticCounter
    {
        event Action StatChanged;

        int CountActive { get; }
        int CountAll { get; }
    }
}
