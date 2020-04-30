using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    public float timeLeft;
    private CurrentGameData m_GameData;
    public GUIStyle ClockStyle;

    private bool StartedGameOverTimer = false;

    //Gane Over
    public GUIStyle GameOverStyle;
    public GameObject GameOverPanel;
    public GameObject CorrectGuessedText;
    public GameObject WrongGuessedText;
    private Scores m_Scores;

    private bool EndGUIActivated;
    // Start is called before the first frame update
    void Start()
    {
        StartedGameOverTimer = false;
        EndGUIActivated = false;
        m_GameData = GameObject.Find("GameDataObject").GetComponent<CurrentGameData>() as CurrentGameData;
        m_Scores = GameObject.Find("Main Camera").GetComponent<Scores>() as Scores;
        if (GameSettings.Instance.GetGameMode() == GameSettings.EGameMode.TIME_TRIAL_MODE)
        {
            this.enabled = true;
        }
        else
        {
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (EndGUIActivated && StartedGameOverTimer == false)
        {
            StartedGameOverTimer = true;
            //StartCoroutine(Sleep(2f));
        }
    }

    //IEnumerator Sleep(float sleepTime)
    //{
    //    yield return new WaitForSeconds(sleepTime);
    //    ActivateGameOverGUI();
    //}

    private void ActivateGameOverGUI()
    {
        UpdateGameHistory();
        CorrectGuessedText.GetComponent<Text>().text = m_Scores.GetCurrentScore().ToString();
        WrongGuessedText.GetComponent<Text>().text = m_Scores.GetCurrentWrongScore().ToString();
        GameOverPanel.SetActive(true);
        EndGUIActivated = true;
    }

    void UpdateGameHistory()
    {
        Config.LastGameResult game_results = new Config.LastGameResult { };
        game_results.correct = m_Scores.GetCurrentScore();
        game_results.total_answers = m_GameData.GetAnswerNumber();
        game_results.game_mode_name = GameSettings.GetGameModeNameFromType(GameSettings.Instance.GetGameMode());
        game_results.subject_name = GameSettings.GetSubjectNameFromType(GameSettings.Instance.GetSubjectType());

        Config.UpdateLastGameScore(GameSettings.Instance.GetSubjectType(), game_results);
    }

    void OnGUI()
    {
        if (timeLeft > 0)
        {
            GUI.Label(new Rect(Screen.width / 2 - 20, 10, 200, 100), "" + (int)timeLeft, ClockStyle);
        }
        else
        {
            if (m_GameData.HasGameFinished() == false && EndGUIActivated == true)
            {
                m_GameData.SetGameOver();
            }
            if (EndGUIActivated == false)
            {
                ActivateGameOverGUI();
            }
            
        }
    }
}
