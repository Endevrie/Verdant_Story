using UnityEngine;
using System;
using System.Collections;
using OnLooker;

[Serializable()]
public class PlayerControl : MonoBehaviour 
{
    [SerializeField]
    private OrbitCamera m_OrbitCamera;
    [SerializeField]
    private UnitMotor m_UnitMotor;

    public OrbitCamera orbitCamera
    {
        get { return m_OrbitCamera; }
        set { m_OrbitCamera = value; }
    }
    public UnitMotor unitMotor
    {
        get { return m_UnitMotor; }
        set { m_UnitMotor = value; }
    }

    Button m_BTNCameraToggle = null;
    Button m_BTNUnitToggle = null;
    Button m_BTNObjectToggle = null;

    
	// Use this for initialization
	void Start () 
    {
        string layoutName = GUIManager.instance.newLayout("PlayerControl", new Rect(5.0f, 5.0f, Screen.width * 0.10f, Screen.height * 0.80f));
        m_BTNCameraToggle = GUIManager.instance.addButton(layoutName, "Camera Toggle", "btnCameraToggle");
        m_BTNUnitToggle = GUIManager.instance.addButton(layoutName, "Unit Toggle", "btnUnitToggle");
        m_BTNObjectToggle = GUIManager.instance.addButton(layoutName, "Object Toggle", "btnObjectToggle");
        GUIManager.instance.registerButtonCallback(layoutName,onButton);
        GUIManager.instance.registerTextFieldCallback(layoutName,onTextField);

	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    void OnGUI()
    {

    }

    public void disableCamera()
    {
        if (m_OrbitCamera != null)
        {
            m_OrbitCamera.enabled = false;
        }
    }
    public void enableCamera()
    {
        if (m_OrbitCamera != null)
        {
            m_OrbitCamera.enabled = true;
        }
    }
    public void disableUnitMotor()
    {
        if (m_UnitMotor != null)
        {
            m_UnitMotor.deactivateMotor();
        }
    }
    public void enableUnitMotor()
    {
        if (m_UnitMotor != null)
        {
            m_UnitMotor.activateMotor();
        }
    }



    private void onButton(Button aSender)
    {
        if (aSender == m_BTNCameraToggle)
        {
            if (m_OrbitCamera != null)
            {
                m_OrbitCamera.enabled = !m_OrbitCamera.enabled;
            }
        }
        if (aSender == m_BTNUnitToggle)
        {
            enableUnitMotor();
        }
        if (aSender == m_BTNObjectToggle)
        {
            disableUnitMotor();
        }
    }
    private void onTextField(TextField aSender, string aText)
    {

    }


}
