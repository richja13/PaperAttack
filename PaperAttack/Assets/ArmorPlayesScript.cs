using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorPlayesScript : MonoBehaviour
{
    public GameObject Player;
    public GameObject Armor1;
    public GameObject Armor2;
    public GameObject Armor3;
    private RectTransform Position;
    private Vector2 StartPos;

    // Start is called before the first frame update
    void Start()
    {
        Position = GetComponent<RectTransform>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.GetComponent<PlayerMovementScript>().HaveArmor == true)
        {
            Position.anchoredPosition = new Vector2(0, 126);

            if (Player.GetComponent<PlayerMovementScript>().ArmorHp == 3)
            {
                Destroy(Armor1);
            }
            if (Player.GetComponent<PlayerMovementScript>().ArmorHp == 2)
            {
                Destroy(Armor2);
            }
            if (Player.GetComponent<PlayerMovementScript>().ArmorHp == 1)
            {
                Destroy(Armor3);
            }

        }
        else
        {
            Position.anchoredPosition = new Vector2(0, -400);
        }
    }
}
