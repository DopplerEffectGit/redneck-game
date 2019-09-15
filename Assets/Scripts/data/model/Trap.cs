using System;
using UnityEngine;

public class Trap : MonoBehaviour
{

    private float startY;
    private float startX;

    private float width;



    void Start()
    {

        startY = transform.position.y;

    }

    void Update()
    {
        var pos = transform.position;
        //pos.x = startX;
        //pos.y = startY + 10;


       // pos.y = pos.y + 5;

        //transform.position = pos;


        //Vector3 axis = new Vector3(0, 0, 1);
       // transform.RotateAround(pos, axis, Time.deltaTime * 50);





    }

    void OnCollisionStay2D(Collision2D col)
    {
        // width = col.transform.localScale.z * col.collider.size.z;


        if (
            col.transform.name.Contains("road"))
        {
            Debug.Log("OnCollisionEnter2D " + col.transform.name);

        }

       

    }
}
