using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorsFacade : MonoBehaviour
{
    [SerializeField] private GameObject R_W;
    [SerializeField] private GameObject Panel;
    [SerializeField] private GameObject GetReady;
    [SerializeField] private Text LVL_Text;

    private ColorsBridge sequencesBridge;
    
    public bool input { private get; set; }

    private void Start()
    {
        sequencesBridge = GameObject.FindObjectOfType<ColorsBridge>();
        sequencesBridge.SetImages(Panel);
        ResetGame();
    }

    public void OnButtonClicked(int index)
    {
        if (input)
        {
            if (sequencesBridge.CheckEqual(index))
            {
                SetRight_Wrong(1);
                if (!GameObject.FindObjectOfType<SoundManager>().IsPlaying())
                    GameObject.FindObjectOfType<SoundManager>().PlayCorrect();
            }
            else
            {
                SetRight_Wrong(0);
                Defeat();
            }
        }
    }

    public void LVL_UP(int lvl)
    {
        LVL_Text.text = lvl.ToString();
        Record.SaveSequencesRecord(lvl);
        GameObject.FindObjectOfType<SoundManager>().PlayLVL_UP();
        ResetGame();
    }

    private void ResetGame()
    {
        sequencesBridge.SetArrToRemember(Difficulty.GetArrSize(sequencesBridge.GetLVL()));

        sequencesBridge.ShowSequence(GetReady);
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
        GameObject.FindObjectOfType<DefeatScript>().OnResetAction -= ResetGame;
        GameObject.FindObjectOfType<DefeatScript>().OnResetAction += ResetGame;
        GameObject.FindObjectOfType<DefeatScript>().Show();
        GameObject.FindObjectOfType<SoundManager>().PlayFail();
    }
}
