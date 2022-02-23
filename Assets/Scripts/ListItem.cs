using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ListItem : MonoBehaviour
{
    public Image image;
    public Text filename, createTimeText;
    private DateTime creationTime;

    private IEnumerator coroutine;

    public void Init(string imagePath)
    {
        creationTime = File.GetCreationTime(imagePath);
        ReplaceSpriteFromPNG(image, imagePath);
        filename.text = Path.GetFileName(imagePath);
        coroutine = UpdateCreationTime();
        StartCoroutine(coroutine);
    }

    private IEnumerator UpdateCreationTime()
    {
        while (true)
        {
            TimeSpan time = (DateTime.Now - creationTime);
            createTimeText.text = String.Format("Created: \n {0} days {1} hours {2} minutes {3} seconds ago", time.Days, time.Hours, time.Minutes, time.Seconds);
            yield return new WaitForSeconds(1);
        }
    }

    private void OnDestroy()
    {
        StopCoroutine(coroutine);
    }

    private Sprite LoadTexture(string filePath)
    {
        Texture2D tex2D;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex2D = new Texture2D(0, 0);
            if (tex2D.LoadImage(fileData))
            {
                Sprite newSprite = Sprite.Create(tex2D, new Rect(0, 0, tex2D.width, tex2D.height), new Vector2(0, 0), 100f);
                return newSprite;
            }
        }
        return null;
    }

    private void ReplaceSpriteFromPNG(Image image, string path)
    {
        image.sprite = LoadTexture(path);
        Vector3 bounds = image.sprite.bounds.size;
        if (bounds.x > bounds.y)
            image.rectTransform.localScale = new Vector3(image.rectTransform.localScale.x, image.rectTransform.localScale.y * bounds.y / bounds.x, 0);
        else
            image.rectTransform.localScale = new Vector3(image.rectTransform.localScale.x * bounds.x / bounds.y, image.rectTransform.localScale.y, 0);
    }
}
