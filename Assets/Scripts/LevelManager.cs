using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject Cube_Prefab_1;
    public GameObject Cube_Prefab_2;
    public GameObject Wall_Prefab_1;

    public bool DontSpawnCubes = false;

    public Transform player;

    public List<GameObject> Cube_Prefabs = new List<GameObject>();
    public List<GameObject> Wall_Prefabs = new List<GameObject>();

    public float distanceBeforeSpawnWall;
    public float distanceBeforeSpawnBlock;

    public int initial_Walls = 4;
    public int initial_Blocks = 4;

    private static LevelManager _instance;

    private float currentWallY;
    private float currentBlockY;

    private Vector3 BlockVector = Vector3.zero;
    private Vector3 WallVector = Vector3.zero;
    private Vector3 Player_Position;
    private Vector3 WallVector_factor = new Vector3(0, 20f, 0);

    public static LevelManager Instance
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
    }
    private void Start()
    {
        if(DontSpawnCubes == true)
        {
            InitializeOnlyWall();
        }
        else
        {
            Initialize();
        }
    }
    private void Update()
    {
        Player_Position = player.position;

        if(DontSpawnCubes == true)
        {
            GenerateOnlyWall();
        }
        else
        {
            Generatelevel();
        }
    }
    private void Initialize()
    {
        for(int i = 0; i < initial_Walls; i++)
        {
            GenerateWallInitial(WallVector);
        }
        for (int j = 0; j < initial_Walls; j++)
        {
            GeneratePlatformInitial(BlockVector);
        }
    }

    private void InitializeOnlyWall()
    {
        for (int i = 0; i < initial_Walls; i++)
        {
            GenerateWallInitial(WallVector);
        }
    }
    private void GenerateWallInitial(Vector3 vector)
    {
        Wall_Prefabs.Add(Instantiate(Wall_Prefab_1, vector, Quaternion.identity));
        currentWallY += 20f;
        WallVector -= WallVector_factor;
    }
    private void GeneratePlatformInitial(Vector3 vector)
    {
        float choice = Random.Range(0f, 100f);

        if (choice >= 50f)
        {
            Cube_Prefabs.Add(Instantiate(Cube_Prefab_1, vector, Quaternion.identity));
            currentBlockY += distanceBeforeSpawnBlock;
            BlockVector -= new Vector3(0, distanceBeforeSpawnBlock, 0);
        }
        else
        {
            Cube_Prefabs.Add(Instantiate(Cube_Prefab_2, vector, Quaternion.identity));
            currentBlockY += distanceBeforeSpawnBlock;
            BlockVector -= new Vector3(0, distanceBeforeSpawnBlock, 0);
        }
    }
    private void GenerateWall(Vector3 vector)
    {
        Wall_Prefabs.Add(Instantiate(Wall_Prefab_1, vector, Quaternion.identity));
        currentWallY += 20f;
        WallVector -= WallVector_factor;
        Destroy(Wall_Prefabs[0]);
        Wall_Prefabs.RemoveAt(0);
    }
    private void GeneratePlatform(Vector3 vector)
    {
        float choice = Random.Range(0f,100f);

        if (choice >= 50f)
        {
            Cube_Prefabs.Add(Instantiate(Cube_Prefab_1, vector, Quaternion.identity));
            currentBlockY += distanceBeforeSpawnBlock;
            BlockVector -= new Vector3(0, distanceBeforeSpawnBlock, 0);
        }
        else
        {
            Cube_Prefabs.Add(Instantiate(Cube_Prefab_2, vector, Quaternion.identity));
            currentBlockY += distanceBeforeSpawnBlock;
            BlockVector -= new Vector3(0, distanceBeforeSpawnBlock, 0);
        }

        Destroy(Cube_Prefabs[0]);
        Cube_Prefabs.RemoveAt(0);
    }
    private void Generatelevel()
    {
        if (Player_Position.y + currentWallY < distanceBeforeSpawnWall)
        {
            GenerateWall(WallVector);
        }

        if (Player_Position.y + currentBlockY < distanceBeforeSpawnBlock)
        {
            GeneratePlatform(BlockVector);
        }
    }

    private void GenerateOnlyWall()
    {
        if (Player_Position.y + currentWallY < distanceBeforeSpawnWall)
        {
            GenerateWall(WallVector);
        }
    }
}
