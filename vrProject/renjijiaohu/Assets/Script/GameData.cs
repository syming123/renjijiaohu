using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{

    public string param;
 
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void give()
    {
        GameObject.Find("GameData").GetComponent<GameData>().param = NewBehaviourScript.date;
    }
}
