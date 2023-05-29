using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class MainMenu : MonoBehaviour
{
    public AudioSource myFX;
    public AudioClip hoverFX;
    public AudioClip clickFX;

    public Slider musicSlider;
    public Slider soundSlider;
    public AudioMixer musicMixer;
    public AudioMixer soundMixer;

    public void SetVolume()
    {
        musicMixer.SetFloat("MusicVolume", musicSlider.value);
        soundMixer.SetFloat("SoundVolume", soundSlider.value);

    }
    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Выход из игры!");
    }

    public void HoverSound()
    {
        myFX.PlayOneShot(hoverFX);
    }

    public void ClickSound()
    {
        myFX.PlayOneShot(clickFX);
    }
}
