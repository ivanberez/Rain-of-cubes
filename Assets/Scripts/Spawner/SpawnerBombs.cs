using UnityEngine;

public class SpawnerBombs : Spawner<Bomb>
{
    public void Spawn(Vector3 position)
    {
        var bomb = Pool.Get();
        bomb.transform.position = position;
    }
}
