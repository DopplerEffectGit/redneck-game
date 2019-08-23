using UnityEngine;
using System.Collections;

public class Ufo : MonoBehaviour
{

    int ufoSpeed = 60;//20

    public static int BEHAVOUR_1 = 1; //from right to left
    public static int BEHAVOUR_2 = 2; //from left to right

    public int behavour = 0;

    public static int scenario1StartPositionX = 1000;
    public static int scenario1StartPositionY = 100;

    public static int scenario2StartPositionX = -30;
    public static int scenario2StartPositionY = 80;

    bool cowArrived = false;
    bool lightBlinkAnimationEnabled = false;
    bool readyDropCow = false;
    //bool readyToEscape = false;

    GameObject light;
    GameObject cow;

    int randomCowLocation;

    void Start()
    {
        randomCowLocation = Random.Range(Screen.width/4, Screen.width - Screen.width / 4);

        light = gameObject.transform.GetChild(1).gameObject;
         cow = gameObject.transform.GetChild(2).gameObject;

        scenario1StartPositionX = (int)(Screen.width);
        scenario1StartPositionY = (int)(Screen.height/2.5f);

        scenario2StartPositionX = (int)(-Screen.width/3);
        scenario2StartPositionY = (int)(Screen.height / 2.5f);

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3((float)Screen.width / 2f, (float)Screen.height / 2f, 10));

        //ufoObject = GameObject.Find("ufo");
        //gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        //gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
        print(gameObject.name);
    }

    private void setStartPosition(int positionX, int positionY)
    {
        var ufoPosition = gameObject.transform.position;
        ufoPosition.x = positionX;
        ufoPosition.y = positionY;
        gameObject.transform.position = ufoPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (behavour != 0)
        {
            switch (behavour) {
                case 1: rightScenario(); break;
                case 2: leftScenario(); break;
            }

        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log("ufo collapse: " + collision.gameObject.name);
        if (collision.gameObject.name.Equals("cow")) {
            cowArrived = true;
        }
    }

    float currCountdownValue;
    public IEnumerator StartCountdown(float countdownValue)
    {
        currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {

            Debug.Log("Countdown: " + currCountdownValue);
            if (currCountdownValue % 2 == 0)
            {
                light.SetActive(false);
            }
            else {
                light.SetActive(true);
            }

            yield return new WaitForSeconds(0.2f);
            currCountdownValue--;
            if (currCountdownValue == 0)
            {
                Debug.Log("ready to escape!");

                light.SetActive(false);
                readyDropCow = true;
            }
        }
    }

    public void SetBehavour( int value) {

        switch (value) {
            case 1: setStartPosition(scenario1StartPositionX, scenario1StartPositionY); break;
            case 2: setStartPosition(scenario2StartPositionX, scenario2StartPositionY); break;
            case 3: break;
        }

        gameObject.SetActive(true);
        behavour = value;
    }

    private void rightScenario() {
        var ufoPosition = gameObject.transform.position;
        if (ufoPosition.x > randomCowLocation) { //Screen.width/1.2f
            ufoPosition.x = ufoPosition.x - 10; //20
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, ufoPosition, ufoSpeed * Time.deltaTime);
        } else
        {
            stealCow();
        }
    }

    private void leftScenario()
    {
        var ufoPosition = gameObject.transform.position;
        if (ufoPosition.x < randomCowLocation)
        {

            ufoPosition.x = ufoPosition.x + 10;
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, ufoPosition, ufoSpeed * Time.deltaTime);
        }
        else {
            stealCow();
        }
    }

    
    private void stealCow() {
        if (!cowArrived)
        {
            light.SetActive(true);
            cow.SetActive(true);

            var cowPosition = cow.transform.position;
            cowPosition.y = cowPosition.y + 5;
            cow.transform.position = Vector3.Lerp(cow.transform.position, cowPosition, ufoSpeed * Time.deltaTime);
        }
        else {
            if (!lightBlinkAnimationEnabled) {
                lightBlinkAnimationEnabled = true;

                cow.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                cow.GetComponent<Rigidbody2D>().angularVelocity = 0;

                float timeLeft = Random.Range(4, 10);
                StartCoroutine(StartCountdown(timeLeft));
                
            }

            if (readyDropCow)
            {
                dropCow();
            }
        }
    }

    private void dropCow() {
        if (cow.transform.position.y > 0)
        {
            Debug.Log("cow falling");

            var cowPosition = cow.transform.position;
            cowPosition.y = cowPosition.y - 15;
            cow.transform.position = Vector3.Lerp(cow.transform.position, cowPosition, ufoSpeed * Time.deltaTime);
        }
        else
        {
            ufoEscape();
        }


    }

    private void ufoEscape()
    {
        int direction = 0;

        switch (behavour) {
            case 1: direction = -20; break;
            case 2: direction = 20; break;
        }
        
        Debug.Log("ufo escapes");

        var ufoPosition = gameObject.transform.position;
        ufoPosition.x = ufoPosition.x + direction;
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, ufoPosition, ufoSpeed * Time.deltaTime);

    }

}
