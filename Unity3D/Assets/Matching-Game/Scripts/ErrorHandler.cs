﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ErrorHandler : MonoBehaviour
{
    #region Singleton

    public static ErrorHandler Instance { get; private set; }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogError("Cannot be more than 1 instance of a singleton object: " + this);
            Destroy(gameObject);
        }
    }

    #endregion

    //The only error accounted for so far.
    string errorMessage = "";
    bool indicatorActive = false;
    [SerializeField]
    TextMeshProUGUI textMessage;

    public void IndicateError(string error)
    {
        errorMessage = error;
        textMessage.text = errorMessage;
        StartCoroutine(AlertError());
    }

    IEnumerator AlertError()
    {
        if (!indicatorActive)
        {
            indicatorActive = true;
            float fadeRate = 0.02f;
            textMessage.color = Color.red;

            while (textMessage.color.a > 0)
            {
                textMessage.color = new Color(textMessage.color.r, textMessage.color.g, textMessage.color.b, textMessage.color.a - fadeRate);
                yield return new WaitForSeconds(0.05f);
            }

            textMessage.color = new Color(1, 0, 0, 0);
            indicatorActive = false;
        }   
        yield return null;      
    }

}
