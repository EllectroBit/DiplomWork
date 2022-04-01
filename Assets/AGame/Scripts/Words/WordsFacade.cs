using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordsFacade : MonoBehaviour
{
    [SerializeField] private Text[] buttons;
    [SerializeField] private GameObject R_W;
    [SerializeField] private GameObject Field;
    [SerializeField] private GameObject Panel;
    [SerializeField] private Text LVL_Text;

    private WordsBridge wordsBridge;

    private void Start()
    {
        wordsBridge = GameObject.FindObjectOfType<WordsBridge>();

        ResetGame();
    }

    public void OnButtonClicked(int index)
    {
        FieldUpdate(wordsBridge.GetWord(index));
        if (wordsBridge.CheckInput(index))
        { 
            SetRight_Wrong(1);
            if(!GameObject.FindObjectOfType<SoundManager>().IsPlaying())
                GameObject.FindObjectOfType<SoundManager>().PlayCorrect();
        }
        else
        {
            SetRight_Wrong(0);
            Defeat();
        }
    }

    public void SetWords(string[] str)
    {
        for(int i = 0; i < str.Length; i++)
        {
            buttons[i].text = str[i];
        }
    }

    public void LVL_UP(int lvl)
    {
        Record.SaveWordsRecord(lvl);
        LVL_Update(lvl);
        GameObject.FindObjectOfType<SoundManager>().PlayLVL_UP();
        ResetGame();
    }

    private void LVL_Update(int lvl)
    {
        LVL_Text.text = lvl.ToString();
    }

    private void FieldUpdate(string word)
    {
        Field.transform.GetChild(1).gameObject.GetComponent<Text>().text = word;
    }

    private void ResetGame()
    {
        int lvl = wordsBridge.GetLVL();
        LVL_Update(lvl);

        if(Difficulty.GetDiffiulty() + (Difficulty.GetDiffiulty() / lvl) < wordsBridge.GetMaxWords() - 1)
            wordsBridge.SetWordsToRemember(Difficulty.GetArrSize(lvl));
        else
            wordsBridge.SetWordsToRemember(wordsBridge.GetMaxWords() - 1);

        wordsBridge.ShowWords(Field, Panel);
    }

    private void SetRight_Wrong(int Bool)
    {
        R_W.transform.GetChild(Bool).gameObject.SetActive(true);
        StartCoroutine(R_W_Off(R_W.transform.GetChild(Bool).gameObject));
    }

    private IEnumerator R_W_Off(GameObject gameObject)
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }

    private void Defeat()
    {
        GameObject.FindObjectOfType<DefeatScript>().OnResetAction += ResetGame;
        GameObject.FindObjectOfType<DefeatScript>().Show();
        GameObject.FindObjectOfType<SoundManager>().PlayFail();
    }
}
