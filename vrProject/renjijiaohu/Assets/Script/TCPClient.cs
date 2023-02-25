//=============================== WXY. ==================================
//
// Purpose: 此脚本用于向接收服务端发送来的数据
//          
//=======================================================================
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;


public class TCPClient : MonoBehaviour
{

	IPEndPoint iPEndPoint;
	Socket socket;
	Thread receiveThread;
	/*
	Thread Thread;
	*/
	/*
	private string message1;
	*/
	char[] x = new char[128];
	char[] y = new char[128];
	char[] z = new char[128];
	int count = 0;
	public static float[] array = new float[4];
	byte[] buffer; //缓冲区，存储接收到的数据
	public static bool quit = false; //系统连接标识
	/*
	public static string message; //客户端收到的信息
	*/
	/*
	public string _ip = "192.168.238.133"; //IP地址——用户自行修改
	*/
	public static string _ip = ""; //IP地址——用户自行修改
	public static int _port = 9999; //端口号，与服务端保持一致

	// Use this for initialization
	
	
	/*private Thread T;
 
	void Start () 
	{
		T = new Thread(ThreadTest);
		T.Start();
	}
	
	void ThreadTest()
	{
		for(int i = 0; i < 3; i++)
		{
			Debug.Log("子线程运行了");
		}
	}*/
	
	void Start()
	{
		try
		{
			string param = GameObject.Find("GameData").GetComponent<GameData>().param;
			print(param);
			_ip = param;
			/*GameObject singleton = new GameObject();
			TCPClient tCPClient = singleton.AddComponent<TCPClient>(); // 实例化T的对象*/
			TCPClient tCPClient = new TCPClient();
			tCPClient.Init();
			//创建接收线程
			receiveThread = new Thread(new ThreadStart(tCPClient.Receive));
			receiveThread.Start();
			
		}
		catch (SocketException e)
		{
			Console.WriteLine(e.Message);
		}
	}
	void Update()
	{
		
        
		if (SocketConnect.roomWidth1 == 0)
		{
				transform.position = new Vector3(5, 1.36144f, -5);

		}
		else
		{
			/*if (count % 2 == 0)
			{
				transform.position = new Vector3(5, 1.36144f, -5);
				count = (count + 1) % 2;
			}
			else
			{
				transform.position = new Vector3(5, 1.36144f, -5.1f);
				count = (count + 1) % 2;
			}*/
			if (array[0]==-1)
			{

			}
			else
			{
                //transform.position = new Vector3(5, 1.36144f, -5);
                transform.position = new Vector3(array[0] * 2 + (SocketConnect.roomWidth1 / 2), 1.36144f, (-array[2]) * 2);
            }
            
        }
        
        /*print(array[0]);
        print(array[1]);
        print(array[2]);
        print("========================");*/
        
        /*
        transform.position += new Vector3(1.5f,0,0);
    */


        /*
		transform.Translate(array[0],array[1],array[2]);
		*/
        //或
        /*
		transform.position += new Vector3(array[0],array[1],array[2]);
	*/
    }
	
	/// <summary>
	/// 与服务端建立连接
	/// </summary>
	public void Init()
	{
		try
		{
			socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			iPEndPoint = new IPEndPoint(IPAddress.Parse(_ip), _port);
			socket.Connect(iPEndPoint);
		}
		catch (SocketException e)
		{
			Debug.Log(e.Message);
		}

	}

	/// <summary>
	/// 接受服务端消息
	/// </summary>
	/// <param name="message">客户端接收到的字节数据</param>
	/// <returns></returns>
	public void  Receive()
	{
		while (!quit)
		{
			if (quit) break;
			buffer = new byte[1024*1024];
			//获取信息长度
			int n = socket.Receive(buffer);
			//byte转String
			string message = Encoding.UTF8.GetString(buffer, 0, n);
			array = Split_data(message);
			
			//数据分割函数，用户自行编写
			/*string message1 = "K#1#(0.11, 0.60, 1.27)#+@K#1#(0.11, 0.60, 1.27)#+@K#1#(0.11, 0.60, 1.27)#+@";
			Split_data(message1);*/
			/*
			print(message);
			*/

			/*print("------------------------------");
			print(array[0]);
			print(array[1]);
			print(array[2]);
			print("------------------------------");*/
			/*print(message);*/
		}

	}
	
	
	void OnApplicationQuit()
	{
		quit = true;
	}

	public static float IsNumeric(string str)
	{
			/*float i;
			if (str != null && System.Text.RegularExpressions.Regex.IsMatch(str, @"^-?\d+$"))
				i = float.Parse(str);
			else
				return -1;
			return i;*/
			try
			{
				float i;
				i = float.Parse(str);
				return i;
			}
			catch
			{
				return -1;
			}
	}

	public static float[] Split_data(string message)
        {

            string x="";
            string y="";
            string z="";
            
            int temp = 0;
            
            for (int i = 5; i < message.Length; i++) {
                
                
                if (message[i].Equals(','))
                {
                    temp++;
                }

                if (temp==0)
                {
                    if (!message[i].Equals(',')) {
                        
                            x += message[i];
                    }
                }
                
                if (temp==1)
                {
                    if (!message[i].Equals(',')&!message[i].Equals(' ')) {
                        
                        y += message[i];
                    }
                }
                
                if (temp==2)
                {
                    if (!message[i].Equals(',')&!message[i].Equals(' ')) {
                        
                        if (message[i].Equals(')'))
                        {
                            break;
                        }
                        z += message[i];
                    }
                    
                }
                 /*else {
                    if (!message[i + 1].Equals(',')) {
                        y[i - 6] = message[i];
                    } else {
                        if (!message[i + 2].Equals(',')) {
                            z[i - 7] = message[i];
                        }
                        else
                        {
                            break;
                        }
                    }

                }*/


            }
		
            /*K#1#(-0.44 0.96, 1.19)#+@K#1#(-0.44, 0.96, 1.19)#+@K#1#(-0.4*/
            /*Console.WriteLine(x);
            Console.WriteLine(y);
            Console.WriteLine(z);*/

            float[] array = new float[4];
            float xx= IsNumeric(x);
			float yy= IsNumeric(y);
			float zz= IsNumeric(z);
			array[0] = xx;
			array[1] = yy;
			array[2] = zz;
			array[3] = 0;
            /*print(x+"\n");
            print(y+"\n");
            print(z+"\n");*/
            /*print(xx+"\n");
            print(yy+"\n");
            print(zz+"\n");
            print(xyz+"\n");*/
            return array;
            /*
             * 数据分割说明：
                一层分割——“@”字符分割为单条数据；
                二层分割——“+”字符进行不同设备数据之间的分割，得到单个设备数据，以设备标识字符（G、K...）进行区分；
                三层分割——“#”字符进行单个数据的分割。
            */

        }
	
	
	/*public static float[] Split_data(string message)
	{

		string x = "";
		string y = "";
		string z = "";

		int temp = 0;
		float[] array = new float[3];

		for (int i = 5; i < message.Length; i++)
		{


			if (message[i].Equals(','))
			{
				temp++;
			}

			if (temp == 0)
			{
				if (!message[i].Equals(','))
				{

					x += message[i];
				}
			}

			if (temp == 1)
			{
				if (!message[i].Equals(',')&!message[i].Equals(' '))
				{

					y += message[i];
				}
			}

			if (temp == 2)
			{
				if (!message[i].Equals(','))
				{

					if (message[i].Equals(')')&!message[i].Equals(' '))
					{
						break;
					}

					z += message[i];
				}

			}

			
			/*float xx = Convert.ToSingle(x);
			float yy = Convert.ToSingle(y);
			float zz = Convert.ToSingle(z);
			array[0] = xx;
			array[1] = yy;
			array[2] = zz;
			print(xx);
			print(yy);
			print(zz);#1#
			print(x+"\n");
			print(y+"\n");
			print(z+"\n");
			/*print(message+"\n");
			print("=================");#1#

			/*Console.WriteLine(x);
			Console.WriteLine(y);
			Console.WriteLine(z);#1#
			/*print(x);
			print(y);
			print(z);#1#
			/*
			 * 数据分割说明：
			    一层分割——“@”字符分割为单条数据；
			    二层分割——“+”字符进行不同设备数据之间的分割，得到单个设备数据，以设备标识字符（G、K...）进行区分；
			    三层分割——“#”字符进行单个数据的分割。
			#1#

		}

		return array;
	}*/
	
	
}
