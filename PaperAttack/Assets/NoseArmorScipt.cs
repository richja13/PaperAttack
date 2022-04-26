
using UnityEngine;

public class NoseArmorScipt : MonoBehaviour
{
    private GameObject Player;
    private GameObject Shop;
    
    void Start()
    {
        Shop = GameObject.Find("Shop");
        Player = GameObject.Find("Player");
    }
    
    void Update()
    {
        if (Shop.GetComponent<ShopMenuScript>().HaveNoseArmor == true)
        {
            transform.position = Player.transform.position;
            transform.rotation = Player.transform.rotation;
        }
    }
}
