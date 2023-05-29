using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class EndGameZone : MonoBehaviour
{
    public List<DraggingItems> items;
    public string[] itemsForWin;
    [SerializeField]
    private string currentItemInZone;

    public GameObject winCanvas;

    int sceneIndex;

    public float timeToWin;
    public Image timerBar;
    private float timeleft;
    private float timeCurrent;

    public AudioSource myFX;
    public AudioClip cookFX;

    private void Start()
    {
        timerBar.enabled = true;
        timeleft = timeToWin;
    }
    void Update()
    {
        CheckForWin();
        if (timeleft > 0)
        {
            timeleft -= Time.deltaTime;
            timerBar.fillAmount = timeleft / timeToWin;
        }
        if (timeleft <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void CheckForWin()
    {
        for (int i = 0; i < itemsForWin.Length; i++) 
        {
            if (itemsForWin[i] ==  currentItemInZone)
            {
                winCanvas.SetActive(true);
                sceneIndex = SceneManager.GetActiveScene().buildIndex;
                PlayerPrefs.SetInt("LevelComplete", sceneIndex);
                timeCurrent = timeleft;
                if(timeCurrent/timeToWin >= 0.8)
                    PlayerPrefs.SetInt("Stars" + sceneIndex, 3);
                else if (timeCurrent/timeToWin < 0.8 && timeCurrent / timeToWin >= 0.6)
                    PlayerPrefs.SetInt("Stars" + sceneIndex, 2);
                else if (timeCurrent / timeToWin < 0.6 && timeCurrent / timeToWin >= 0.4)
                    PlayerPrefs.SetInt("Stars" + sceneIndex, 1);
                else if (timeCurrent / timeToWin < 0.4)
                    PlayerPrefs.SetInt("Stars" + sceneIndex, 0);

                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DraggingItems item = collision.GetComponent<DraggingItems>();
        if (collision.tag == "Item")
        {
            currentItemInZone += item.itemName;
            items.Add(item);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        DraggingItems item = collision.GetComponent<DraggingItems>();
        if (collision.tag == "Item")
        {
            currentItemInZone = currentItemInZone.Replace(item.itemName, "");
            items.Remove(item);
        }
    }
}
