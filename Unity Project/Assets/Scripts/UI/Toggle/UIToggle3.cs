using UnityEngine;
using System;
using System.Collections;

namespace OnLooker
{
    namespace UI
    {
        [Serializable]
        public class UIToggle3 : UIToggle
        {
            [SerializeField]
            [HideInInspector]
            private float m_TranslationSpeed = 5.0f;
            [SerializeField]
            [HideInInspector]
            private Vector3 m_Position = Vector3.zero;
            [SerializeField]
            [HideInInspector]
            private Vector3 m_Rotation = Vector3.zero;
            [SerializeField]
            [HideInInspector]
            private UIAnchorTarget m_AnchorTarget = UIAnchorTarget.CAMERA;
            [SerializeField]
            [HideInInspector]
            private Transform m_Target = null;

            void OnMouseEnter()
            {
                onMouseEnter();
            }
            void OnMouseExit()
            {
                onMouseExit();
            }

            public virtual void updateTransform(bool aLerp)
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

                switch (m_AnchorTarget)
                {
                    case UIAnchorTarget.NONE:
                        transform.position = m_Position;
                        transform.rotation = Quaternion.Euler(m_Rotation);
                        break;
                    case UIAnchorTarget.CAMERA:
                        if (aLerp == true)
                        {

                            worldPoint.position = uiCamera.position;
                            worldPoint.Translate(m_Position, uiCamera);
                            transform.position = Vector3.Lerp(transform.position, worldPoint.position, m_TranslationSpeed * Time.deltaTime);
                            transform.LookAt(transform.position + uiCamera.rotation * Vector3.forward, uiCamera.rotation * Vector3.up);
                            transform.Rotate(m_Rotation);

                        }
                        else
                        {
                            transform.position = uiCamera.position;
                            transform.Translate(m_Position, uiCamera);
                            transform.LookAt(transform.position + uiCamera.rotation * Vector3.forward, uiCamera.rotation * Vector3.up);
                            transform.Rotate(m_Rotation);
                        }
                        break;

                    case UIAnchorTarget.OBJECT:
                        if (m_Target == null)
                        {
                            return;
                        }
                        if (aLerp == true)
                        {

                            worldPoint.position = m_Target.position;
                            worldPoint.Translate(m_Position, m_Target);
                            transform.position = Vector3.Lerp(transform.position, worldPoint.position, m_TranslationSpeed * Time.deltaTime);
                            transform.LookAt(transform.position + uiCamera.rotation * Vector3.forward, uiCamera.rotation * Vector3.up);
                            transform.Rotate(m_Rotation);

                        }
                        else
                        {
                            transform.position = m_Target.position;
                            transform.Translate(m_Position, m_Target);
                            transform.LookAt(transform.position + uiCamera.rotation * Vector3.forward, uiCamera.rotation * Vector3.up);
                            transform.Rotate(m_Rotation);
                        }
                        break;
                }
            }

            public float translationSpeed
            {
                get{return m_TranslationSpeed;}
                set{m_TranslationSpeed = value;}
            }
            public Vector3 position
            {
                get { return m_Position; }
                set { m_Position = value; }
            }
            public Vector3 rotation
            {
                get { return m_Rotation; }
                set { m_Rotation = value; }
            }
            public UIAnchorTarget anchorTarget
            {
                get { return m_AnchorTarget; }
                set { m_AnchorTarget = value; }
            }
            public Transform target
            {
                get { return m_Target; }
                set { m_Target = value; }
            }
        }
    }
}