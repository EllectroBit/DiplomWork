using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private Text NumbersRec;
    [SerializeField] private Text WordsRec;
    [SerializeField] private Text SequencesRec;

    private string scene;

    private void Start()
    {
        Load();
        GameObject.FindObjectOfType<LocalizationManager>().OnLanguageChanged += Load;
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void OnGameModeClicked(string gameMode)
    {
        scene = gameMode;
        GameObject.FindGameObjectWithTag("Difficulty").transform.GetChild(0).gameObject.SetActive(true);
    }

    public void ChooseDifficulty(int diff)
    {
        switch (diff)
        {
            case 0:
                Difficulty.SetDifficulty(Difficulty.Diff.EASY);
                break;
            case 1:
                Difficulty.SetDifficulty(Difficulty.Diff.NORMAL);
                break;
            case 2:
                Difficulty.SetDifficulty(Difficulty.Diff.HARD);
                break;
        }

        SceneManager.LoadScene(scene);
    }

    private void Load()
    {
        NumbersRec.text = Record.GetNumbersRecord();
        WordsRec.text = Record.GetWordsRecord();
        SequencesRec.text = Record.GetSequencesRecord();
    }
}
