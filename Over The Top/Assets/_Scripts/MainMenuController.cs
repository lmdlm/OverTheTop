using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//add this for scene management
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    //handle button events
    //start button, options button
    //use SceneManager object to load and unload
    //all methods are static
    //SceneLoaded, SceneUnloaded, SceneChange for custom logic
    //LoadSceneAsync to load a new scene
    //SceneManager is in UnityEngine.SceneManagement library

    //OnClick Event Handler
    public void Start_OnClick()
    {
        SceneManager.LoadSceneAsync("Level1");
    }

    public void Options_OnClick()
    {
        SceneManager.LoadSceneAsync("OptionsMenu", LoadSceneMode.Additive);
    }
}
