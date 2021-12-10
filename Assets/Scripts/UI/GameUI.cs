using UnityEngine;
using UnityEngine.InputSystem;

public class GameUI : MonoBehaviour
{
    public bool isGamePaused = false;

    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject pauseUI;
    private GameObject pauseMenu;
    private GameObject optionsMenu;

    // Start is called before the first frame update
    private void Start()
    {
        pauseMenu = pauseUI.transform.Find("PauseMenu").gameObject;
        optionsMenu = pauseUI.transform.Find("OptionsMenu").gameObject;
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        isGamePaused = !isGamePaused;
        hud.SetActive(!hud.activeSelf);
        pauseUI.SetActive(!pauseUI.activeSelf);
        if (isGamePaused)
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            optionsMenu.SetActive(false);
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
