using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyEdgeCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //COLLISION DETECTION=======================================================
    void OnTriggerEnter2D(Collider2D col)
    {

        Debug.Log("EDGE collider on " + col.transform.name);


        if (col.transform.name.Contains("platform"))
        {
            GetComponentInParent<Enemy>().enterPlatform();
            Debug.Log("collider on platform" );


        }


    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.transform.name.Contains("platform"))
        {
            GetComponentInParent<Enemy>().enterPlatform();
            Debug.Log("collider on platform");


        }
    }

    void OnTriggerExit2D(Collider2D col)
    {


        if (col.transform.name.Contains("platform"))
        {
            GetComponentInParent<Enemy>().escapePlatform();
            Debug.Log("collider escaped platform");

        }
    }
}
