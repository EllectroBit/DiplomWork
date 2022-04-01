using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class DefeatScript : MonoBehaviour
{
    public delegate void ResetAction();
    public event ResetAction OnResetAction;

    public void OnExitClicked()
    {
        SceneManager.LoadScene("Main");
    }

    public void Show()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void OnHeartClicked()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        OnResetAction?.Invoke();
    }
}
