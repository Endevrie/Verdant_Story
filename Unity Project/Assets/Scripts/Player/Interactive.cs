using UnityEngine;
using System.Collections;

public class Interactive : MonoBehaviour {

    //float distance = Vector3.Distance(target.transform.position,transform.position);
    //Vector3 dir = (target.transform.position - transform.position).normalized;
    //float direction = Vector3.Dot(dir, transform.forward);

    public Transform m_Camera = null;

	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () 
    {
	    
	}
    void FixedUpdate()
    {
        if (m_Camera == null)
        {
            return;
        }

        //The layer mask is for objects this raycast is to check for only.
        int m_LayerMask = 1 << 8;
        RaycastHit hit;
        //Raycast from the cameras current position forwards to see if the interactive object gets hit.
        if (Physics.Raycast(m_Camera.position, m_Camera.forward, out hit,500.0f,m_LayerMask))
        {
            Transform hitForm = hit.transform;
            Vector3 hitPoint = hit.point;
            if (hitForm != null)
            {
                Debug.DrawLine(m_Camera.position, hitForm.position, Color.blue, 1.0f);
            }
            
            Debug.DrawLine(m_Camera.position, hitPoint,Color.green,0.1f);
        }

        
    }

    void DrawGizmos()
    {

    }
}
