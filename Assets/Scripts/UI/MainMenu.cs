using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    private static int MAIN_MENU = 0;
    private static int LEVEL_SELECT = 1;
    private static int GAME = 2;
    private static int SHOP = 3;

    GameObject collaiderLayout;
    GameObject bottomCollaider;

    public GameObject ufo;
    private GameObject prefabInstantiation;
    //UfoModel ufoModel;

    float respawnTime = -1;
    private void Start()
    {
        //var back = GameObject.Find("background");

        //prefabInstantiation = Instantiate(ufo, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        //prefabInstantiation.transform.parent = back.transform;


        //float timeLeft = Random.Range(1, 4);
       // StartCoroutine(StartCountdown(timeLeft));


        collaiderLayout = GameObject.Find("collaiderLayout");
        bottomCollaider = GameObject.Find("bottomCollaider");
    }

    void Update() {
        Debug.Log("Main update Countdown: " + respawnTime);

        respawnTime -= Time.deltaTime;
        if (respawnTime < 0)
        {
            respawnTime = 30;
            spawnUfo();
        }
    }

    private void spawnUfo() {

        var back = GameObject.Find("background");
        prefabInstantiation = Instantiate(ufo, new Vector3(10, 3, 0), Quaternion.identity) as GameObject;
        prefabInstantiation.transform.parent = back.transform;
        float timeLeft = Random.Range(5, 7);
        StartCoroutine(StartCountdown(timeLeft));
    }



    
    float currCountdownValue;
    public IEnumerator StartCountdown(float countdownValue)
    {
        currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            
            Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
            if (currCountdownValue == 0) {
                startUfoAction();
            }
        }
    }

    private void startUfoAction() {
        switch (randomNumber())
        {
            case 1: prefabInstantiation.gameObject.GetComponent<Ufo>().SetBehavour(Ufo.BEHAVOUR_1); break;
            case 2: prefabInstantiation.gameObject.GetComponent<Ufo>().SetBehavour(Ufo.BEHAVOUR_2); break;
            case 3: break;
        }
    }

    private int randomNumber() {
        int number = Random.Range(1, 3);
        Debug.Log("random number: " + number);
        return number;
        //return 2;

    }

    //void OnTriggerEnter2D(Collider2D collision)
    //{

    //    Debug.Log("collapse: " + collision.gameObject.name + "!=====");

    //    if (collision.gameObject.name == "ufo")
    //    {
    //        collaiderLayout.GetComponent<BoxCollider2D>().isTrigger = false;
    //    }
    //}

    //public void cowArrived() {
    //    Debug.Log("ARRIVED");
    //}

    //public void ufoOut() {

    //    Debug.Log("ufoOut!");

    //}


    public void openShop() {SceneManager.LoadScene(SHOP);}
    public void openLevelSelect() {
        //SceneManager.LoadScene(2);
        SceneManager.LoadScene("new_new_grigorill");

    }//LEVEL_SELECT

}
