using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SubjectButton : MonoBehaviour
{
    public string Name;

    public void OnClick()
    {
        GameSettings.Instance.SetSubjectName(Name);
    }
}
