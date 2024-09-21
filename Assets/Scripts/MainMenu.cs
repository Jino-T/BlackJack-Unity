using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() {
        //load poker game
        SceneManager.LoadSceneAsync(1);
    }
    public void ReturnToMenu() {
        //load menu
        SceneManager.LoadSceneAsync(0);
    }
}
