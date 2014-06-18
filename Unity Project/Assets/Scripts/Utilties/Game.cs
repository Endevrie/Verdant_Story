﻿using UnityEngine;
using System.Collections;
using OnLooker;




public class Game : MonoBehaviour
{

    public static IEnumerator wait(MonoBehaviour aSender, WaitCallback aCallback, WaitCallBackArgs aArgs)
    {
        
        if (aArgs != null)
        {
            Debug.Log("Wait - Begin");
            yield return new WaitForSeconds(aArgs.time);
            if (aCallback != null)
            {
                aCallback.Invoke(aSender, aArgs);
            }
            Debug.Log("Wait - End");
        }

    }

    public static float clampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

    private void Start()
    {
        //TODO: All Game Startup stuff here
    }

    private void Update()
    {
        //TODO: Any global game updates here
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            bool cursorLock = Screen.lockCursor = !Screen.lockCursor;
            if (cursorLock == true)
            {
                Debug.Log("Locked mouse cursor");
            }
            else
            {
                Debug.Log("Unlocked mouse cursor");
            }
        }
    }
    private void OnGUI()
    {
        GUIManager.instance.update();
    }

    
}
