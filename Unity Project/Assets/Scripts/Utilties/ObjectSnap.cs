using UnityEngine;
using System;
using System.Collections;

[ExecuteInEditMode]
//This class snap object via the mouse to whatever surface the mouse is pointing.
//At the location of the mouse cursor a gizmo is showing to indicate the range of the snap.
[Serializable]
public class ObjectSnap : MonoBehaviour {
	
	[SerializeField]
	private Transform m_Target = null;
	[SerializeField]
	private Transform m_ObjectInHand = null;
	[SerializeField]
	private bool m_DidSnap = false;
	[SerializeField]
	private Collider[] m_HitColliders;
	[SerializeField]
	private float m_MouseDistance;
	[SerializeField]
	private float m_ObjRayCastRange;
	[SerializeField]
	private Vector3 m_MouseCurrentPos;
	[SerializeField]
	private Vector3 m_WorldPos;
	[SerializeField]
	private Quaternion m_GizmoRotation;
	
	
	
	
	public delegate void MouseClick();
	public static event MouseClick didClick;
	
	public delegate void MouseRelease();
	public static event MouseRelease didRelease;
	
	
	
	public Transform target
	{
		get { return m_Target; }
		set{m_Target = value;}
	}
	
	public Transform objectInHand
	{
		get {return m_ObjectInHand;}
		set {m_ObjectInHand = value;}
	}
	
	public bool didSnap
	{
		get {return m_DidSnap;}
	}

	public Collider[] hitColliders
	{
		get{return m_HitColliders;}
		set {m_HitColliders = value;}
	}
	
	public float mouseDistance
	{
		get { return m_MouseDistance = Vector3.Distance(transform.position, worldPos); }
	}
	
	public float objRayCastRange
	{
		
		get { return m_ObjRayCastRange; }
		set { m_ObjRayCastRange = value;}
		
	}
	
	public Vector3 mouseCurrentPos
	{
		get { return m_MouseCurrentPos = Input.mousePosition; }
	}
	
	public Vector3 worldPos
	{
		get { return m_WorldPos = Camera.main.ScreenToWorldPoint(mouseCurrentPos); }
	}
	
	public Quaternion gizmoRotation
	{
		get { return m_GizmoRotation; }
		set { m_GizmoRotation = value; }
	}
	
	
	// Use this for initialization
	void Start () {
		
		didClick += clicking;
		didRelease += releasing;
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void mouseRayCast()
	{
		
		Ray ray;
		RaycastHit hit;
		
		ray = new Ray(worldPos, worldPos);
		Debug.DrawRay(worldPos, worldPos, Color.red);
		
		if(Physics.Raycast(ray, out hit, 1f))
		{
			if(hit.collider.rigidbody != null)
			{
				Debug.Log ("Touching: " + hit.collider.rigidbody.name);
				target = hit.transform;
			}
		}
		
		
	}
	
	void objectRayCast()
	{
		
		Ray ray;
		RaycastHit hit;
		
		ray = new Ray(transform.position, Vector3.forward * objRayCastRange);
		Debug.DrawRay(worldPos, Vector3.forward * objRayCastRange, Color.blue);


	
		if(Physics.Raycast(ray, out hit, objRayCastRange))
		{
			if(hit.collider.rigidbody != null)
			{
				transform.position = Vector3.Lerp(transform.position, hit.transform.position, Time.deltaTime * 50f);
				Debug.Log ("Snaping On: " + hit.collider.rigidbody.name);
			}
			
		}

		//		hitColliders = Physics.OverlapSphere(transform.position, objRayCastRange);
		//
		//		for (int i = 0; i < hitColliders.Length; i++)
		//		{
		//
		//			Debug.Log ("Hit: " + hitColliders[i].collider.name);
		//		
		//			if(hitColliders != null) 
		//			{
		//				
		//				
		//			}
		//
		//		}
		
	}
	
	void clicking()
	{
		if(Input.GetButtonDown("Fire1"))
		{
			Debug.Log("Clicked");
		}
	}
	
	void releasing()
	{
		if(Input.GetButtonUp("Fire1"))
		{
			Debug.Log("Release");
		}
	}
}
