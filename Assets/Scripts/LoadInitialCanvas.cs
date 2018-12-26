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

    void Start()
    {
        Canvas canvas = GetComponent<Canvas>();
        for (int i = 0; i < levels.Length; i++)
        {
            var button = GameObject.Instantiate(pickLevelButton, new Vector3(0f, -50f * i, 0f), Quaternion.identity);
            button.transform.SetParent(canvas.transform, false);  // drop positioning

            var buttonText = button.GetComponentInChildren<Text>();
            buttonText.text = levels[i].displayName;

            var iStored = i;
            button.GetComponent<Button>().onClick.AddListener(delegate { LoadLevel(levels[iStored]); });
        }
    }

    void LoadLevel(Level level)
    {
        SceneManager.LoadScene(level.sceneName, LoadSceneMode.Single);
    }

}
