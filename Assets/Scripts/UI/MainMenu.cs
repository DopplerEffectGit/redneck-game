using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    private static int MAIN_MENU = 0;
    private static int LEVEL_SELECT = 1;
    private static int GAME = 2;
    private static int SHOP = 3;

    GameObject collaiderLayout;
    GameObject ufo;
    UfoModel ufoModel;
    int animationKey;


    private void Start()
    {
        collaiderLayout = GameObject.Find("collaiderLayout");
        if (collaiderLayout == null) {
            Debug.Log("its null");

        }

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
            //GameObject.Find("collaiderLayout").SetActive(false);
            collaiderLayout.GetComponent<BoxCollider2D>().isTrigger = false;

        }
        //if (collision.gameObject.name == "cow") animationKey = UfoModel.ANIMATION_SNEAK;//ufoModel.onUpdate(UfoModel.ANIMATION_COW);


    }

    public void openShop() {SceneManager.LoadScene(SHOP);}
    public void openLevelSelect() {SceneManager.LoadScene(LEVEL_SELECT);}   
}
