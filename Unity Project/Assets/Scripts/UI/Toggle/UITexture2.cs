using UnityEngine;
using System;
using System.Collections;

namespace OnLooker
{
    namespace UI
    {

        [Serializable]
        public class UITexture2 : UIToggle2
        {
            [SerializeField]
            [HideInInspector]
            private GUITexture m_TextureComponent = null;
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

            }

            public void setDefault(bool aForce)
            {
                bool init = false;
                if (m_TextureComponent == null)
                {
                    init = true;
                }


                //If this component needs to initialize or is forced to then do so
                if (init == true || aForce == true)
                {
                    initialize();
                }
            }
            public void initialize()
            {
                m_TextureComponent = GetComponent<GUITexture>();
            }


            public Texture texture
            {
                get { return m_TextureComponent.texture; }
                set { m_TextureComponent.texture = value; }
            }
            public Color color
            {
                get { return m_TextureComponent.color; }
                set { m_TextureComponent.color = value; }
            }
        }
    }
}