
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;
using UnityEngine.Audio;

public class PlayerMovementScript : MonoBehaviour
{
    public float maxSpeed;
    public float accel;
    public float turnSpeed;
    private Renderer[] renderers;
    private bool isWrappingX;
    private bool isWrappingY;
    private float horizontal;
    private float vertical;
    private Rigidbody2D rb;
    public GameObject PlayerGhost;
    public Camera mainCamera;
    public int Score;
    public Text ScoreText;
    public GameObject Guns;
    public GameObject NoseArmor;
    public GameObject Shop;
    public bool HaveNoseArmor = false;
    public bool HaveGun = false;
    public bool HaveArmor;
    public GameObject Armor;
    public int Ammo = 10;
    public Text AmmoText;
    public int ArmorHp = 4;
    public Slider DashSlider;
    public AudioSource asource;
    public GameObject ScoreTextGameObject;
    public GameObject DeadScreen;
    public bool Dead;
    private int highscore;
    public Text highscoreText;
    public bool DashPlayed;
    public AudioClip Dash;
    public GameObject NoseCollider;
    public AudioSource aDeath;
    private bool DeathSoundPlayed;

    // Start is called before the first frame update
    void Start()
    {
        DeathSoundPlayed = false;
        highscore = PlayerPrefs.GetInt("highscore", highscore);
        Score = 0;
        rb = gameObject.GetComponent<Rigidbody2D>();
        renderers = GetComponentsInChildren<Renderer>();
        DashSlider.maxValue = 100;
        DashSlider.minValue = 0;
        DashSlider.value = 100;
        transform.position += new Vector3(0f, 0f, 1f);
        asource = GameObject.Find("MusicManagerDash").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        NoseCollider.transform.position = transform.position;
        NoseCollider.transform.rotation = transform.rotation;

        if (Dead)
        {
            if (!DeathSoundPlayed)
            {
                DeathSoundPlayed = true;
                aDeath.Play();
            }
            Time.timeScale = 0.4f;
            DashPlayed = false;
            GameObject.Find("NoseCollider").GetComponent<PolygonCollider2D>().enabled = false;
            GetComponent<PolygonCollider2D>().enabled = false;
            NoseArmor.GetComponent<PolygonCollider2D>().enabled = false;
            DeadScreen.GetComponent<DeadScreenScirpt>().Dead = true;
            if (Score > highscore)
            {
                highscore = Score;
                PlayerPrefs.SetInt("highscore", highscore);
                PlayerPrefs.Save();
            }

            highscoreText.text = "" + highscore;
        }

        //ScoreTextGameObject.transform.position = new Vector2( ScoreTextGameObject.transform.position.x + Mathf.Sin(Time.time * 1) * 1, ScoreTextGameObject.transform.position.y);



        if (!Dead)
        {
            if (Shop.GetComponent<ShopMenuScript>().ShopOpen == false)
            {
                Time.timeScale = 1;
            }

            if (Input.GetKey(KeyCode.W))
            {
                //transform.Translate(Vector2.up * 0.06f);
                rb.AddForce(transform.up * 220f * Time.deltaTime);
            }

            if (DashPlayed)
            {
                asource.PlayOneShot(Dash);
            }
            else
            {
                asource.Stop();
            }

            if (Input.GetKey(KeyCode.LeftShift) && DashSlider.value >= 0.7f)
            {
                DashPlayed = true;
                CameraShaker.Instance.ShakeOnce(0.2f, 0.2f, 0.1f, 0.1f);
                rb.AddForce(transform.up * 500f * Time.deltaTime);
                DashSlider.value -= 1.15f;
                // rb.AddForce(-Vector2.up * 1f);
            }
            else
            {
                DashPlayed = false;
            }

            if (DashSlider.value <= DashSlider.maxValue)
            {
                DashSlider.value += 0.32f;
            }

            if (Input.GetKey(KeyCode.D))
            {
                //.Rotate(-Vector3.forward * 225f * Time.deltaTime);
                rb.AddTorque(-10.5f * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.A))
            {
                //transform.Rotate(Vector3.forward * 225f * Time.deltaTime);
                rb.AddTorque(10.5f * Time.deltaTime);
            }


            AmmoText.text = Ammo + "Ammo";

            if (Shop.GetComponent<ShopMenuScript>().HaveNoseArmor == true && HaveNoseArmor == false)
            {
                HaveNoseArmor = true;
                Instantiate(NoseArmor, transform.position, transform.rotation);
            }

            if (Shop.GetComponent<ShopMenuScript>().HaveGun == true && !HaveGun)
            {
                HaveGun = true;
                Instantiate(Guns, transform.position, transform.rotation);
            }

            if (Shop.GetComponent<ShopMenuScript>().HaveArmor == true && !HaveArmor)
            {
                Debug.Log("ARMOR");
                HaveArmor = true;
                Instantiate(Armor, transform.position, transform.rotation);
            }

            Guns.transform.position = transform.position;
            Guns.transform.rotation = transform.rotation;

            Armor.transform.position = transform.position;
            Armor.transform.rotation = transform.rotation;

            ScreenWrap();

            ScoreText.text = Score + "";


            if (rb.velocity.magnitude >= 2f)
            {
                /*if (mainCamera.orthographicSize < 2.5f)
                {
                    mainCamera.orthographicSize += 0.05f;
                }
                */
                Instantiate(PlayerGhost, transform.position, transform.rotation);
            }
            else
            {
                /*
                if (mainCamera.orthographicSize > 1.5f)
                {
                    mainCamera.orthographicSize -= 0.1f;
                }
                */
            }

            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");

            //rb.velocity = new Vector2(horizontal * 2f, vertical * 2f);


            /*if (Input.GetAxis("Vertical") > 0)
            {
                
                GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * accel);
    
    
                if (GetComponent<Rigidbody2D>().velocity.magnitude > maxSpeed)
                {
    
                    GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized * maxSpeed;
    
                }
            }
            
            
            if (Input.GetAxis("Horizontal") != 0)
            {
                float scale = Mathf.Lerp(0f, turnSpeed, GetComponent<Rigidbody2D>().velocity.magnitude / maxSpeed);
                
                GetComponent<Rigidbody2D>().AddTorque(-Input.GetAxis("Horizontal") * scale);
            }
            
        }
    */
        }
    }

    public void RestartPosition()
    {
        transform.position = new Vector2(0f, 0f);
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

            if (newPosition.x > 0.001f || newPosition.x < 0)
            {
                newPosition.x = -newPosition.x;
                isWrappingX = true;
            }

            if (newPosition.y > 0.001f || newPosition.y < 0)
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


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Enemy")
            {
                if (other.GetComponent<BirdSript>().invincible == false && DashPlayed == false)
                {
                    if (HaveArmor == true)
                    {
                        ArmorHp--;
                        if (ArmorHp <= 0)
                        {
                            Dead = true;
                            GetComponent<SpriteRenderer>().color = Color.clear;
                        }
                    }
                    else
                    {
                        Debug.Log(other.GetComponent<BirdSript>().invincible + other.gameObject.name);
                        Dead = true;
                        GetComponent<SpriteRenderer>().color = Color.clear;
                    }
                }
            }
            
                if (other.gameObject.tag == "Ballon")
                {
                    Score += 5;
                }

                if (other.gameObject.tag == "AmmoBox")
                {
                    Ammo += 10;
                    Destroy(other.gameObject);
                }
        }
}
        


