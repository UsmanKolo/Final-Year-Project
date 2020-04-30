using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scores : MonoBehaviour
{
    public Text ScoreText;

    private CurrentGameData m_GameData;
    private int m_AnswerNumber;
    private int m_Scores;
    private int m_WrongScores;
    private bool Initialised = false;
    // Start is called before the first frame update
    void Start()
    {
        m_GameData = GameObject.Find("GameDataObject").GetComponent<CurrentGameData>() as CurrentGameData;
        m_Scores = 0;
        m_WrongScores = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Initialised)
        {
            if (GameSettings.Instance.GetGameMode() == GameSettings.EGameMode.NOT_SET)
            {
                m_AnswerNumber = 0;
            }
            else
            {
                m_AnswerNumber = m_GameData.GetAnswerNumber();
            }
            Initialised = true;
        }
    }

    public int GetCurrentScore() { return m_Scores; }
    public int GetCurrentWrongScore() { return m_WrongScores; }
    public int GetQuestionNumber() { return m_AnswerNumber; }

    public void AddScores()
    {
        if (m_Scores < m_AnswerNumber)
        {
            m_Scores += 1;
            DisplayScores();
        }
    }

    public void RemoveScore()
    {
        if (m_Scores > 0)
        {
            m_Scores -= 1;
            DisplayScores();
        }
    }

    public void AddWrongScore()
    {
        if (m_WrongScores <= m_AnswerNumber)
        {
            m_WrongScores += 1;
        }
    }

    void DisplayScores()
    {
        string DisplayString = "Scores: " + m_Scores + "/" + m_AnswerNumber;
        ScoreText.text = DisplayString;
    }
}
