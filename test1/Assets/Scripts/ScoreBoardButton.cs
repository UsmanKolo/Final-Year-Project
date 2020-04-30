using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoardButton : MonoBehaviour
{
    public Sprite SurvivalGameIcon;
    public Sprite TimeTrialIcon;

    public Sprite AdditionButton;
    public Sprite SubtractionButton;
    public Sprite MultiplicationButton;
    public Sprite DivisionButton;

    public int ButtonIndex = 0;

    public GameObject ScoreButton;
    public GameObject GameModeIcon;
    public GameObject CorrectNumberText;
    public GameObject TotalAnswerNumberText;
    // Start is called before the first frame update
    void Start()
    {
        if (ButtonIndex >= Config.LastGameScores.Count)
        {
            Debug.LogError("Button index is too light, there is not enough data");
        }
        else
        {
            var record = Config.LastGameScores[ButtonIndex];
            var subject = GameSettings.GetSubjectTypeFromString(record.subject_name);

            if (subject == GameSettings.ESubjectType.E_ADDITION) ScoreButton.GetComponent<Image>().sprite = AdditionButton;
            if (subject == GameSettings.ESubjectType.E_SUBTRACTION) ScoreButton.GetComponent<Image>().sprite = SubtractionButton;
            if (subject == GameSettings.ESubjectType.E_MULTIPLICATION) ScoreButton.GetComponent<Image>().sprite = MultiplicationButton;
            if (subject == GameSettings.ESubjectType.E_DIVISION) ScoreButton.GetComponent<Image>().sprite = DivisionButton;

            var game_mode_type = GameSettings.GetGameModeTypeFromString(record.game_mode_name);
            if (game_mode_type == GameSettings.EGameMode.SURVIVAL_MODE) GameModeIcon.GetComponent<Image>().sprite = SurvivalGameIcon;
            if (game_mode_type == GameSettings.EGameMode.TIME_TRIAL_MODE) GameModeIcon.GetComponent<Image>().sprite = TimeTrialIcon;

            CorrectNumberText.GetComponent<Text>().text = record.correct.ToString();
            TotalAnswerNumberText.GetComponent<Text>().text = record.total_answers.ToString();
        }
    }

}
