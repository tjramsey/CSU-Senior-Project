using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject MainMenu;

    [SerializeField]
    private GameObject OptionsMenu;

    [SerializeField]
    private GameObject LoadMenu;

    public void NewGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void returnOptions(){
        OptionsMenu.SetActive(false);
        MainMenu.SetActive(true);
    }
    public void goOptions(){
        MainMenu.SetActive(false);
        OptionsMenu.SetActive(true);
    }
    public void returnLoad(){
        LoadMenu.SetActive(false);
        MainMenu.SetActive(true);
    }
    public void goLoad(){
        LoadMenu.SetActive(true);
        MainMenu.SetActive(false);
    }
    public void quit(){
        Application.Quit();
    }
}
