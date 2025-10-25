using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Play()
    {
        SceneManager.LoadScene("Global Map", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }

    public void Options()
    {

    }



}
