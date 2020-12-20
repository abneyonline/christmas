using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuOptions : MonoBehaviour
{
    public Slider VolumeSlider;
    public AudioSource AudioPlayer;

    public void VolumeChanged()
    {
        AudioPlayer.volume = VolumeSlider.value;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
