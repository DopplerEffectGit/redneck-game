using System;
using UnityEngine;

public class Trap : MonoBehaviour
{

    public Rigidbody2D body2d;
    public float leftpush=-0.3f;
    public float rightpush=0.3f;
    public float velocity=120;



    void Start()
    {

        body2d = GetComponent<Rigidbody2D>();
        body2d.angularVelocity = velocity;

    }

    void Update()
    {
        push();
    }

    public void push()
    {
        if (transform.rotation.z > 0 && transform.rotation.z < rightpush && (body2d.angularVelocity > 0) && body2d.angularVelocity < velocity)
        {
            body2d.angularVelocity = velocity;
        }
        else if (transform.rotation.z < 0 && transform.rotation.z > leftpush && (body2d.angularVelocity < 0) && body2d.angularVelocity > velocity * -1)
        {
            body2d.angularVelocity = velocity * -1;
        }
    }
}
