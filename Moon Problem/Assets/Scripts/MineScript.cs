using UnityEngine;

public class MineScript : MonoBehaviour
{

    private Rigidbody2D _rigidbody2D;
    private bool _facingRight;
    private Vector2 _direction;
    public void Start()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        var playerPosition = FindPlayerPosition();
        _direction = CalculateVectorBetweenPlayerAndMine(playerPosition);
        ChangeFacing();
        ChangeSize();
        _rigidbody2D.AddForce(_direction.normalized * 750);
    }

    private void ChangeSize()
    {
        var size = _rigidbody2D.transform.localScale;
        if (IsDifferent(size)) Flip();
    }

    private Vector2 FindPlayerPosition()
    {
        return GameObject.Find("Player").transform.position;
    }

    private Vector2 CalculateVectorBetweenPlayerAndMine(Vector2 playerPosition)
    {
        var position = _rigidbody2D.position;
        
        Vector2 result = new Vector2(playerPosition.x-position.x,0);
        return result;
    }

    private void ChangeFacing()
    {
        _facingRight = _direction.x > 0;
    }

    private bool IsDifferent(Vector2 size)
    {
        return _facingRight == size.x > 0;
    }

    private void Flip()
    {
        var size = _rigidbody2D.transform.position;
        size.x *= -1;
        _rigidbody2D.transform.position = size;
    }
}
