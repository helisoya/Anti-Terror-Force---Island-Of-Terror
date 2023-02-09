using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MissionScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questName;

    void Start()
    {
        GameManager.instance.SaveGame();
        Cursor.lockState = CursorLockMode.None;
        questName.text = GameManager.instance.GetActiveQuest().questName;
    }


    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

}
