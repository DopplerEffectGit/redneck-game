using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hero : MonoBehaviour
{

    public float speed = 5f;
    public float jumpSpeed = 8f;
    private float movement = 0f;
    private Rigidbody2D rigidBody;
    private Animator playerAnimation;

    private bool onGround;
    private bool attackActive = false;
    public ParticleSystem dust;    


    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        playerAnimation = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("hero update");
        //movement = Input.GetAxis("Horizontal");// разкомментить чтобы управлять  клавиатурой

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
        if (Input.GetButtonDown("Jump") && onGround)
        {
            Debug.Log("hero jump");

            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
        }


        if (playerAnimation.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        {
            float animationPercent = (playerAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime % 1.0f);
            Debug.Log(animationPercent);
            if (animationPercent < 0.9f)
            {
                attackActive = true;
            }
            else {
                attackActive = false;
            }
        }
        else
        {
            attackActive = false;
        }





        playerAnimation.SetBool("shooting", attackActive);
        playerAnimation.SetFloat("speed", Mathf.Abs(rigidBody.velocity.x));
        playerAnimation.SetBool("onGround", onGround);
    }


    //COLLISION DETECTION=======================================================
    void OnCollisionStay2D(Collision2D col)
    {

        if (
            col.transform.name.Contains("road") ||
            col.transform.name.Contains("box") ||
            col.transform.name.Contains("platform")) 
        {
            Debug.Log("OnCollisionEnter2D " + col.transform.name);
            onGround = true;
        }

        if (col.transform.name.Contains("platform")) {
            transform.parent = col.transform;
        }
        if (col.transform.name.Contains("pika")|| col.transform.name.Contains("vundervaffel"))
        {
            rigidBody.constraints = RigidbodyConstraints2D.None;
        }

    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (
                    col.transform.name.Contains("road") ||
                    col.transform.name.Contains("box") ||
                    col.transform.name.Contains("platform"))
        {
            Debug.Log("OnCollisionExit2D " + col.transform.name);
            onGround = false; 
        }

        if (col.transform.name.Contains("platform"))
        {
            transform.parent = null;
        }
    }

    //BUTTONS=======================================================

    public void jumpButtonClick()
    {
        if (onGround) {
            CreateDust();
            Debug.Log("jump button ");
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
        }
    }

    public void attackButtonClick() {
        Debug.Log("attack button ");

        attackActive = true;
        playerAnimation.Play("attack");
    }

    public void leftButtonDown()
    {
        CreateDust();
        Debug.Log("left down ");
        movement = -1f;
    }

    public void leftButtonUp()
    {
        CreateDust();
        Debug.Log("left up ");
        movement = 0;
    }

    public void rightButtonDown()
    {
        CreateDust();
        Debug.Log("right down ");
        movement = 1f;
    }

    public void rightButtonUp()
    {
        CreateDust();
        Debug.Log("right up ");
        movement = 0;
    }



    //
    void CreateDust()
    {
        dust.Play();
    }
}
