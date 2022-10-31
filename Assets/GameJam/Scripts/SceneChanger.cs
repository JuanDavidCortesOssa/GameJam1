using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadMainMenuScene()
    {
        ChangeScene("UIScene");
    }

    public void LoadLevelScene()
    {
        ChangeScene("Level1");
    }

    public void LoadWin()
    {
        ChangeScene("Win");
    }

    public void LoadTutorialScene()
    {
        ChangeScene("TutorialScene");
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
