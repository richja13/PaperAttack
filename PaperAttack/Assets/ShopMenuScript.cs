
using UnityEngine;

using UnityEngine.Audio;
public class ShopMenuScript : MonoBehaviour
{
    public bool ShopOpen;
    private bool CanBuyGun;
    private bool CanBuyArmor;
    private bool CanBuyNoseArmor;
    public bool HaveGun = false;
    public bool HaveArmor = false;
    public bool HaveNoseArmor = false;
    private GameObject Player;
    private float PlayerScore;
    private AudioSource audioSorce1;
    private AudioSource audioSorce2;
    private RectTransform recttransform;
    private Vector2 startPos;
    public GameObject SpawnEnemies;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        audioSorce1 = GameObject.Find("MusicManagerClick").GetComponent<AudioSource>();
        audioSorce2 = GameObject.Find("MusicManagerUpgrade").GetComponent<AudioSource>();
        recttransform = this.gameObject.GetComponent<RectTransform>();
        startPos = recttransform.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(ShopOpen);

  
        if (ShopOpen)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1;
        }
        
        
       PlayerScore = Player.GetComponent<PlayerMovementScript>().Score;

       if (PlayerScore >= 40 && !HaveNoseArmor)
       {
           CanBuyNoseArmor = true;
       }
       else
       {
           CanBuyNoseArmor = false;
       }
       
       if (PlayerScore >= 180 && !HaveGun)
       {
           CanBuyGun = true;
       }
       else
       {
           CanBuyGun = false;
       }
       
       if (PlayerScore >= 300 && !HaveArmor)
       {
           CanBuyArmor = true;
       }
       else
       {
           CanBuyArmor = false;
       }
       
    }

    public void BuyNoseArmor()
    {
        if (CanBuyNoseArmor)
        {
            SpawnEnemies.GetComponent<SpawnEnemies>().Stages++;
            audioSorce2.Play();
            HaveNoseArmor = true;
            Player.GetComponent<PlayerMovementScript>().Score -= 40;
        }
    }
    
    
    public void BuyArmor()
    {
        if (CanBuyArmor)
        {
            SpawnEnemies.GetComponent<SpawnEnemies>().Stages++;
            audioSorce2.Play();
            HaveArmor = true;
            Player.GetComponent<PlayerMovementScript>().Score -= 300;
        }
    }

    public void BuyGuns()
    {
        if (CanBuyGun)
        {
            SpawnEnemies.GetComponent<SpawnEnemies>().Stages++;
            audioSorce2.Play();
            HaveGun = true;
            Player.GetComponent<PlayerMovementScript>().Score -= 180;
        }
    }



    public void BuyAmmo()
    {
        audioSorce2.Play();
        if (Player.GetComponent<PlayerMovementScript>().Score >= 35)
        {
            Player.GetComponent<PlayerMovementScript>().Score -= 35;
            Player.GetComponent<PlayerMovementScript>().Ammo += 10;
        }
    }
    
    
    public void OpenShop()
    {
        audioSorce1.Play();
        if (ShopOpen == false)
        {
            ShopOpen = true;
            recttransform.anchoredPosition = new Vector2(0, 0);
        }
        else if(ShopOpen == true)
        {
            ShopOpen = false;
            recttransform.anchoredPosition = startPos;
        }
    }
    
}
