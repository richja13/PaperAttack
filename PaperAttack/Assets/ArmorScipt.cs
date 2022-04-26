
using UnityEngine;

public class ArmorScipt : MonoBehaviour
{
    private GameObject Player;
    
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.transform.position;
        transform.rotation = Player.transform.rotation;
    }
}
