using UnityEngine;
public class Rotation : MonoBehaviour
    {
        void Update()
        {
		
            /*print(array[0]);
            print(array[1]);
            print(array[2]);
            print("========================");*/
            if (transform.eulerAngles.y < 0)
            {
                TCPClient.array[3] = 360 - (-transform.eulerAngles.y % 360);
            }
            else
            {
                TCPClient.array[3] = transform.eulerAngles.y % 360;
            }
            
        }
    }
