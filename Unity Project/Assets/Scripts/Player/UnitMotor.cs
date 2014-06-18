using UnityEngine;
using System;
using System.Collections;

public enum UnitStance
{
    STAND,
    CROUCH
}

[RequireComponent(typeof(Rigidbody))]
[Serializable]
public class UnitMotor : MonoBehaviour 
{
    private Rigidbody m_Rigidbody;
    [SerializeField]
    [HideInInspector]
    private Animation m_Animation = null;


    //Movement Controls
    [HideInInspector]
    [SerializeField]
    private float m_MovementSpeed = 2.0f;
    [HideInInspector]
    [SerializeField]
    private float m_TurnSpeed = 140.0f;
    [HideInInspector]
    [SerializeField]
    private float m_JumpHeight = 2.0f;
    [HideInInspector]
    [SerializeField]
    private float m_MaxVelocity = 10.0f;
    [HideInInspector]
    [SerializeField]
    private float m_Gravity = 9.81f;
    //The Stance the unit is in. Affects movement speed.
    [HideInInspector]
    [SerializeField]
    private UnitStance m_UnitStance = UnitStance.STAND;
    //Use this Camera for movement reference
    [HideInInspector]
    [SerializeField]
    private Transform m_Camera = null;

    //Ground Detection
    [HideInInspector]
    [SerializeField]
    private bool m_Grounded = false;
    [HideInInspector]
    [SerializeField]
    private float m_GroundedDistanceCheck = 1.0f;
    [HideInInspector]
    [SerializeField]
    private float m_GroundedRadiusSweep = 1.0f;

    //Whether or not the unit is using something
    [HideInInspector]
    [SerializeField]
    private bool m_UseState = false;

    //Input
    [HideInInspector]
    [SerializeField]
    private string m_ForwardAxis = "Vertical";
    [HideInInspector]
    [SerializeField]
    private string m_SideAxis = "Horizontal";
    [HideInInspector]
    [SerializeField]
    private string m_JumpButton = "Jump";
    [HideInInspector]
    [SerializeField]
    private KeyCode m_StanceKey = KeyCode.Alpha1;
    [HideInInspector]
    [SerializeField]
    private KeyCode m_UseKey = KeyCode.E;


    [HideInInspector]
    [SerializeField]
    private bool m_ActiveMotor = true;

    

	// Use this for initialization
	void Start () 
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        
	}

	void FixedUpdate () 
    {
        if (m_ActiveMotor == false)
        {
            onInActive();
            return;
        }

        //Quickly check to see if the unit is grounded
        m_Grounded = checkGrounded();
        //Get Inputs
        if (Input.GetKeyDown(m_StanceKey))
        {
            toggleStance();
        }
        
        //If the use key was hit we need to reset the rigid body velocity except that on the y axis. The unit
        //should come to a complete hault and begin its using animation. The player will get control back once
        //the animation is done.
        if (m_UseState == true)
        {
            m_Rigidbody.AddForce(new Vector3(0.0f, -m_Gravity * m_Rigidbody.mass, 0.0f));
            m_Animation.CrossFade("Male_PickUp");
            return;
        }

        if (m_Camera == null)
        {
            Debug.Log("Camera was null, please assign a camera");
            return;
        }

        //Check the two movement inputs
        float verticalMoveFlag = Input.GetAxis(m_ForwardAxis);
        float horizontalMoveFlag = Input.GetAxis(m_SideAxis);


        //If were moving lets move in the direction the camera is facing
        // Calculate how fast we should be moving
		Vector3 targetVelocity = new Vector3(horizontalMoveFlag, 0.0f, verticalMoveFlag);
        targetVelocity = m_Camera.TransformDirection(targetVelocity);

        //If were moving in any direction we have to adjust the camera rotation.
        //We also need to change the y value to zero so the unit doesnt rotate to weird angles.
        if (verticalMoveFlag != 0.0f || horizontalMoveFlag != 0.0f)
        {
            Vector3 moveDirection = targetVelocity;
            moveDirection.y = 0.0f;
            Quaternion newRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 8.0f);
        }

        //Set the actual movement speed depending on the stance.
        if (m_UnitStance == UnitStance.STAND)
        {
            targetVelocity *= m_MovementSpeed;
        }
        else //Half for crouch
        {
            targetVelocity *= m_MovementSpeed * 0.5f;
        }
		// Apply a force that attempts to reach our target velocity
		Vector3 velocity = rigidbody.velocity;
		Vector3 velocityChange = (targetVelocity - velocity);
		velocityChange.x = Mathf.Clamp(velocityChange.x, -m_MaxVelocity, m_MaxVelocity);
		velocityChange.z = Mathf.Clamp(velocityChange.z, -m_MaxVelocity, m_MaxVelocity);
		velocityChange.y = 0;
		rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
		// Jump
		if (m_Grounded && Input.GetButton(m_JumpButton))
		{
            jump(velocity);
		}
        //Apply gravity
        m_Rigidbody.AddForce(new Vector3(0.0f, -m_Gravity * m_Rigidbody.mass, 0.0f));
        //Reset Rigidbody
        m_Rigidbody.angularVelocity = Vector3.zero;

        //Handle Animations
        if (m_Animation != null)
        {
            //Check Standing Stances
            switch (m_UnitStance)
            {
                //Standing Animations
                case UnitStance.STAND:
                    {
                        //Forward Animation
                        if((horizontalMoveFlag != 0.0f || verticalMoveFlag != 0.0f) && m_Grounded == true)
                        {
                            m_Animation.CrossFade("Male_runForward");
                        }
                        //Idle Animation
                        else if (horizontalMoveFlag == 0.0f && verticalMoveFlag == 0.0f && m_Grounded == true)
                        {
                            m_Animation.CrossFade("Male_idle");
                        }
                        //Jump Animation
                        else
                        {
                            m_Animation.CrossFade("Male_runJump_InAir");
                        }
                    }
                    break;
                //Crouch Animations
                case UnitStance.CROUCH:
                    {
                        //Forward Animation
                        if ((horizontalMoveFlag != 0.0f || verticalMoveFlag != 0.0f) && m_Grounded == true)
                        {
                            m_Animation.CrossFade("Male_crouch_WalkForward");
                        }
                        //Idle Animation
                        else if (verticalMoveFlag == 0.0f && horizontalMoveFlag == 0.0f && m_Grounded == true)
                        {
                            m_Animation.CrossFade("Male_crouch_Idle");
                        }
                        //Jump Animation
                        else
                        {
                            m_Animation.CrossFade("Male_runJump_InAir");
                        }
                    }
                    break;
            }
        }
	}

    void onInActive()
    {
        m_Grounded = checkGrounded();
        //Apply gravity
        m_Rigidbody.AddForce(new Vector3(0.0f, -m_Gravity * m_Rigidbody.mass, 0.0f));
        //Reset Rigidbody
        m_Rigidbody.angularVelocity = Vector3.zero;

        m_UnitStance = UnitStance.STAND;
        if (m_Grounded == true)
        {
            m_Animation.CrossFade("Male_idle");
        }
        else
        {
            m_Animation.CrossFade("Male_runJump_InAir");
        }
    }
    /*
    *   Function: onUseEnd
    *   Return Type: void
    *   Description: Call back used for wait "use" coroutine
    *   Parameters: none
    *   Date Reviewed: 08/06/2014 by Nathan Hanlan
    */
    private void onUseEnd(MonoBehaviour aSender, WaitCallBackArgs aArgs)
    {
        if (aArgs != null)
        {
            if (aArgs.message == "Use")
            {
                m_UseState = false;
            }
        }
    }
    public void onUseBegin()
    {
        if (m_UseState == false)
        {
            Debug.Log("Using");
            //Reset Velocity
            Vector3 rigidVelocity = m_Rigidbody.velocity;
            rigidVelocity.x = 0.0f;
            rigidVelocity.z = 0.0f;
            m_Rigidbody.velocity = rigidVelocity;

            //Add gravity and begin animation
            m_Rigidbody.AddForce(new Vector3(0.0f, -m_Gravity * m_Rigidbody.mass, 0.0f));
            m_Animation.CrossFade("Male_PickUp");
            m_UseState = true;
            //Wait animation clip length seconds until we reset and give control back
            StartCoroutine(Game.wait(this, onUseEnd, new WaitCallBackArgs("Use", m_Animation.GetClip("Male_PickUp").length * 0.70f)));
        }
    }

    /*
    *   Function: toggleStance
    *   Return Type: void
    *   Description: This method allows the unit's stance to be toggled quickly.
    *   Parameters: none
    *   Date Reviewed: 08/06/2014 by Nathan Hanlan
    */
    private void toggleStance()
    {
        if (m_UnitStance == UnitStance.STAND)
        {
            m_UnitStance = UnitStance.CROUCH;
        }
        else
        {
            m_UnitStance = UnitStance.STAND;
        }
    }


    /*
    *   Function: calculateJumpVerticalSpeed
    *   Return Type: float
    *   Description: From the jump height and gravity we deduce the upwards speed 
    *   for the character to reach at the apex.
    *   Parameters: none
    *   Date Reviewed: 08/06/2014 by Nathan Hanlan
    */
    private float calculateJumpVerticalSpeed() 
    {
	    return Mathf.Sqrt(2 * m_JumpHeight * m_Gravity);
	}

    /*
    *   Function: jump
    *   Return Type: void
    *   Description: Sets the proper stance, velocity and animation
    *   Parameters:  @Vector3 aCurrentVelocity - The current velocity for the x and z axis
    *   Date Reviewed: 08/06/2014 by Nathan Hanlan
    */
    private void jump(Vector3 aCurrentVelocity)
    {
        m_Rigidbody.velocity = new Vector3(aCurrentVelocity.x, calculateJumpVerticalSpeed(), aCurrentVelocity.z);
        m_UnitStance = UnitStance.STAND;
        m_Animation.CrossFade("Male_runJump_InAir");
    }



    /*
    *   Function: checkGrounded
    *   Return Type: bool
    *   Description: Raycasts in a down direction to check if the player is grounded
    *   returns true if grounded, returns false if not grounded
    *   Parameters: none
    *   Date Reviewed: 08/06/2014 by Nathan Hanlan
    */
    private bool checkGrounded()
    {
        int hits = 0;
        //Ray cast to the ground
        RaycastHit hit;
        
        //Middle
        if (Physics.Raycast(transform.position, Vector3.down, out hit, m_GroundedDistanceCheck))
        {
            if (hit.collider.transform != transform)
            {
                hits+=4;
            }
            else
            {
                return false;
            }
        }
        //First
        Vector3 location = new Vector3(m_GroundedRadiusSweep, 0.0f, m_GroundedRadiusSweep);
        if (Physics.Raycast(transform.TransformPoint(location), Vector3.down, out hit, m_GroundedDistanceCheck))
        {
            if (hit.collider.transform != transform)
            {
                hits += 1;
            }
        }
        //Second
        location.x = -m_GroundedRadiusSweep;
        if (Physics.Raycast(transform.TransformPoint(location), Vector3.down, out hit, m_GroundedDistanceCheck))
        {
            if (hit.collider.transform != transform)
            {
                hits += 1;
            }
        }
        //Third
        location.z = -m_GroundedRadiusSweep;
        if (Physics.Raycast(transform.TransformPoint(location), Vector3.down, out hit, m_GroundedDistanceCheck))
        {
            if (hit.collider.transform != transform)
            {
                hits += 1;
            }
        }
        //Fourth
        location.x = m_GroundedRadiusSweep;
        if (Physics.Raycast(transform.TransformPoint(location), Vector3.down, out hit, m_GroundedDistanceCheck))
        {
            if (hit.collider.transform != transform)
            {
                hits += 1;
            }
        }

        if (hits >= 1)
        {
            return true;
        }

        return false; //Is not grounded
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        //Middle Line
        Gizmos.DrawLine(transform.position, transform.TransformPoint(0.0f, -m_GroundedDistanceCheck, 0.0f));
        //First Line
        Vector3 location = new Vector3(m_GroundedRadiusSweep, 0.0f, m_GroundedRadiusSweep);
        Vector3 hitPoint = location;
        hitPoint.y = -m_GroundedDistanceCheck;
        Gizmos.DrawLine(transform.TransformPoint(location), transform.TransformPoint(hitPoint));
        //Second Line
        location.x = -m_GroundedRadiusSweep;
        hitPoint = location;
        hitPoint.y = -m_GroundedDistanceCheck;
        Gizmos.DrawLine(transform.TransformPoint(location), transform.TransformPoint(hitPoint));
        //Third Line
        location.z = -m_GroundedRadiusSweep;
        hitPoint = location;
        hitPoint.y = -m_GroundedDistanceCheck;
        Gizmos.DrawLine(transform.TransformPoint(location), transform.TransformPoint(hitPoint));
        //Fourth Line
        location.x = m_GroundedRadiusSweep;
        hitPoint = location;
        hitPoint.y = -m_GroundedDistanceCheck;
        Gizmos.DrawLine(transform.TransformPoint(location), transform.TransformPoint(hitPoint));
    }


    public void activateMotor()
    {
        m_Rigidbody.velocity = Vector3.zero;
        m_ActiveMotor = true;
    }
    public void deactivateMotor()
    {
        m_ActiveMotor = false;
    }

    public Animation unitAnimation
    {
        get { return m_Animation; }
        set { m_Animation = value; }
    }

    public float movementSpeed
    {
        get { return m_MovementSpeed; }
        set { m_MovementSpeed = value; }
    }
    public float turnSpeed
    {
        get { return m_TurnSpeed; }
        set { m_TurnSpeed = value; }
    }
    public float jumpHeight
    {
        get { return m_JumpHeight; }
        set { m_JumpHeight = value; }
    }
    public float maxVelocity
    {
        get { return m_MaxVelocity; }
        set { m_MaxVelocity = value; }
    }
    public float gravity
    {
        get { return m_Gravity; }
        set { m_Gravity = value; }
    }
    public UnitStance stance
    {
        get { return m_UnitStance; }
    }

    public Transform relativeTransform
    {
        get { return m_Camera; }
        set { m_Camera = value; }
    }
    public bool grounded
    {
        get { return m_Grounded; }
    }
    public float groundedDistanceCheck
    {
        get { return m_GroundedDistanceCheck; }
        set { m_GroundedDistanceCheck = value; }
    }
    public float groundedRadiusSweep
    {
        get { return m_GroundedRadiusSweep; }
        set { m_GroundedRadiusSweep = value; }
    }
    public bool isUsing
    {
        get { return m_UseState; }
    }
    public string forwardAxis
    {
        get { return m_ForwardAxis; }
        set { m_ForwardAxis = value; }
    }
    public string sideAxis
    {
        get { return m_SideAxis; }
        set { m_SideAxis = value; }
    }
    public string jumpButton
    {
        get { return m_JumpButton; }
        set { m_JumpButton = value; }
    }
    public KeyCode stanceKey
    {
        get { return m_StanceKey; }
        set { m_StanceKey = value; }
    }
    public KeyCode useKey
    {
        get { return m_UseKey; }
        set { m_UseKey = value; }
    }

}
