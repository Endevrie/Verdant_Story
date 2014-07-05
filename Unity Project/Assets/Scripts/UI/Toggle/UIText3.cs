using UnityEngine;
using System;
using System.Collections;

namespace OnLooker
{
    namespace UI
    {


        public class UIText3 : UIToggle3
        {
            [SerializeField]
            [HideInInspector]
            private TextMesh m_TextMesh;
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


            public void setDefault(bool aForce)
            {
                bool init = false;
                if(m_TextMesh == null || m_MeshRenderer == null)
                {
                    init = true;
                }

                if(init == true || aForce == true)
                {
                    initialize();
                    m_Materials = new Material[1];
                    m_Materials[0] = new Material(meshRenderer.sharedMaterial);
                    meshRenderer.renderer.materials = m_Materials;
                }
            }
            public void initialize()
            {
                m_TextMesh = GetComponent<TextMesh>();
                m_MeshRenderer = GetComponent<MeshRenderer>();
            }

            public MeshRenderer meshRenderer
            {
                get { return m_MeshRenderer; }
                set { m_MeshRenderer = value; }
            }
            public TextMesh textMesh
            {
                get { return m_TextMesh; }
                set { m_TextMesh = value; }
            }
            public Material material
            {
                get { return m_Materials[0]; }
            }

            public string text
            {
                get { return m_TextMesh.text; }
                set { m_TextMesh.text = value; }
            }
            public Font font
            {
                get { return m_TextMesh.font; }
                set { m_TextMesh.font = value; }
            }
            public int fontSize
            {
                get { return m_TextMesh.fontSize; }
                set { m_TextMesh.fontSize = value; }
            }
            public FontStyle fontStyle
            {
                get { return m_TextMesh.fontStyle; }
                set { m_TextMesh.fontStyle = value; }
            }
            public Color color
            {
                get { return m_Materials[0].color; }
                set { m_Materials[0].color = value; }
            }

        }
    }
}