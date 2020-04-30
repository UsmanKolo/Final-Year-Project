using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    private Dictionary<ESubjectType, string> _SceneName = new Dictionary<ESubjectType, string>();
    public enum ESubjectType
    {
        E_NOT_SET = 0,
        E_ADDITION,
        E_SUBTRACTION,
        E_MULTIPLICATION,
        E_DIVISION
    };

    public enum EGameMode
    {
        NOT_SET,
        TIME_TRIAL_MODE,//Play till time up
        SURVIVAL_MODE
    };

    private EGameMode _GameMode;
    private ESubjectType _Subject;
    private string SubjectName;
    public static GameSettings Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetSceneNameAndType();
        _GameMode = EGameMode.NOT_SET;
        _Subject = ESubjectType.E_NOT_SET;
    }

    private void SetSceneNameAndType()
    {
        _SceneName.Add(ESubjectType.E_ADDITION, "Game");
        _SceneName.Add(ESubjectType.E_SUBTRACTION, "Game");
        _SceneName.Add(ESubjectType.E_MULTIPLICATION, "Game");
        _SceneName.Add(ESubjectType.E_DIVISION, "Game");
    }

    public string GetSubjectSceneName()
    {
        string name;
        if (_SceneName.TryGetValue(_Subject, out name))
        {
            return name;
        }
        else
        {
            Debug.Log("Error: Subject scene not found");
            return ("Error: Subject scene not found");
        }
    }

    public void SetSubjectType(ESubjectType type)
    {
        _Subject = type;
    }

    public void SetGameMode(EGameMode mode)
    {
        _GameMode = mode;
    }

    public EGameMode GetGameMode()
    {
        return _GameMode;
    }

    public ESubjectType GetSubjectType()
    {
        return _Subject;
    } 

    public void SetSubjectName(string name)
    {
        SetSubjectType(GetSubjectTypeFromString(name));
        SubjectName = name;
    }

    public static ESubjectType GetSubjectTypeFromString(string type)
    {
        switch (type)
        {
            case "ADDITION": return ESubjectType.E_ADDITION;
            case "SUBTRACTION": return ESubjectType.E_SUBTRACTION;
            case "MULTIPLICATION": return ESubjectType.E_MULTIPLICATION;
            case "DIVISION": return ESubjectType.E_DIVISION;
            default: return ESubjectType.E_NOT_SET;
        }
    }

    public static string GetSubjectNameFromType(ESubjectType type)
    {
        switch (type)
        {
            case ESubjectType.E_ADDITION: return "ADDITION";
            case ESubjectType.E_SUBTRACTION: return "SUBTRACTION";
            case ESubjectType.E_MULTIPLICATION: return "MULTIPLICATION";
            case ESubjectType.E_DIVISION: return "DIVISION";
            default: return "NOT_SET";
        }
    }

    public static string GetGameModeNameFromType(EGameMode type)
    {
        switch (type)
        {
            case EGameMode.SURVIVAL_MODE: return "SURVIVAL";
            case EGameMode.TIME_TRIAL_MODE: return "TIME TRIAL";
            default: return "NOT_SET";
        }
    }

    public static EGameMode GetGameModeTypeFromString(string mode)
    {
        switch (mode)
        {
            case "SURVIVAL": return EGameMode.SURVIVAL_MODE;
            case "TIME TRIAL": return EGameMode.TIME_TRIAL_MODE;
            default: return EGameMode.NOT_SET;
        }
    }
}
