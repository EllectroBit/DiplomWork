using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelSwitcher : MonoBehaviour
{
    public void OnNumbersClicked()
    {
        SceneManager.LoadScene("Numbers");
    }
    public void OnWordsClicked()
    {
        SceneManager.LoadScene("Words");
    }
    public void OnSequencesClicked()
    {
        SceneManager.LoadScene("Sequences");
    }
    public void OnExitClicked()
    {
        SceneManager.LoadScene("Main");
    }
}
