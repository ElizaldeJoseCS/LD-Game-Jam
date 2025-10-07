using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuReturn : MonoBehaviour
{
        public void LoadMainMenu()
    {
        SceneManager.LoadScene(0); // or use the build index instead of the name
    }

}
