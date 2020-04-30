using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Number : MonoBehaviour
{
    private NumberManager m_NumberManager;
    private bool LoadNewGame = false;
    private bool IsHit = false;
    private SurvivalHearts m_SurvivalHearts;

    private Checkbox m_Checkbox;
    private CurrentGameData m_GameData;
    private Scores m_Scores;
    [HideInInspector]
    public int AnswerIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_NumberManager = GameObject.Find("Main Camera").GetComponent<NumberManager>() as NumberManager;
        m_Checkbox = GameObject.Find("Checkbox").GetComponent<Checkbox>() as Checkbox;
        m_GameData = GameObject.Find("GameDataObject").GetComponent<CurrentGameData>() as CurrentGameData;
        m_SurvivalHearts = GameObject.Find("Main Camera").GetComponent<SurvivalHearts>() as SurvivalHearts;
        m_Scores = GameObject.Find("Main Camera").GetComponent<Scores>() as Scores;
    }

    // Update is called once per frame
    void Update()
    {
        if (LoadNewGame == true && IsHit == false)
        {
            if (m_Checkbox.AnimationCompleted())
            {
                m_NumberManager.LoadNextGame();
                LoadNewGame = false;
                m_Checkbox.Clear();
            }
        }
    }

    IEnumerator Sleep()
    {
        IsHit = true;
        yield return new WaitForSeconds(1f);
        IsHit = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsHit == false && m_GameData.HasGameFinished() == false)
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                if (AnswerIndex == m_GameData.GetFinalAnswerIndex())
                {
                    m_Checkbox.Correct();
                    m_Scores.AddScores();
                    m_GameData.SetGuessed();
                }
                else
                {
                    m_Scores.AddWrongScore();
                    if (GameSettings.Instance.GetGameMode() == GameSettings.EGameMode.SURVIVAL_MODE)
                    {
                        m_SurvivalHearts.RemoveLife();
                    }
                    m_Checkbox.Wrong();
                }
                LoadNewGame = true;
            }
        }
        StartCoroutine(Sleep());
    }

    public void SetAnswerIndex(int index)
    {
        AnswerIndex = index;
    }
}
