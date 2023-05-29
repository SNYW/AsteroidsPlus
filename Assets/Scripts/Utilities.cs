using UnityEngine;
using Random = UnityEngine.Random;

public static class Utilities
{
   public static RangeFloat ToRangeFloat(this Vector2 vector2)
   {
      return new RangeFloat(vector2.x, vector2.y);
   }

   public static void UpdateScreenWrap(this Transform t, Renderer r = null)
   {
      if(r != null && r.isVisible) return;
      
      var viewportPosition = Camera.main.WorldToViewportPoint(t.position);
      var wrapPos = t.position;

      if (viewportPosition.x is > 1 or < 0)
         wrapPos.x = -wrapPos.x;
      if (viewportPosition.y is > 1 or < 0)
         wrapPos.y = -wrapPos.y;

      t.position = wrapPos;
   }
   
   public static bool IsVisibleToCamera(this Transform t, Camera cam, Renderer r)
   {
      var viewportPosition = cam.WorldToViewportPoint(t.position);

      if (r != null && r.isVisible)
         return true;

      return r.isVisible || viewportPosition.x is <= 1 and >= 0 || viewportPosition.y is <= 1 or >= 0; 
   }
}
