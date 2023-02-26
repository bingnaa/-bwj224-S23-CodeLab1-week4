using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public static HealthBar Instance;
    public TMP_Text healthB;
    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        
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

    private void Update()
    {
        healthB.text = "hp " + GameManager.Instance.health.ToString() + "\n" +
                       "difficulty " + GameManager.Instance.Mode.ToString();
    }
}
