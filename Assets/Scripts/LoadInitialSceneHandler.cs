using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadInitialSceneHandler : MonoBehaviour
{
    public void Go()
    {
        SceneManager.LoadScene("InitialScene");
    }
}