
using UnityEngine;

public class ControlsScript : MonoBehaviour
{
    private bool OpenControls;
    public GameObject DeadScreen;
    private RectTransform recttranfsorm;
    private Vector2 StartPos;

    void Start()
    {
      recttranfsorm = this.gameObject.GetComponent<RectTransform>();
      StartPos = recttranfsorm.anchoredPosition;
    }

    void Update()
    {
        if (OpenControls == true)
        {
            DeadScreen.GetComponent<DeadScreenScirpt>().ControlsOpened = true;

            if (recttranfsorm.anchoredPosition.y > 0)
            {
                recttranfsorm.anchoredPosition = Vector2.down;
            }
        }
        else if(OpenControls == false)
        {
            DeadScreen.GetComponent<DeadScreenScirpt>().ControlsOpened = false;

            if (recttranfsorm.anchoredPosition.y < 885)
            {
                recttranfsorm.anchoredPosition = StartPos;
            }
        }
    }

    
    public void OpenControlsMenu()
    {
        if (!OpenControls)
        {
            OpenControls = true;
        }
        else if (OpenControls)
        {
            OpenControls = false;
        }
        
    }
}
