using UnityEngine;
using System.Collections.Generic;

public delegate void TriggerEvent(Transform aSender, Collider aCollider);

public class TriggerHandler : MonoBehaviour
{

    private event TriggerEvent m_Enter;
    private event TriggerEvent m_Exit;
    private event TriggerEvent m_Stay;

    public void register(TriggerEvent aEnter, TriggerEvent aStay, TriggerEvent aExit)
    {
        m_Enter += aEnter;
        m_Stay += aStay;
        m_Exit += aExit;
    }
    public void unregister(TriggerEvent aEnter, TriggerEvent aStay, TriggerEvent aExit)
    {
        m_Enter -= aEnter;
        m_Stay -= aStay;
        m_Exit -= aExit;
    }

    void OnTriggerEnter(Collider aCollider)
    {
        m_Enter.Invoke(transform, aCollider);
    }
    void OnTriggerStay(Collider aCollider)
    {
        m_Stay.Invoke(transform, aCollider);
    }
    void OnTriggerExit(Collider aCollider)
    {
        m_Exit.Invoke(transform, aCollider);
    }
	
}
