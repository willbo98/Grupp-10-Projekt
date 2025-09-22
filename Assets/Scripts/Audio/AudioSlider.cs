using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{


    [SerializeField] Slider volumeSlider;
    float mastervolume;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        mastervolume = volumeSlider.value;
        AudioListener.volume = mastervolume;
        PlayerPrefs.SetFloat("MasterVolume", mastervolume);
        PlayerPrefs.Save();
        
    }
}
