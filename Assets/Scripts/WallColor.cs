using UnityEngine;

public class WallColor : MonoBehaviour
{
    public Color temp;

    private Renderer ren_Left;
    private Renderer ren_right;
    private void Awake()
    {
        ren_Left = GetComponentsInChildren<Renderer>()[0];
        ren_right = GetComponentsInChildren<Renderer>()[1];
    }

    private void Start()
    {
        temp = Random.ColorHSV();
        ChangeColor(temp, ren_Left);
        ChangeColor(temp, ren_right);
    }

    private static void ChangeColor(Color color, Renderer renderer)
    {
        if (renderer.gameObject != null)
        {
            renderer.material.SetColor("_Color", color);
        }
    }
}
