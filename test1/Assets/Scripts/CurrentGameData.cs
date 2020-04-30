using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentGameData : MonoBehaviour
{
    [HideInInspector]
    public int FirstAnswerIndex = 0;
    [HideInInspector]
    public int SecondAnswerIndex;
    [HideInInspector]
    public int ThirdAnswerIndex;
    [HideInInspector]
    public int FinalAnswerIndex;

    private int PrevFinalAnswerIndex; //Make sure answer is not selected twice
    private int SubjectsPerGame = 10; // How many answers should be in a game
    private bool GameFinished = false;

    public void ResetGameOver() { GameFinished = false; }
    public bool HasGameFinished() { return GameFinished; }
    public void SetGameOver() 
    { 
        GameFinished = true;
        if (GameFinished)
        {
            int NumberOfAnswers = GameData.Instance.SubjectPerGame.Length;
            Config.UpdateScoreList();
            for (int i = 0; i < NumberOfAnswers; i++)
            {
                Config.SaveScore(i, GameData.Instance.SubjectPerGame[i].Guessed, (int)GameSettings.Instance.GetSubjectType());
            }
            Config.SaveScoreList();
            Config.UpdateScoreList();
        }
    }

    void Start()
    {
        FinalAnswerIndex = 0;
        PrevFinalAnswerIndex = 0;
        FirstAnswerIndex = 0;
        SecondAnswerIndex = 0;
        ThirdAnswerIndex = 0;
        GameData.Instance.AssignArrayOfSubjects();
        if (SubjectsPerGame >= GameData.Instance.SubjectDataSet.Length)
        {
            SubjectsPerGame = GameData.Instance.SubjectDataSet.Length;
        }
        GameData.Instance.SubjectPerGame = new GameData.SubjectData[SubjectsPerGame];
        GameFinished = false;

        PickSubjectsForGame();
        GetNewSubjects();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PickSubjectsForGame()
    {
        int PickedSubjectNumber = 0;

        for (int i = 0; i < GameData.Instance.SubjectDataSet.Length; i++)
        {
            if (PickedSubjectNumber >= SubjectsPerGame)
            {
                break;
            }
            else
            {
                if (GameData.Instance.SubjectDataSet[i].Guessed == false)
                {
                    GameData.Instance.SubjectPerGame[PickedSubjectNumber] = GameData.Instance.SubjectDataSet[i];
                    PickedSubjectNumber++;
                }
            }
        }

        if (PickedSubjectNumber < SubjectsPerGame -1)
        {//if we ont have enough answers select random
            for (int i = 0; i < GameData.Instance.SubjectDataSet.Length; i++)
            {
                if (PickedSubjectNumber >= SubjectsPerGame)
                {
                    break;
                }
                else
                {
                    if (GameData.Instance.SubjectDataSet[i].Guessed == true)
                    {
                        GameData.Instance.SubjectPerGame[PickedSubjectNumber] = GameData.Instance.SubjectDataSet[i];
                        PickedSubjectNumber++;
                    }
                }
            }
        }
    }

    public void GetNewSubjects()
    {
        PrevFinalAnswerIndex = FinalAnswerIndex;
        if (GetNumberOfSubjectsLeft() > 0)
        {
            do
            {
                FinalAnswerIndex = (int)Random.Range(0, GameData.Instance.SubjectPerGame.Length);
            } while (PrevFinalAnswerIndex == FinalAnswerIndex || GameData.Instance.SubjectPerGame[FinalAnswerIndex].Guessed == true);

            do
            {
                FirstAnswerIndex = (int)Random.Range(0, GameData.Instance.SubjectPerGame.Length);
            } while (FirstAnswerIndex == FinalAnswerIndex);

            do
            {
                SecondAnswerIndex = (int)Random.Range(0, GameData.Instance.SubjectPerGame.Length);
            } while (SecondAnswerIndex == FirstAnswerIndex || SecondAnswerIndex == FinalAnswerIndex);

            do
            {
                ThirdAnswerIndex = (int)Random.Range(0, GameData.Instance.SubjectPerGame.Length);
            } while (ThirdAnswerIndex == SecondAnswerIndex || ThirdAnswerIndex == FinalAnswerIndex);

            GameData.Instance.SubjectPerGame[FinalAnswerIndex].BeenQuestioned = true;
        }
        else
        {
            //Level Completed
            GameFinished = true;
        }
    }

    private int GetNumberOfSubjectsLeft()
    {
        int left = 0;
        for (int i = 0; i < GameData.Instance.SubjectPerGame.Length; i++)
        {
            if (GameData.Instance.SubjectPerGame[i].Guessed == false)
            {
                left++;
            }
        }
        return left;
    }

    public string GetAnswerName(int index)
    {
        return GameData.Instance.SubjectPerGame[index].Question;
    }

    public int GetAnswerNameLength(int answerIndex)
    {
        return GameData.Instance.SubjectPerGame[answerIndex].Question.Length;
    }

    public int GetFirstAnswerIndex()
    {
        return FirstAnswerIndex;
    }

    public int GetSecondAnswerIndex()
    {
        return SecondAnswerIndex;
    }

    public int GetThirdAnswerIndex()
    {
        return ThirdAnswerIndex;
    }

    public int GetFinalAnswerIndex()
    {
        return FinalAnswerIndex;
    }

    public void SetGuessed()
    {
        GameData.Instance.SubjectPerGame[FinalAnswerIndex].Guessed = true;
    }
    //Return sprite image for selected answer.
    public Sprite GetAnswerSpriteIndex(int answerIndex)
    {
        return GameData.Instance.SubjectPerGame[answerIndex].Answer;
    }

    public int GetAnswerNumber()
    {
        return GameData.Instance.SubjectDataSet.Length;
    }
}
