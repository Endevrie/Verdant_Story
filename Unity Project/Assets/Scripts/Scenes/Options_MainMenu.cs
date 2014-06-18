using UnityEngine;
using System.Collections;

public class Options_MainMenu : MonoBehaviour 
{

    public Texture m_Background = null;

    

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}


    void OnGUI()
    {
        if (m_Background != null)
        {
            GUI.DrawTexture(new Rect(0.0f, 0.0f, Screen.width, Screen.height), m_Background);
        }


        if (GUI.Button(new Rect(60.0f, 60.0f, 100.0f, 45.0f), "Back"))
        {
            Application.LoadLevel("MainMenu");
        }
    }
}
