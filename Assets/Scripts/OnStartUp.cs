using UnityEngine;
using UnityEngine.SceneManagement;

public class OnStartUp : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetInt("Play_Button", 1);
        SceneManager.LoadSceneAsync("1", LoadSceneMode.Single);
    }
}