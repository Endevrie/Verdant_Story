using UnityEngine;
using System;
using System.Collections;

namespace OnLooker
{
    namespace UI
    {
        [Serializable]
        public class UIToggle2 : UIToggle
        {
            [SerializeField]
            [HideInInspector]
            private Rect m_Bounds = new Rect();//Anchored Position
            [SerializeField]
            [HideInInspector]
            private Rect m_ScaledBounds = new Rect();

            [SerializeField]
            [HideInInspector]
            private Vector2 m_Scale = Vector2.one;
            [SerializeField]
            [HideInInspector]
            private bool m_UniformScale = true;


            // Use this for initialization
            void Start()
            {
            }

            // Update is called once per frame
            protected override void gameUpdate()
            {
 	             //Do continuous check to see if mouse in bounds here
                Vector3 mouse = Input.mousePosition;
                
                if (OnLookerUtils.pointInRect(mouse, m_ScaledBounds,false))
                {
                    
                    if (mouseInBounds == false)
                    {
                        m_MouseInBounds = true;
                        onMouseEnter();
                    }
                }
                else
                {
                    
                    if (mouseInBounds == true)
                    {
                        m_MouseInBounds = false;
                        onMouseExit();
                    }
                }
            }

            public bool hitTest()
            {
                Vector3 mouse = Input.mousePosition;
                //Debug.Log(name + " Hit Test");
                if (OnLookerUtils.pointInRect(mouse, m_ScaledBounds,false))
                {
                    return true;
                }
                return false;
            }
            public void setBounds(float x, float y, float w, float h)
            {
                m_Bounds.x = x;
                m_Bounds.y = y;
                m_Bounds.width = w;
                m_Bounds.height = h;

                if (m_UniformScale == true)
                {
                    x = m_Bounds.x - Mathf.Abs(w - w * scale.x) * 0.5f;
                    y = m_Bounds.y - Mathf.Abs(h - h * scale.x) * 0.5f;
                    m_ScaledBounds.Set(x, y, w * scale.x, h * scale.x);
                }
                else
                {
                    x = m_Bounds.x - Mathf.Abs(w - w * scale.x) * 0.5f;
                    y = m_Bounds.y - Mathf.Abs(h - h * scale.y) * 0.5f;
                    m_ScaledBounds.Set(x, y, w * scale.x, h * scale.y);
                }
            }

            public void translate(Vector2 aValue)
            {
                translate(aValue.x, aValue.y);
            }
            public void translate(float x, float y)
            {
                setBounds(bounds.x + x, bounds.y + y, bounds.width, bounds.height);
            }
            //Uniform scaling only
            public void scaleAmount(float aAmount)
            {
                m_UniformScale = true;
                m_Scale.x += aAmount;
                setBounds(bounds.x, bounds.y, bounds.width, bounds.height);
            }


            public Rect bounds
            {
                get { return m_Bounds; }
            }
            public Rect scaledBounds
            {
                get { return m_ScaledBounds; }
            }
            public Vector2 scale
            {
                get { return m_Scale; }
                set { m_Scale = value; }
            }
            public bool uniformScale
            {
                get { return m_UniformScale; }
                set { m_UniformScale = value; }
            }

        }
    }
}