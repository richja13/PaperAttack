
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position += new Vector3(0f,0f,1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up *  0.1f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(this.gameObject);
    }
}
