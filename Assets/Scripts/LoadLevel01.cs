using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel01 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadScene()
    {
        if (GameObject.Find("[ViveInputUtility]")) Destroy(GameObject.Find("[ViveInputUtility]"));
        SceneManager.LoadScene(1);
    }

    public void loadCredits()
    {
        SceneManager.LoadScene(3);
    }

    public void endGame()
    {
        Application.Quit();
    }

    public void goBack()
    {
        SceneManager.LoadScene(0);
    }
}
