using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playButton()
    {
        SceneManager.LoadScene("31_KohJhinKiat_project");
    }

    public void restartButton()
    {
        SceneManager.LoadScene("31_KohJhinKiat_project");
    }

    public void instructionsButton()
    {
        SceneManager.LoadScene("InstructionsScene");
    }

    public void creditsButton()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    public void menuButton()
    {
        SceneManager.LoadScene("MenuScene");
    }

}
