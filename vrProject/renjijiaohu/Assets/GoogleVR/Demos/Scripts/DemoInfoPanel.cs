//-----------------------------------------------------------------------
// <copyright file="DemoInfoPanel.cs" company="Google LLC">
// Copyright 2019 Google LLC. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
//-----------------------------------------------------------------------

namespace GoogleVR.Demos
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;
    using System.Net;
    using System.Net.Sockets;

    /// <summary>
    /// Displays and logs various app/sdk/etc versions.
    /// </summary>
    public class DemoInfoPanel : MonoBehaviour
    {
        private void Start()
        {
            Text textObj = GetComponentInChildren<Text>();
            string HostName = Dns.GetHostName(); //得到主机名
            IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
            for (int i = 0; i < IpEntry.AddressList.Length; i++)
            {
                if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    textObj.text = "ip Adress:" + IpEntry.AddressList[i].ToString() + "\n";
                }
            }

#if !UNITY_EDITOR
            
#endif
        }
    }
}
