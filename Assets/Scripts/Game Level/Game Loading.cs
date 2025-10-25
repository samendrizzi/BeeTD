using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoading : MonoBehaviour
{
    void Awake()
    {
        if (SceneManager.sceneCount == 1)
        {
            SceneManager.LoadScene("Main Menu", LoadSceneMode.Additive);
        }
    }
}
