using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class NumberBridge : MonoBehaviour
{
    private int[] NumbersToRemember;

    private int MIN = 1;
    private int MAX = 100;

    public int[] GetNumbers()
    {
        return NumbersToRemember;
    }

    public void GenerateNumbersToRemember(int lvl)
    {
        GenerateNumbersToRemember(Difficulty.GetArrSize(lvl), MIN, MAX);

        if (lvl % 10 == 0)
            MAX += 50;

        foreach (int i in NumbersToRemember) { Debug.Log(i); };
    }

    public bool CheckEqual(int number, int counter)
    {
        return number == NumbersToRemember[counter];
    }

    public void ShowNumbers(GameObject field, GameObject panel)
    {
        StartCoroutine(Show(field, panel));
    }

    private void GenerateNumbersToRemember(int size, int from, int to)
    {
        NumbersToRemember = new int[size];

        NumbersToRemember[0] = Random.Range(from, to);
        for (int i = 1; i < NumbersToRemember.Length; i++)
        {
            NumbersToRemember[i] = GetUnicValue(from, to, NumbersToRemember[i - 1]);
        }
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

        foreach (int i in NumbersToRemember)
        {
            text.text = i.ToString();
            yield return new WaitForSeconds(1.5f);
        }

        text.text = "";
        panel.SetActive(true);
    }

    private int GetUnicValue(int from, int to, int last)
    {
        int temp = Random.Range(from, to);
        if (temp == last)
        {
            return GetUnicValue(from, to, last);
        }
        return temp;
    }
}
