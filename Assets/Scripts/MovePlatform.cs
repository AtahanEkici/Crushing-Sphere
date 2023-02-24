using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public bool direction = true;

    private void Update()
    {
        Movement(0.2f, Time.deltaTime,0.01f);
    }

    private void Movement(float a, float timing, float position)
    {
        if (direction == true)
        {
            transform.position -= new Vector3(timing * a, 0);
        }
        else
        {
            transform.position += new Vector3(timing * a, 0);
        }

        if (transform.position.x >= position)
        {
            direction = true;
        }
        else
        {
            direction = false;
        }
    }
}