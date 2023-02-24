using UnityEngine;

public class ButtonResizer : MonoBehaviour
{
    private bool Expend = true;

    [SerializeField] private float Animation_Speed = 200f;
    [SerializeField] private Vector3 scale;
    [SerializeField] private Vector3 rate = new Vector3(0.01f, 0.01f, 0.01f);
    [SerializeField] private float MAX_Scale = 2.5f;
    [SerializeField] private float MIN_Scale = 1.3f;

    private void Start()
    {
        scale = gameObject.transform.localScale;
    }
    private void Update()
    {
            ScaleAnimation(Time.unscaledDeltaTime);
    }
    private void ScaleAnimation(float a)
    {
        if(Expend == true)
        {
            scale = gameObject.transform.localScale += rate * a * Animation_Speed;

            float X = scale.x;

            if (X >= MAX_Scale)
            {
                Expend = false;
            }
        }
        else if(Expend == false)
        {
            scale = gameObject.transform.localScale -= rate * a * Animation_Speed;

            float X = scale.x;

            if (X <= MIN_Scale)
            {
                Expend = true;
            }
        }
    }
}