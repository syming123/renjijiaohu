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
    int[]furnitureWidth=new int[] {1,1,1,1,2,2,2,2,1,1,1,1,1,3,2,3,1,2,2,1,3,2,2};
    int[]furnitureHeight=new int[] {1,1,1,1,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1};
    int[] rotated = new int[] {0,2,0,0,3,3,1,0,0,0,0,0,0,1,1,1,2,1,2,1,2,1,0};

    static bool newMessage=false;
    static string data;
    Thread thread1;
    Room room;
    Furniture[] furniture=null;
    float roomWidth;
    float roomHeight;
    void Start()
    {
        Thread thread1 = new Thread(new ThreadStart(Thread1));
        thread1.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (newMessage)
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

                    go = GameObject.Find("PointLight");
                    go.transform.position=new Vector3(roomWidth / 2, 5, -(roomHeight / 2));
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
                        int id=tfurniture[i].id;
                        float x=0, y=0;
                        if (rotate==1||rotate==3)
                        {
                            x = tfurniture[i].x + ((float)furnitureHeight[classID] / 2)-1;
                            y = -(tfurniture[i].y + ((float)furnitureWidth[classID] / 2-1));
                        }
                        else if (rotate == 2 || rotate == 0)
                        {
                            x = tfurniture[i].x + ((float)furnitureWidth[classID]/2)-1;
                            y = -(tfurniture[i].y+ ((float)furnitureHeight[classID] / 2-1));
                        }
                        go = Resources.Load<GameObject>(""+classID);
                        gameObject1 = Instantiate(go);
                        gameObject1.name = "" + id;
                        gameObject1.transform.position=new Vector3(x,0,y);  
                        gameObject1.transform.localEulerAngles = new Vector3(0, 90 * (rotate + rotated[classID]),0);
                    }
                }
                //删除家具
                if (furniture != null && furniture.Length > tfurniture.Length)
                {
                    for (int i = 0; i < furniture.Length; i++)
                    {
                        int id = furniture[i].id;
                        bool findID = false;
                        for (int j = 0; j < tfurniture.Length; j++)
                        {
                            if (tfurniture[j].id == id)
                            {
                                findID = true;
                                break;
                            }
                        }
                        if (!findID)
                        {
                            go = GameObject.Find("" + id);
                            GameObject.Destroy(go);
                        }
                    }
                    
                }
                //修改家具信息
                else if(furniture != null&& furniture.Length == tfurniture.Length)
                {
                    for(int i = 0; i < furniture.Length; i++)
                    {
                        
                        int classID = tfurniture[i].classID;
                        int rotate = tfurniture[i].rotate;
                        float x=tfurniture[i].x;
                        float y = tfurniture[i].y;
                        int id = tfurniture[i].id;
                        if (furniture[i].x != x|| furniture[i].y != y)
                        {
                            if (rotate == 1 || rotate == 3)
                            {
                                x = tfurniture[i].x + ((float)furnitureHeight[classID] / 2-1);
                                y = -(tfurniture[i].y + ((float)furnitureWidth[classID] / 2-1));
                            }
                            else if (rotate == 2 || rotate == 0)
                            {
                                x = tfurniture[i].x + ((float)furnitureWidth[classID] / 2-1);
                                y = -(tfurniture[i].y + ((float)furnitureHeight[classID] / 2 - 1));
                            }
                            GameObject gameObject = GameObject.Find(""+id);
                            gameObject.transform.position = new Vector3(x, 0, y);
                        }else if (furniture[i].rotate!=rotate)
                        {
                            if (rotate == 1 || rotate == 3)
                            {
                                x = tfurniture[i].x + ((float)furnitureHeight[classID] / 2 - 1);
                                y = -(tfurniture[i].y + ((float)furnitureWidth[classID] / 2 - 1));
                            }
                            else if (rotate == 2 || rotate == 0)
                            {
                                x = tfurniture[i].x + ((float)furnitureWidth[classID] / 2 - 1);
                                y = -(tfurniture[i].y + ((float)furnitureHeight[classID] / 2 - 1));
                            }
                            GameObject gameObject = GameObject.Find("" + id);
                            gameObject.transform.position = new Vector3(x, 0, y);
                            gameObject.transform.localEulerAngles = new Vector3(0, 90 * (rotate + rotated[classID]), 0);
                        }
                    }  
                }
                furniture = tfurniture;
            }
            newMessage = false;
        }
    }
    void OnDestroy()
    {
        
    }
    private void OnApplicationQuit()
    {
        thread1.Abort();
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
            newMessage = true;
            data = tdata;
            byte[] send = Encoding.ASCII.GetBytes("Success receive the message,send the back the message\n");
            socket.Send(send, 0);
        }
    }
}
