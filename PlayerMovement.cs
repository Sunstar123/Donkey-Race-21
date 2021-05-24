using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private int axis = 0;  // -1: horizontal, 0: none, 1: vertical
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveLogic();
    }

    void MoveLogic()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        if (change.x != 0 && change.y != 0)  // trying to move in both axis at once?
        {
            if (axis == 1)  // already moving in y axis?
            {
                change.x = 0;
            }
            else
            {
                change.y = 0; // default to x axis -> if both axis tried at the same time, always go in the x axis
            }
        }

        // update axis
        if (change.x != 0)
        {
            axis = -1;
        }
        else if (change.y != 0)
        {
            axis = 1;
        }
        else
        {
            axis = 0;
        }

        UpdateAnimation();
        if (change != Vector3.zero)
        {
            MoveCharacter();
        }
    }

    void UpdateAnimation()
    {
        if(change != Vector3.zero)
        {
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    void MoveCharacter()
    {
        myRigidbody.MovePosition(  // messes with gravity by smoothly moving toward a point (aka don't use gravity with this)
            transform.position + change * speed * Time.deltaTime);
    }
}
