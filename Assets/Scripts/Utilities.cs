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
      if (r != null && r.isVisible)
         return true;
         
      var topRight = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth, cam.pixelHeight));
      var bottomLeft = cam.ScreenToWorldPoint(new Vector3(0, 0));

      Bounds screenBounds = new Bounds();
      screenBounds.Encapsulate(topRight);
      screenBounds.Encapsulate(bottomLeft);

      return screenBounds.Contains(t.position);
   }
}
