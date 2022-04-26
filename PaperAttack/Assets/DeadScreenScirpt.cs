
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;
using UnityEngine.SceneManagement;
public class DeadScreenScirpt : MonoBehaviour
{
    private Vector2 StartPos;
    public bool Dead;
    public bool DeadShake;
    public bool ControlsOpened;
    public Animator anim;
    private RectTransform recttransform;

    // Start is called before the first frame update
    public void Start()
    {
        DeadShake = true;
        Dead = false;
        recttransform = this.gameObject.GetComponent<RectTransform>();
        StartPos = recttransform.anchoredPosition;
    }

    // Update is called once per frame
    public void Update()
    {
        if (ControlsOpened)
        {
            recttransform.anchoredPosition = StartPos;
        }
     
        if (Dead)
        {
            Debug.Log("FASLE ALARM");
            
            if (DeadShake)
            {
                DeadShake = false;
                CameraShaker.Instance.ShakeOnce(1, 2, 1, 2);
            }

            if(recttransform.anchoredPosition.x > 0)
            {
                recttransform.anchoredPosition -= new Vector2(10,0f);
            }
            else if(recttransform.anchoredPosition.x < 0)
            {
                recttransform.anchoredPosition += new Vector2(10, 0f);
            }


            if (recttransform.anchoredPosition.y > 0 && !ControlsOpened)
            {

                recttransform.anchoredPosition += Vector2.down * 20;
            }

        }
    }

    public void RestartGame()
    {
        anim.SetTrigger("start");
        Invoke("LoadLvl", 0.6f);
    }

    public void Controls()
    {
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadLvl()
    {
        Debug.Log("ELCO");
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }


}
