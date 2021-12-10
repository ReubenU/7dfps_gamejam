using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
