using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonColorer : MonoBehaviour
{
    [SerializeField] private Image imaj;
    [SerializeField] TextMeshProUGUI textmeshPro;

    private Color Color_Inverter(Color player_color)
    {
        return new Color((1 - player_color.r), (1 - player_color.g), (1 - player_color.b), 1);
    }

    private void Awake()
    {
        imaj = GetComponent<Image>();
    }

    private void Start()
    {
        Color Selected_Color = Random.ColorHSV();
        imaj.color = Selected_Color;
        textmeshPro.color = Color_Inverter(Selected_Color);
    }
}
