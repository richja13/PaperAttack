
using UnityEngine;

public class BallonScript : MonoBehaviour
{
    public Rigidbody2D rb;
    private Renderer[] renderers;
    private bool isWrappingX;
    private bool isWrappingY;
    private GameObject Player;
    

    public ParticleSystem Particles;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        renderers = GetComponentsInChildren<Renderer>();
        transform.position += new Vector3(0f,0f,1f);
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(Vector2.down * Time.deltaTime * 0.7f);
        ScreenWrap();
    }

 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            GameObject.Find("Text").GetComponent<ScoreScript>().StartShake = true;
            Particles.Play();
            GetComponent<SpriteRenderer>().color = Color.clear;
            GetComponent<CircleCollider2D>().enabled = false;
            Destroy(this.gameObject, 0.2f);
            //Player.GetComponent<PlayerMovementScript>().Score += 5;
        }
        
        
        if (other.gameObject.name == "Player")
        {
            GameObject.Find("Text").GetComponent<ScoreScript>().StartShake = true;
            Particles.Play();
            GetComponent<SpriteRenderer>().color = Color.clear;
            GetComponent<CircleCollider2D>().enabled = false;
            Destroy(this.gameObject, 0.2f);
            //Player.GetComponent<PlayerMovementScript>().Score += 5;
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
}
