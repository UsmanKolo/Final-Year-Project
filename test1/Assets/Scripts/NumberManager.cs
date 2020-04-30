using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberManager : MonoBehaviour
{

    public GameObject[] answerObjects;
    public Text DisplayText;
    private int NumberOfAnswerObjects = 0;

    //Answer Data
    private CurrentGameData m_GameData;
    private bool isFirstRun = false;
    // Start is called before the first frame update
    void Start()
    {
        NumberOfAnswerObjects = answerObjects.Length;
        m_GameData = GameObject.Find("GameDataObject").GetComponent<CurrentGameData>() as CurrentGameData;
        isFirstRun = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFirstRun)
        {
            FirstRun();
        }
    }

    void FirstRun()
    {
        AssignAnswers();
        UpdateDisplayText();
        isFirstRun = false;
    }

    public void LoadNextGame()
    {
        StartCoroutine(LoopRotation(90f));
        m_GameData.GetNewSubjects();
        UpdateDisplayText();
    }

    public void AssignAnswers()
    {
        int FinalAnswerPosition = (int)Random.Range(0, NumberOfAnswerObjects);
        switch (FinalAnswerPosition)
        {
            case 0:
                answerObjects[0].GetComponent<SpriteRenderer>().sprite = m_GameData.GetAnswerSpriteIndex(m_GameData.GetFinalAnswerIndex());
                answerObjects[1].GetComponent<SpriteRenderer>().sprite = m_GameData.GetAnswerSpriteIndex(m_GameData.GetFirstAnswerIndex());
                answerObjects[2].GetComponent<SpriteRenderer>().sprite = m_GameData.GetAnswerSpriteIndex(m_GameData.GetSecondAnswerIndex());
                answerObjects[3].GetComponent<SpriteRenderer>().sprite = m_GameData.GetAnswerSpriteIndex(m_GameData.GetThirdAnswerIndex());

                answerObjects[0].GetComponent<Number>().SetAnswerIndex(m_GameData.GetFinalAnswerIndex());
                answerObjects[1].GetComponent<Number>().SetAnswerIndex(m_GameData.GetFirstAnswerIndex());
                answerObjects[2].GetComponent<Number>().SetAnswerIndex(m_GameData.GetSecondAnswerIndex());
                answerObjects[3].GetComponent<Number>().SetAnswerIndex(m_GameData.GetThirdAnswerIndex());
                break;
            case 1:
                answerObjects[0].GetComponent<SpriteRenderer>().sprite = m_GameData.GetAnswerSpriteIndex(m_GameData.GetFirstAnswerIndex());
                answerObjects[1].GetComponent<SpriteRenderer>().sprite = m_GameData.GetAnswerSpriteIndex(m_GameData.GetFinalAnswerIndex());
                answerObjects[2].GetComponent<SpriteRenderer>().sprite = m_GameData.GetAnswerSpriteIndex(m_GameData.GetSecondAnswerIndex());
                answerObjects[3].GetComponent<SpriteRenderer>().sprite = m_GameData.GetAnswerSpriteIndex(m_GameData.GetThirdAnswerIndex());

                answerObjects[0].GetComponent<Number>().SetAnswerIndex(m_GameData.GetFirstAnswerIndex());
                answerObjects[1].GetComponent<Number>().SetAnswerIndex(m_GameData.GetFinalAnswerIndex());
                answerObjects[2].GetComponent<Number>().SetAnswerIndex(m_GameData.GetSecondAnswerIndex());
                answerObjects[3].GetComponent<Number>().SetAnswerIndex(m_GameData.GetThirdAnswerIndex());
                break;
            case 2:
                answerObjects[0].GetComponent<SpriteRenderer>().sprite = m_GameData.GetAnswerSpriteIndex(m_GameData.GetFirstAnswerIndex());
                answerObjects[1].GetComponent<SpriteRenderer>().sprite = m_GameData.GetAnswerSpriteIndex(m_GameData.GetSecondAnswerIndex());
                answerObjects[2].GetComponent<SpriteRenderer>().sprite = m_GameData.GetAnswerSpriteIndex(m_GameData.GetFinalAnswerIndex());
                answerObjects[3].GetComponent<SpriteRenderer>().sprite = m_GameData.GetAnswerSpriteIndex(m_GameData.GetThirdAnswerIndex());

                answerObjects[0].GetComponent<Number>().SetAnswerIndex(m_GameData.GetFirstAnswerIndex());
                answerObjects[1].GetComponent<Number>().SetAnswerIndex(m_GameData.GetSecondAnswerIndex());
                answerObjects[2].GetComponent<Number>().SetAnswerIndex(m_GameData.GetFinalAnswerIndex());
                answerObjects[3].GetComponent<Number>().SetAnswerIndex(m_GameData.GetThirdAnswerIndex());
                break;
            case 3:
                answerObjects[0].GetComponent<SpriteRenderer>().sprite = m_GameData.GetAnswerSpriteIndex(m_GameData.GetFirstAnswerIndex());
                answerObjects[1].GetComponent<SpriteRenderer>().sprite = m_GameData.GetAnswerSpriteIndex(m_GameData.GetSecondAnswerIndex());
                answerObjects[2].GetComponent<SpriteRenderer>().sprite = m_GameData.GetAnswerSpriteIndex(m_GameData.GetThirdAnswerIndex());
                answerObjects[3].GetComponent<SpriteRenderer>().sprite = m_GameData.GetAnswerSpriteIndex(m_GameData.GetFinalAnswerIndex());

                answerObjects[0].GetComponent<Number>().SetAnswerIndex(m_GameData.GetFirstAnswerIndex());
                answerObjects[1].GetComponent<Number>().SetAnswerIndex(m_GameData.GetSecondAnswerIndex());
                answerObjects[2].GetComponent<Number>().SetAnswerIndex(m_GameData.GetThirdAnswerIndex());
                answerObjects[3].GetComponent<Number>().SetAnswerIndex(m_GameData.GetFinalAnswerIndex());
                break;
        }
    }

    IEnumerator LoopRotation(float angle)
    {
        float dir = 1f;
        float rotSpeed = 90.0f;
        float startANgle = angle;
        bool assigned = false;

        while (angle > 0)
        {
            float step = Time.deltaTime * rotSpeed;
            answerObjects[0].GetComponent<Transform>().Rotate(new Vector3(0, 2, 0) * step * dir);
            answerObjects[1].GetComponent<Transform>().Rotate(new Vector3(0, 2, 0) * step * dir);
            answerObjects[2].GetComponent<Transform>().Rotate(new Vector3(0, 2, 0) * step * dir);
            answerObjects[3].GetComponent<Transform>().Rotate(new Vector3(0, 2, 0) * step * dir);

            if (angle <= (startANgle / 3) && assigned == false)
            {
                AssignAnswers();
                assigned = true;
            }

            angle -= 2;

            yield return null;

        }

        answerObjects[0].GetComponent<Transform>().rotation = Quaternion.identity;
        answerObjects[1].GetComponent<Transform>().rotation = Quaternion.identity;
        answerObjects[2].GetComponent<Transform>().rotation = Quaternion.identity;
        answerObjects[3].GetComponent<Transform>().rotation = Quaternion.identity;

    }

    void UpdateDisplayText()
    {
        DisplayText.text = m_GameData.GetAnswerName(m_GameData.GetFinalAnswerIndex());
    }
}
