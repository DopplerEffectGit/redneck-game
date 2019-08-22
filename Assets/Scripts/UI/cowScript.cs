using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cowScript : MonoBehaviour
{

    GameObject menu;
    // Start is called before the first frame update

    void Start()
    {
        menu = GameObject.Find("MainMenu");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("cow script collapse: " + collision.gameObject.name + "!=====");
       

        if (collision.gameObject.name.Equals("bottomCollaider"))
        {
            menu.SendMessage("ufoOut");

        }
        else {
            menu.SendMessage("cowArrived");
        }
    }
}
