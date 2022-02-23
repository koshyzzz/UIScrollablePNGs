using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ListManager : MonoBehaviour
{
    public static ListManager singleton;
    public GameObject itemPrefab;
    // Start is called before the first frame update
    void Start()
    {
        singleton = this;
        RefreshImages();
    }

    public void RefreshImages()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        string[] files = GetFiles(PlayerPrefs.GetString("path", Application.dataPath));
        if (files == null)
            return;
        foreach (string file in files)
        {
            if (!file.EndsWith(".png"))
                continue;
            GameObject o = Instantiate(itemPrefab, transform);
            Debug.Log(file);
            o.GetComponent<ListItem>().Init(file);
        }
    }

    public static string[] GetFiles(string path)
    {
        try
        {
            string[] files = Directory.GetFiles(path);
            return files;
        }
        catch { return null; }
        return null;
    }
}
