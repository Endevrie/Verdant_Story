using UnityEngine;
using System.Collections;

public class Sunflower : Interactive
{
    private bool m_IsUsing = false;

    [SerializeField]
    private Transform m_DummyHead = null;
    [SerializeField]
    private Transform m_Camera = null;
    private MaterialSwitch m_Head = null;
    private MaterialSwitch m_Stem = null;
    [SerializeField]
    private float m_MinY = -30.0f;
    [SerializeField]
    private float m_MaxY = 80.0f;
    [SerializeField]
    private float m_CurrentRotation = 0.0f;
    [SerializeField]
    private float m_RotationSpeed = 4.0f;

    [SerializeField]
    private string m_XRotationAxis = "Mouse X";
    [SerializeField]
    private string m_YRotationAxis = "Mouse Y";

	// Use this for initialization
	void Start () 
    {
        Transform [] transform = GetComponentsInChildren<Transform>();

        int transformsFound = 0;

        for (int i = 0; i < transform.Length; i++)
        {
            if (transform[i].name == "D_Head")
            {
                m_DummyHead = transform[i];
                transformsFound++;
            }
            if (transform[i].name == "C_Camera")
            {
                m_Camera = transform[i];
                transformsFound++;
            }
            if(transform[i].name == "M_Head")
            {
                m_Head = transform[i].GetComponent<MaterialSwitch>();
                transformsFound++;
            }
            if(transform[i].name == "M_Stem")
            {
                m_Stem = transform[i].GetComponent<MaterialSwitch>();
                transformsFound++;
            }
            if (transformsFound >= 4)
            {
                break;
            }
        }
        m_Camera.gameObject.SetActive(false);
	}

    // Update is called once per frame
    void Update() 
    {
        if (m_IsUsing == true && m_DummyHead != null)
        {
            float x = Input.GetAxis(m_XRotationAxis) * Time.deltaTime * m_RotationSpeed;
            float y = Input.GetAxis(m_YRotationAxis) * Time.deltaTime * m_RotationSpeed;

            float tryRotation = m_CurrentRotation + y;
            if (tryRotation > m_MaxY)
            {
                return;
            }
            else if (tryRotation < m_MinY)
            {
                return;
            }
            else
            {
                m_CurrentRotation = tryRotation;
            }

            transform.Rotate(0.0f, x, 0.0f);
            m_DummyHead.Rotate(y, 0.0f, 0.0f);
            

        }
	}

    public override void onPlayerEnter(PlayerControl aPlayer)
    {
        //Debug.Log("Sunflower Enter");
    }
    public override void onPlayerStay(PlayerControl aPlayer)
    {
        //Debug.Log("Sunflower Stay");
    }
    public override void onPlayerExit(PlayerControl aPlayer)
    {
        //Debug.Log("Sunflower Exit");
    }
    public override void onPlayerFocus(PlayerControl aPlayer)
    {
        //Debug.Log("Sunflower Focus");
        if (m_Head != null && m_Stem != null)
        {
            m_Head.setOther(0);
            m_Stem.setOther(0);
        }
    }
    public override void onPlayerFocusEnter(PlayerControl aPlayer)
    {
        //Debug.Log("Sunflower Focus Enter");
    }
    public override void onPlayerFocusExit(PlayerControl aPlayer)
    {
        //Debug.Log("Sunflower Focus Exit");
        if (m_Head != null && m_Stem != null)
        {
            m_Head.setDefault();
            m_Stem.setDefault();
        }
    }
    public override void onUse(PlayerControl aPlayer)
    {
        Debug.Log("Sunflower On Use");
        if (m_Camera == null)
        {
            Debug.Log("Please attach a camera to this script");
            aPlayer.stopUsing();
            return;
        }
        m_Camera.gameObject.SetActive(true);
        m_IsUsing = true;

        
    }
    public override void onUseEnd(PlayerControl aPlayer)
    {
        Debug.Log("Sunflower On Use End");
        m_IsUsing = false;
        m_Camera.gameObject.SetActive(false);
    }
}
