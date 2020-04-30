using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalHearts : MonoBehaviour
{
    public List<GameObject> Hearts;
    public GameObject GameOverPanel;
    public GameObject CorrectGuessed;
    public GameObject WrongGuessed;

    private Scores m_Scores;
    private int LifeNumber = 5;
    private CurrentGameData m_GameData;
    // Start is called before the first frame update
    void Start()
    {
        LifeNumber = 5;
        if (GameSettings.Instance.GetGameMode() == GameSettings.EGameMode.SURVIVAL_MODE)
        {
            this.enabled = true;
            foreach (GameObject h in Hearts)
            {
                h.SetActive(true);
            }
        }
        else
        {
            this.enabled = false;
        }

        m_GameData = GameObject.Find("GameDataObject").GetComponent<CurrentGameData>() as CurrentGameData;
        m_Scores = GameObject.Find("Main Camera").GetComponent<Scores>() as Scores;
    }

    public void RemoveLife()
    {
        if (LifeNumber > 0)
        {
            LifeNumber--;
            Hearts[LifeNumber].SetActive(false);
        }
        if (LifeNumber == 0)
        {
            UpdateGameHistory();
            CorrectGuessed.GetComponent<Text>().text = m_Scores.GetCurrentScore().ToString();
            WrongGuessed.GetComponent<Text>().text = m_Scores.GetCurrentWrongScore().ToString();
            GameOverPanel.SetActive(true);
            m_GameData.SetGameOver();
        }
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
