using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpSpeed = 10f;
    [SerializeField] private enum MovementState { idle, jumping, running, falling };
    private float dirX = 0f;
    // MovementState state = MovementState.idle;
    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log("Initialized...");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Handling the "Horizontal" movement.
        // dirX = Input.GetAxisRaw("Horizontal");
        // rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        // Auto "Horizontal" movement.
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        // anim.SetBool("running", true);

        // Handling the Jump scenario.
        if(Input.GetButtonDown("Jump") && HandleGrounded()) {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }

        // if(Math.Round(rb.position.x, 0)%30 == 0) {
        //     Debug.Log("Move Speed: "+ moveSpeed);
        //     moveSpeed = moveSpeed+0.05f;
        // }

        // AnimationHandler();
        // HandleIntegerAnimations();
        HandleBooleanAnimations();
    }

    // private void AnimationHandler() {
    //     if(dirX == 0f) {
    //         // Idle
    //         anim.SetBool("running", false);
    //     } else {
    //         // Running towards the left or right side
    //         anim.SetBool("running", true);
    //         sprite.flipX = dirX < 0f; // To flip the character when running to the left or the back side.
    //     }
    // }

    private void HandleIntegerAnimations() {

        MovementState state;
        if(dirX == 0f) {
            // Idle
            state = MovementState.idle;
        } else {
            // Running towards the left or right side
            state = MovementState.running;
            sprite.flipX = dirX < 0f; // To flip the character when running to the left or the back side.
        }

        if(rb.velocity.y > .1f) {
            // Handling animations for jumping
            state = MovementState.jumping;
        } else if (rb.velocity.y < -.1f) {
            // Handling animations for falling
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private void HandleBooleanAnimations() {
        MovementState state;

        Debug.Log("velocity");
        Debug.Log(rb.velocity.y);
        Debug.Log("*****************");
        if(rb.velocity.y > .1f) {
            state = MovementState.jumping;
        } else {
            state = MovementState.running;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool HandleGrounded() {
        /** 
            1. gets the center of the object
            2. gets the size of the object
            3. the angle
            4. direction
            5. 
            6. Layer or field
        */
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
