using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class MainMenuUI : MonoBehaviour
{
    [Header("Main Screen")]
    [SerializeField] private GameObject mainScreenRoot;
    [SerializeField] private GameObject continueButton;

    [Header("Options")]
    [SerializeField] private GameObject optionsRoot;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;

    private Resolution[] resolutions;


    private bool saveFileExists
    {
        get
        {
            return File.Exists(FileManager.savPath + "save.sav");
        }
    }


    void Start()
    {
        continueButton.SetActive(saveFileExists);
    }


    public void Quit()
    {
        Application.Quit();
    }

    public void NewGame()
    {
        GameManager.instance.NewGame();
    }

    public void LoadGame()
    {
        if (saveFileExists)
        {
            GameManager.instance.LoadGame();
        }
    }

    public void OpenOptions()
    {
        mainScreenRoot.SetActive(false);
        optionsRoot.SetActive(true);
        resolutions = Screen.resolutions;


        Resolution currentRes = Screen.currentResolution;
        int currentIndex = 0;
        List<string> names = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            names.Add(resolutions[i].width + "x" + resolutions[i].height + " (" + resolutions[i].refreshRate + ")");
            if (currentIndex == 0 &&
                resolutions[i].width == currentRes.width &&
                resolutions[i].height == currentRes.height &&
                resolutions[i].refreshRate == currentRes.refreshRate)
            {
                currentIndex = i;
            }
        }


        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(names);
        resolutionDropdown.SetValueWithoutNotify(currentIndex);

        fullscreenToggle.SetIsOnWithoutNotify(Screen.fullScreen);
    }

    public void CloseOptions()
    {
        mainScreenRoot.SetActive(true);
        optionsRoot.SetActive(false);
    }

    public void ChangeResolution(int newIndex)
    {
        Resolution newRes = resolutions[newIndex];
        Screen.SetResolution(newRes.width, newRes.height, Screen.fullScreen, newRes.refreshRate);
    }

    public void ChangeFullscreen(bool newVal)
    {
        Resolution res = Screen.currentResolution;
        Screen.SetResolution(res.width, res.height, newVal, res.refreshRate);
    }


}
