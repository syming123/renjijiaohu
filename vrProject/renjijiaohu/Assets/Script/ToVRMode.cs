using UnityEngine;
using UnityEngine.VR;
using UnityEngine.XR;

public class ToVRMode : MonoBehaviour
{
    public Camera vrCamera;

    //当前是否在VR模式下
    public static bool VrMode => XRSettings.enabled;
    
    
    void Start()
    {
        SwitchToVR();
    }
    
    /// <summary>
    /// 进入VR画面
    /// 进入VR画面
    /// </summary>
    public static void SwitchToVR()
    {
        XRSettings.enabled = true;
    }
    
    
}
