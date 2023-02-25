using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//场景
public class change : MonoBehaviour
{
    
    public void Scene2()
    {
        SceneManager.LoadScene("SampleScene");
        //方式二 SceneManager.LoadScene(1);
    }
}