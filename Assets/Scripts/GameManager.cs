using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private Canvas gameOver_Canvas;
    [SerializeField] private Canvas Overlay_Canvas;
    [SerializeField] private Canvas Play_Canvas;

    private AsyncOperation asyncLoadLevel;
    private static GameManager _instance;

    private static GameManager Instance
    {
        get { return _instance; }
    }
    void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;

        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    void Start()
    {
        Application.targetFrameRate = int.Parse(Screen.currentResolution.refreshRateRatio.ToString());

        if(PlayerPrefs.GetInt("Play_Button") == 1)
        {
            Time.timeScale = 0;
            Play_Canvas.gameObject.SetActive(true);
            gameOver_Canvas.gameObject.SetActive(false);
            Overlay_Canvas.gameObject.SetActive(false);
            PlayerPrefs.SetInt("Play_Button", 0);
        }
        else
        {
            Overlay_Canvas.gameObject.SetActive(true);
            gameOver_Canvas.gameObject.SetActive(false);
            Play_Canvas.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public static void PravicyPolicy()
    {
        Application.OpenURL("https://sites.google.com/view/crushing-sphere-pravicy-policy/ana-sayfa");
    }
    public void Start_Game()
    {
        Time.timeScale = 1;
        Play_Canvas.gameObject.SetActive(false);
        Overlay_Canvas.gameObject.SetActive(true);
    }
    private void CheckPosition(float distance)
    {
        if (player != null)
        {
            if (player.transform.position.y <= distance)
            {
                GameOver();
            }
        }
    }
    public static void Quit_Game()
    {
        Application.Quit();
    }
    public void GameOver()
    {
        player.gameObject.SetActive(false);
        gameOver_Canvas.gameObject.SetActive(true);
        Overlay_Canvas.gameObject.SetActive(false);
    }
    public void POR() // Pause Or Resume Function //
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Pause_Or_Resume();
        }
    }
    private void Pause_Or_Resume()
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        } 
    }
    private IEnumerator Load_Level()
    {
        asyncLoadLevel = SceneManager.LoadSceneAsync("1", LoadSceneMode.Single);

        while (!asyncLoadLevel.isDone)
        {
            yield return null;
        }
    }
    public static void V_Sync()
    {
        if(QualitySettings.vSyncCount == 1)
        {
            QualitySettings.vSyncCount = 0;
        }
        else
        {
            QualitySettings.vSyncCount = 1;
        }
    }
    public void Force_Frame_Rate(int given_frame_rate)
    {
        Application.targetFrameRate = given_frame_rate;
    }
    public void Restart_The_Game()
    {
        StartCoroutine(Load_Level());
    }
}
