using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    private int levelInfo;
    public int level;
    public Image starImage;
    public Sprite[] starSprites;

    private void Start()
    {
        levelInfo = PlayerPrefs.GetInt("Stars" + level);
        switch (levelInfo)
        {
            case 0:
                starImage.sprite = starSprites[0]; break;
            case 1:
                starImage.sprite = starSprites[1]; break;
            case 2:
                starImage.sprite = starSprites[2]; break;
            case 3:
                starImage.sprite = starSprites[3]; break;
        }
    }
}
