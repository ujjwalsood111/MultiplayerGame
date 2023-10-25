using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneMan : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playing()
    {
        SceneManager.LoadScene("Environment_Free");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void restart()
    {
        SceneManager.LoadScene("Environment_Free");
    }

    public void menu()
    {
        SceneManager.LoadScene("Start");
    }

    public void contr()
    {
        SceneManager.LoadScene("Controllingsss");
    }
}
