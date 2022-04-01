using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorsBridge : MonoBehaviour
{
    private const int MIN = 0;
    private const int MAX = 24;

    private int[] ArrToRemember;
    private Animation[] Buttons;
    private int Current = 0;
    private int LVL = 1;

    private ColorsFacade sequencesFacade;

    private void Awake()
    {
        sequencesFacade = GameObject.FindObjectOfType<ColorsFacade>();
    }

    public void ShowSequence(GameObject Field)
    {
        StartCoroutine(Show(Field));
    }

    public int GetLVL()
    {
        return LVL;
    }

    public bool CheckEqual(int index)
    {
        if (ArrToRemember[Current] == index)
        {
            if (Current + 1 == ArrToRemember.Length)
            {
                LVL++;
                sequencesFacade.LVL_UP(LVL);
                return true;
            }
            Buttons[index].Play("Press");
            Current++;
            return true;
        }

        return false;
    }

    public void SetImages(GameObject gameObject)
    {
        Buttons = new Animation[gameObject.transform.childCount];
        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            Buttons[i] = gameObject.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.GetComponent<Animation>();
        }
    }

    public void SetArrToRemember(int size)
    {
        Current = 0;

        int[] arr = new int[size];
        arr[0] = Random.Range(MIN, MAX);

        for(int i = 1; i < arr.Length; i++)
        {
            arr[i] = GetUnicValue(arr[i - 1]);
        }

        ArrToRemember = arr;

        foreach(int i in ArrToRemember) { Debug.Log(i); }
    }

    private int GetUnicValue(int last)
    {
        int temp = Random.Range(MIN, MAX);
        if (temp == last)
            return GetUnicValue(last);
        return temp;
    }

    private IEnumerator Show(GameObject Field)
    {
        sequencesFacade.input = false;

        Field.SetActive(true);
        yield return new WaitForSeconds(2);

        Field.SetActive(false);

        foreach(int i in ArrToRemember)
        {
            Buttons[i].Play("Colors");
            yield return new WaitForSeconds(1.5f);
        }

        sequencesFacade.input = true;
    }
}
