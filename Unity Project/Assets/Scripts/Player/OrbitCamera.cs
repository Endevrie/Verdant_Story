using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class OrbitCamera : MonoBehaviour 
{
    //The target to follow
    [HideInInspector]
    [SerializeField]
    private Transform m_Target = null;
    //Rotation Speeds
    [HideInInspector]
    [SerializeField]
    private float m_XSpeed = 120.0f;
    [HideInInspector]
    [SerializeField]
    private float m_YSpeed = 120.0f;
    //Min max y angles
    [HideInInspector]
    [SerializeField]
    private float m_YMinLimit = -20.0f;
    [HideInInspector]
    [SerializeField]
    private float m_YMaxLimit = 80.0f;
    //Current Angles
    [HideInInspector]
    [SerializeField]
    private float m_X = 0.0f;
    [HideInInspector]
    [SerializeField]
    private float m_Y = 0.0f;

    //Min max zoom distance
    [HideInInspector]
    [SerializeField]
    private float m_MinDistance = 0.5f;
    [HideInInspector]
    [SerializeField]
    private float m_MaxDistance = 15.0f;
    [HideInInspector]
    [SerializeField]
    private float m_ZoomSpeed = 5.0f;
    //Current Distance
    [HideInInspector]
    [SerializeField]
    private float m_Distance = 5.0f;
    [HideInInspector]
    [SerializeField]
    private float m_TargetDistance = 5.0f;
    [HideInInspector]
    [SerializeField]
    private bool m_InCollision = false;

    //Input Names
    [HideInInspector]
    [SerializeField]
    private string m_XRotationInputName = "Mouse X";
    [HideInInspector]
    [SerializeField]
    private string m_YRotationInputName = "Mouse Y";
    [HideInInspector]
    [SerializeField]
    private string m_ZoomInputName = "Mouse ScrollWheel";


    public Transform target
    {
        get { return m_Target; }
        set{m_Target = value;}
    }
    public float distance
    {
        get { return m_Distance; }
    }
    public float targetDistance
    {
        get { return m_TargetDistance; }
    }
    public bool inCollision
    {
        get { return m_InCollision; }
    }
    public float yMinLimit
    {
        get { return m_YMinLimit; }
        set { m_YMinLimit = value; }
    }
    public float yMaxLimit
    {
        get { return m_YMaxLimit; }
        set { m_YMaxLimit = value; }
    }
    public float maxDistance
    {
        get { return m_MaxDistance; }
        set { m_MaxDistance = value; }
    }
    public float zoomSpeed
    {
        get { return m_ZoomSpeed; }
        set { m_ZoomSpeed = value; }
    }
    public string xRotationInputName
    {
        get { return m_XRotationInputName; }
        set { m_XRotationInputName = value; }
    }
    public string yRotationInputName
    {
        get { return m_YRotationInputName; }
        set { m_YRotationInputName = value; }
    }
    public string zoomInputName
    {
        get { return m_ZoomInputName; }
        set { m_ZoomInputName = value; }
    }


    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        m_X = angles.y;
        m_Y = angles.x;

        // Make the rigid body not change rotation
        if (rigidbody)
        {
            rigidbody.freezeRotation = true;
        }
    }

    void LateUpdate()
    {
        if (m_Target)
        {
            //Get Rotation Input
            m_X += Input.GetAxis(m_XRotationInputName) * m_XSpeed * m_Distance * 0.02f;
            m_Y -= Input.GetAxis(m_YRotationInputName) * m_YSpeed * 0.02f;
            m_Y = Game.clampAngle(m_Y, m_YMinLimit, m_YMaxLimit);

            //Calculate Rotation
            Quaternion rotation = Quaternion.Euler(m_Y, m_X, 0);

            
            //Calculate distance
            m_TargetDistance = Mathf.Clamp(m_TargetDistance - Input.GetAxis(m_ZoomInputName) * m_ZoomSpeed, m_MinDistance, m_MaxDistance);
            if (m_InCollision == false)
            {
                m_Distance = Mathf.Lerp(m_Distance, m_TargetDistance, 4.0f * Time.deltaTime);
            }
            

            //Do a line cast to see if there is an object between the target and the camera
            //Adjust the position so the camera doesnt clip through walls
            RaycastHit hit;
            if (Physics.Linecast(m_Target.position, transform.position, out hit))
            {
                m_Distance = hit.distance;
                m_InCollision = true;
            }
            else
            {
                m_InCollision = false;
            }
            Vector3 negativeDistance = new Vector3(0.0f, 0.0f, -m_Distance);
            Vector3 position = rotation * negativeDistance + m_Target.position;

            //Set the position and rotation
            transform.rotation = rotation;
            transform.position = position;
        }

    }

    
}
