using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AudioVolume : MonoBehaviour
{

    float volume;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        volume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        AudioListener.volume = volume;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
