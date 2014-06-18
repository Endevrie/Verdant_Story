using UnityEngine;
using System.Collections;



public class MainMenu : MonoBehaviour 
{
    public Vector2 m_ButtonSize = Vector2.zero;
    public float m_Offset;

    public string[] m_ButtonNames;
    public Texture m_Background = null;
    private GUIButtonCallback m_ButtonCallback;



    private delegate void GUIButtonCallback(string aName, int aIndex);


    
	// Use this for initialization
	void Start () 
    {
        m_ButtonCallback = onButtonClick;
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}


    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0.0f, 0.0f, Screen.width, Screen.height), m_Background);

        float startX = Screen.width * 0.5f - m_ButtonSize.x * 0.5f;
        float startY = Screen.height * 0.5f - (m_ButtonSize.y * 0.5f * m_ButtonNames.Length - m_Offset);

        Rect buttonRect = new Rect(startX, startY, m_ButtonSize.x, m_ButtonSize.y);

        for (int i = 0; i < m_ButtonNames.Length; i++)
        {
            if (GUI.Button(buttonRect, m_ButtonNames[i]))
            {
                m_ButtonCallback.Invoke(m_ButtonNames[i], i);
            }
            buttonRect.y += m_ButtonSize.y + m_Offset;
        }
        

    }

    void onButtonClick(string aName, int aIndex)
    {
        if (aName == "Play")
        {
            //Debug.Log("Play pressed");
            Application.LoadLevel("ProfileSelect");
        }
        if (aName == "Options")
        {
            //Debug.Log("Options pressed");
            Application.LoadLevel("Options_MainMenu");
        }
        if (aName == "Quit")
        {
            Debug.Log("Quit pressed");
        }
    }
}
