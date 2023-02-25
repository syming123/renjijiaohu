using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NewBehaviourScript : MonoBehaviour
{
    
    public InputField input1;
    public static string date;
    
    public void OnClick()
    {
        date = input1.text;
    }

}
