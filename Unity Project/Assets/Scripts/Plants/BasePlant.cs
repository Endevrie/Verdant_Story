using UnityEngine;
using System.Collections;


public enum PlantType
{
    BASE,
    SUNFLOWER,
    MUSHROOM,
    VINE
}


public class BasePlant : MonoBehaviour 
{
    public PlayerEventTimeStamp m_PlayerTimeStamp;
    public float m_LightLevel;

    

    protected event PlayerTrigger onTriggerEnter;
    protected event PlayerTrigger onTriggerExit;
    protected event PlayerTrigger onTriggerStay;


	// Use this for initialization
	void Start () {
        m_PlayerTimeStamp = new PlayerEventTimeStamp();
	}
	
	// Update is called once per frame
	void Update () 
    {


	}


    void FixedUpdate()
    {
        
    }

    void OnTriggerEnter(Collider aCollider)
    {
        PlayerControl player = aCollider.GetComponent<PlayerControl>();
        if (player != null)
        {
            m_PlayerTimeStamp.playerEnterTime = Time.time;

            if (onTriggerEnter != null)
            {
                onTriggerEnter.Invoke(this, new PlayerTriggerArgs(m_PlayerTimeStamp));
            }

        }
    }
    void OnTriggerExit(Collider aCollider)
    {
        PlayerControl player = aCollider.GetComponent<PlayerControl>();
        if (player != null)
        {
            m_PlayerTimeStamp.playerExitTime = Time.time;
            m_PlayerTimeStamp.playerDeltaTime = Time.time - m_PlayerTimeStamp.playerEnterTime;

            if (onTriggerExit != null)
            {
                onTriggerExit.Invoke(this, new PlayerTriggerArgs(m_PlayerTimeStamp));
            }
        }
    }
    void OnTriggerStay(Collider aCollider)
    {
        PlayerControl player = aCollider.GetComponent<PlayerControl>();
        if (player != null)
        {
            m_PlayerTimeStamp.playerDeltaTime = Time.time - m_PlayerTimeStamp.playerEnterTime;

            if (onTriggerStay != null)
            {
                onTriggerStay.Invoke(this, new PlayerTriggerArgs(m_PlayerTimeStamp));
            }
        }
    }

    public void subscribeOnTriggerStay(PlayerTrigger aCallback)
    {
        if (aCallback != null)
        {
            onTriggerStay += aCallback;
        }
    }
    public void subscribeOnTriggerEnter(PlayerTrigger aCallback)
    {
        if (aCallback != null)
        {
            onTriggerEnter += aCallback;
        }
    }
    public void subscribeOnTriggerExit(PlayerTrigger aCallback)
    {
        if (aCallback != null)
        {
            onTriggerExit += aCallback;
        }
    }

    public void unsubscribeOnTriggerStay(PlayerTrigger aCallback)
    {
        if (aCallback != null)
        {
            onTriggerStay -= aCallback;
        }
    }
    public void unsubscribeOnTriggerEnter(PlayerTrigger aCallback)
    {
        if (aCallback != null)
        {
            onTriggerEnter -= aCallback;
        }
    }
    public void unsubscribeOnTriggerExit(PlayerTrigger aCallback)
    {
        if (aCallback != null)
        {
            onTriggerExit -= aCallback;
        }
    }

    public virtual PlantType plantType
    {
        get { return PlantType.BASE; }
    }

    

    
}

public delegate void PlayerTrigger(MonoBehaviour aSender, PlayerTriggerArgs aArgs);
public delegate void PlayerInteraction(MonoBehaviour aSender,  PlayerInteractionArgs aArgs);


public class PlayerEventTimeStamp
{
    private float m_PlayerEnter = 0.0f;
    private float m_PlayerExit = 0.0f;
    private float m_PlayerDelta = 0.0f;

    public float playerEnterTime
    {
        get { return m_PlayerEnter; }
        set { m_PlayerEnter = value; }
    }
    public float playerExitTime
    {
        get { return m_PlayerExit; }
        set { m_PlayerExit = value; }
    }
    public float playerDeltaTime
    {
        get { return m_PlayerDelta; }
        set { m_PlayerDelta = value; }
    }
}

public class PlayerTriggerArgs
{
    private PlayerEventTimeStamp m_TimeStamp;
    public PlayerTriggerArgs(PlayerEventTimeStamp aTimeStamp)
    {
        m_TimeStamp = aTimeStamp;
    }
    public PlayerEventTimeStamp timeStamp
    {
        get{return m_TimeStamp;}
    }
}


//On Use
//
public class PlayerInteractionArgs
{
    private string m_Interaction;

    public PlayerInteractionArgs(string aInteraction)
    {
        m_Interaction = aInteraction;
    }

    public static PlayerInteractionArgs onUse
    {
        
        get{return new PlayerInteractionArgs("OnUse");}
    }
    public static PlayerInteractionArgs onUseEnd
    {
        get{return new PlayerInteractionArgs("OnUse-End");}
    }
    public static PlayerInteractionArgs onTargetted
    {
        get{return new PlayerInteractionArgs("OnTargetted");}
    }
    public static PlayerInteractionArgs onTargettedEnd
    {
        get { return new PlayerInteractionArgs("OnTargetted-End"); }
    }

    public string interaction
    {
        get { return m_Interaction; }
    }
}