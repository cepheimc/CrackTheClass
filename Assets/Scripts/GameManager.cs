using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Serializable]
    public class Level
    {
        public GameObject grid;
        public string name;
        public GameObject playerStart;
        public GameObject teacherStart;
        public GameObject exit;
    }

    public Level[] levels;

    public GameObject pickLevelButton;
    public GameObject initialScreen;
    public GameObject player;

    // There is always only one level (tile grid) loaded
    // This singleton will carry it's pathfinding object 
    public PathFinding levelPathFinding;

    public static GameManager instance = null;


    private GameObject canvas;
    private GameObject level;
    


    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        canvas = GameObject.Instantiate(initialScreen, new Vector3(0f, 0f, 0f), Quaternion.identity);

        for (int i = 0; i < levels.Length; i++)
        {
            var button = GameObject.Instantiate(pickLevelButton, new Vector3(0f, 0f, 0f), Quaternion.identity);
            button.transform.SetParent(canvas.transform, false);  // drop positioning

            Vector3 position = button.transform.position;
            position.x += 50;
            position.y += 50;
            position.y -= 40 * i;
            
            button.transform.position = position;

            var buttonText = button.GetComponentInChildren<Text>();
            buttonText.text = levels[i].name;

            var iStored = i;
            button.GetComponent<Button>().onClick.AddListener(delegate { LoadLevel(levels[iStored]); });

        }
        
    }

    void LoadLevel(Level level)
    {
        Debug.Log(1);
        Destroy(canvas);
        Debug.Log(2);
        Debug.Log(level.grid);
        Instantiate(level.grid, new Vector3(2f, 0f, 0f), Quaternion.identity);
        Debug.Log(3);
        Instantiate(level.exit, level.exit.transform.position, Quaternion.identity);
        Debug.Log(4);
        Instantiate(player, level.playerStart.transform.position, Quaternion.identity);
        
        
    }

    void Restart()
    {
        SceneManager.LoadScene(0);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;    
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;


    }
}
