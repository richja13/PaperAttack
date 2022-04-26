
using UnityEngine;
using EZCameraShake;
using UnityEngine.Audio;

public class GunsScript : MonoBehaviour
{
    public GameObject Player;
    public GameObject Bullet;
    public bool ChangeBullet;
    public float FireDelay;
    public Transform FirePlace1;
    public Transform FirePlace2;
    public ParticleSystem FirePlace1Particle;
    public ParticleSystem FirePlace2Particle;
    private GameObject Shop;
    private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        Shop = GameObject.Find("Shop");
         Player = GameObject.Find("Player");
         audioSource = GameObject.Find("MusicManagerShoot").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.transform.position;
        transform.rotation = Player.transform.rotation;
        
        if (Input.GetMouseButtonDown(0) && Player.GetComponent<PlayerMovementScript>().Ammo > 0 && Shop.GetComponent<ShopMenuScript>().ShopOpen != true)
        {
            audioSource.Play();
            CameraShaker.Instance.ShakeOnce(0.8f, 0.9f, 0.1f, 0.1f);
            Instantiate(Bullet, FirePlace1.position, transform.rotation);
            Instantiate(Bullet, FirePlace2.position, transform.rotation);
            FirePlace1Particle.Play();
            FirePlace2Particle.Play();
            Player.GetComponent<PlayerMovementScript>().Ammo -= 2;
        }
        
        
    }
}
