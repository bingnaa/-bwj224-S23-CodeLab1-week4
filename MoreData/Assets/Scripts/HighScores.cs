using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScores : MonoBehaviour
{
    public TMP_Text scoreTexts;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreTexts.text = GameManager.Instance.final;
    }
}
