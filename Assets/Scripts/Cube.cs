using UnityEngine;
using System.Collections.Generic;

public class Cube : MonoBehaviour
{
    public Color color;
    public PhysicsMaterial targetmaterial;

    private PlayerController player;
    private int ChildCount;
    private int Random_cube_number;
    private List<Renderer> References = new List<Renderer>();
    private GameObject Chosen_One;

    readonly private new string tag = "Cube_target";
        private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>(); // Catch reference from game environment //

        ChildCount = transform.childCount; // Get the Child Count from the Prefab //

        for (int i = 0; i < ChildCount; i++) // Collect children's references inside the loop to a list //
        {
            References.Add(transform.GetChild(i).GetComponent<Renderer>()); // Store the references inside the list //
        }
    }
    void Start()
    {
        Setup();
    }
    private Color Color_Inverter(Color player_color)
    {
        return new Color((1 - player_color.r), (1 - player_color.g),(1 - player_color.b),1);
    }
    void Update()
    {
        if (player != null)
        {
            Check_Color();
        }
    }
    private Color GetPlayerColor()
    {
        return player.GetComponent<Renderer>().material.GetColor("_Color");
    }
    private void Check_Color()
    {
        Color col = GetPlayerColor();

        if (col != color)
        {
            color = col;

            if (Chosen_One != null && References[Random_cube_number].material.color != color)
            {
                ChangeColor(color, References[Random_cube_number]);
            }

            CheckOthers(col);
        }
    }
    private void ChangeColor(Color color, Renderer renderer)
    {
        if (renderer.gameObject != null)
        {
            renderer.material.SetColor("_Color", color);
        }
    }

    private void CheckOthers(Color player_color)
    {
        Color temp = Color_Inverter(player_color);

        for (int i = 0; i < ChildCount; i++) // Collect children's references inside the loop to a list //
        {
            if(i != Random_cube_number)
            {
                ChangeColor(temp, References[i]);
            }
        }
    }

    private void Setup()
    {
        Random_cube_number = Random.Range(0, ChildCount); // Pick a random cube from the preFab //
        ChangeColor(color, References[Random_cube_number]); // Change Color of the chosen cube //
        References[Random_cube_number].tag = tag; // change its tag also //
        Chosen_One = References[Random_cube_number].gameObject; // pick up the chosen one's reference //
        Collider col = Chosen_One.GetComponent<Collider>();
        col.material = targetmaterial;
        CheckOthers(GetPlayerColor());
    }
}
