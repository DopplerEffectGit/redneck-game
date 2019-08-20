using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {
    private static int MAIN_MENU = 0;
    private static int LEVEL_SELECT = 1;
    private static int GAME = 2;
    private static int SHOP = 3;
    // Start is called before the first frame update
    void Start() {
      
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void openMainMenu() {
        SceneManager.LoadScene(MAIN_MENU);
    }


}
