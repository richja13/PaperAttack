using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuControlsScript : MonoBehaviour
{
    private bool OpenControls;
    private Vector2 startPos;

    private RectTransform recttranfsorm;
    private Vector2 StartPos;

    void Start()
    {
        recttranfsorm = this.gameObject.GetComponent<RectTransform>();
        StartPos = recttranfsorm.anchoredPosition;
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(recttranfsorm.anchoredPosition);

        if (OpenControls == true)
        {
            if (recttranfsorm.anchoredPosition.y > 0)
            {
                recttranfsorm.anchoredPosition = Vector2.down;
            }
        }
        else if(OpenControls == false)
        {
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
