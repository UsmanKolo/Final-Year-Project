using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameProgressSummary : MonoBehaviour
{
    public GameObject AnswerButton;
    public GameObject ButtonParent;
    public GameObject TitleText;
    public Sprite CorrectImage;
    public Sprite WrongImage;

    private int NumberOfAnswers;
    // Start is called before the first frame update
    void Start()
    {
        Config.UpdateScoreList();
        GameData.Instance.AssignArrayOfSubjects();
        NumberOfAnswers = GameData.Instance.SubjectDataSet.Length;

        switch (GameSettings.Instance.GetSubjectType())
        {
            case GameSettings.ESubjectType.E_ADDITION:
                TitleText.GetComponent<Text>().text = "ADDITION PROGRESS";
                break;
            case GameSettings.ESubjectType.E_SUBTRACTION:
                TitleText.GetComponent<Text>().text = "SUBTRACTION PROGRESS";
                break;
            case GameSettings.ESubjectType.E_MULTIPLICATION:
                TitleText.GetComponent<Text>().text = "MULTIPLICATION PROGRESS";
                break;
            case GameSettings.ESubjectType.E_DIVISION:
                TitleText.GetComponent<Text>().text = "DIVISION PROGRESS";
                break;
        }

        for (int i = 0; i < NumberOfAnswers; i++)
        {
            CreateButton(i);
        }
    }

    void CreateButton(int index)
    {
        GameObject button = (GameObject)Instantiate(AnswerButton);
        button.GetComponent<RectTransform>().SetParent(ButtonParent.transform, false);

        string name = GameData.Instance.SubjectDataSet[index].Question;
        int MaxLength = 14;
        int Lines = name.Length / MaxLength;

        if (name.Length > MaxLength)
        {
            string result = "";
            for (int i = 0; i < Lines; i++)
            {
                if (i < 1)
                {
                    result += name.Substring(0, MaxLength);
                }
                int number = -1;
                for (int counter = 0; counter < result.Length; counter++)
                {
                    if (result[counter] == ' ')
                    {
                        number = counter;
                    }
                }

                if (number >= 0)
                {
                    result = name.Substring(0, number);
                    result += "\n";
                    result += name.Substring(number + 1);
                    name = result;
                }
            }
        }

        button.transform.GetChild(0).GetComponent<Text>().text = name;

        if (Config.IsAnswerGuessed(index))
        {
            button.transform.GetChild(1).GetComponent<Image>().sprite = CorrectImage;
        }
        else
        {
            button.transform.GetChild(1).GetComponent<Image>().sprite = WrongImage;
        }
        button.transform.GetChild(2).GetComponent<Image>().sprite = GameData.Instance.SubjectDataSet[index].Answer;
    }
}
