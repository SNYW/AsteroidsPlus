using UnityEngine;
using Random = UnityEngine.Random;

public static class Utilities
{
   public static RangeFloat ToRangeFloat(this Vector2 vector2)
   {
      return new RangeFloat(vector2.x, vector2.y);
   }
}
