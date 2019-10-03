using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

    public float speed = 20f;
    public Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("bullet " + col.name);


        if (!col.transform.name.Contains("heroDetector"))
        {
            Destroy(gameObject);
        }

    }
}
