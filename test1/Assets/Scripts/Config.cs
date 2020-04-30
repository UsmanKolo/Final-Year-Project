using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;
using System;
using System.Linq;

public class Config : MonoBehaviour
{
    static string dir = Directory.GetCurrentDirectory();//Application.persistentDataPath;//Directory.GetCurrentDirectory();
    static string file = @"\Maths.ini";
    static string path = dir + file;

    private static bool DebugMode = true;

    private static int numberOfScoreRecords;

    public static List<int> ScoreList;

    private static List<int> SubjectScores = new int[4].ToList();

    private static List<int> SubjectLengths;
    private const int NumberofSubjects = 4;

    //History
    private static int Max_History_Records = 4;
    private static char History_Dividor = '.';

    public class LastGameResult
    {
        public int correct = 0;
        public int total_answers = 0;
        public string game_mode_name = "";
        public string subject_name = "";
    }

    public static List<LastGameResult> LastGameScores = new List<LastGameResult>();

    public static int GetAdditionScore() { return SubjectScores[0]; }
    public static int GetSubtractionScore() { return SubjectScores[1]; }
    public static int GetMultiplicationScore() { return SubjectScores[2]; }
    public static int GetDivisionScore() { return SubjectScores[3]; }

    public static void UpdateLastGameScore(GameSettings.ESubjectType subject, LastGameResult subject_game_results)
    {
        var subject_name = GameSettings.GetSubjectNameFromType(subject);
        LastGameScores.Insert(0, subject_game_results);
        SaveScoreList();
    }

    public static void ClearData()
    {
        for (int i = 0; i < numberOfScoreRecords; i++)
        {
            ScoreList[i] = 0;
        }
    }

    public static void CreatScoreFile()
    {
        for (int i = 0; i < NumberofSubjects; i++)
        {
            SubjectScores[i] = 0;
        }
        SubjectLengths = new int[NumberofSubjects]
        {
            GameData.Instance.AdditionDataSet.Length,
            GameData.Instance.SubtractionDataSet.Length,
            GameData.Instance.MultiplicationDataSet.Length,
            GameData.Instance.DivisionDataSet.Length,
        }.ToList();

        foreach (var subject_length in SubjectLengths)
        {
            numberOfScoreRecords += subject_length;
        }
        numberOfScoreRecords += NumberofSubjects;
        ScoreList = new int[numberOfScoreRecords].ToList();

        if (File.Exists(path) == false)
        {
            ClearData();
            SaveScoreList();
        }

        UpdateScoreList();
    }

    public static void SaveScoreList()
    {
        int current_subject = 0;
        int answer_index = 0;

        File.WriteAllText(path, string.Empty);
        StreamWriter writer = new StreamWriter(path, false);

        for (int i = 0; i < numberOfScoreRecords; i++)
        {
            if ((i == 0) || (current_subject <= NumberofSubjects) && (answer_index == SubjectLengths[current_subject]))
            {
                if (DebugMode)
                {
                    string DebugSubjectName = "";
                    if (i == 0)
                    {
                        DebugSubjectName = "//ADDITION";
                    }
                    else
                    {
                        switch (current_subject)
                        {
                            case 0: DebugSubjectName = "//SUBTACTION"; break;
                            case 1: DebugSubjectName = "//MULTIPLICATION"; break;
                            case 2: DebugSubjectName = "//DIVISION"; break;
                        }
                    }

                    writer.WriteLine("#." + current_subject.ToString() + DebugSubjectName);
                }
                else
                {
                    writer.WriteLine("#." + current_subject.ToString());
                }

                if (i > 0)
                {
                    current_subject++;
                    answer_index = 0;
                }
            }
            else
            {
                if (DebugMode)
                {
                    string DebugSubjectName = "";
                    switch (current_subject)
                    {
                        case 0: DebugSubjectName = GameData.Instance.AdditionDataSet[answer_index].Question; break;
                        case 1: DebugSubjectName = GameData.Instance.SubtractionDataSet[answer_index].Question; break;
                        case 2: DebugSubjectName = GameData.Instance.MultiplicationDataSet[answer_index].Question; break;
                        case 3: DebugSubjectName = GameData.Instance.DivisionDataSet[answer_index].Question; break;
                    }
                    writer.WriteLine(i.ToString() + "." + ScoreList[i].ToString() + "0" + "       //" + DebugSubjectName);
                }
                else
                {
                    writer.WriteLine(i.ToString() + "." + ScoreList[i].ToString() + "0" + "       //");
                }
                answer_index++;
            }
        }

        //Write Game History
        int current_record_index = 0;
        foreach (var record in LastGameScores)
        {
            if (current_record_index < Max_History_Records)
            {
                string record_str;
                record_str = "#H" + current_record_index.ToString() + History_Dividor
                    + record.game_mode_name + History_Dividor
                    + record.subject_name + History_Dividor
                    + record.correct.ToString() + History_Dividor
                    + record.total_answers.ToString();

                writer.WriteLine(record_str);
            }
            current_record_index++;
        }

        writer.Close();
    }

    public static void UpdateScoreList()
    {
        LastGameScores.Clear();
        ScoreList.Clear();
        StreamReader file = new StreamReader(path);
        string line;
        while ((line = file.ReadLine()) != null)
        {
            if (line[0] != '#')
            {
                string[] line_part = line.Split('.');
                string[] part_substring = Regex.Split(line_part[1], "D");
                int score;
                if (int.TryParse(part_substring[0], out score))
                {
                    ScoreList.Add(score);
                }
                else
                {
                    ScoreList.Add(0);
                }

            }
            else
            {
                ScoreList.Add(4);
            }

            //Read History Records
            if (line[0] == '#' && line[1] == 'H')
            {
                string[] record_line = line.Split(History_Dividor);
                LastGameResult record = new LastGameResult();
                record.game_mode_name = record_line[1];
                record.subject_name = record_line[2];
                if (int.TryParse(record_line[3], out record.correct) == false)
                {
                    record.correct = 0;
                }
                if (int.TryParse(record_line[4], out record.total_answers) == false)
                {
                    record.total_answers = 0;
                }

                LastGameScores.Add(record);
            }
        }

        file.Close();
        UpdateSubjectScores();
    }

    private static void UpdateSubjectScores()
    {
        for (int i = 0; i < NumberofSubjects; i++)
        {
            SubjectScores[i] = 0;
        }

        int SearchingSubjects = 0;
        int lastPosition = FindLastPositionInSubjects(SearchingSubjects);

        for (int i = 0; i < numberOfScoreRecords; i++)
        {
            if (i <= lastPosition && (ScoreList[i] == 1))
            {
                SubjectScores[SearchingSubjects]++;
            }
            else if (i > lastPosition)
            {
                if (SearchingSubjects < 3)
                {
                    SearchingSubjects++;
                }
                lastPosition = FindLastPositionInSubjects(SearchingSubjects);
                lastPosition += SearchingSubjects;
            }
        }

    }

    private static int FindLastPositionInSubjects(int subject_index)
    {
        int position = 0;
        for (int i = 0; i <= subject_index; i++)
        {
            position += SubjectLengths[i];
        }
        return position;
    }

    public static void SaveScore(int AnswerIndex, bool Corrrect, int SubjectIndex)
    {
        int FirstPosition = FindPositionOfFirstAnswerInSubjects();
        if (SubjectIndex == 1)//Addition
        {
            if (Corrrect && (ScoreList[FirstPosition + AnswerIndex] == 0))
            {
                ScoreList[FirstPosition + AnswerIndex] = 1;
            }
            else
            {
                if (Corrrect && (ScoreList[FirstPosition + (AnswerIndex + SubjectIndex)] == 0))
                {
                    ScoreList[FirstPosition + (AnswerIndex + SubjectIndex)] = 1;
                }
            }
        }
    }

    private static int FindPositionOfFirstAnswerInSubjects()
    {
        int SubjectIndex = (int)GameSettings.Instance.GetSubjectType() - 1;
        int position = 0;
        for (int i = 0; i < SubjectIndex; i++)
        {
            position += SubjectLengths[i];
        }
        if (SubjectIndex == 0)
        {
            position += 1;
        }
        return position;
    }

    public static void ResetGameProgress()
    {
        ClearData();
        SaveScoreList();
        UpdateScoreList();
    }

    public static bool IsAnswerGuessed(int AnswerIndex)
    {
        bool correct = false;
        int SubjectIndex = (int)GameSettings.Instance.GetSubjectType() - 1;
        int SearchingAnswerIndex = FindFirstPositionOfSubject((int)GameSettings.Instance.GetSubjectType() - 1);

        if (SubjectIndex == 0)
        {
            SearchingAnswerIndex += AnswerIndex;
        }
        else
        {
            SearchingAnswerIndex += AnswerIndex + 1;
        }
        if (ScoreList[SearchingAnswerIndex] == 1)
        {
            correct = true;
        }
        return correct;
    }

    private static int FindFirstPositionOfSubject(int searchingSubjectIndex)
    {
        int SubjectIndex = searchingSubjectIndex;
        int position = 0;

        for (int i = 0; i < SubjectIndex; i++)
        {
            position += SubjectLengths[i];
            position += 1;
        }

        if (SubjectIndex == 0)
        {
            position += 1;
        }

        return position;
    }
}
