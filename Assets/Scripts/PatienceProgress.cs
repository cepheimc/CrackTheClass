using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class PatienceProgress : MonoBehaviour
{
    Image foregroundImage;
    Text percents;
    PlayerScript player;

    void Start()
    {
        // I'm so shamed of this code ((
        foregroundImage = GameObject.Find("ProgressFore").GetComponent<Image>();
        percents = GameObject.Find("ProgressNumber").GetComponent<Text>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        int patience = player.GetPatience();

        foregroundImage.fillAmount = patience / 100f;
        percents.text = string.Format("{0}%", patience);
    }
}
