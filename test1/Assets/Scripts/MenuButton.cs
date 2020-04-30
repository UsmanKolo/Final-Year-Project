using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public GameObject mGuiPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneAndClearData(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ShowGameModePanel()
    {
        mGuiPanel.SetActive(true);
    }

    public void HideGameModePanel()
    {
        mGuiPanel.SetActive(false);
    }

    public void StartTimeTrial()
    {
        GameSettings.Instance.SetGameMode(GameSettings.EGameMode.TIME_TRIAL_MODE);
        LoadScene(GameSettings.Instance.GetSubjectSceneName());
    }

    public void StartSurvival()
    {
        GameSettings.Instance.SetGameMode(GameSettings.EGameMode.SURVIVAL_MODE);
        LoadScene(GameSettings.Instance.GetSubjectSceneName());
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ResetGameProgress()
    {
        Config.ResetGameProgress();
    }
}
