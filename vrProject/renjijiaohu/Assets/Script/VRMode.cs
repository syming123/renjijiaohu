using UnityEngine;
using UnityEngine.VR;
using UnityEngine.XR;

public class VRMode : MonoBehaviour
{
    public Camera vrCamera;

    //当前是否在VR模式下
    public static bool VrMode => XRSettings.enabled;
    
    /*private void Reset()
    {
        vrCamera = Camera.main;
    }*/

    // Start is called before the first frame update
    void Start()
    {
        SwitchTo2D();
    }

    /*private void Update()
    {
#if UNITY_EDITOR
#else
        //在2D模式下，需要自己控制摄像机的旋转
        if (!VrMode)
        {
            Quaternion headQuaternion = UnityEngine.XR.InputTracking.GetLocalRotation(UnityEngine.XR.XRNode.Head);
            vrCamera.transform.rotation = Quaternion.Slerp(transform.rotation, headQuaternion, 10);
        }
#endif
    }

    /// <summary>
    /// 进入VR画面
    /// 进入VR画面
    /// </summary>
    public static void SwitchToVR()
    {
        XRSettings.enabled = true;
    }*/

    /// <summary>
    /// 进入2D画面
    /// </summary>
    public static void SwitchTo2D()
    {
        XRSettings.enabled = false;
    }

    /*void OnGUI()
    {
        int width = 200;
        int height = 50;
        int left = Screen.width / 2 - width / 2;
        int top = Screen.height - height - 10;

        string text = VrMode ? "To 2D" : "To VR";

        if (GUI.Button(new Rect(left, top, width, height), text))
        {
#if UNITY_EDITOR
#else
            if (VrMode == true)
            {
                SwitchTo2D();
            }
            else
            {
                SwitchToVR();
            }
#endif
        }
    }*/
}