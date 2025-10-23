
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour
{
    public void LoadScenes(string cena)
    {
        SceneManager.LoadScene(cena);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
