using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hearts : MonoBehaviour
{
    public int health;
    public int heartNum;

    public Image[] heartImage;

    public Sprite fullHeartSprite;
    public Sprite emptyHeartSprite;


    // Update is called once per frame
    void Update()
    {

        for(int i = 0; i < heartImage.Length; i++)
        {

            if(i < health)
            {
                heartImage[i].sprite = fullHeartSprite;
            }

            else
            {
                heartImage[i].sprite = emptyHeartSprite;
            }


            if(i < heartNum)
            {
                heartImage[i].enabled = true;
            }

            else
            {
                heartImage[i].enabled = false;
            }
        }
        
    }

    public void subtractHealth()
    {
        health--;
    }

    public int returnHealth()
    {
        return health;
    }

    public void setHealth(int num)
    {
        health = num;
    }
}
