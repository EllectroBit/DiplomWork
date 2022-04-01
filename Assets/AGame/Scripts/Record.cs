using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Record
{
    public static void SaveNumbersRecord(int i)
    {
        if (PlayerPrefs.HasKey("NumbersRecord") && PlayerPrefs.GetInt("NumbersRecord") < i)
        {
            PlayerPrefs.SetInt("NumbersRecord", i);
        }
        else if (!PlayerPrefs.HasKey("NumbersRecord"))
        {
            PlayerPrefs.SetInt("NumbersRecord", i);
        }
    }

    public static void SaveWordsRecord(int i)
    {
        if (PlayerPrefs.HasKey("WordsRecord") && PlayerPrefs.GetInt("WordsRecord") < i)
        {
            PlayerPrefs.SetInt("WordsRecord", i);
        }
        else if (!PlayerPrefs.HasKey("WordsRecord"))
        {
            PlayerPrefs.SetInt("WordsRecord", i);
        }
    }

    public static void SaveSequencesRecord(int i)
    {
        if (PlayerPrefs.HasKey("SequencesRecord") && PlayerPrefs.GetInt("SequencesRecord") < i)
        {
            PlayerPrefs.SetInt("SequencesRecord", i);
        }
        else if (!PlayerPrefs.HasKey("SequencesRecord"))
        {
            PlayerPrefs.SetInt("SequencesRecord", i);
        }
    }

    public static string GetWordsRecord()
    {
        if (PlayerPrefs.HasKey("WordsRecord"))
            return PlayerPrefs.GetInt("WordsRecord") + TextForm();
        return 1 + TextForm();
    }

    public static string GetNumbersRecord()
    {
        if (PlayerPrefs.HasKey("NumbersRecord"))
            return PlayerPrefs.GetInt("NumbersRecord") + TextForm();
        return 1 + TextForm();
    }

    public static string GetSequencesRecord()
    {
        if (PlayerPrefs.HasKey("SequencesRecord"))
            return PlayerPrefs.GetInt("SequencesRecord") + TextForm();
        return 1 + TextForm();
    }

    public static void SetToLeaderBoard()
    {
        if(PlayerPrefs.HasKey("NumbersRecord"))
            Social.ReportScore(PlayerPrefs.GetInt("NumbersRecord"), "CgkI0aaV2L0fEAIQAQ", (bool s) => { });
        if (PlayerPrefs.HasKey("WordsRecord"))
            Social.ReportScore(PlayerPrefs.GetInt("WordsRecord"), "CgkI0aaV2L0fEAIQAg", (bool s) => { });
        if (PlayerPrefs.HasKey("SequencesRecord"))
            Social.ReportScore(PlayerPrefs.GetInt("SequencesRecord"), "CgkI0aaV2L0fEAIQAw", (bool s) => { });
    }

    private static string TextForm()
    {
        string res = " LVL";

        if (PlayerPrefs.HasKey("Language") && PlayerPrefs.GetString("Language") == "ru_RU")
        {
            res = " Уровень";
        } else if (PlayerPrefs.HasKey("Language") && PlayerPrefs.GetString("Language") == "uk_UK")
        {
            res = " Рiвень";
        }

        return res;
    }
}
