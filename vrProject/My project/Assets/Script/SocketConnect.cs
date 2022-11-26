using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using UnityEngine;
using System.Threading;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using UnityEngine.UIElements;


public class Room{
    public int roomWidth;
    public int roomHeight;
    public Furniture[] furniture;
}
public class Furniture
{
    public int id;
    public int classID;
    public int x;
    public int y;
    public int rotate;
}

public class SocketConnect : MonoBehaviour
{
    string[] className = new string[] {"piano","Textile_chair"};
    int[]furnitureWidth=new int[] {2,1};
    int[]furnitureHeight=new int[] {1,1};

    static bool creat=false;
    static string data;
    Thread thread1;
    Room room;
    Furniture[] furniture=null;
    float roomWidth;
    float roomHeight;
    void Start()
    {
        data = "{\"type\": \"room\",\"roomWidth\": 10,\"roomHeight\": 15,\"furniture\": [{\"id\": 1,\"classID\": 0,\"x\": 5,\"y\": 6,\"rotate\": 1},{\"id\": 2,\"classID\": 1,\"x\": 1,\"y\": 1,\"rotate\": 1}]}";
        creat = true;
        Thread thread1 = new Thread(new ThreadStart(Thread1));
        //thread1.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (creat)
        {
            if (data.IndexOf("room") != -1)
            {
                Room room1 = JsonConvert.DeserializeObject<Room>(data);
                
                GameObject go;
                GameObject gameObject1;
                Furniture[] tfurniture = room1.furniture;
                if (furniture == null)//初始化房间墙体等
                {
                    roomWidth = room1.roomWidth;
                    roomHeight = room1.roomHeight;
                    go = GameObject.Find("Ground");
                    go.transform.localScale = new Vector3((float)(roomWidth / 5+0.2), 1, (float)(roomHeight / 5+0.2));
                    go = GameObject.Find("wall0");
                    go.transform.localScale = new Vector3(roomWidth , 10, 1);
                    go.transform.position=new Vector3(roomWidth / 2, 5 , (float)0.5);
                    go = GameObject.Find("wall1");
                    go.transform.localScale = new Vector3(roomHeight, 10, 1);
                    go.transform.position = new Vector3((float)(roomWidth+0.5),5, -(roomHeight / 2));
                    go = GameObject.Find("wall2");
                    go.transform.localScale = new Vector3(roomWidth, 10, 1);
                    go.transform.position = new Vector3(roomWidth / 2, 5, -(float)(roomHeight+0.5));
                    go = GameObject.Find("wall3");
                    go.transform.localScale = new Vector3(roomHeight, 10, 1);
                    go.transform.position = new Vector3((float)-0.5, 5, -(roomHeight / 2));

                }
                //添加家具
                if (furniture==null|| furniture.Length < tfurniture.Length)
                {
                    int i;
                    if (furniture == null)
                        i = 0;
                    else i = furniture.Length;
                    for(; i < tfurniture.Length; i++)
                    {   
                        int rotate=tfurniture[i].rotate;
                        int classID=tfurniture[i].classID;
                        float x=0, y=0;
                        if (rotate==1||rotate==3)
                        {
                            x = tfurniture[i].x + ((float)furnitureHeight[classID] / 2);
                            y = -(tfurniture[i].y + ((float)furnitureWidth[classID] / 2));
                        }
                        else if (rotate == 2 || rotate == 0)
                        {
                            x = tfurniture[i].x + (float)(furnitureWidth[classID]/2);
                            y = -(tfurniture[i].y+ (float)(furnitureHeight[classID] / 2));
                        }
                        go = Resources.Load<GameObject>(className[classID]);
                        gameObject1 = Instantiate(go);
                        gameObject1.name = "" + i;
                        gameObject1.transform.position=new Vector3(x,0,y);  
                        gameObject1.transform.localEulerAngles = new Vector3(0,90*rotate,0);
                    }
                    furniture = tfurniture;
                }
                //修改家具信息
                else if(furniture != null&&furniture.Length==tfurniture.Length)
                {
                    for(int i = 0; i < furniture.Length; i++)
                    {
                        int x = tfurniture[i].x;
                        int y = tfurniture[i].y;
                        int rotate = tfurniture[i].rotate;
                        if (furniture[i].x != x|| furniture[i].y != y)
                        {
                            GameObject gameObject = GameObject.Find(""+i);
                            gameObject.transform.position = new Vector3(x, 0, y);
                            gameObject.transform.localEulerAngles = new Vector3(0, 90 * rotate, 0);
                        }
                    }
                    furniture = tfurniture;
                }
            }
            creat = false;
        }
    }
    private void OnDestroy()
    {
        this.thread1.Abort();
    }
    //网络连接线程
    static void Thread1()
    {

        Socket ReceiveSocket;
        int port = 8886;
        IPAddress ip = IPAddress.Any;  // 侦听所有网络客户接口的客活动
        ReceiveSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//使用指定的地址簇协议、套接字类型和通信协议   <br>            ReceiveSocket.SetSocketOption(SocketOptionLevel.Socket,SocketOptionName.ReuseAddress,true);  //有关套接字设置
        IPEndPoint endPoint = new IPEndPoint(ip, port);
        ReceiveSocket.Bind(endPoint); //绑定IP地址和端口号
        ReceiveSocket.Listen(10);  //设定最多有10个排队连接请求
        Socket socket = ReceiveSocket.Accept();

        while (true)
        {
            byte[] receive = new byte[1024];
            socket.Receive(receive);
            string tdata = Encoding.UTF8.GetString(receive);
            creat = true;
            data = tdata;
            byte[] send = Encoding.ASCII.GetBytes("Success receive the message,send the back the message\n");
            socket.Send(send, 0);
        }
    }
}
