using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Color selected_Color;
    public Color outline_Color;
    public float transition_time;

    public GameManager gm;
    public ParticleController particles;
    public CameraShake camshake;
    public ScoreManager score;
    public ParticleSystem flames;

    private Renderer Sphere_Renderer;
    private Transform flames_transform;
    private static PlayerController _instance;
    public static PlayerController Instance
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

        Sphere_Renderer = GetComponent<Renderer>();
        flames_transform = gameObject.GetComponentInChildren<ParticleSystem>().transform;
    }

    private void Start()
    {
        ChangeColor(Color_Randomizer(), Sphere_Renderer);
    }

    public static Color Color_Randomizer()
    {
        float r = Random.Range(0.0f, 1.0f);
        float g = Random.Range(0.0f, 1.0f);
        float b = Random.Range(0.0f, 1.0f);
        return new Color(r,g,b,1); // Alpha value in rgba is always 1 //
    }
    private void ChangeColor(Color color, Renderer renderer)
    {
        if(renderer.gameObject != null)
        {
            renderer.material.SetColor("_Color", color);
        }
    }
    private void OnCollisionEnter(Collision other_object)
    {
        if (other_object.gameObject.CompareTag("Cube"))
        {
            flames_transform.parent = other_object.gameObject.transform;
            flames_transform.position = other_object.gameObject.transform.position;

            var flame_main = flames.main;
            flame_main.startLifetime = 0.5f;

            var flame_emission = flames.emission;
            flame_emission.rateOverTime = 500;

            camshake.InduceStress(5, 8, 1f);
            particles.PlayerDestroyed(transform.position);
            gm.GameOver();
        }

        else if (other_object.gameObject.CompareTag("Cube_target")) // Destroyable Cube //
        {
            Destroy(other_object.gameObject);
            camshake.InduceStress(2, 2, 1f);
            particles.ObjectDestroyed(other_object.transform.position);
            ChangeColor(Color_Randomizer(), Sphere_Renderer);
            score.IncrementScore(10);
        }
    }
}
