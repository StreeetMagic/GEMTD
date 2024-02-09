using UnityEngine;

namespace Infrastructure.Utilities
{
  public static class VectorExtensions
  {
    public static void SetX(this ref Vector3 vector, float x)
    {
      vector = new Vector3(x, vector.y, vector.z);
    }

    public static void SetY(this ref Vector3 vector, float y)
    {
      vector = new Vector3(vector.x, y, vector.z);
    }

    public static void SetZ(this ref Vector3 vector, float z)
    {
      vector = new Vector3(vector.x, vector.y, z);
    }
  }
}