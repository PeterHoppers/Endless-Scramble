using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuSceneButton : MonoBehaviour {

    public string sceneName;

    public void GoToScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
