using UnityEngine;
using System.Collections;

public delegate void CollisionEvent(Transform aSender, Collision aCollision);

public class CollisionHandler : MonoBehaviour 
{

    private event CollisionEvent m_Enter;
    private event CollisionEvent m_Stay;
    private event CollisionEvent m_Exit;

    public void register(CollisionEvent aEnter, CollisionEvent aStay, CollisionEvent aExit)
    {
        m_Enter += aEnter;
        m_Stay += aStay;
        m_Exit += aExit;
    }
    public void unregister(CollisionEvent aEnter, CollisionEvent aStay, CollisionEvent aExit)
    {
        m_Enter -= aEnter;
        m_Stay -= aStay;
        m_Exit -= aExit;
    }


    private void OnCollisionEnter(Collision aCollision)
    {
        m_Enter.Invoke(transform, aCollision);
    }
    private void OnCollisionStay(Collision aCollision)
    {
        m_Stay.Invoke(transform, aCollision);
    }
    private void OnCollisionExit(Collision aCollision)
    {
        m_Exit.Invoke(transform, aCollision);
    }

    public void invokeEnter(Collision aCollision)
    {
        m_Enter.Invoke(transform, aCollision);
    }
}
