
using UnityEngine;
using UnityEngine.Audio;

public class PlaneScript : MonoBehaviour
{
    private Renderer[] renderers;
    private GameObject Player;
    private bool isWrappingX;
    private bool isWrappingY;
    public Rigidbody2D rb;
    public int Hp;
    private AudioSource audioSource;
    public ParticleSystem Boom;
    public bool invincible;
    [SerializeField] private Vector2 Direction;
    private bool AddScore;

    // Start is called before the first frame update
    void Start()
    {
        AddScore = false;
        audioSource = GameObject.Find("MusicManagerPlaneBoom").GetComponent<AudioSource>();
        Hp = 8;
        Player = GameObject.Find("Player");
        renderers = GetComponentsInChildren<Renderer>();
        transform.position += new Vector3(0f,0f,1f);
    }

   
    // Update is called once per frame
    void Update()
    {
        if (AddScore == true)
        {
            Player.GetComponent<PlayerMovementScript>().Score += 8;
        }

        Invoke("ChangeColor", 0.2f);
        
        rb.AddForce(Direction * Time.deltaTime * 1.2f);
        
        ScreenWrap();

        if (Hp <= 0)
        {
            GameObject.Find("Text").GetComponent<ScoreScript>().StartShake = true;
            audioSource.Play();
            AddScore = true;
            GetComponent<SpriteRenderer>().color = Color.clear;
            Destroy(this.gameObject ,0.7f);
            GetComponent<PolygonCollider2D>().enabled = false;
            Boom.Play();
        }
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

        if (newPosition.x > 1 || newPosition.x < 0)
        {
            newPosition.x = -newPosition.x;
            isWrappingX = true;
        }

        if (newPosition.y > 1 || newPosition.y < 0)
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

    public void ChangeColor()
    {
        if (GetComponent<SpriteRenderer>().color == Color.red)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Immortal")
        {
            invincible = true;
        }
        else
        {
            invincible = false;
        }
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
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
        
        if (other.gameObject.tag == "Bullet")
        {
            Destroy(other.gameObject);
            Hp -= 1;
            GetComponent<SpriteRenderer>().color = Color.red;
            
        }
        
    }
}
