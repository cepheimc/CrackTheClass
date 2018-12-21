using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] levelGrids;
    public string[] levelNames;

    public static GameManager instance = null;


    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {
        Debug.Log(string.Format("Level {0} loaded", level));
    }

    void SetUpLevel()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
