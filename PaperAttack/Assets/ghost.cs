
using UnityEngine;
using Color = System.Drawing.Color;

public class ghost : MonoBehaviour
{
    public GameObject Player;
    public SpriteRenderer Sghost;
    private float timer = 0.2f;
    private float AlphaColor = 120;
    
    // Start is called before the first frame update
    void Start()
    {
        //sprite = GetComponent<SpriteRenderer>();
       
        //transform.localScale = Player.transform.localScale;

        //sprite.sprite = Player.GetComponent<SpriteRenderer>().sprite;
        

    }

    // Update is called once per frame
    void Update()
    {
        AlphaColor -= 5;
        
        //GetComponent<SpriteRenderer>().color = new UnityEngine.Color(0 ,0.039f,0.100f,AlphaColor);
        
        //transform.position = Player.transform.position;
        
        
        Destroy(gameObject,0.2f);
    }
}
