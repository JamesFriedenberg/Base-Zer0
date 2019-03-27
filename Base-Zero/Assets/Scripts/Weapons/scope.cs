using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scope : MonoBehaviour
{
    public bool upgrade = false;

    //the name of the scope (in the canvas), if there is no scope name then when you ADS no scope will appear
    public string scopeImage;

    //the field of view of the camera when you ADS
    public int fov = 30;

    //the sensitivity of the player while ADS
    public float sensativity;

    //changes the position of the gun in case of tall site
    public Vector3 newTransform = Vector3.zero;
}
