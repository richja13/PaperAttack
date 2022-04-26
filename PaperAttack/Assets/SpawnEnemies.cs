
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnEnemies : MonoBehaviour
{
    public Transform TopSpawner;
    public Transform BottomSpawner;
    public Transform LeftSpawner;
    public Transform RightSpawner;
    public GameObject Enemy;
    public GameObject HEnemy;
    public GameObject EnemyPlane;
    public GameObject Ballon;
    private float SpawnTimer = 3.5f;
    private bool CanSpawn = false;
    private int SpawnRandom;
    private int maxEnemies = 10;
    private float PlaneSpawnTimer = 60f;
    private bool CanSpanwPlane;
    private float BaloonSpawnTimer = 2f;
    private bool CanSpawnBaloon = false;
    private GameObject[] Birds;
    private GameObject[] Ballons;
    public int Stages;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        
    }

    
    // Update is called once per frame
    void Update()
    {
        Ballons = GameObject.FindGameObjectsWithTag("Ballon");

        Birds = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject[] Planes = GameObject.FindGameObjectsWithTag("Plane");
        
        if (!CanSpawnBaloon)
        {
            BaloonSpawnTimer -= Time.deltaTime;
            if (BaloonSpawnTimer <= 0 && Ballons.Length <= 10)
            {

                BaloonSpawnTimer = 2f;
                CanSpawnBaloon = true;
            }
        }

        if (CanSpawnBaloon)
        {
            CanSpawnBaloon = false;
            Quaternion newRotation1 = Quaternion.Euler(0, 0, TopSpawner.eulerAngles.z);
            Instantiate(Ballon, new Vector2(Random.Range(-2.1f, 2.1f), TopSpawner.position.y),
                newRotation1);
        }
        
        
        if (CanSpawn == false)
        {
            SpawnTimer -= Time.deltaTime;
            if (SpawnTimer <= 0)
            {
                if (Stages == 0)
                {
                    SpawnTimer = 6.2f;
                }
                else if (Stages == 1)
                {
                    SpawnTimer = 4.2f;
                }
                else if (Stages >= 2)
                {
                    SpawnTimer = 3.1f;
                }

                CanSpawn = true;
            }
        }

        if (CanSpanwPlane == false)
        {
            PlaneSpawnTimer -= Time.deltaTime;
            if (PlaneSpawnTimer <= 0 && Planes.Length <= 2)
            {
                if (Stages == 0)
                {
                    PlaneSpawnTimer = 80f;
                }
                else if (Stages == 1)
                {
                    PlaneSpawnTimer = 60f;
                }
                else if (Stages >= 2)
                {
                    PlaneSpawnTimer = 40f;
                }
                CanSpanwPlane = true;
            }
        }

        if (CanSpanwPlane == true)
        {
            CanSpanwPlane = false;
            Instantiate(EnemyPlane, new Vector2(RightSpawner.position.x,Random.Range(-2f,1f)), RightSpawner.rotation);
        }
        
        
        if (CanSpawn == true && Birds.Length <= 6)
        {
            SpawnRandom = Random.Range(1, 2);

            CanSpawn = false;
            
            switch (SpawnRandom)
            {
                case 1:
                    Quaternion newRotation1 = Quaternion.Euler(0, 0, TopSpawner.eulerAngles.z);
                    Instantiate(Enemy, new Vector2(Random.Range(-0.9f, 0.9f), TopSpawner.position.y),
                        newRotation1);
                    break;

             case 2:
                    Quaternion newRotation2 = Quaternion.Euler(0, 0, BottomSpawner.eulerAngles.z);
                    Instantiate(Enemy, new Vector2(Random.Range(-2.1f, 2.1f), BottomSpawner.position.y),
                        newRotation2);
                    break;
                /*  
                    case 3:
                        Quaternion newRotation3 = Quaternion.Euler(0, 0, LeftSpawner.eulerAngles.z + 90);
                        Instantiate(HEnemy, new Vector2(LeftSpawner.position.x, Random.Range(-2.15f, 1.15f)),
                            newRotation3);
                        break
                            ;
               /* 
                    case 4:
    
                        Quaternion newRotation4 = Quaternion.Euler(0, 0, RightSpawner.eulerAngles.z);
    
                        Instantiate(HEnemy, new Vector2(RightSpawner.position.x, Random.Range(-2.15f, 1.15f)),
                            newRotation4);
                        break
                            ; */
            }
        }

    }
}
