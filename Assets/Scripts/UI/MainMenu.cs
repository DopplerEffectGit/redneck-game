using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    private static int MAIN_MENU = 0;
    private static int LEVEL_SELECT = 1;
    private static int GAME = 2;
    private static int SHOP = 3;

    GameObject collaiderLayout;
    GameObject bottomCollaider;
    GameObject ufo;
    UfoModel ufoModel;
    int animationKey;


    private void Start()
    {
        collaiderLayout = GameObject.Find("collaiderLayout");
        bottomCollaider = GameObject.Find("bottomCollaider");


        ufoModel = new UfoModel(GameObject.Find("ufo"));
        animationKey = UfoModel.ANIMATION_FLY;
    }

    void Update() {
        ufoModel.onUpdate(animationKey);
        //Debug.Log("update: ");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log("collapse: " + collision.gameObject.name + "!=====");

        if (collision.gameObject.name == "ufo")
        {
            animationKey = UfoModel.ANIMATION_COW;//ufoModel.onUpdate(UfoModel.ANIMATION_COW);
            collaiderLayout.GetComponent<BoxCollider2D>().isTrigger = false;

        }


    }

    public void cowArrived() {
        Debug.Log("ARRIVED");
        animationKey = UfoModel.ANIMATION_SNEAK;
    }

    public void ufoOut() {
        bottomCollaider.GetComponent<BoxCollider2D>().isTrigger = false;

        Debug.Log("ufoOut!");
       animationKey = UfoModel.ANIMATION_AWAY;

    }


    public void openShop() {SceneManager.LoadScene(SHOP);}
    public void openLevelSelect() {SceneManager.LoadScene(LEVEL_SELECT);}   
}
