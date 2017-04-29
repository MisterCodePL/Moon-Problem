using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public float MovementSpeed;
    protected Transform Transform;
    protected Collider2D Collider2D;
    protected bool FacingRight;


    public virtual void Start()
    {
        Transform = gameObject.GetComponent<Transform>();
        Collider2D = gameObject.GetComponent<Collider2D>();
        FacingRight = GetFacingRight();
    }

    public virtual void FixedUpdate()
    {
        Move();
    }

    protected void Flip()
    {
        FacingRight = !FacingRight;
        var scale = Transform.localScale;
        scale.x *= -1;
        Transform.localScale = scale;
    }

    private void Move()
    {
        var position = Transform.position;
        if (IsMoving() && FacingRight)
        {
            position.x += MovementSpeed;
        }
        if (IsMoving() && !FacingRight)
        {
            position.x -= MovementSpeed;
        }
        Transform.position = position;
    }

    private bool GetFacingRight()
    {
        return Transform.localScale.x > 0;
    }

    protected abstract bool IsMoving();

}
