using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField] GameObject main;
    [SerializeField] GameObject origin;

    public void TogglePauseMenu()
    {
        main.SetActive(!main.activeSelf);
        Time.timeScale = main.activeSelf ? 0f : 1f;
    }

    public void OpenSubmenu(GameObject _menu)
    {
        origin.SetActive(false);
        _menu.SetActive(true);
    }

    public void BackToMain(GameObject _current)
    {
        _current.SetActive(false);
        origin.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
        print("App Quit, but not actually because this isn't a build.");
    }
}
