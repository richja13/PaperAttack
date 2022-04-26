using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraScript : MonoBehaviour
{
    private AudioSource source;
    private bool MuteMusic ;
    private int saveMuteInfo ;
    
    // Start is called before the first frame update
    void Start()
    {
        saveMuteInfo = PlayerPrefs.GetInt("Mute");
        source = GameObject.Find("MusicManagerMainMusic").GetComponent<AudioSource>();
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
        PlayerPrefs.SetInt("Mute",saveMuteInfo);
        PlayerPrefs.Save();
        
        if (saveMuteInfo == 1)
        {
            source.mute = true;
        }
        else if(saveMuteInfo == 0)
        {
            source.mute = false;
        }
    }
    
    public void MuteMusicButton()
    {
        if (saveMuteInfo == 0)
        {
            saveMuteInfo = 1;
        }
        else if(saveMuteInfo == 1)
        {
            saveMuteInfo = 0;
            MuteMusic = false;
        }
    }
}
