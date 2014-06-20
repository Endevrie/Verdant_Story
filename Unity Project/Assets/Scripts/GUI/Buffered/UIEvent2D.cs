using UnityEngine;
using System.Collections;


namespace OnLooker
{
    //Enum to declare the different mouse buttons
    public enum MouseButton
    {
        LEFT,
        RIGHT,
        MIDDLE
    }
    namespace UI
    {
        public class UIEvent2D : MonoBehaviour
        {
            //Data has the two attributes 'SerializeField' for making the object saveable and 'HideInInspector' to hide it from the inspector because
            //This class is used in a Custom Editor script. Editor/UIEvent2DEditor.cs
            //Components
            [HideInInspector][SerializeField]private GUITexture m_TextureComponent;
            [HideInInspector][SerializeField]private GUIText m_TextComponent;
            [HideInInspector][SerializeField]private GUITexture m_DebugTextureComponent;

            //Cache text and texture bounds here for when they are clamped to content
            [HideInInspector][SerializeField]private Vector2 m_TextBounds;
            [HideInInspector][SerializeField]private Rect m_TextureBounds;

            //Determine if the mouse is in bounds or not
            private bool m_MouseInBounds = false;
            //Determine if the mouse is in focus or not
            private bool m_InFocus = false;

            //The bounding rectangle of the control
            [HideInInspector][SerializeField]private Rect m_Bounds = new Rect(0.0f, 0.0f, 50.0f, 50.0f);
            //The flags for debug mode (editor) and clamping
            //Clamping the UI clamps the text position and texture position/size to the bounds
            [HideInInspector][SerializeField]private bool m_DebugMode = false;
            [HideInInspector][SerializeField]private bool m_ClampContent = true;
            

            
            


            //For Debugging
            private Label m_GUIMousePos = null;
            private Label m_ScreenMousePos = null;

            void Start()
            {
                //For Debugging
                GUIManager.instance.newLayout("Debug", new Rect(0.0f, 0.0f, 225.0f, 48.0f));
                m_GUIMousePos = GUIManager.instance.addLabel("Debug", "GUI: ", "GUIlbl");
                m_ScreenMousePos = GUIManager.instance.addLabel("Debug", "Screen: ", "Screenlbl");


                initialize();
                hideDebug();
#if !UNITY_EDITOR
                if (m_ClampContent == true)
                {
                    textComponent.pixelOffset = new Vector2(bounds.x, bounds.y);
                    textureComponent.pixelInset = bounds;
                    debugTextureComponent.pixelInset = bounds;
                    hideDebug();
                }
#endif
            }

            //Simple initialize function that looks for the texture and text components in the sprite sub objects
            public void initialize()
            {
                GUITexture[] textures = GetComponentsInChildren<GUITexture>();
                for (int i = 0; i < textures.Length; i++)
                {
                    if (textures[i].name == "Debug_Bounds")
                    {
                        m_DebugTextureComponent = textures[i];
                    }
                    if (textures[i].name == "Texture")
                    {
                        m_TextureComponent = textures[i];
                    }
                }
                m_TextComponent = GetComponentInChildren<GUIText>();
            }

            void Update()
            {
                Vector3 mouse = Input.mousePosition;


                Vector2 guiMousePos = convertScreenToGUI(mouse);
                Vector2 screenMousePos = convertGUIToScreen(guiMousePos);

                //Debug GUI Stuff
                m_GUIMousePos.text = "GUI: x(" + guiMousePos.x.ToString() + ") y(" + guiMousePos.y.ToString() + ")";
                m_ScreenMousePos.text = "Screen: x(" + screenMousePos.x.ToString() + ") y(" + screenMousePos.y.ToString() + ")";

                //Where the magic happens
                //If the mouse cursor is inside this control we'll need to check for several mouse events.
                if (Game.pointInRect(guiMousePos, m_Bounds))
                {
                    //One time, if the mouse was bounds
                    if (m_MouseInBounds == false)
                    {
                        m_MouseInBounds = true;
                        onMouseEnter(mouse);
                    }
                    //Is a mouse button down?
                    //Was a mouse button released
                    bool mouseAction = false;

                    //Mouse Down "Click"
                    if (Input.GetMouseButtonDown((int)MouseButton.LEFT))
                    {
                        onMouseDown(MouseButton.LEFT);
                        mouseAction = true;
                    }
                    if (Input.GetMouseButtonDown((int)MouseButton.MIDDLE))
                    {
                        onMouseDown(MouseButton.MIDDLE);
                        mouseAction = true;
                    }
                    if (Input.GetMouseButtonDown((int)MouseButton.RIGHT))
                    {
                        onMouseDown(MouseButton.RIGHT);
                        mouseAction = true;
                    }
                    //Mouse Up
                    if (Input.GetMouseButtonUp((int)MouseButton.LEFT))
                    {
                        onMouseUp(MouseButton.LEFT);
                        mouseAction = true;
                    }
                    if (Input.GetMouseButtonUp((int)MouseButton.MIDDLE))
                    {
                        onMouseUp(MouseButton.MIDDLE);
                        mouseAction = true;
                    }
                    if (Input.GetMouseButtonUp((int)MouseButton.RIGHT))
                    {
                        onMouseUp(MouseButton.RIGHT);
                        mouseAction = true;
                    }
                    //Mouse Down "Continous
                    if (Input.GetMouseButton((int)MouseButton.LEFT))
                    {
                        onMouseDownStay(MouseButton.LEFT);
                        mouseAction = true;
                    }
                    if (Input.GetMouseButton((int)MouseButton.MIDDLE))
                    {
                        onMouseDownStay(MouseButton.MIDDLE);
                        mouseAction = true;
                    }
                    if (Input.GetMouseButton((int)MouseButton.RIGHT))
                    {
                        onMouseDownStay(MouseButton.RIGHT);
                        mouseAction = true;
                    }
                    if (mouseAction == false)
                    {
                        onMouseHover();
                    }

                }
                else
                {
                    //The mouse is not in focus of the control.
                    if (m_MouseInBounds == true)
                    {
                        m_MouseInBounds = false;
                        onMouseExit(mouse);
                    }
                }
            }

            //Helpers to convert the 0,0 center to 0,0 bottom left
            Vector2 convertGUIToScreen(Vector2 aGUI)
            {
                aGUI.x = aGUI.x + Screen.width * 0.5f;
                aGUI.y = aGUI.y + Screen.height * 0.5f;
                return aGUI;
            }
            //Helpers to convert the 0,0 bottom left to the 0,0 center
            Vector2 convertScreenToGUI(Vector2 aScreen)
            {
                aScreen.x = aScreen.x - Screen.width * 0.5f;
                aScreen.y = aScreen.y - Screen.height * 0.5f;
                return aScreen;
            }

            //Helper methods to enable / disable debug GUI drawing
            public void showDebug()
            {
                if (m_DebugTextureComponent != null)
                {
                    m_DebugTextureComponent.enabled = true;
                }
            }
            public void hideDebug()
            {
                if (m_DebugTextureComponent != null)
                {
                    m_DebugTextureComponent.enabled = false;
                }
            }
            

            //Event Functions
            void onMouseEnter(Vector3 aPosition)
            {
                Debug.Log("Mouse Enter");
            }
            void onMouseExit(Vector3 aPosition)
            {
                Debug.Log("Mouse Exit");
            }
            void onMouseDown(MouseButton aButton)
            {
                Debug.Log("Mouse Down");
            }
            void onMouseDownStay(MouseButton aButton)
            {
                Debug.Log("Mouse Down Stay");
            }
            void onMouseUp(MouseButton aButton)
            {
                Debug.Log("Mouse Up");
            }
            void onMouseHover()
            {
                Debug.Log("Mouse Hover");
            }

            //Properties
            //Component getters
            public GUIText textComponent
            {
                get { return m_TextComponent; }
            }
            public GUITexture textureComponent
            {
                get { return m_TextureComponent; }
            }
            public GUITexture debugTextureComponent
            {
                get { return m_DebugTextureComponent; }
            }
            //Check for mouse in bounds
            public bool mouseInBounds
            {
                get { return m_MouseInBounds; }
            }
            //Accessors for flags
            public bool debugMode
            {
                get { return m_DebugMode; }
                set { m_DebugMode = value; }
            }
            public bool clampContent
            {
                get { return m_ClampContent; }
                set { m_ClampContent = value; }
            }
            //Bounds getters
            public Vector2 textBounds
            {
                get { return m_TextBounds; }
                set { m_TextBounds = value; }
            }
            public Rect textureBounds
            {
                get { return m_TextureBounds; }
                set { m_TextureBounds = value; }
            }
            public Rect bounds
            {
                get { return m_Bounds; }
                set { m_Bounds = value; }
            }

            //Text Accessors
            public string text
            {
                get { return m_TextComponent.text; }
                set { m_TextComponent.text = value; }
            }
            public Font font
            {
                get { return m_TextComponent.font; }
                set { m_TextComponent.font = value; }
            }
            public FontStyle fontStyle
            {
                get { return m_TextComponent.fontStyle; }
                set { m_TextComponent.fontStyle = value; }
            }
            public int fontSize
            {
                get { return m_TextComponent.fontSize; }
                set { m_TextComponent.fontSize = value; }
            }
            public Color fontColor
            {
                get { return m_TextComponent.color; }
                set { m_TextComponent.color = value; }
            }
            //Texture Accessors
            public Texture texture
            {
                get { return m_TextureComponent.texture; }
                set { m_TextureComponent.texture = value; }
            }
            public Color textureColor
            {
                get { return m_TextureComponent.color; }
                set { m_TextureComponent.color = value; }
            }

        }
    }
}