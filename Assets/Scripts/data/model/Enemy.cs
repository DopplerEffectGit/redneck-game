using System;
public class Enemy
{
    private int complexity_WEAK = 0;
    private int complexity_NORMAL = 1;
    private int complexity_HARD = 2;
    private int complexity_BOSS = 3;

    private int behevour_runaway = -1;
    private int behevour_neutral = 0;
    private int behevour_dodge = 1;
    private int behevour_atack = 2;
    private int behevour_boss = 3;


    public string name;
    public int health;
    public int demage;
    public int stage;
    public int behevour;

    public Enemy(string name, int health, int demage, int stage, int behevour)
    {
        this.name = name;
        this.health = health;
        this.demage = demage;
        this.stage = stage;
        this.behevour = behevour;
    }
}
