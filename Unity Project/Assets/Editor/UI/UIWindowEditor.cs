using UnityEngine;
using UnityEditor;
using System.Collections;

namespace OnLooker
{
    namespace UI
    {
        public enum UIWindowMode
        {
            M_2D,
            M_3D
            
        }
        public enum UIControlType2
        {
            TEXT_2,
            TEXTURE_2
        }
        public enum UIControlType3
        {
            TEXT_3,
            TEXTURE_3
        }

        public class UIWindow : EditorWindow
        {

            [SerializeField]
            private UIWindowMode m_Mode;
            [SerializeField]
            private UIControlType2 m_SelectedType2;
            [SerializeField]
            private UIControlType3 m_SelectedType3;


            [SerializeField]
            private Transform m_Root;
            [SerializeField]
            private UIManager m_Manager;

            [SerializeField]
            private UIParameters m_Parameters = new UIParameters();

            [MenuItem("OnLooker/UI")]
            static void Init()
            {
                UIWindow window = EditorWindow.GetWindow<UIWindow>();
                window.title = "OnLooker UI";
            }

            void Update()
            {
                if (m_Root == null)
                {
                    GameObject root = GameObject.Find("UI");
                    if (root != null)
                    {
                        m_Root = root.GetComponent<Transform>();
                    }
                    else
                    {
                        root = new GameObject("UI");
                        m_Manager = root.AddComponent<UIManager>();
                    }
                }
            }

            void OnGUI()
            {
                EditorGUILayout.BeginVertical();
                GUI.enabled = false;
                m_Root = UIEditorUtilities.transformField("Root", m_Root);
                m_Manager = (UIManager)EditorGUILayout.ObjectField("Manager", m_Manager, typeof(UIManager), true);
                GUI.enabled = true;

                if (m_Root != null)
                {
                    if (m_Manager == null)
                    {
                        m_Manager = m_Root.GetComponent<UIManager>();
                    }
                    //Draw the Header
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("GUI Creation Properties", EditorStyles.boldLabel);
                    if (m_Mode == UIWindowMode.M_2D)
                    {
                        m_SelectedType2 = (UIControlType2)EditorGUILayout.EnumPopup("Type", m_SelectedType2);
                        if (GUILayout.Button("3D"))
                        {
                            m_Mode = UIWindowMode.M_3D;
                        }
                    }
                    else
                    {
                        m_SelectedType3 = (UIControlType3)EditorGUILayout.EnumPopup("Type", m_SelectedType3);
                        if (GUILayout.Button("2D"))
                        {
                            m_Mode = UIWindowMode.M_2D;
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    switch (m_Mode)
                    {
                        case UIWindowMode.M_2D:
                            gui2D();
                            break;
                        case UIWindowMode.M_3D:
                            gui3D();
                            break;
                    }
                }
                EditorGUILayout.EndVertical();
            }


            void gui2D()
            {
                switch (m_SelectedType2)
                {
                    case UIControlType2.TEXT_2:
                        drawText2Menu();
                        break;
                    case UIControlType2.TEXTURE_2:
                        drawTexture2Menu();
                        break;
                }
            }
            void gui3D()
            {
                switch (m_SelectedType3)
                {
                    case UIControlType3.TEXT_3:
                        drawText3Menu();
                        break;
                    case UIControlType3.TEXTURE_3:
                        drawTexture3Menu();
                        break;
                }
            }
            

            //Example Menu Drawing

            void drawText2Menu()
            {
                m_Parameters.toggleName = EditorGUILayout.TextField("Toggle Name", m_Parameters.toggleName);
                m_Parameters.interactive = EditorGUILayout.Toggle("Interactive", m_Parameters.interactive);
                m_Parameters.text = EditorGUILayout.TextField("Text", m_Parameters.text);
                m_Parameters.font = UIEditorUtilities.fontField("Font", m_Parameters.font);
                m_Parameters.fontSize = EditorGUILayout.IntField("Font Size", m_Parameters.fontSize);
                m_Parameters.fontStyle = UIEditorUtilities.fontStyleEnum("Font Style", m_Parameters.fontStyle);
                m_Parameters.color = EditorGUILayout.ColorField("Font Color", m_Parameters.color);
                m_Parameters.scale = EditorGUILayout.FloatField("Scale", m_Parameters.scale);
                m_Parameters.bounds = EditorGUILayout.RectField("Bounds", m_Parameters.bounds);
                if (GUILayout.Button("Create"))
                {
                    m_Manager.createText2(m_Parameters);
                }
            }
            void drawTexture2Menu()
            {
                m_Parameters.toggleName = EditorGUILayout.TextField("Toggle Name", m_Parameters.toggleName);
                m_Parameters.interactive = EditorGUILayout.Toggle("Interactive", m_Parameters.interactive);
                m_Parameters.color = EditorGUILayout.ColorField("Font Color", m_Parameters.color);
                m_Parameters.scale = EditorGUILayout.FloatField("Scale", m_Parameters.scale);
                m_Parameters.bounds = EditorGUILayout.RectField("Bounds", m_Parameters.bounds);
                m_Parameters.texture = UIEditorUtilities.textureField("Texture", m_Parameters.texture);
                if (GUILayout.Button("Create"))
                {
                    m_Manager.createTexture2(m_Parameters);
                }
            }
            void drawText3Menu()
            {
                m_Parameters.toggleName = EditorGUILayout.TextField("Toggle Name", m_Parameters.toggleName);
                m_Parameters.interactive = EditorGUILayout.Toggle("Interactive", m_Parameters.interactive);
                m_Parameters.text = EditorGUILayout.TextField("Text", m_Parameters.text);
                m_Parameters.font = UIEditorUtilities.fontField("Font", m_Parameters.font);
                m_Parameters.fontSize = EditorGUILayout.IntField("Font Size", m_Parameters.fontSize);
                m_Parameters.fontStyle = UIEditorUtilities.fontStyleEnum("Font Style", m_Parameters.fontStyle);
                m_Parameters.color = EditorGUILayout.ColorField("Font Color", m_Parameters.color);
                m_Parameters.position = EditorGUILayout.Vector3Field("Position", m_Parameters.position);
                m_Parameters.rotation = EditorGUILayout.Vector3Field("Rotation", m_Parameters.rotation);
                if (GUILayout.Button("Create"))
                {
                    m_Manager.createText3(m_Parameters);
                }
            }
            void drawTexture3Menu()
            {
                m_Parameters.toggleName = EditorGUILayout.TextField("Toggle Name", m_Parameters.toggleName);
                m_Parameters.interactive = EditorGUILayout.Toggle("Interactive", m_Parameters.interactive);
                m_Parameters.color = EditorGUILayout.ColorField("Font Color", m_Parameters.color);
                m_Parameters.position = EditorGUILayout.Vector3Field("Position", m_Parameters.position);
                m_Parameters.rotation = EditorGUILayout.Vector3Field("Rotation", m_Parameters.rotation);
                m_Parameters.texture = UIEditorUtilities.textureField("Texture", m_Parameters.texture);
                if (GUILayout.Button("Create"))
                {
                    m_Manager.createTexture3(m_Parameters);
                }
            }

            //Todo
            //Make Menu Draw Functions for Basic Controls
            //-Label
            //-Image
            //-Button
            //-TextField

        }
    }
}