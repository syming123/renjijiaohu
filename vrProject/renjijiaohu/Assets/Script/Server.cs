using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
namespace Script
{
    public class Server : MonoBehaviour
    {
        static Socket ReceiveSocket;
        void Start()
        {
            Thread thread1 = new Thread(new ThreadStart(Thread1));
            thread1.Start();
        }
        static void Thread1()
        {
            int port = 8885;
            //侦听所有网络客户接口的客活动
            IPAddress ip = IPAddress.Any;
            //有关套接字设置
            ReceiveSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endPoint = new IPEndPoint(ip, port);
            ReceiveSocket.Bind(new IPEndPoint(ip, port)); //绑定IP地址和端口号
            ReceiveSocket.Listen(10);  //设定最多有10个排队连接请求
            Console.WriteLine("建立连接");
            Socket socket = ReceiveSocket.Accept();
 
            byte[] receive = new byte[1024];
            socket.Receive(receive);
            
            string message = Encoding.UTF8.GetString(receive, 0, receive.Length);
            

            Console.WriteLine("接收到消息：" + Encoding.UTF8.GetString(receive));
            byte[] send = Encoding.UTF8.GetBytes("成功接收消息，并回发消息。");
            socket.Send(send);
            Console.WriteLine("发送消息为：" + Encoding.UTF8.GetString(send));
        }
    }

    }
