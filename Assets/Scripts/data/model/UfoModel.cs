using System;
using System.Collections;
using UnityEngine;

public class UfoModel
{

    public static int ANIMATION_FLY = 1;
    public static int ANIMATION_COW = 2;
    public static int ANIMATION_SNEAK = 3;
    public static int ANIMATION_AWAY = 4;


    GameObject ufo;
    GameObject light;
    GameObject cow;

    int frames = 0;


    int animationPeriod;


    private int verticalSpeed = 20;
    private float direction = 0.2f;

    public UfoModel(GameObject ufo)
    {
        this.ufo = ufo;
        this.light = ufo.transform.GetChild(1).gameObject;
        this.cow = ufo.transform.GetChild(2).gameObject;
        light.SetActive(false);
        //cow.SetActive(false);
    }

    public void onUpdate(int action) {
        //menuAnimationScript();
        Debug.Log("update");

        switch (action)
        {
            case 0: disappear(); break;
            case 1: menuAnimationScript(); break;
            case 2: cowScrypt(); break;
            case 3: sneakSkript(); break;
            case 4: awayScript(); break;

        }
    }

    private void disappear() {

    }

    private void menuAnimationScript()
    {

        var ufoPosition = ufo.transform.position;
        ufoPosition.x = ufoPosition.x + 10;
        ufo.transform.position = Vector3.Lerp(ufo.transform.position, ufoPosition, verticalSpeed * Time.deltaTime);
    }

    private void cowScrypt()
    {
        //var velocity = cow.GetComponent<Rigidbody2D>().velocity;
        //var angularVelocity = cow.GetComponent<Rigidbody2D>().angularVelocity;

        Debug.Log("steal cow skript!=====");
        cow.SetActive(true);
        //cow.transform.parent = null;
        light.SetActive(true);
        
        ufo.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        ufo.GetComponent<Rigidbody2D>().angularVelocity = 0;

        var ufoPosition = ufo.transform.position;
        //ufoPosition.y = ufoPosition.y + 2;
        ufoPosition.x = ufoPosition.x - verticalSpeed / 2;
        ufo.transform.position = Vector3.Lerp(ufo.transform.position, ufoPosition, verticalSpeed * Time.deltaTime);







        //cow.GetComponent<Rigidbody2D>().velocity = velocity;
        //cow.GetComponent<Rigidbody2D>().angularVelocity = angularVelocity;

        var cowPosition = cow.transform.position;
        cowPosition.y = cowPosition.y + 5;
        cow.transform.position = Vector3.Lerp(ufo.transform.position, cowPosition, 60 * Time.deltaTime);

    }

    private void sneakSkript() {
        Debug.Log("sneak skript: !");



        ufo.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        ufo.GetComponent<Rigidbody2D>().angularVelocity = 0;

        

        var ufoPosition = ufo.transform.position;
        //ufoPosition.y = ufoPosition.y + 2;
        ufoPosition.x = ufoPosition.x + verticalSpeed / 2;
        ufo.transform.position = Vector3.Lerp(ufo.transform.position, ufoPosition, verticalSpeed * Time.deltaTime);





        var cowPosition = cow.transform.position;

        if (frames < 30)
        {
            light.SetActive(false);
            cowPosition.y = cowPosition.y - 15;

        }
        else if (frames < 40)
        {
            light.SetActive(true);
            //cowPosition.y = cowPosition.y + 2;


        }
        else if (frames < 65)
        {
            light.SetActive(false);

            cowPosition.y = cowPosition.y - 15;

        }
        else if (frames < 70)
        {
            //cowPosition.y = cowPosition.y - 1;
            light.SetActive(true);

        }
        else {
            
            light.SetActive(false);
            cowPosition.y = cowPosition.y - 15;

        }
        frames++;





        cow.transform.position = Vector3.Lerp(ufo.transform.position, cowPosition, 60 * Time.deltaTime);

        //menuAnimationScript();
        //light.SetActive(false);

    }

    private void awayScript() {
        Debug.Log("awayScript!");

        cow.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        cow.GetComponent<Rigidbody2D>().angularVelocity = 0;

        var ufoPosition = ufo.transform.position;
        ufoPosition.x = ufoPosition.x - 30;
        ufoPosition.y = ufoPosition.y + 5;

        ufo.transform.position = Vector3.Lerp(ufo.transform.position, ufoPosition, verticalSpeed * Time.deltaTime);
    } 
}
