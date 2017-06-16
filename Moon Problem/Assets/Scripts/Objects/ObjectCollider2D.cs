using UnityEngine;

public class ObjectCollider2D : MonoBehaviour
{
    public bool IsCollide { get; private set; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IsCollide = collision.gameObject.CompareTag("Player");
    }
}
