using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    public ScoreBoardButton ScoreBoardButton;
    public float offset = 200.0f;
    public Transform StartYPosition;
    // Start is called before the first frame update
    void Start()
    {
        Config.UpdateScoreList();
        float current_position = StartYPosition.position.y;
        for (int i = 0; i < Config.LastGameScores.Count; i++)
        {
            var game_object = Instantiate(ScoreBoardButton, this.transform) as ScoreBoardButton;
            game_object.ButtonIndex = i;
            game_object.transform.position = new Vector3(this.transform.position.x, current_position, this.transform.position.z);
            current_position -= offset;
            current_position -= ScoreBoardButton.GetComponent<Image>().rectTransform.rect.height * ScoreBoardButton.GetComponent<Image>().rectTransform.localScale.y;
        }
    }
}
