using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{
    public void LoadScene(string sceneName = "Main Menu") {
        SceneManager.LoadScene(sceneName);
    }

    // overloading methods
    public void LoadScene(int buildIndex = 0) {
        SceneManager.LoadScene(buildIndex);
    }
    public void RestartScene() {
        // Get the build index of the current scene and reload it
        int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneBuildIndex);
    }

    public void QuitGame () {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
