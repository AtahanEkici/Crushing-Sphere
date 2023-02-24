using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI Score_text;
    public TextMeshProUGUI GameOver_Score_text;
    public TextMeshProUGUI GameOver_text;
    public TextMeshProUGUI Max_Score_text;

    public PlayerController player;
    public Button Try_Again_Button;

    private Material player_material;
    private TextMeshProUGUI Button_text;
    private Color font_color;
    private int score;
    private bool isActive;
    private ColorBlock cb;
    private string currentScoreString;
    private static ScoreManager _instance;
    private int max_score;
    private float font_size = 10f;

    void OnDisable()
    {
        PlayerPrefs.SetInt("max_score", max_score); // On game close write the new data to the database //
    }

    void OnEnable()
    {
        max_score = PlayerPrefs.GetInt("max_score"); // when the object is enabled get the highest score //
        Max_Score_text.SetText("H.SCORE: "+max_score+"");
    }

    public static ScoreManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
        isActive = false;
        player_material = player.GetComponent<Renderer>().material;
        Button_text = Try_Again_Button.GetComponentInChildren<TextMeshProUGUI>();
        cb = Try_Again_Button.colors;
        font_size = Score_text.fontSize;
    }
    void Start()
    {
        font_color = GetPlayerColor();
        AdjustTexts();
    }
    void Update()
    {
        IncrementAnimation(Score_text);
        CheckColor();
    }
    private Color GetPlayerColor()
    {
        if (player != null)
        {
            return player_material.GetColor("_Color");
        }
        else
        {
            return Color.white;
        }    
    }
    private Color Color_Inverter(Color player_color)
    {
        return new Color((1 - player_color.r), (1 - player_color.g), (1 - player_color.b));
    }
    private void SetFontColor(Color color)
    {
        Score_text.color = color;
        Score_text.outlineColor = Color_Inverter(font_color);

        GameOver_Score_text.color = color;
        GameOver_Score_text.outlineColor = Color_Inverter(font_color);

        Max_Score_text.color = color;
        Max_Score_text.outlineColor = Color_Inverter(font_color);

        GameOver_text.color = color;
        GameOver_text.outlineColor = Color_Inverter(font_color);
    }
    private void CheckColor()
    {
        if(player != null && font_color != GetPlayerColor())
        {
            font_color = GetPlayerColor();
            SetFontColor(font_color);
        }
    }
    private void AdjustTexts()
    {
        cb.normalColor = font_color;
        Try_Again_Button.image.color = font_color;
        Button_text.color = Color_Inverter(font_color);
    }
    public void IncrementScore(int value) // too messy code  need to update it once i understood the textmesh pro better//
    {
        score += value;

        if (score > max_score)
        {
            max_score = score;
            Max_Score_text.SetText("H.SCORE: " + max_score + "");
        }
        
        currentScoreString = "SCORE: " + score.ToString();

        Score_text.SetText(currentScoreString);
        GameOver_Score_text.SetText(currentScoreString);

        AdjustTexts();

        isActive = true;
    }
    private void IncrementAnimation(TextMeshProUGUI scoretext)
    {
        float speed = 300f;

        if (isActive == true)
        {
            scoretext.fontSize += speed * Time.deltaTime;

            if (scoretext.fontSize >= font_size*2)
            {
                isActive = false;
            }
        }
        else if (isActive == false && scoretext.fontSize >= font_size)
        {
            scoretext.fontSize -= speed * Time.deltaTime;
        }
    }
}
