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

    public Button buttonLeft;
    public Button buttonRight;
    public Button buttonJump;

    // Use this for initialization
    void Start()
    {

        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        playerAnimation = GetComponent<Animator>();





        buttonLeft.onClick.AddListener(delegate () {

            Debug.Log("left button ");

        });



        //buttonLeft = (Instantiate(button) as Button).gameObject;

        //buttonLeft = GameObject.FindGameObjectWithTag("buttonLeft");
        //buttonLeft = gameObject.GetComponent<Text>().text;


        //buttonLeft = GameObject.FindGameObjectWithTag("buttonLeft") as Button;



    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("hero update");

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
        if (Input.GetButtonDown("Jump") && onGround)
        {
            Debug.Log("hero jump");

            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
        }
        playerAnimation.SetFloat("speed", Mathf.Abs(rigidBody.velocity.x));
        playerAnimation.SetBool("onGround", onGround);

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (
            col.transform.name.Contains("road") ||
            col.transform.name.Contains("box") ||
            col.transform.name.Contains("platform")) 
        {
            Debug.Log("OnCollisionEnter2D " + col.transform.name);

            onGround = true;

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
    }

    public void leftButtonClick() {
        Debug.Log("left button ");


    }

    public void rightButtonClick()
    {
        Debug.Log("right button ");


    }
    public void jumpButtonClick()
    {
        Debug.Log("jump button ");


    }
    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    Debug.Log("OnTriggerEnter2D: " + other.name);


    //}


    //void OnTriggerExit2D(Collider2D collider)
    //{
    //    //Debug.Log("OnTriggerExit2D Here!");
    //    print("No longer in contact with " + collider.transform.name);

    //}

}