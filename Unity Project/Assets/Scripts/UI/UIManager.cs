using UnityEngine;
using System.Collections.Generic;

namespace OnLooker
{
    namespace UI
    {
        [ExecuteInEditMode]
        public class UIManager : MonoBehaviour
        {
            #region singleton
            private static UIManager s_Instance = null;
            private static int s_InstanceCount = 0;
            private static void setInstance(UIManager aManager)
            {
                if (s_Instance == null && aManager != null)
                {
                    if (s_InstanceCount == 0)
                    {
                        s_Instance = aManager;
                        s_InstanceCount++;
                    }
                    else
                    {
                        Debug.LogWarning("Invalid amount of UIManagers. There must only be one");
                    }
                }
                else if (s_Instance != null && aManager == null)
                {
                    if ((s_InstanceCount <= 0) == false)
                    {
                        s_Instance = null;
                        s_InstanceCount--;
                    }
                }
            }
            public static UIManager instance
            {
                get { return s_Instance; }
            }
            #endregion

            private float m_DoubleClickTime = 0.25f;
            [SerializeField]
            private Camera m_MainCamera = null;
            [SerializeField]
            private Camera m_UserCamera = null;
            [SerializeField]
            private bool m_AllowClickOff = true;
            [SerializeField]
            private bool m_MouseSupport = false;
            [SerializeField]
            private bool m_KeyboardSupport = false;
            [SerializeField]
            private KeyCode m_NextKey = KeyCode.UpArrow;
            [SerializeField]
            private KeyCode m_PreviousKey = KeyCode.DownArrow;
            [SerializeField]
            private KeyCode[] m_ActionKeys = null;

            [SerializeField]
            private Mesh m_PlaneMesh;
            [SerializeField]
            private Material m_TextureMaterial;
            [SerializeField]
            private Material m_TextMaterial;

            [SerializeField]
            private List<UIToggle> m_Toggles = new List<UIToggle>();
            [SerializeField]
            private UIToggle m_FocusedToggle;

            void Start()
            {
                setInstance(this);
                if (Application.isPlaying)
                {

                }
                else
                {

                }

            }
            void OnDestroy()
            {
                setInstance(null);
            }


            void FixedUpdate()
            {
                //Todo: Write Input Handling Here.
                handleInputEvents();
            }

            public bool validateState()
            {
                UIManager.setInstance(this);
                if (OnLookerUtils.worldPoint == null)
                {
                    OnLookerUtils.worldPoint = GameObject.Find("WorldPoint").transform;
                }
                if (getCurrentCamera() == null)
                {
                    return false;
                }
                if (UIManager.currentCamera == null)
                {
                    return false;
                }
                if (m_PlaneMesh == null)
                {
                    return false;
                }
                if (m_TextMaterial == null)
                {
                    return false;
                }
                if (m_TextureMaterial == null)
                {
                    return false;
                }
                return true;
            }

            bool toggleHitTest(out UIToggle2 aToggle)
            {
                aToggle = null;
                for (int i = 0; i < m_Toggles.Count; i++)
                {
                    if (m_Toggles[i].GetType().IsSubclassOf(typeof(UIToggle2)) == false)
                    {
                        continue;
                    }
                    UIToggle2 toggle = (UIToggle2)m_Toggles[i];
                    if (toggle.hitTest())
                    {
                        aToggle = toggle;
                        return true;
                    }
                }
                return false;
            }

            void handleInputEvents()
            {
                if (m_Toggles.Count == 0)
                {
                    Debug.Log("No toggles");
                    return;
                }


                //Handle Keyboard Events?
                if (Input.GetKeyDown(m_NextKey))
                {
                    nextToggle();
                }
                if (Input.GetKeyDown(m_PreviousKey))
                {
                    previousToggle();
                }
                if (m_FocusedToggle != null)
                {
                    if (m_FocusedToggle.processKeyEvents(m_ActionKeys) == true)
                    {
                        return;
                    }
                }

                //Check 2D
                if (handle2DToggles() != null)
                {
                    //Debug.Log("2D Control Action");
                    return;
                }
                //Check 3D
                Camera currentCam = getCurrentCamera();
                if (currentCam == null)
                {
                    Debug.Log("Bad Camera");
                    return;
                }
                if (handle3DToggles(currentCam) != null)
                {
                    return;
                }

                



                //No Hit Zone
                if (OnLookerUtils.anyMouseButtonDown())
                {
                    if (m_FocusedToggle != null && m_AllowClickOff == true)
                    {
                        m_FocusedToggle.setFocus(false);
                        m_FocusedToggle = null;
                    }
                }


            }

            UIToggle2 handle2DToggles()
            {
                UIToggle2 uiToggle2 = null;
                bool toggleInFocus = toggleHitTest(out uiToggle2);
                if (toggleInFocus == true)
                {
                    if (OnLookerUtils.anyMouseButtonDown())
                    {
                        //A toggle exists
                        if (m_FocusedToggle != null)
                        {
                            //Verify they arent the same
                            //Verify that the new toggle is interactive
                            if (m_FocusedToggle != uiToggle2 && uiToggle2.isInteractive)
                            {
                                m_FocusedToggle.setFocus(false);
                                m_FocusedToggle = uiToggle2;
                                m_FocusedToggle.setFocus(true);
                            }
                        }
                        else
                        {
                            //Verify the new toggle is interactive
                            if (uiToggle2.isInteractive)
                            {
                                m_FocusedToggle = uiToggle2;
                                m_FocusedToggle.setFocus(true);
                            }
                        }
                    }
                    uiToggle2.processEvents();
                }
                return uiToggle2;
            }
            UIToggle3 handle3DToggles(Camera aCamera)
            {
                int layerMask = 1 << 9;
                RaycastHit hit;
                Ray ray = aCamera.ScreenPointToRay(Input.mousePosition);
                UIToggle3 toggle = null;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
                {
                    toggle = hit.collider.GetComponent<UIToggle3>();
                    if (toggle != null)
                    {
                        if (OnLookerUtils.anyMouseButtonDown())
                        {
                            if (m_FocusedToggle != null)
                            {
                                if (m_FocusedToggle != toggle && toggle.isInteractive == true)
                                {
                                    m_FocusedToggle.setFocus(false);
                                    m_FocusedToggle = toggle;
                                    m_FocusedToggle.setFocus(true);
                                }
                            }
                            else
                            {
                                if (toggle.isInteractive == true)
                                {
                                    m_FocusedToggle = toggle;
                                    m_FocusedToggle.setFocus(true);
                                }
                            }
                        }
                        toggle.processEvents();
                    }
                }
                return toggle;
            }

            public void registerToggle(UIToggle aToggle)
            {
                if (m_Toggles.Contains(aToggle) || aToggle == null)
                {
                    return;
                }
                
                if (aToggle.isInteractive == true && m_Toggles.Count == 0 && m_AllowClickOff == false)
                {
                    m_FocusedToggle = aToggle;
                    m_FocusedToggle.setFocus(true);
                }
                m_Toggles.Add(aToggle);

                
            }
            public void unregisterToggle(UIToggle aToggle)
            {
                if (aToggle == null || m_Toggles.Contains(aToggle) == false)
                {
                    return;
                }

                if (aToggle == m_FocusedToggle)
                {
                    m_FocusedToggle.setFocus(false);
                }
                m_Toggles.Remove(aToggle);

                if (m_AllowClickOff == false)
                {
                    for (int i = 0; i < m_Toggles.Count; i++)
                    {
                        if (m_Toggles[i].isInteractive == true)
                        {
                            m_FocusedToggle = m_Toggles[i];
                            break;
                        }
                    }
                }
            }

            void nextToggle()
            {
                if (m_FocusedToggle == null || m_Toggles.Count <= 1)
                {
                    return;
                }
                int currentToggleIndex = 0;
                int toggleCount = m_Toggles.Count;
                for (int i = 0; i < toggleCount; i++)
                {
                    if (m_Toggles[i] == m_FocusedToggle)
                    {
                        currentToggleIndex = i;
                        break;
                    }
                }

                for(int i = 0; i < toggleCount; i++)
                {
                    if (currentToggleIndex + 1 >= m_Toggles.Count)
                    {
                        currentToggleIndex = 0;
                    }
                    else
                    {
                        currentToggleIndex++;
                    }
                    if (m_Toggles[currentToggleIndex].isInteractive == true)
                    {
                        m_FocusedToggle.setFocus(false);
                        m_FocusedToggle = m_Toggles[currentToggleIndex];
                        m_FocusedToggle.setFocus(true);
                        break;
                    }
                }

                

            }
            void previousToggle()
            {
                
                if (m_FocusedToggle == null || m_Toggles.Count <= 1)
                {
                    return;
                }
                //Find the current toggle index
                int currentToggleIndex = 0;
                int toggleCount = m_Toggles.Count;
                for (int i = 0; i < toggleCount; i++)
                {
                    if (m_Toggles[i] == m_FocusedToggle)
                    {
                        currentToggleIndex = i;
                        break;
                    }
                }

                //Loop until we find the next or until we've made one lap around
                for(int i = 0; i < toggleCount; i++)
                {
                    if (currentToggleIndex - 1 < 0)
                    {
                        currentToggleIndex = toggleCount - 1;
                    }
                    else
                    {
                        currentToggleIndex--;
                    }
                    if (m_Toggles[currentToggleIndex].isInteractive == true)
                    {
                        m_FocusedToggle.setFocus(false);
                        m_FocusedToggle = m_Toggles[currentToggleIndex];
                        m_FocusedToggle.setFocus(true);
                        return;
                    }
                }
            }

            //Creation Methods

            public UIText2 createText2(UIParameters aArgs)
            {
                //Create the game object
                GameObject uiGameObject = new GameObject("Sprite(Text2)");
                Transform uiTransform = uiGameObject.GetComponent<Transform>();
                uiTransform.rotation = Quaternion.identity;
                uiTransform.position = Vector3.zero;
                uiTransform.localRotation = Quaternion.identity;
                uiTransform.localPosition = Vector3.zero;
                uiTransform.localScale = new Vector3(0.0f, 0.0f, 1.0f);
                uiTransform.parent = transform;
                
                //Create the required Components
                GUIText uiGUIText = uiGameObject.AddComponent<GUIText>();
                //Create the UIText2 Component
                UIText2 uiText = uiGameObject.AddComponent<UIText2>();
                //Set the Defaults
                uiGUIText.anchor = TextAnchor.LowerLeft;
                uiGUIText.alignment = TextAlignment.Left;
                uiText.setManager(this);
                uiText.setDefault(true);
                //Set the UIParamaters


                uiText.toggleName = aArgs.toggleName;
                uiText.isInteractive = aArgs.interactive;
                uiText.text = aArgs.text;
                uiText.font = aArgs.font;
                uiText.fontSize = aArgs.fontSize;
                uiText.fontStyle = aArgs.fontStyle;
                uiText.fontColor = aArgs.color;
                uiText.scale = new Vector2(aArgs.scale,aArgs.scale);
                uiText.setBounds(aArgs.bounds.x, aArgs.bounds.y, aArgs.bounds.width, aArgs.bounds.height);
                //Register the Control
                registerToggle(uiText);
                return uiText;
            }
            public UITexture2 createTexture2(UIParameters aArgs)
            {
                //Create the Game Object
                GameObject uiGameObject = new GameObject("Sprite(Texture2)");
                Transform uiTransform = uiGameObject.GetComponent<Transform>();
                uiTransform.rotation = Quaternion.identity;
                uiTransform.position = Vector3.zero;
                uiTransform.localRotation = Quaternion.identity;
                uiTransform.localPosition = Vector3.zero;
                uiTransform.localScale = new Vector3(0.0f, 0.0f, 1.0f);
                uiTransform.parent = transform;
                     
                //Create the required Components
                GUITexture uiGUITexture = uiGameObject.AddComponent<GUITexture>();
                //Create the UITexture2 Component
                UITexture2 uiTexture = uiGameObject.AddComponent<UITexture2>();
                //Set the Defaults
                uiTexture.setManager(this);
                uiTexture.setDefault(true);

                //Set the UIParameters
                uiTexture.toggleName = aArgs.toggleName;
                uiTexture.isInteractive = aArgs.interactive;
                uiTexture.texture = aArgs.texture;
                uiTexture.color = aArgs.color;
                uiTexture.scale = new Vector2(aArgs.scale, aArgs.scale);
                uiTexture.setBounds(aArgs.bounds.x, aArgs.bounds.y, aArgs.bounds.width, aArgs.bounds.height);

                //Register the Control
                registerToggle(uiTexture);
                return uiTexture;
            }
            public UIText3 createText3(UIParameters aArgs)
            {
                //Create the Game Object
                GameObject uiGameObject = new GameObject("Sprite(Text3)");
                Transform uiTransform = uiGameObject.GetComponent<Transform>();
                uiTransform.rotation = Quaternion.identity;
                uiTransform.position = Vector3.zero;
                uiTransform.localRotation = Quaternion.identity;
                uiTransform.localPosition = Vector3.zero;
                uiTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                uiTransform.parent = transform;

                //Create the required components
                MeshRenderer uiMeshRenderer = uiGameObject.AddComponent<MeshRenderer>();
                TextMesh uiTextMesh = uiGameObject.AddComponent<TextMesh>();

                uiMeshRenderer.material = m_TextMaterial;
                uiTextMesh.characterSize = 0.1f;
                uiTextMesh.anchor = TextAnchor.MiddleCenter;
                uiTextMesh.alignment = TextAlignment.Center;

                //Create the UIText3 Component
                UIText3 uiText = uiGameObject.AddComponent<UIText3>();
                // set The default
                uiText.setManager(this);
                uiText.setDefault(true);
                //set the UIParameters
                uiText.toggleName = aArgs.toggleName;
                uiText.isInteractive = aArgs.interactive;
                uiText.text = aArgs.text;
                uiText.font = aArgs.font;
                uiText.fontSize = aArgs.fontSize;
                uiText.fontStyle = aArgs.fontStyle;
                uiText.color = aArgs.color;
                uiText.position = aArgs.position;
                uiText.rotation = aArgs.rotation;

                uiTextMesh.font = aArgs.font;
                //Register the Control
                registerToggle(uiText);

                uiText.updateTransform(false);
                return uiText;
            }
            public UITexture3 createTexture3(UIParameters aArgs)
            {
                //Create the Game Object
                GameObject uiGameObject = new GameObject("Sprite(Texture3)");
                Transform uiTransform = uiGameObject.GetComponent<Transform>();
                uiTransform.rotation = Quaternion.identity;
                uiTransform.position = Vector3.zero;
                uiTransform.localRotation = Quaternion.identity;
                uiTransform.localPosition = Vector3.zero;
                uiTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                uiTransform.parent = transform;

                //Create the required components
                MeshRenderer uiMeshRenderer = uiGameObject.AddComponent<MeshRenderer>();
                MeshFilter uiMeshFilter = uiGameObject.AddComponent<MeshFilter>();

                uiMeshRenderer.material = m_TextureMaterial;
                uiMeshFilter.mesh = m_PlaneMesh;

                //Create the UIText3 Component
                UITexture3 uiTexture = uiGameObject.AddComponent<UITexture3>();
                // set The default
                uiTexture.setManager(this);
                uiTexture.setDefault(true);
                //set the UIParameters
                uiTexture.toggleName = aArgs.toggleName;
                uiTexture.isInteractive = aArgs.interactive;
                uiTexture.texture = aArgs.texture;
                uiTexture.color = aArgs.color;
                uiTexture.position = aArgs.position;
                uiTexture.rotation = aArgs.rotation;

                //Register the Control
                registerToggle(uiTexture);

                uiTexture.updateTransform(false);
                return uiTexture;
            }

            public UILabel createLabel2(UIParameters aArgs)
            {
                bool defaultInteractive = aArgs.interactive;
                Texture defaultTexture = aArgs.texture;
                //Create the GameObject
                GameObject uiGO = new GameObject("Sprite (Label 2)");
                Transform uiTransform = uiGO.transform;
                uiTransform.rotation = Quaternion.identity;
                uiTransform.position = Vector3.zero;
                uiTransform.localRotation = Quaternion.identity;
                uiTransform.localPosition = Vector3.zero;
                uiTransform.localScale = new Vector3(0.0f, 0.0f, 1.0f);
                uiTransform.parent = transform;
                //Create Text2 && Texture2
                aArgs.interactive = true;
                UIText2 text2 = createText2(aArgs);
                aArgs.interactive = false;
                aArgs.texture = null;
                UITexture2 texture2 = createTexture2(aArgs);
                
                //Create UILabel
                UILabel label = uiGO.AddComponent<UILabel>();
                label.uiText = text2;
                label.uiTexture = texture2;
                text2.setHandler(label);
                texture2.setHandler(label);
                text2.transform.parent = label.transform;
                texture2.transform.parent = label.transform;





                aArgs.interactive = defaultInteractive;
                aArgs.texture = defaultTexture;

                return null;
            }
            public UILabel createLabel3(UIParameters aArgs)
            {
                return null;
            }


            public UIToggle getToggle(string aName)
            {
                for (int i = 0; i < m_Toggles.Count; i++)
                {
                    if (m_Toggles[i].toggleName == aName)
                    {
                        return m_Toggles[i];
                    }
                }
                return null;
            }
            public UIToggle getToggle(int aIndex)
            {
                if (aIndex >= 0 && aIndex < m_Toggles.Count)
                {
                    return m_Toggles[aIndex];
                }
                return null;
            }
            public UIToggle getLastToggle()
            {
                if (m_Toggles.Count == 0)
                {
                    return null;
                }
                return m_Toggles[m_Toggles.Count - 1];
            }
            public UIToggle toggleInFocus
            {
                get { return m_FocusedToggle; }
            }

            //Todo
            //Make Menu Create functions for Basic Controls
            //-Label
            //-Image
            //-Button
            //-TextField

            //Creation Methods End

            public bool allowClickOff
            {
                get { return m_AllowClickOff; }
                set { m_AllowClickOff = value; }
            }

            public Camera getMainCamera()
            {
                return m_MainCamera;
            }
            public Camera getUserCamera()
            {
                return m_UserCamera;
            }
            public Camera getCurrentCamera()
            {
                if (m_UserCamera != null)
                {
                    return m_UserCamera;
                }
                return m_MainCamera;
            }
            public void setUserCamera(Camera aCamera)
            {
                m_UserCamera = aCamera;
            }


            /// <summary>
            /// Static UIManager
            /// </summary>

            static public float doubleClickTime
            {
                get
                {
                    if (s_Instance == null)
                    {
                        return 0.25f;
                    } 
                    return s_Instance.m_DoubleClickTime;
                }
            }

            static public Camera mainCamera
            {
                get
                {
                    if (instance == null)
                    {
                        return null;
                    }
                    return instance.m_MainCamera;
                }
            }

            static public Camera userCamera
            {
                get
                {
                    if (instance == null)
                    {
                        return null;
                    }
                    return instance.m_UserCamera;
                }
                set
                {
                    if (instance != null)
                    {
                        instance.m_UserCamera = value;
                    }
                }
            }
            static public Camera currentCamera
            {
                get
                {
                    if (instance == null)
                    {

                        return null;
                    }
                    if (instance.m_UserCamera != null)
                    {
                        Debug.Log("Giving User Cam");
                        return instance.m_UserCamera;
                    }
                    else
                    {
                        Debug.Log("Giving Main Cam");
                        return instance.m_MainCamera;
                    }
                }
            }
        }
    }
}