using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.otherCollider.gameObject != gameObject) return;

        SystemEventManager.RaiseEvent(SystemEventManager.ActionType.GameReset, null);
        transform.position = Vector2.zero;
    }
}
