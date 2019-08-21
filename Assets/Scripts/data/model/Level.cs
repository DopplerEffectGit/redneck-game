using System;
using System.Collections.Generic;
[Serializable]

public class Level
{

    private int RATING_0 = 0;
    private int RATING_1 = 1;
    private int RATING_2 = 2;
    private int RATING_3 = 3;


    private StageState StageState;

    //private string name;
    public int number;
    public int stage;
    public int available;
    public int rating;
    public int complexity;
    public List<Enemy> enemyList;
    //public List<Box> boxesList;



    public Level(int number, int stage, int available,  int rating, int complexity, List<Enemy> enemyList)
    {
        this.number = number;
        this.stage = stage;
        this.available = available;
        this.rating= rating;
        this.complexity = complexity;
        this.enemyList = enemyList;
    }

}