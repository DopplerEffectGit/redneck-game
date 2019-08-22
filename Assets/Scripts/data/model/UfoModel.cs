using System;
using UnityEngine;

public class UfoModel
{

    public static int ANIMATION_FLY = 1;
    public static int ANIMATION_COW = 2;
    public static int ANIMATION_SNEAK = 3;


    GameObject ufo;
    GameObject light;
    GameObject cow;

    int animationPeriod;


    private int verticalSpeed = 20;
    private float direction = 0.2f;

    public UfoModel(GameObject ufo)
    {
        this.ufo = ufo;
        this.light = ufo.transform.GetChild(1).gameObject;
        this.cow = light.transform.GetChild(0).gameObject;
        light.SetActive(false);
        cow.SetActive(false);
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
        Debug.Log("steal cow skript!=====");

        light.SetActive(true);

        ufo.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        ufo.GetComponent<Rigidbody2D>().angularVelocity = 0;

        var ufoPosition = ufo.transform.position;
        ufoPosition.y = ufoPosition.y + 2;
        ufoPosition.x = ufoPosition.x - verticalSpeed / 2;
        ufo.transform.position = Vector3.Lerp(ufo.transform.position, ufoPosition, verticalSpeed * Time.deltaTime);




        cow.SetActive(true);

        //var velocity = cow.GetComponent<Rigidbody2D>().velocity;
        //var angularVelocity = cow.GetComponent<Rigidbody2D>().angularVelocity;

        //cow.GetComponent<Rigidbody2D>().velocity = velocity;
        //cow.GetComponent<Rigidbody2D>().angularVelocity = angularVelocity;

        var cowPosition = cow.transform.position;
        cowPosition.y = cowPosition.y + 1;
        cow.transform.position = Vector3.Lerp(ufo.transform.position, cowPosition, 1 * Time.deltaTime);

    }

    private void sneakSkript() {
        Debug.Log("sneak skript: ");

        //cow.resetparent

        menuAnimationScript();
        light.SetActive(false);

    }
}
