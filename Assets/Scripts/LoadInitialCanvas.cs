using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadInitialCanvas : MonoBehaviour
{
    [Serializable]
    public class Level
    {
        public string sceneName;
        public string displayName;
    }

    public Level[] levels;
    public GameObject pickLevelButton;

    private Canvas canvas;

    void Start()
    {
        canvas = GetComponent<Canvas>();

        int i;
        for (i = 0; i <= levels.Length; i++)
        {
            var button = CreateButton(new Vector3(0f, -50f * i, 0f));

            if (i == levels.Length)
            {
                // Last button - exit button
                button.GetComponentInChildren<Text>().text = "E X I T";
                button.GetComponent<Button>().onClick.AddListener(Application.Quit);
            }
            else
            {
                button.GetComponentInChildren<Text>().text = levels[i].displayName;
                var iCopy = i;
                button.GetComponent<Button>().onClick.AddListener(delegate { LoadLevel(levels[iCopy]); });
            }            
        }
        
    }

    GameObject CreateButton(Vector3 position)
    {
        var button = GameObject.Instantiate(pickLevelButton, position, Quaternion.identity);
        button.transform.SetParent(canvas.transform, false);  // drop positioning

        return button;
    }

    void LoadLevel(Level level)
    {
        SceneManager.LoadScene(level.sceneName, LoadSceneMode.Single);
    }

    public void LoadThis()
    {
        SceneManager.LoadScene("InitialScene", LoadSceneMode.Single);
    }

}
