using UnityEngine;
using System.Collections.Generic;

public class GamePreferences : MonoBehaviour
{
    //public CharacterData characterData;

    public int levelNumber = 15;
    private List<Level> levels;

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.S))
    //        SaveCharacter(characterData, 0);

    //    if (Input.GetKeyDown(KeyCode.L))
    //        characterData = LoadCharacter(0);
    //}

    static void saveVolume(int volumeOn) {
        PlayerPrefs.SetInt("volumeOn", volumeOn);
        PlayerPrefs.Save();
    }
    static int getVolume(){return PlayerPrefs.GetInt("volumeOn");}

    static void saveMusicLevel(float musicLevel)
    {
        PlayerPrefs.SetFloat("musicLevel", musicLevel);
        PlayerPrefs.Save();
    }
    static float getMusicLevel(){return PlayerPrefs.GetFloat("musicLevel");}

    static void saveSfxLevel(float sfxLevel)
    {
        PlayerPrefs.SetFloat("sfxLevel", sfxLevel);
        PlayerPrefs.Save();
    }
    static float getSfxLevel(){return PlayerPrefs.GetFloat("sfxLevel");}


    static void saveLevel(Level level, int levelNumber)
    {
        PlayerPrefs.SetInt(levelNumber + "_level_number", level.number);
        PlayerPrefs.SetInt(levelNumber + "_level_avalible", level.available);
        PlayerPrefs.SetInt(levelNumber + "_level_rating", level.rating);
        PlayerPrefs.SetInt(levelNumber + "_level_complexity", level.complexity);
        PlayerPrefs.SetInt(levelNumber + "_level_enemies_list_size", level.enemyList.Count);


        for (int i = 0; i < level.enemyList.Count; i++)
        {
            PlayerPrefs.SetString(levelNumber + "_level_enemy_" + i + "_name", level.enemyList[i].name);
            PlayerPrefs.SetInt(levelNumber + "_level_enemy_" + i + "_health", level.enemyList[i].health);
            PlayerPrefs.SetInt(levelNumber + "_level_enemy_" + i + "_demage", level.enemyList[i].demage);
            PlayerPrefs.SetInt(levelNumber + "_level_enemy_" + i + "_behevour", level.enemyList[i].behavour);
        }

        PlayerPrefs.Save();
    }

    static Level LoadLevel(int levelNumber)
    {
        //CharacterData loadedCharacter = new CharacterData();
        int number = PlayerPrefs.GetInt(levelNumber + "_level_number");
        int stage = PlayerPrefs.GetInt(levelNumber + "_level_stage");
        int available = PlayerPrefs.GetInt("_level_avalible");
        int rating = PlayerPrefs.GetInt(levelNumber + "_level_rating");
        int complexity = PlayerPrefs.GetInt(levelNumber + "_level_complexity");

        int enemiesListSize = PlayerPrefs.GetInt(levelNumber + "_level_enemies_list_ize");
        List<Enemy> enemyList = new List<Enemy>();
        for (int i = 0; i < enemiesListSize; i++)
        {
            string name = PlayerPrefs.GetString(levelNumber + "_level_enemy_" + i + "_name");
            int health = PlayerPrefs.GetInt(levelNumber + "_level_enemy_" + i + "_health");
            int demage = PlayerPrefs.GetInt(levelNumber + "_level_enemy_" + i + "_demage");
            int enemyStage = PlayerPrefs.GetInt(levelNumber + "_level_enemy_" + i + "_stage");
            int behevour = PlayerPrefs.GetInt(levelNumber + "_level_enemy_" + i + "_behevour");

            //enemyList.Add(new Enemy(name, health, demage, enemyStage, behevour));
        }

        return new Level(number, stage, available, rating, complexity, enemyList);
    }

}