using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private bool sceneLoaded = false;
    private int mode = 1;
    private int score = 0;

    private const string DIR_DATA = "/Data/";
    private const string FILE_MODE = "mode.txt";
    private string PATH_MODE;

    string FILE_PATH;
    const string FILE_NAME = "highScores.txt";

    public const string PREF_MODE = "mode";

    public int currentLevel = 0;
    public int slimeB = 0;
    public int targetSl = 1;
    public int health = 100;
    public string final = "n/a";

    public int Mode
    {
        get { return mode; }
        set
        {
            if (value < 3)
            {
                mode = value;
            }

            Directory.CreateDirectory(Application.dataPath + DIR_DATA);
            File.WriteAllText(PATH_MODE, "" + mode);
        }
    }

    public int Score
    {
        get { return score; }

        set { score = value; }
    }

    public List<int> highScores = new List<int>();

    void Awake()
    {

        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PATH_MODE = Application.dataPath + DIR_DATA + FILE_MODE;
        FILE_PATH = Application.dataPath + DIR_DATA + FILE_NAME;

        Mode = PlayerPrefs.GetInt(PREF_MODE, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            File.Delete(PATH_MODE);
        }

        if (slimeB == targetSl)
        {
            currentLevel++;
            slimeB = 0;
            targetSl *= 2;
            SceneManager.LoadScene(currentLevel);
        }

        if (health < 0 && sceneLoaded == false)
        {
            SceneManager.LoadScene("Scenes/gameOver");
            sceneLoaded = true;
            UpdateHighScores();
        }
    }

    void UpdateHighScores()
    {
        if (highScores.Count == 0)
        {
            if (File.Exists(FILE_PATH))
            {
                string fileC = File.ReadAllText(FILE_PATH);
                string[] fileSp = fileC.Split('\n');
                for (int i = 1; i < fileSp.Length - 1; i++)
                {
                    highScores.Add(Int32.Parse(fileSp[i]));
                }
            }
            else
            {
                highScores.Add(0);
            }
        }

        for (int i = 0; i < highScores.Count; i++)
        {
            if (highScores[i] < Score)
            {
                highScores.Insert(i, Score);
                break;
            }
        }

        if (highScores.Count > 3)
        {
            highScores.RemoveRange(3, highScores.Count - 3);
        }

        string highScoreStr = "High Scores\n";

        for (int i = 0; i < highScores.Count; i++)
        {
            highScoreStr += highScores[i] + "\n";
        }
        File.WriteAllText(FILE_PATH, highScoreStr);
        final = highScoreStr;
    }
}
