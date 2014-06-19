using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Interactive : MonoBehaviour , IConditional
{
    //Gets called when the player enters the plant trigger area
    public virtual void onPlayerEnter(PlayerControl aPlayer)
    {

    }
    //Gets called when the player stays within the plant trigger area
    public virtual void onPlayerStay(PlayerControl aPlayer)
    {

    }
    //Gets called when the player leaves the plant trigger area
    public virtual void onPlayerExit(PlayerControl aPlayer)
    {

    }
    //Gets called when the player looks at an object with a collider who has their layer set to "Object Interaction" ie 8th Layer
    public virtual void onPlayerFocusEnter(PlayerControl aPlayer)
    {
        Debug.Log("Focus Enter");
    }
    //Gets called for every frame the player looks at an object with a collider who has their layer set to "Object Interaction" ie 8th Layer
    public virtual void onPlayerFocus(PlayerControl aPlayer)
    {

    }
    //Gets called when the player stops looking at the object with a collider who has their layer set to "Object Interaction" ie 8th Layer
    public virtual void onPlayerFocusExit(PlayerControl aPlayer)
    {
        Debug.Log("Focus Exit");
    }
    //Gets called when the player starts using this object.
    public virtual void onUse(PlayerControl aPlayer)
    {

    }
    //Gets called when the player stops using this object
    public virtual void onUseEnd(PlayerControl aPlayer)
    {

    }
    //The condition to check before the player may use this object.
    public virtual bool condition(Transform aPlayer, Transform aPlant)
    {
        //This is an example condition which checks if the player is facing the plant
        //And is within the minimum distance from the target
        float minDistance = 3.0f;

        //facing > 0 = facing forward
        //facing < 0 = facing backward
        float distance = Vector3.Distance(aPlant.position, aPlayer.position);
        Vector3 direction = (aPlant.position - aPlayer.position).normalized;
        float facing = Vector3.Dot(direction, aPlayer.forward);

        if(distance <= minDistance && facing > 0.0f)
        {
            return true;
        }
        return false;
    }

}
