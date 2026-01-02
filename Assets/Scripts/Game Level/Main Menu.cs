using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{

    [Header("References")]
    [SerializeField] public TextMeshProUGUI versionTracker;

    // Start is called before the first frame update
    private void Start()
    {
        versionTracker.text = GlobalValues.main.gameVersion;
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
