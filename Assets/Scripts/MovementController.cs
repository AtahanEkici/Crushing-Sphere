using System;
using UnityEngine;
public class MovementController : MonoBehaviour
{
    public float speed = 15f;
    public float maxvelocity = 50f;
    public Camera cam;
    public ParticleController Particle_Controller;

    private Rigidbody rb;
    private Vector3 tempVect;
    private Vector3 bounce;
    private float mp;
    private float screen_width;
    private static MovementController _instance;

    public MovementController Instance
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
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }
    private void FixedUpdate()
    {
        LimitVelocity();
        Move_With_Mouse();
    }
    private void Update()
    {
        ParticleControl();
    }
    private void ParticleControl()
    {
        float velocity = Math.Abs(rb.velocity.y);

        if(velocity >= 50f)
        {
            Particle_Controller.FlameOn();
        }
        else if(velocity < 50f)
        {
            Particle_Controller.FlameOff();
        }
    }
    private void LimitVelocity()
    {
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxvelocity);

        if (rb.velocity.x != 0)
        {
            rb.velocity = new Vector3(0, rb.velocity.y);
        }
    }
    private void Move_With_Mouse()
    {
        mp = Input.mousePosition.x;
        screen_width = Screen.width;

        if (Input.GetMouseButton(0)) 
        {
            if (mp < (screen_width / 2) && transform.position.x >= -10.5f)
            {
                Movement(-1f);
            }
            else if (transform.position.x <= 9.3f)
            {
                Movement(1f);
            }
        }
    }
    private void Movement(float a)
    {
        tempVect = new Vector3(a, 0, 0);
        tempVect = speed * Time.fixedDeltaTime * tempVect.normalized;
        rb.MovePosition(transform.position + tempVect);
    }
    private void OnCollisionEnter(Collision other_object)
    {
         if (other_object.gameObject.CompareTag("Cube_target"))
        {
            float velocity = rb.velocity.y;

            if(velocity >= 70)
            {
                bounce = new Vector3(0, -(rb.velocity.y / 2),0);
                rb.AddForce(bounce, ForceMode.Impulse);
            }
            else if(velocity >= 60)
            {
                bounce = new Vector3(0, -(rb.velocity.y / 3),0);
                rb.AddForce(bounce, ForceMode.Impulse);
            }
            else if(velocity >= 50)
            {
                bounce = new Vector3(0, -(rb.velocity.y / 4),0);
                rb.AddForce(bounce, ForceMode.Impulse);
            }
            else if(velocity >= 40)
            {
                bounce = new Vector3(0, -(rb.velocity.y / 5),0);
                rb.AddForce(bounce, ForceMode.Impulse);
            }
            else if(velocity >= 30)
            {
                bounce = new Vector3(0, -(rb.velocity.y / 6),0);
                rb.AddForce(bounce, ForceMode.Impulse);
            }
            else if(velocity >= 20)
            {
                bounce = new Vector3(0, -(rb.velocity.y / 7),0);
                rb.AddForce(bounce, ForceMode.Impulse);
            }
            else
            {
                bounce = new Vector3(0, -(rb.velocity.y / 8),0);
                rb.AddForce(bounce, ForceMode.Impulse);
            }       
        }
    }
}
