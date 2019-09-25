using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDetectorCollider : MonoBehaviour
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

       // Debug.Log("hero collider on " + col.transform.name);
        if (col.transform.name.Contains("hero"))
        {
             Debug.Log("hero enter collider on " + col.transform.name);

            Vector3 heroPos = col.transform.position;

            GetComponentInParent<Enemy>().heroCoordinates = heroPos;
            GetComponentInParent<Enemy>().setBehavourAttack(); 

        }


    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.transform.name.Contains("hero"))
        {
            Vector3 heroPos = col.transform.position;
            GetComponentInParent<Enemy>().heroCoordinates = heroPos;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        Debug.Log("hero escape collider on " + col.transform.name);

        if (col.transform.name.Contains("hero"))
        {
            GetComponentInParent<Enemy>().setBehavourIdle();
            GetComponentInParent<Enemy>().heroCoordinates = Vector3.zero;
        }
    }
}
