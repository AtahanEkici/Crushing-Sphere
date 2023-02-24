using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public GameObject player;
    public ParticleSystem player_destroyed;
    public ParticleSystem object_destroyed;
    public ParticleSystem flames;

    private Material player_renderer_material;
    private static ParticleController _instance;

    public static ParticleController Instance
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
    
        player_renderer_material = player.GetComponent<Renderer>().material;
        flames.Pause();
    }
    private Color GetPlayerColor()
    {
        if(player != null)
        {
            return player_renderer_material.GetColor("_Color");
        }
        else
        {
            return Color.white;
        }
    }
    private void ChangeColor(Color color, Renderer renderer)
    {
        if (renderer.gameObject != null)
        {
            renderer.sharedMaterial.SetColor("_Color", color);
        }
    }
    public void ObjectDestroyed(Vector3 vector)
    {
        ChangeColor(GetPlayerColor(),object_destroyed.GetComponent<Renderer>());
        Destroy(Instantiate(object_destroyed, vector,Quaternion.identity), 10f);
    }
    public void PlayerDestroyed(Vector3 vector)
    {
        ChangeColor(GetPlayerColor(), player_destroyed.GetComponent<Renderer>());
        Destroy(Instantiate(player_destroyed, vector, Quaternion.identity), 10f);
    }
    public void FlameOn()
    {
        flames.Play();
    }
    public void FlameOff()
    {
        if(player != null)
        flames.Pause();
    }
}
