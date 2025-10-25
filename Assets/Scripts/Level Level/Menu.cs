using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Animator anim;

    private bool isMenuOpen = false;

    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        anim.SetBool("MenuOpen", isMenuOpen);
    }

    public void CloseMenu()
    {
        isMenuOpen = false;
        anim.SetBool("MenuOpen", isMenuOpen);
    }

    public void OpenMenu()
    {
        isMenuOpen = true;
        anim.SetBool("MenuOpen", isMenuOpen);
    }

    private void OnGUI()
    {
        
    }

    public void SetSelected()
    {

    }
}
