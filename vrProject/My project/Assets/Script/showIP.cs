using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using TMPro;
using UnityEngine;

public class showIP : MonoBehaviour
{
    public TextMeshProUGUI Text;
    // Start is called before the first frame update
    void Start()
    {
        
        Text = transform.GetComponent<TextMeshProUGUI>();
        string name = Dns.GetHostName();
        Text.text = "";
        string HostName = Dns.GetHostName(); //得到主机名
        IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
        for (int i = 0; i < IpEntry.AddressList.Length; i++)
        {
            if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
            {
                Text.text="ip Adress:"+ IpEntry.AddressList[i].ToString()+"\n";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
