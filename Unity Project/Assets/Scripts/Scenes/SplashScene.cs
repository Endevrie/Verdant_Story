using UnityEngine;
using System.Collections;

public class SplashScene : MonoBehaviour 
{
    private float m_CurrentTime = 0.0f;
    public float m_TransitionTime = 3.0f;
    public Texture m_Background = null;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        m_CurrentTime += Time.deltaTime;
        if (m_CurrentTime >= m_TransitionTime)
        {
            Application.LoadLevel("MainMenu");
        }
	}

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0.0f, 0.0f, Screen.width, Screen.height), m_Background);
    }
}
