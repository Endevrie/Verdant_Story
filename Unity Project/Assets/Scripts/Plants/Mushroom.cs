using UnityEngine;
using System.Collections;

public class Mushroom : Interactive
{
    public TriggerHandler m_MushroomHead;

    private void Start()
    {
        m_MushroomHead = GetComponentInChildren<TriggerHandler>();
        if (m_MushroomHead != null)
        {
            m_MushroomHead.register(onHeadEnter, onHeadStay, onHeadExit);
        }
    }

    public void onHeadEnter(Transform aSender, Collider aCollision)
    {
        Rigidbody body = aCollision.rigidbody;

        if (body == null)
        {
            Debug.Log("No body");
            return;
        }




        float maxForce = 400.0f;
        float bounceFactor = 2.0f;

        Vector3 force = Vector3.zero;
        force.x = body.velocity.x;
        force.y = -body.velocity.y * body.mass;
        force.z = body.velocity.z;

        force.y = Mathf.Abs(force.y) > maxForce ? (maxForce * Mathf.Sign(force.y)) : force.y;
        force.y = force.y * (bounceFactor * aSender.localScale.magnitude);
        Debug.Log("Force:" + force.ToString());
        body.AddForce(force, ForceMode.Impulse);
    }
    public void onHeadStay(Transform aSender, Collider aCollision)
    {

    }
    public void onHeadExit(Transform aSender, Collider aCollision)
    {

    }
    //Gets called when the player enters the plant trigger area
    public override void onPlayerEnter(PlayerControl aPlayer)
    {

    }
    //Gets called when the player stays within the plant trigger area
    public override void onPlayerStay(PlayerControl aPlayer)
    {

    }
    //Gets called when the player leaves the plant trigger area
    public override void onPlayerExit(PlayerControl aPlayer)
    {

    }
    //Gets called when the player looks at an object with a collider who has their layer set to "Object Interaction" ie 8th Layer
    public override void onPlayerFocusEnter(PlayerControl aPlayer)
    {

    }
    //Gets called for every frame the player looks at an object with a collider who has their layer set to "Object Interaction" ie 8th Layer
    public override void onPlayerFocus(PlayerControl aPlayer)
    {

    }
    //Gets called when the player stops looking at the object with a collider who has their layer set to "Object Interaction" ie 8th Layer
    public override void onPlayerFocusExit(PlayerControl aPlayer)
    {

    }
    //Gets called when the player starts using this object.
    public override void onUse(PlayerControl aPlayer)
    {

    }
    //Gets called when the player stops using this object
    public override void onUseEnd(PlayerControl aPlayer)
    {

    }
}
