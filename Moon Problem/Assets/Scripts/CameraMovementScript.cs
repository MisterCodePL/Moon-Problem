using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    public Transform PlayerTransform;
    public float MaxVerticalOffset = 3.0f;
    public float ChangingVerticalPositionPerFrame = 0.075f;
    private Transform _transform;
    void Start ()
    {
        _transform = GetComponent<Transform>();
    }
	

	void LateUpdate ()
	{
	    Vector3 position = _transform.position;
	    position.x = PlayerTransform.position.x;
	    if (PlayerTransform.position.y - position.y > MaxVerticalOffset)
	    {
            position.y += ChangingVerticalPositionPerFrame;
	    }

        if (PlayerTransform.position.y - position.y < -MaxVerticalOffset)
        {
            position.y -= ChangingVerticalPositionPerFrame;
        }

        _transform.position = position;
	}
}
