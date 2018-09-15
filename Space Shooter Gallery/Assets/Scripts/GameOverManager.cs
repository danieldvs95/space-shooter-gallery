using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {

    [SerializeField]
    Button retryButton;

    [SerializeField]
    Button quitButton;

    LoaderController loaderController;

	void Start () {
        if (retryButton != null)
        {
            retryButton.onClick.AddListener(ReloadLevel);
        }
        if (quitButton != null) {
            quitButton.onClick.AddListener(QuitGame);
        }
        loaderController = FindObjectOfType<LoaderController>();
	}
	
    void QuitGame()
    {
        if (loaderController != null) 
        {
            loaderController.FadeOut();
        }
        Invoke("GoToMenu", 1);
    }

    void ReloadLevel()
    {
        if (loaderController != null)
        {
            loaderController.FadeOut();
        }
        Invoke("LoadLevel", 1);
    }

    void GoToMenu () {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    void LoadLevel () {
        SceneManager.LoadScene("FirstLevel", LoadSceneMode.Single);
    }
}
