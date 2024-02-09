using UnityEngine;

namespace Gameplay.Fields.Enemies.Movers
{
  public class EnemyMoverModel
  {
    public EnemyMoverModel(Vector3 position, Vector2Int[] points, EnemyModel model, float speed)
    {
      Position = position;
      Points = points;
      Model = model;
      Speed = speed;
    }

    public EnemyModel Model { get; }
    public Vector3 Position { get; set; }
    public float Speed { get; set; }
    public Vector2Int[] Points { get; }

    public void Move(Vector3 position, Vector3 damagePosition)
    {
      Position = position;
      Model.DamagePosition = damagePosition;
    }
  }
}