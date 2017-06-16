using System;
using System.Net;
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
	    try
	    {
	        FollowCamera();
        }
	    catch (MissingReferenceException)
	    {
	        ;
	    }
	}

    private void FollowCamera()
    {
        var canControl = PlayerTransform.gameObject.GetComponent<PlayerMovementScript>().CanControl;
        var position = _transform.position;
        position.x = PlayerTransform.position.x;

        if (!(PlayerTransform.position.y < -7.5f))
        {
            if (PlayerTransform.position.y - position.y > MaxVerticalOffset && canControl)
            {
                position.y += ChangingVerticalPositionPerFrame;
            }

            if (PlayerTransform.position.y - position.y < -MaxVerticalOffset && canControl)
            {
                position.y -= ChangingVerticalPositionPerFrame;
            }

            _transform.position = position;
        }
    }
}
