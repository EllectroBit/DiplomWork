using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberFacade : MonoBehaviour
{
    [SerializeField] private GameObject R_W;
    [SerializeField] private GameObject Field;
    [SerializeField] private GameObject Panel;
    [SerializeField] private Text LVL_Text;

    private NumberBridge numberBridge;
    private int LVL = 1;
    private int iCounter = 0;

    private void Start()
    {
        numberBridge = GameObject.FindObjectOfType<NumberBridge>();

        ResetGame();
    }

    public void SetInput(int input)
    {
        if(iCounter < numberBridge.GetNumbers().Length)
        {
            CheckEquals(input);

            if(iCounter + 1 == numberBridge.GetNumbers().Length + 1)
            {
                LVL++;
                GameObject.FindObjectOfType<SoundManager>().PlayLVL_UP();
                ResetGame();
            }
        }
    }

    public void UpdateValue(int i)
    {
        Field.transform.GetChild(1).gameObject.GetComponent<Text>().text = i.ToString();
    }

    public void UpdateLVL()
    {
        Record.SaveNumbersRecord(LVL);
        LVL_Text.text = LVL.ToString();
    }

    private void CheckEquals(int input)
    {
        if (numberBridge.CheckEqual(input, iCounter))
        {
            iCounter++;
            SetRight_Wrong(1);
            GameObject.FindObjectOfType<SoundManager>().PlayCorrect();
        }
        else
        {
            SetRight_Wrong(0);
            Defeat();
        }
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

    private void ResetGame()
    {
        iCounter = 0;

        UpdateLVL();

        numberBridge.GenerateNumbersToRemember(LVL);

        numberBridge.ShowNumbers(Field, Panel);
    }

    private void Defeat()
    {
        GameObject.FindObjectOfType<DefeatScript>().OnResetAction += ResetGame;
        GameObject.FindObjectOfType<DefeatScript>().Show();
        GameObject.FindObjectOfType<SoundManager>().PlayFail();
    }
}
