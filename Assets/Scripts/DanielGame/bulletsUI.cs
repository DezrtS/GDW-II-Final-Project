using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bulletsUI : MonoBehaviour
{
    public int bulletsNum;
    public int bulletNumMax;
    public Image[] BulletImage;

    public Sprite bulletSprite;
    public Sprite emptySprite;


    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < BulletImage.Length; i++)
        {

            if (i < bulletsNum)
            {
                BulletImage[i].sprite = bulletSprite;
                BulletImage[i].color = Color.white;
            }

            else
            {
                BulletImage[i].sprite = emptySprite;
                BulletImage[i].color = Color.clear;
            }


            if (i < bulletNumMax)
            {
                BulletImage[i].enabled = true;
            }

            else
            {
                BulletImage[i].enabled = false;
            }
        }

    }

    public void subtractAmmo()
    {
        bulletsNum--;
    }

    public void AddAmmo()
    {
        bulletsNum++;
    }

    public int returnAmmo()
    {
        return bulletsNum;
    }
}
