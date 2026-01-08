using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{

    [Header("References")]
    [SerializeField] public TextMeshProUGUI versionTracker;
    [SerializeField] public SoundType clickInteresting;


    // Start is called before the first frame update
    private void Start()
    {
        versionTracker.text = GlobalValues.main.gameVersion;
    }

    public void Quit()
    {
        SoundManager.main.PlaySound(clickInteresting);
        Application.Quit();
    }

    public void Play()
    {
        SoundManager.main.PlaySound(clickInteresting);
        SceneManager.LoadScene("Global Map", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(gameObject.scene);
    }

    public void Options()
    {
        SoundManager.main.PlaySound(clickInteresting);
    }

    public void Tutorial()
    {
        SoundManager.main.PlaySound(clickInteresting);
    }
}
