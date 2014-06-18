using UnityEngine;
using System.Collections;

//The delegate for WaitCallbacks
//Make methods with the same return type & parameters for call backs after the wait has finished.
public delegate void WaitCallback(MonoBehaviour aSender, WaitCallBackArgs aArgs);


public class WaitCallBackArgs
{
    private string m_Message;
    private float m_Time;

    public WaitCallBackArgs(float aTime)
    {
        m_Message = string.Empty;
        m_Time = aTime;
    }
    public WaitCallBackArgs(string aMessage, float aTime)
    {
        m_Message = aMessage;
        m_Time = aTime;
    }

    public string message
    {
        get { return m_Message; }
        set { m_Message = value; }
    }
    public float time
    {
        get { return m_Time; }
        set { m_Time = value; }
    }
}