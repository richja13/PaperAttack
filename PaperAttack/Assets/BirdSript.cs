using System;
using System.Collections.Generic;
using System.Numerics;
//using System.Web.Configuratiom;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using Vector3 = UnityEngine.Vector3;
using Vector2 = UnityEngine.Vector2;
using EZCameraShake;
public class BirdSript : MonoBehaviour
{
    private GameObject Player;
    private bool CanDestroy;
    private float PlayerVelocity;
    public ParticleSystem BloodParticles;
    private Renderer[] renderers;
    private bool isWrappingX;
    private bool isWrappingY;
    public Rigidbody2D rb;
    [SerializeField] private Vector2 Direction;
    public AudioClip Slash;
    private AudioSource audioSorce;
    private int maxSpeed;
    public bool invincible;
    
    
     
    void Start()
    {
        invincible = false;
        maxSpeed = 2;
        Player = GameObject.Find("Player");
        renderers = GetComponentsInChildren<Renderer>();
        audioSorce = GameObject.Find("MusicManager").GetComponent<AudioSource>();
        transform.position += new Vector3(0f,0f,1f);
    }
    
    void Update()
    {

        if (rb.velocity.magnitude <= maxSpeed )
        {
            rb.AddForce(Direction * Time.deltaTime * 5.5f);
        }
        
        //Debug.Log(rb.velocity.magnitude);

        ScreenWrap();
        
        //Gizmos.DrawWireCube(transform.position,new Vector2(10,10));
        
        PlayerVelocity = Player.GetComponent<Rigidbody2D>().velocity.magnitude;
        
        //transform.Translate(Vector2.up);
        
        if (PlayerVelocity >= 1.32f || Player.GetComponent<PlayerMovementScript>().HaveNoseArmor == true || Player.GetComponent<PlayerMovementScript>().DashPlayed == true)
        {
            CanDestroy = true;
        }
        else
        {
            CanDestroy = false;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Immortal")
        {
            invincible = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "Immortal")
        {
            invincible = true;
        }
        
        
       /*
        if (other.gameObject.name == "Player" && !invincible)
        {
            if (Player.GetComponent<PlayerMovementScript>().HaveArmor == true)
            {
                Player.GetComponent<PlayerMovementScript>().ArmorHp--;
                if (Player.GetComponent<PlayerMovementScript>().ArmorHp <= 0)
                {
                    Player.GetComponent<PlayerMovementScript>().Dead = true;
                    other.GetComponent<SpriteRenderer>().color = Color.clear;
                }
            }
            else
            {
                Player.GetComponent<PlayerMovementScript>().Dead = true;
                other.GetComponent<SpriteRenderer>().color = Color.clear;
            }
        }
       */
     
        if ((other.gameObject.tag == "Nose" && CanDestroy == true ) || other.gameObject.tag == "Bullet" )
        {
            Debug.Log("KOLIZJA");
            GameObject.Find("Text").GetComponent<ScoreScript>().StartShake = true;
            Player.GetComponent<PlayerMovementScript>().Score += 10;
            GetComponent<SpriteRenderer>().color = Color.clear;
            Destroy(this.gameObject ,0.2f);
            GetComponent<PolygonCollider2D>().enabled = false;
            BloodParticles.Play();
            audioSorce.Play();
            CameraShaker.Instance.ShakeOnce(1f, 2f, 0.1f, 0.2f);
        }
        else if(other.gameObject.tag == "Nose")
        {
            Debug.Log("FINAL SOLUTION");
        }
    }

    
    public void OnCollisionEnter2D(Collision2D other)
    {
        
    }
    
    void ScreenWrap()
    {
        
        bool isVisible = CheckRenderers();

        if (isVisible)
        {
            isWrappingX = false;
            isWrappingY = false;
            return;
        }

        if (isWrappingX && isWrappingY)
        {
            return;
        }

        Vector3 newPosition = transform.position;
        
        if (newPosition.x > 0.1f || newPosition.x < 0)
        {
            newPosition.x = -newPosition.x;
            isWrappingX = true;
        }

        if (newPosition.y > 0.1f || newPosition.y < 0)
        {
            newPosition.y = -newPosition.y;
            isWrappingY = true;
        }

        transform.position = newPosition;

    }
    
    bool CheckRenderers()
    {
        foreach (Renderer renderer in renderers)
        {
            if (renderer.isVisible)
            {
                return true;
            }
        }

        return false;
    }
}
