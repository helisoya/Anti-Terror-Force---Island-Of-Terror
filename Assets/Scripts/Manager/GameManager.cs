using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public SAVEFILE save;

    public Quest[] quests;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveGame()
    {
        FileManager.SaveJSON(FileManager.savPath + "save.sav", save);
    }

    public void LoadGame()
    {
        SAVEFILE loaded = FileManager.LoadJSON<SAVEFILE>(FileManager.savPath + "save.sav");
        if (loaded != null)
        {
            save = loaded;
            SceneManager.LoadScene("MissionScreen");
        }
    }


    public Quest GetQuest(string id)
    {
        foreach (Quest quest in save.quests)
        {
            if (quest.questID == id) return quest;
        }
        return null;
    }

    public Quest GetActiveQuest()
    {
        foreach (Quest quest in save.quests)
        {
            if (quest.questStatus == 1) return quest;
        }
        return null;
    }


    void ResetSaveFile()
    {
        save = new SAVEFILE();
        save.gunsOnPlayer[0] = "SilencedPistol";
        save.quests = new Quest[quests.Length];
        for (int i = 0; i < quests.Length; i++)
        {
            save.quests[i] = new Quest(quests[i]);
        }
        save.quests[0].questStatus = 1;
    }


    public void NewGame()
    {
        ResetSaveFile();
        SceneManager.LoadScene("PORT");
    }
}

