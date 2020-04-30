using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Transform LoadingBar;
    public Transform TextIndicator;
    public GameSettings.ESubjectType SubjectType;

    private float TargetAmount;
    private float CurrentAmount = 0.0f;
    private float Speed = 30;

    // Start is called before the first frame update
    void Start()
    {
        CurrentAmount = 0.0f;
        TextIndicator.GetComponent<Text>().text = ((0).ToString() + "%");

        switch (SubjectType)
        {
            case GameSettings.ESubjectType.E_ADDITION:
                {
                    float currentAnswerPerc = ((int)Config.GetAdditionScore() / (float)GameData.Instance.AdditionDataSet.Length);
                    TargetAmount = (float)currentAnswerPerc * 100.0f;
                }
                break;
            case GameSettings.ESubjectType.E_SUBTRACTION:
                {
                    float currentAnswerPerc = ((int)Config.GetSubtractionScore() / (float)GameData.Instance.SubtractionDataSet.Length);
                    TargetAmount = (float)currentAnswerPerc * 100.0f;
                }
                break;
            case GameSettings.ESubjectType.E_MULTIPLICATION:
                {
                    float currentAnswerPerc = ((int)Config.GetMultiplicationScore() / (float)GameData.Instance.MultiplicationDataSet.Length);
                    TargetAmount = (float)currentAnswerPerc * 100.0f;
                }
                break;
            case GameSettings.ESubjectType.E_DIVISION:
                {
                    float currentAnswerPerc = ((int)Config.GetDivisionScore() / (float)GameData.Instance.DivisionDataSet.Length);
                    TargetAmount = (float)currentAnswerPerc * 100.0f;
                }
                break;
            case GameSettings.ESubjectType.E_NOT_SET:
                {
                    TargetAmount = 0.0f;
                }
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentAmount < TargetAmount)
        {
            CurrentAmount += Speed * Time.deltaTime;
            TextIndicator.GetComponent<Text>().text = (((int)CurrentAmount).ToString() + "%");
            LoadingBar.GetComponent<Image>().fillAmount = (float)CurrentAmount / 100.0f;
        }
    }
}
