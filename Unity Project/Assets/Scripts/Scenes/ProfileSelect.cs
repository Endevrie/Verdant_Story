using UnityEngine;
using System.Collections;

public class ProfileSelect : MonoBehaviour 
{
    //Button Size and Offset Variables
    public Vector2 m_ButtonSize;
    public float m_Offset = 5.0f;

    //The Texture in the background
    public Texture m_Background = null;

    //Control states between displaying the profiles and creating a profile
    private enum MenuState
    {
        PROFILE_SELECTION,
        PROFILE_CREATING
    }
    [SerializeField]
    private MenuState m_State = MenuState.PROFILE_SELECTION;



    //Profile Creation
    public string m_ProfileName;
    public int m_CurrentCreationIndex;

	// Use this for initialization
	void Start () 
    {
        ProfileManager.instance.loadProfiles();
	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}

    void OnGUI()
    {
        //Draw the background
        if (m_Background != null)
        {
            GUI.DrawTexture(new Rect(0.0f, 0.0f, Screen.width, Screen.height), m_Background);
        }
        //Check State
        if (m_State == MenuState.PROFILE_SELECTION)
        {
            //Display a back button to take the user back to the main menu
            if (GUI.Button(new Rect(60.0f, 60.0f, 100.0f, 45.0f), "Back"))
            {
                ProfileManager.instance.saveProfiles();
                Application.LoadLevel("MainMenu");
            }

            //How many profiles are available
            const int PROFILE_COUNT = 3;

            //Get Start x and y offsets
            float startX = Screen.width * 0.5f - m_ButtonSize.x * 0.5f;
            float startY = Screen.height * 0.5f - (m_ButtonSize.y * 0.5f * PROFILE_COUNT - m_Offset);

            //Create a rect for each GUILayout.Area
            Rect buttonRect = new Rect(startX, startY, m_ButtonSize.x, m_ButtonSize.y);


            //For all the profiles display the name, progression and two buttons
            for (int i = 0; i < PROFILE_COUNT; i++)
            {
                Profile profile = ProfileManager.instance.getProfile(i);

                GUI.color = Color.black;
                GUILayout.BeginArea(buttonRect);
                GUILayout.BeginHorizontal();
                //Name - Progression
                GUILayout.BeginVertical();
                if (profile != null)
                {
                    GUILayout.Label(profile.name);
                    GUILayout.Label("Progression: " + profile.progressionLevel.ToString());
                }
                else
                {
                    GUILayout.Label("Empty");
                }
                GUILayout.EndVertical();
                //Button(Create/Play - Delete)
                GUILayout.BeginVertical();
                if (profile != null)
                {
                    if (GUILayout.Button("Play"))
                    {
                        //Create profile or enter the game.
                        Debug.Log("Play");
                        ProfileManager.instance.saveProfiles();
                        Application.LoadLevel("Test_Level_00");
                    }
                    if (GUILayout.Button("Delete"))
                    {
                        //Delete the profile if there is one there.
                        Debug.Log("Delete");
                        ProfileManager.instance.removeProfile(i);
                    }
                }
                else
                {
                    if (GUILayout.Button("New"))
                    {
                        //Create profile or enter the game.
                        Debug.Log("New");
                        m_State = MenuState.PROFILE_CREATING;
                        m_CurrentCreationIndex = i;
                    }
                }

                GUILayout.EndVertical();
                GUILayout.EndHorizontal();
                GUILayout.EndArea();
                //Change rect position
                buttonRect.y += m_ButtonSize.y + m_Offset;
            }
        }
        else
        {
            if (GUI.Button(new Rect(60.0f, 60.0f, 100.0f, 45.0f), "Back"))
            {
                m_State = MenuState.PROFILE_SELECTION;
            }
            if (createProfile(m_CurrentCreationIndex))
            {
                //Load Game
                Debug.Log("Play");
            }
        }
    }

    bool createProfile(int aIndex)
    {
        const int CONTROL_COUNT = 3;
        bool success = false;

        float startX = Screen.width * 0.5f - m_ButtonSize.x * 0.5f;
        float startY = Screen.height * 0.5f - (m_ButtonSize.y * 0.5f * CONTROL_COUNT - m_Offset);

        Rect buttonRect = new Rect(startX, startY, m_ButtonSize.x, m_ButtonSize.y * 5);

        GUI.color = Color.black;
        GUILayout.BeginArea(buttonRect);
        GUILayout.BeginVertical();
        GUILayout.Label("Name");
        m_ProfileName = GUILayout.TextField(m_ProfileName);
        if (GUILayout.Button("Create") && m_ProfileName.Length != 0)
        {
            ProfileManager.instance.setProfle(aIndex, new Profile(m_ProfileName));
            m_State = MenuState.PROFILE_SELECTION;
            success = true;
        }
        GUILayout.EndVertical();
        GUILayout.EndArea();

        return success;
    }
}
