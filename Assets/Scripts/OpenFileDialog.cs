using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
public class OpenFileDialog : MonoBehaviour
{
    public GameObject fileBrowserPanel;
    public InputField filepath;
    public Text warning;

    private void Start()
    {
        warning.enabled = false;
        fileBrowserPanel.SetActive(false);
    }

    public void OpenFile()
    {
#if UNITY_EDITOR
        string path = EditorUtility.OpenFolderPanel("Choose folder", Application.dataPath, "");
        Debug.Log(path);
        if (ListManager.GetFiles(path) == null)
            return;
        PlayerPrefs.SetString("path", path);
        ListManager.singleton.RefreshImages();
#else
        fileBrowserPanel.SetActive(true);
#endif
    }

    public void ClosePathPanel()
    {
        warning.enabled = false;
        fileBrowserPanel.SetActive(false);
    }

    public void ConfirmPathChange()
    {
        if (ListManager.GetFiles(filepath.text) == null)
        {
            warning.enabled = true;
            return;
        }
        warning.enabled = false;
        PlayerPrefs.SetString("path", filepath.text);
        ListManager.singleton.RefreshImages();
        fileBrowserPanel.SetActive(false);
    }
}
