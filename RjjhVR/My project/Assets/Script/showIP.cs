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
            //从IP地址列表中筛选出IPv4类型的IP地址
            //AddressFamily.InterNetwork表示此IP为IPv4,
            //AddressFamily.InterNetworkV6表示此地址为IPv6类型
            if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
            {
                Text.text="ip地址为："+ IpEntry.AddressList[i].ToString()+"\n";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
