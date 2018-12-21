using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] levelGrids;
    public string[] levelNames;
    public GameObject pickLevelButton;
    public GameObject initialScreen;

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

        for (int i = 0; i < levelGrids.Length; i++)
        {
            var button = GameObject.Instantiate(pickLevelButton, new Vector3(0f, 0f, 0f), Quaternion.identity);
            button.transform.SetParent(canvas.transform, false);  // drop positioning

            Vector3 position = button.transform.position;
            position.x += 50;
            position.y += 50;
            position.y -= 40 * i;
            
            button.transform.position = position;

            var buttonText = button.GetComponentInChildren<Text>();
            buttonText.text = i > levelNames.Length
                              ? string.Format("Level {0}", i + 1)
                              : levelNames[i];
            Debug.Log(string.Format("Loading {0}", i));
            var iStored = i;
            button.GetComponent<Button>().onClick.AddListener(delegate { LoadLevel(levelGrids[iStored]); });
        }
        
    }

    void LoadLevel(GameObject levelToInstantiate)
    {
        Destroy(canvas);

        Instantiate(levelToInstantiate);

        Invoke("Restart", 1f);
        
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

    void SetUpLevel()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
