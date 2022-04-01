using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class WordsBridge : MonoBehaviour
{
    private string[] AllWords;
    private string[] WordsToRemember;
    private string[] WordsToOutput;

    private int LVL = 1;
    private int Current = 0;

    private void Awake()
    {
        LoadWords();
    }

    public void SetWordsToRemember(int size)
    {
        WordsToRemember = new string[size];

        WordsToRemember[0] = GetUnicWord("");
        for (int i = 1; i < WordsToRemember.Length; i++)
        {
            WordsToRemember[i] = GetUnicWord(WordsToRemember[i - 1]);
        }

        foreach (string i in WordsToRemember) { Debug.Log(i); };

        Current = 0;
        WordsToOutput = GetUnicWordsArrWith(8, WordsToRemember[Current]);
    }

    public bool CheckInput(int i)
    {
        if (WordsToOutput[i] == WordsToRemember[Current])
        {
            if (Current + 1 == WordsToRemember.Length)
            {
                LVL++;
                GameObject.FindObjectOfType<WordsFacade>().LVL_UP(LVL);
                return true;
            }

            Current++;
            WordsToOutput = GetUnicWordsArrWith(8, WordsToRemember[Current]);
            GameObject.FindObjectOfType<WordsFacade>().SetWords(WordsToOutput);

            return true;
        }
        else
            return false;
    }

    public void ShowWords(GameObject Field, GameObject Panel)
    {
        StartCoroutine(Show(Field, Panel));
    }

    public string[] GetWords()
    {
        return WordsToOutput;
    }

    public int GetLVL()
    {
        return LVL;
    }

    public string GetWord(int i)
    {
        return WordsToOutput[i];
    }

    public int GetMaxWords()
    {
        return AllWords.Length;
    }

    private void LoadWords()
    {
        string path = Application.streamingAssetsPath;
        path += PlayerPrefs.GetString("Language") == "ru_RU" ? "/ruWords.json" : PlayerPrefs.GetString("Language") == "uk_UK" ? "/ukWords.json" : "/enWords.json";
        string JsonData;

        if (Application.platform == RuntimePlatform.Android)
        {
            WWW reader = new WWW(path);
            while (!reader.isDone) { }

            JsonData = reader.text;
        }
        else
        {
            JsonData = File.ReadAllText(path);
        }

        SerData serData = JsonUtility.FromJson<SerData>(JsonData);
        AllWords = serData.items;
    }

    private string[] GetUnicWordsArrWith(int size, string word)
    {
        string[] res = new string[size];
        res[0] = word;
        for(int i = 1; i < size; i++)
        {
            SetUnicWordsIn(res, i);
        }

        ArrMixer(res);
        return res;
    }

    private void ArrMixer(string[] str)
    {
        System.Random random = new System.Random();

        for (int i = str.Length - 1; i >= 1; i--)
        {
            int j = random.Next(i + 1);
            string temp = str[j];
            str[j] = str[i];
            str[i] = temp;
        }
    }

    private void SetUnicWordsIn(string[] str, int count)
    {
        string temp = AllWords[Random.Range(1, AllWords.Length)];
        bool Match = false;

        for(int i = 0; i < str.Length; i++)
        {
            if (temp == str[i])
                Match = true;
        }

        if (!Match)
            str[count] = temp;
        else
            SetUnicWordsIn(str, count);
    }

    private string GetUnicWord(string last)
    {
        string temp = AllWords[Random.Range(0, AllWords.Length)];

        if(temp == last)
        {
            return GetUnicWord(last);
        }
        return temp;
    }

    private IEnumerator Show(GameObject field, GameObject panel)
    {
        Text text = field.transform.GetChild(1).gameObject.GetComponent<Text>();

        panel.SetActive(false);
        field.transform.GetChild(1).gameObject.SetActive(false);
        field.transform.GetChild(0).gameObject.SetActive(true);

        yield return new WaitForSeconds(2);

        field.transform.GetChild(0).gameObject.SetActive(false);
        field.transform.GetChild(1).gameObject.SetActive(true);

        foreach (string i in WordsToRemember)
        {
            text.text = i;
            yield return new WaitForSeconds(1.5f);
        }

        text.text = "";
        panel.SetActive(true);

        GameObject.FindObjectOfType<WordsFacade>().SetWords(WordsToOutput);
    }
}
