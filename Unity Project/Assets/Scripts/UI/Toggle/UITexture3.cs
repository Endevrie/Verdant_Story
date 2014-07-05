using UnityEngine;
using System;
using System.Collections;

namespace OnLooker
{
    namespace UI
    {

        [Serializable]
        public class UITexture3 : UIToggle3
        {
            [SerializeField]
            [HideInInspector]
            private MeshFilter m_MeshFilter;
            [SerializeField]
            [HideInInspector]
            private MeshRenderer m_MeshRenderer;
            [SerializeField]
            [HideInInspector]
            private Material[] m_Materials = null;


            // Use this for initialization
            void Start()
            {
                if (Application.isPlaying == true)
                {
                    initialize();
                }
            }

            // Update is called once per frame
            void Update()
            {
                
                updateTransform(true);
            }

            public override void updateTransform(bool aLerp)
            {
                Transform worldPoint = OnLookerUtils.worldPoint;
                Transform uiCamera = Camera.main.transform;
                if (worldPoint == null)
                {
                    Debug.LogWarning("World Point is not set");
                    return;
                }
                if (uiCamera == null)
                {
                    Debug.LogWarning("No Camera for UIToggle3");
                    return;
                }

                switch (anchorTarget)
                {
                    case UIAnchorTarget.NONE:
                        transform.position = position;
                        transform.rotation = Quaternion.Euler(rotation);
                        break;
                    case UIAnchorTarget.CAMERA:
                        if (aLerp == true)
                        {

                            worldPoint.position = uiCamera.position;
                            worldPoint.Translate(position, uiCamera);
                            transform.position = Vector3.Lerp(transform.position, worldPoint.position, translationSpeed * Time.deltaTime);
                            transform.LookAt(transform.position + uiCamera.rotation * Vector3.forward, uiCamera.rotation * Vector3.up);
                            transform.Rotate(rotation.x, rotation.y - 180.0f, rotation.z);

                        }
                        else
                        {
                            transform.position = uiCamera.position;
                            transform.Translate(position, uiCamera);
                            transform.LookAt(transform.position + uiCamera.rotation * Vector3.forward, uiCamera.rotation * Vector3.up);
                            transform.Rotate(rotation.x, rotation.y - 180.0f, rotation.z);
                        }
                        break;

                    case UIAnchorTarget.OBJECT:
                        if (target == null)
                        {
                            return;
                        }
                        if (aLerp == true)
                        {
                            worldPoint.position = target.position;
                            worldPoint.Translate(position, target);
                            transform.position = Vector3.Lerp(transform.position, worldPoint.position, translationSpeed * Time.deltaTime);
                            transform.LookAt(transform.position + uiCamera.rotation * Vector3.forward, uiCamera.rotation * Vector3.up);
                            transform.Rotate(rotation.x, rotation.y - 180.0f, rotation.z);
                        }
                        else
                        {
                            transform.position = target.position;
                            transform.Translate(position, target);
                            transform.LookAt(transform.position + uiCamera.rotation * Vector3.forward, uiCamera.rotation * Vector3.up);
                            transform.Rotate(rotation.x, rotation.y - 180.0f, rotation.z);
                        }
                        break;
                }
            }

            public void setDefault(bool aForce)
            {
                bool init = false;
                if (m_MeshFilter == null || m_MeshRenderer == null)
                {
                    init = true;
                }


                //If this component needs to initialize or is forced to then do so
                if (init == true || aForce == true)
                {
                    initialize();
                    if (m_Materials == null)
                    {
                        m_Materials = new Material[1];
                        m_Materials[0] = new Material(meshRenderer.sharedMaterial);
                    }
                    meshRenderer.renderer.materials = m_Materials;
                }
            }
            public void initialize()
            {
                m_MeshFilter = GetComponent<MeshFilter>();
                m_MeshRenderer = GetComponent<MeshRenderer>();
            }

            public MeshFilter meshFilter
            {
                get { return m_MeshFilter; }
                set { m_MeshFilter = value; }
            }
            public MeshRenderer meshRenderer
            {
                get { return m_MeshRenderer; }
                set { m_MeshRenderer = value; }
            }
            public Color color
            {
                get { return m_Materials[0].color; }
                set { m_Materials[0].color = value; }
            }
            public Texture texture
            {
                get { return m_Materials[0].mainTexture; }
                set { m_Materials[0].mainTexture = value; }
            }
            
        }
    }
}