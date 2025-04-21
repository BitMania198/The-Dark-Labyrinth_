using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject LorePanel;
    public GameObject StartPanel;
    public GameObject HelpPanel;
    void Start()
    {
        StartPanel.SetActive(false);
        HelpPanel.SetActive(false);
    }

    public void StartPressed()
    {
        StartPanel.SetActive(true);
    }
    public void HelpPressed()
    {
        HelpPanel.SetActive(true);
    }
    public void QuitPressed()
    {
        Application.Quit();
    }
    public void HelpClosed()
    {
        HelpPanel.SetActive(false);
    }
    public void LoreClosed()
    {
        StartCoroutine("PaperAnimation");
    }
    public void PlayerOnePressed()
    {
        SceneManager.LoadScene("PickuUpTest");
    }
    public void PlayerTwoPressed()
    {
        //add Two player scene
        SceneManager.LoadScene("");
    }
    IEnumerator PaperAnimation()
    {
        LorePanel.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("PaperFade");
        yield return new WaitForSeconds(1);
        LorePanel.SetActive(false);
    }
}
