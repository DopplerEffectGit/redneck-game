using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour {
    private static int MAIN_MENU = 0;
    private static int LEVEL_SELECT = 1;
    private static int GAME = 2;
    private static int SHOP = 3;
    // Start is called before the first frame update

    GameObject grid;
    

    void Start() {
        grid = GameObject.Find("Grid");

        for (int i = 0; i < 8; i++) {
            var level = Instantiate(Resources.Load("LevelLayoutWithText")) as GameObject;
            //var level = GameObject.Find("LevelLayoutWithText");

            //        level.transform.SetParent(grid.transform, false);


            //float width = this.gameObject.GetComponent<RectTransform>().rect.width;
            //Vector2 newSize = new Vector2(120, 120);
            //this.gameObject.GetComponent<GridLayoutGroup>().cellSize = newSize;

            level.transform.SetParent(grid.transform);
        }


        


    }

    // Update is called once per frame
    void Update() {
        
    }

    public void openMainMenu() {
        SceneManager.LoadScene(MAIN_MENU);
    }

    //public void addItemToGrid(Level level) {

    //    GameObject newButton = Instantiate(prefab);
    //    newButton.transform.setParent(MyPanel.transform, false);
    //}


}
