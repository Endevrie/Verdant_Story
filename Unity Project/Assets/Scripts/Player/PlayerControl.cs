using UnityEngine;
using System;
using System.Collections.Generic;
using OnLooker;
using System.Linq;
using System.Linq.Expressions;




[Serializable()]
public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    private OrbitCamera m_OrbitCamera;
    [SerializeField]
    private UnitMotor m_UnitMotor;
    private List<Interactive> m_InteractiveObjects = new List<Interactive>();

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

    Interactive m_FocusedObject = null;
    Interactive m_ObjectInUse = null;



    // Use this for initialization
    void Start()
    {


    }

    void OnTriggerEnter(Collider aCollider)
    {
        Interactive triggeringObject = aCollider.GetComponent<Interactive>();
        if (triggeringObject != null)
        {
            triggeringObject.onPlayerEnter(this);
            m_InteractiveObjects.Add(triggeringObject);
        }
    }
    void OnTriggerStay(Collider aCollider)
    {
        Interactive triggeringObject = aCollider.GetComponent<Interactive>();
        if (triggeringObject != null)
        {
            triggeringObject.onPlayerStay(this);
        }
    }
    void OnTriggerExit(Collider aCollider)
    {
        Interactive triggeringObject = aCollider.GetComponent<Interactive>();
        if (triggeringObject != null)
        {
            triggeringObject.onPlayerExit(this);
            m_InteractiveObjects.Remove(triggeringObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_UnitMotor == null)
        {
            Debug.Log("Please attach a unit motor to this script");
            return;
        }

        

        if (Input.GetKeyDown(m_UnitMotor.useKey))
        {
            
            if (m_ObjectInUse != null)
            {
                stopUsing();
                return;
            }
            if (m_InteractiveObjects.Count == 0)
            {
                return;
            }
            m_InteractiveObjects.Sort(delegate(Interactive x, Interactive y)
            {
                return Vector3.Distance(this.transform.position, x.transform.position).CompareTo((Vector3.Distance(this.transform.position, y.transform.position)));
            });
            for (int i = 0; i < m_InteractiveObjects.Count; i++)
            {
                if (m_InteractiveObjects[i].condition(transform, m_InteractiveObjects[i].transform))
                {
                    use(m_InteractiveObjects[i]);
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void FixedUpdate()
    {
        if (m_OrbitCamera == null)
        {
            Debug.Log("Please attach a camera with an Orbit Camera script to this script");
            return;
        }

        Vector3 origin = m_OrbitCamera.transform.position;
        Vector3 direction = m_OrbitCamera.transform.forward;
        
        //The layer mask is for objects this raycast is to check for only.
        int m_LayerMask = 1 << 8;
        RaycastHit hit;
        //Raycast from the cameras current position forwards to see if an interactive object gets hit
        if (Physics.Raycast(origin, direction, out hit, 500.0f, m_LayerMask))
        {
            //What transform was hit
            Transform hitForm = hit.transform;
            //What point was hit xyz
            Vector3 hitPoint = hit.point;
            Interactive interactiveObject = hitForm.GetComponent<Interactive>();
            if (interactiveObject != null)
            {
                checkForObject(interactiveObject);
            }
            else
            {
                Transform parent = hitForm.parent;
                if (parent != null)
                {
                    interactiveObject = parent.GetComponent<Interactive>();
                    if (interactiveObject != null)
                    {
                        checkForObject(interactiveObject);
                    }
                }
            }
        }
        //No Valid collider
        else
        {
            if (m_FocusedObject != null)
            {
                m_FocusedObject.onPlayerFocusExit(this);
                m_FocusedObject = null;
            }
        }
    }

    public void checkForObject(Interactive aObject)
    {
        //This is an interactiveObject
        if (aObject != null)
        {
            //Do we already have an interactive object?
            if (m_FocusedObject == null)
            {
                //Make an entry call
                //Make a stay call
                m_FocusedObject = aObject;
                m_FocusedObject.onPlayerFocusEnter(this);
                m_FocusedObject.onPlayerFocus(this);
            }
            else if (aObject == m_FocusedObject)
            {
                //Make a stay call
                m_FocusedObject.onPlayerFocus(this);
            }
            //Not Equal and not null
            else if (aObject != m_FocusedObject)
            {
                //Make an exit call on the current focused object
                m_FocusedObject.onPlayerFocusExit(this);
                //Make stay and entry call on the new interactive object
                m_FocusedObject = aObject;
                m_FocusedObject.onPlayerFocusEnter(this);
                m_FocusedObject.onPlayerFocus(this);
            }
        }//end if This is an interactiveObject
    }

    //Helper Methods for disabling / enabling the two components of the Unit safely.
    /// <summary>
    /// Wrapped function to disable the camera safely
    /// </summary>
    public void disableCamera()
    {
        if (m_OrbitCamera != null)
        {
            m_OrbitCamera.enabled = false;
        }
    }
    /// <summary>
    /// Wrapped function to enable the camera safely
    /// </summary>
    public void enableCamera()
    {
        if (m_OrbitCamera != null)
        {
            m_OrbitCamera.enabled = true;
        }
    }
    /// <summary>
    /// Wrapped function to disable the unit motor safely
    /// </summary>
    public void disableUnitMotor()
    {
        if (m_UnitMotor != null)
        {
            m_UnitMotor.deactivateMotor();
        }
    }
    /// <summary>
    /// Wrapped function to enable the unit motor safely
    /// </summary>
    public void enableUnitMotor()
    {
        if (m_UnitMotor != null)
        {
            m_UnitMotor.activateMotor();
        }
    }

    //Disables the player Camera and unit motor
    //Invokes an the interactive object's onUseEnd
    private void use(Interactive aInteractiveObject)
    {
        if (aInteractiveObject == null)
        {
            return;
        }
        if (m_ObjectInUse != null)
        {
            m_ObjectInUse.onUseEnd(this);
        }
        m_ObjectInUse = aInteractiveObject;
        m_ObjectInUse.onUse(this);
        disableCamera();
        disableUnitMotor();
    }
    private void stopUsing()
    {
        if (m_ObjectInUse != null)
        {
            m_ObjectInUse.onUseEnd(this);
        }
        m_ObjectInUse = null;
        enableCamera();
        enableUnitMotor();
    }


}