using System;

public static class SystemEventManager
{
   private static event Action<ActionType, object> OnGameAction;

   public static void RaiseEvent(ActionType type, object payload)
   {
      OnGameAction?.Invoke(type, payload);
   }

   public static void Subscribe(Action<ActionType, object> action)
   {
      OnGameAction += action;
   }
   
   public static void Unsubscribe(Action<ActionType, object> action)
   {
      OnGameAction -= action;
   }

   public enum ActionType
   {
      AsteroidDeath,
      AsteroidSpawn,
      ShipUpgraded,
      ShotFired
   }
}
