using UnityEngine;
using System.Collections;
public class Enemy : MonoBehaviour
{
    private int complexity_WEAK = 0;
    private int complexity_NORMAL = 1;
    private int complexity_HARD = 2;
    private int complexity_BOSS = 3;


    private int behevour_idle = 0;
    private int behevour_neutral = 1;
    private int behevour_atack = 2;


    public string name;
    public int health;
    public int demage;
    public int behavour;


    public float speed = 5f;
    public float jumpSpeed = 8f;
    private float movement = 0f;

    private Rigidbody2D rigidBody;
    private Animator enemyAnimation;

    private float startX;
    private float startY;

    private bool flagActionDone = false;

    private static float IDLE_TIME = 3;

    private float idleTime = IDLE_TIME;

    // Use this for initialization
    void Start()
    {

        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        enemyAnimation = GetComponent<Animator>();

        startX = transform.position.x;
        startY = transform.position.y;


        behavour = 1;

    }


    //Update is called once per frame
    void Update()
    {

        //behavours
        switch (behavour) {
            case 0: idle(); break;
            case 1: behavourNeutral(); break;

        }



        //movement
        if (movement > 0f)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);
        }
        else if (movement < 0f)
        {

            transform.localRotation = Quaternion.Euler(0, 0, 0);
            rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);
        }
        else
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }

        //if (Input.GetButtonDown("Jump") && onGround)
        //{
        //    Debug.Log("hero jump");

        //    rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
        //}

        enemyAnimation.SetFloat("speed", Mathf.Abs(rigidBody.velocity.x));
       //enemyAnimation.SetBool("onGround", onGround);
    }

    private void behavourNeutral() {
        if (!flagActionDone)
        {
            if (transform.position.x < startX + 3)
            {
                movement = 1f;
            }
            else
            {
                movement = 0;
                idleTime -= Time.deltaTime;
                if (idleTime < 0)
                {
                    idleTime = IDLE_TIME;
                    flagActionDone = true;
                }
            }
        }
        else {
            if (transform.position.x > startX - 3)
            {
                movement = -1f;
            }
            else
            {
                movement = 0;
                idleTime -= Time.deltaTime;
                if (idleTime < 0)
                {
                    idleTime = IDLE_TIME;
                    flagActionDone = false;
                }

            }
        }
       
    }

    private void behavourAttack() {

    }




    private void idle() {
        movement = 0;

   
    }

}