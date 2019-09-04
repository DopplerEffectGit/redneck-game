using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hero : MonoBehaviour
{

    public float speed = 5f;
    public float jumpSpeed = 8f;
    private float movement = 0f;
    private Rigidbody2D rigidBody;
    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;
    private Animator playerAnimation;

    public BoxCollider2D groundCollider;
    // Use this for initialization
    void Start()
    {
        groundCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();

        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        playerAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("hero update");

//        isTouchingGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
        movement = Input.GetAxis("Horizontal");
        if (movement > 0f)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);
        }
        else if (movement < 0f)
        {

            transform.localRotation = Quaternion.Euler(0, 180, 0);
            rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);
        }
        else
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("hero jump");

            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
        }
        playerAnimation.SetFloat("speed", Mathf.Abs(rigidBody.velocity.x));
        //playerAnimation.SetBool("onGround", isTouchingGround);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D: " + other.name);

        
    }


}