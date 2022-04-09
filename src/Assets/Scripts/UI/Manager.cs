using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;


public class Manager : MonoBehaviour
{
    private void Resume(){
        UiManagerScript.MyInstance.OpenCloseGameMenu();

    }
    // public void Save(){

    //     //inventory.Save();
    //     //Equipment.Save();
    // }
    // public void Load(){
    //     ///inventory.Load();
    //     //Equipment.Load();
    // }
    public void goOptions(){
        UiManagerScript.MyInstance.OpenCloseOptions();

    }
    public void mainMenu(){
        //inventory.Clear();
        //Equipment.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + -2);

    }
}
