using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIHandler : MonoBehaviour
{
    public static UIHandler Instance;

    [SerializeField] TextMeshProUGUI hightscoreText;
    [SerializeField] TMP_InputField playerNameText;
    [SerializeField] Text mainScoreText;

    GameManager gameManager;

    // Start is called before the first frame update
    void Awake()
    {
        //Just to check i don't create another one
        if(Instance != null)
        {
            Destroy(Instance);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        gameManager = GameManager.instance;

        //In case the hight score is different from 0, show a message
        if (gameManager.hightScore != 0)
        {
            //Save data on local variables
            string playerName = gameManager.playerName;
            int hightScore = gameManager.hightScore;
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (sceneIndex == 0)
            {
                //unhide the text
                hightscoreText.gameObject.SetActive(true);

                //Set values
                hightscoreText.text = "Hight score: " + hightScore + ", made by: " + playerName;
                playerNameText.text = playerName;
            }
            else
            {
                //unhide the text
                mainScoreText.gameObject.SetActive(true);

                //Set values
                mainScoreText.text = "Hight score: " + hightScore + " - " + playerName;
            }
        }
    }

    public void TakePlayerName()
    {
        string playerName = playerNameText.text;

        //give to the GameManager the name
        if (!string.IsNullOrEmpty(playerName))
        {
            gameManager.newPlayerName = playerNameText.text;
            SceneManager.LoadScene(1);
        }
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
