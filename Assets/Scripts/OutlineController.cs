
using System.Collections;
using UnityEngine;

public class OutlineController : MonoBehaviour
{
    public CameraShake camshake;
    public ParticleController particles;
    public GameManager gm;

    public static Color outline_Color;
    public static Color default_color = Color.black;
    public static Color targetColor = Color.red;

    private static bool isExited = true;
    private static OutlineController _instance;
    private static Material material;

    private static OutlineController Instance
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
        material = GetComponent<Renderer>().material;
    }

    private static void setOutlineColor(Color desired_color)
    {
        material.SetColor("_OutlineColor", desired_color);
    }
    private static Color getOutlineColor()
    {
       return material.GetColor("_OutlineColor");
    }

    private void Start()
    {
        outline_Color = getOutlineColor();
    }

    private void OnCollisionStay(Collision other_object)
    {
        if(other_object.gameObject.CompareTag("Wall"))
        {
            isExited = false;
            outline_Color = Color.Lerp(getOutlineColor(),Color.red,0.3f);
            setOutlineColor(outline_Color);

            if (outline_Color == targetColor)
            {
                camshake.InduceStress(5, 8, 1f);
                particles.PlayerDestroyed(transform.position);
                gm.GameOver();
            } 
        }
    }

    private void OnCollisionExit(Collision other_object)
    {
        if (other_object.gameObject.CompareTag("Wall"))
        {
            isExited = true;
        }
    }
    private void Update()
    {
        if(isExited == true && getOutlineColor() != default_color)
        {
            outline_Color = Color.Lerp(getOutlineColor(), default_color, 1f * Time.deltaTime);
            setOutlineColor(outline_Color);
        }
    }
}


