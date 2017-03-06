using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {
    [HideInInspector]
    public bool FacingRight = true;
    public float MovementSpeed = 0.25f;
    private Transform _transform;
    private Animator _animator;

    void Start()
    {
        _transform = gameObject.GetComponent<Transform>();
        _animator = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && FacingRight) Flip();
        if(Input.GetKey(KeyCode.RightArrow) && !FacingRight) Flip();
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) _animator.SetBool("startMoving", true);
        if(!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)) _animator.SetBool("startMoving", false);
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector3 position = _transform.position;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            position.x += MovementSpeed;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            position.x -= MovementSpeed;
        }
        _transform.position = position;
    }

    void Flip()
    {
        FacingRight = !FacingRight;
        Vector3 scale = _transform.localScale;
        scale.x *= -1;
        _transform.localScale = scale;
    }
}
