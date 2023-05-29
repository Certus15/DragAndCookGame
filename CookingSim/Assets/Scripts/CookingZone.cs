using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class CookingZone : MonoBehaviour
{
    public List<DraggingItems> items;
    public string[] recipes;
    public DraggingItems[] recipeResult;
    [SerializeField] 
    private string currentRecipeString;

    public float timeToCook;
    public Image timerBar;
    private float timeleft;

    public AudioSource myFX;
    public AudioClip cookFX;
    public AudioClip wrongRecFX;

    private void Update()
    {
        if(timeleft > 0)
        {
            timeleft -= Time.deltaTime;
            timerBar.fillAmount = 1 - (timeleft / timeToCook);
        }
    }
    public void Cooking()
    {

        if (recipes.Contains(currentRecipeString)) 
        {
            myFX.PlayOneShot(cookFX);
            StartCoroutine(CheckForRecipe(timeToCook));
            timerBar.enabled = true;
            timeleft = timeToCook;
        }
        else
            myFX.PlayOneShot(wrongRecFX);
    }

    IEnumerator CheckForRecipe(float time)
    {
        yield return new WaitForSeconds(time);
        for (int i = 0; i < recipes.Length; i++)
        {
            if (recipes[i] == currentRecipeString)
            {
                GameObject craftedItem = Instantiate(recipeResult[i].gameObject, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
                Debug.Log("Готово!");
                for (int j = items.Count; j > 0; j--)
                {
                    Destroy(items[j-1].gameObject);
                }
                items.Clear();
                currentRecipeString = "";
                timerBar.enabled = false;
                myFX.Stop();
            }
            else
            {
                Debug.Log("Неверный рецепт!");
                timerBar.enabled = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DraggingItems item = collision.GetComponent<DraggingItems>();
        if (collision.tag == "Item")
        {  
            currentRecipeString += item.itemName; 
            items.Add(item);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        DraggingItems item = collision.GetComponent<DraggingItems>();
        if (collision.tag == "Item")
        {
            currentRecipeString = currentRecipeString.Replace(item.itemName, "");
            items.Remove(item);
        }
    }

}
