using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScreen1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        reloadMM();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(0);
        }
    }

    IEnumerator reloadMM()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(0);
    }
}
