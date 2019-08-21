using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    private static int MAIN_MENU = 0;
    private static int LEVEL_SELECT = 1;
    private static int GAME = 2;
    private static int SHOP = 3;

    public void openShop() {

        //SceneManager.GetSceneByName("Shop");
        SceneManager.LoadScene(SHOP);
    }

    public void openLevelSelect() {
        SceneManager.LoadScene(LEVEL_SELECT);
    }
}
