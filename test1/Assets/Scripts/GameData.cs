using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{
    [System.Serializable]
    public struct SubjectData
    {
        public string Question;
        public Sprite Answer;
        public bool Guessed;
        public bool BeenQuestioned;
    }

    public SubjectData[] AdditionDataSet;
    public SubjectData[] SubtractionDataSet;
    public SubjectData[] MultiplicationDataSet;
    public SubjectData[] DivisionDataSet;
    [HideInInspector]
    public SubjectData[] SubjectPerGame;
    [HideInInspector]
    public SubjectData[] SubjectDataSet;

    public static GameData Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
            Config.CreatScoreFile();
        }
        else
        {
            Destroy(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AssignArrayOfSubjects()
    {
        switch (GameSettings.Instance.GetSubjectType())
        {
            case GameSettings.ESubjectType.E_ADDITION:
                SubjectDataSet = new SubjectData[AdditionDataSet.Length];
                AdditionDataSet.CopyTo(SubjectDataSet, 0);
                break;
            case GameSettings.ESubjectType.E_SUBTRACTION:
                SubjectDataSet = new SubjectData[SubtractionDataSet.Length];
                SubtractionDataSet.CopyTo(SubjectDataSet, 0);
                break;
            case GameSettings.ESubjectType.E_MULTIPLICATION:
                SubjectDataSet = new SubjectData[MultiplicationDataSet.Length];
                MultiplicationDataSet.CopyTo(SubjectDataSet, 0);
                break;
            case GameSettings.ESubjectType.E_DIVISION:
                SubjectDataSet = new SubjectData[DivisionDataSet.Length];
                DivisionDataSet.CopyTo(SubjectDataSet, 0);
                break;
            case GameSettings.ESubjectType.E_NOT_SET:
                break;
        }
    }
}
