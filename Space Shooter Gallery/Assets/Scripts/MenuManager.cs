using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [SerializeField]
    Button playButton;

    [SerializeField]
    Button quitButton;


    void Start()
    {
        if (playButton != null)
            playButton.onClick.AddListener(StartGame);
        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);
    }

    void StartGame()
    {
        SceneManager.LoadScene("FirstLevel", LoadSceneMode.Single);
    }

    void QuitGame()
    {
        Application.Quit();
    }

}
