using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberInputScript : MonoBehaviour
{
    private int Input = 0;

    public void OnNumberButtonClicked(int i)
    {
        Input = Input * 10 + i;
        GameObject.FindObjectOfType<NumberFacade>().UpdateValue(Input);
    }

    public void OnDeleteButtonClicked()
    {
        Input /= 10;
        GameObject.FindObjectOfType<NumberFacade>().UpdateValue(Input);
    }

    public void OnEnterButtonClicked()
    {
        GameObject.FindObjectOfType<NumberFacade>().SetInput(Input);
        GameObject.FindObjectOfType<NumberFacade>().UpdateValue(0);
        Input = 0;
    }
}
