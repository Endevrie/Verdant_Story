using UnityEngine;
using UnityEditor;
using System.Collections;
namespace OnLooker
{

    namespace UI
    {

        public class UIEditorUtilities
        {
            public static void toggleInspector(UIToggle aToggle)
            {
                
                //Debug
                if (aToggle.debug == true)
                {
                    GUI.enabled = false;
                    EditorGUILayout.ObjectField("Manager", aToggle.manager, typeof(UIManager), true);
                    GUI.enabled = true;
                }
                aToggle.toggleName = EditorGUILayout.TextField("Toggle Name", aToggle.toggleName);
                //Debug
                if (aToggle.debug == true)
                {
                    GUI.enabled = false;
                    //Debug.Log(aToggle.mouseInBounds);
                    EditorGUILayout.Toggle("Mouse In Bounds", aToggle.mouseInBounds);
                    EditorGUILayout.Toggle("Is Focused", aToggle.isFocused);
                    GUI.enabled = true;
                }
                aToggle.isInteractive = EditorGUILayout.Toggle("Interactive", aToggle.isInteractive);
                aToggle.trapDoubleClick = EditorGUILayout.Toggle("Trap Double Click", aToggle.trapDoubleClick);
                
            }
            public static void toggle2Inspector(UIToggle2 aToggle)
            {
                Rect bounds = EditorGUILayout.RectField("Bounds", aToggle.bounds);
                if (aToggle.debug == true)
                {
                    GUI.enabled = false;
                    EditorGUILayout.RectField("Scaled Bounds", aToggle.scaledBounds);
                    GUI.enabled = true;
                }
                aToggle.uniformScale = EditorGUILayout.Toggle("Uniform scale", aToggle.uniformScale);
                if (aToggle.uniformScale == false)
                {
                    aToggle.scale = EditorGUILayout.Vector2Field("Scale", aToggle.scale);
                }
                else
                {
                    Vector2 scale = aToggle.scale;
                    scale.x = EditorGUILayout.FloatField("Scale", aToggle.scale.x);
                    aToggle.scale = scale;
                }
                aToggle.setBounds(bounds.x, bounds.y, bounds.width, bounds.height);

            }
            public static void toggle3Inspector(UIToggle3 aToggle)
            {
                aToggle.translationSpeed = EditorGUILayout.FloatField("Translation Speed", aToggle.translationSpeed);
                aToggle.position = EditorGUILayout.Vector3Field("Position", aToggle.position);
                aToggle.rotation = EditorGUILayout.Vector3Field("Rotation", aToggle.rotation);
                aToggle.anchorTarget = (UIAnchorTarget)EditorGUILayout.EnumPopup("Anchor Target", aToggle.anchorTarget);
                aToggle.target = (Transform)EditorGUILayout.ObjectField("Target", aToggle.target, typeof(Transform), true);

                if (Application.isPlaying == false)
                {
                    aToggle.updateTransform(false);
                }
            }

            public static void text2Inspector(UIText2 aToggle)
            {
                
                if(aToggle.debug == true)
                {
                    GUI.enabled = false;
                    EditorGUILayout.ObjectField("Text Component", aToggle.guiText, typeof(GUIText), true);
                    EditorGUILayout.ObjectField("Debug Bounds", aToggle.guiTexture, typeof(GUITexture), true);
                    GUI.enabled = true;
                }
                aToggle.text = EditorGUILayout.TextField("Text", aToggle.text);
                aToggle.font = (Font)EditorGUILayout.ObjectField("Font", aToggle.font, typeof(Font), true);
                aToggle.fontSize = EditorGUILayout.IntField("Font Size", aToggle.fontSize);
                aToggle.fontStyle = (FontStyle)EditorGUILayout.EnumPopup("Font Style", aToggle.fontStyle);
                aToggle.fontColor = EditorGUILayout.ColorField("Font Color", aToggle.fontColor);
            }
            public static void texture2Inspector(UITexture2 aToggle)
            {
                if (aToggle.debug == true)
                {
                    GUI.enabled = false;
                    EditorGUILayout.ObjectField("Texture Component", aToggle.guiTexture, typeof(GUITexture), true);
                    GUI.enabled = true;
                }
                aToggle.texture = (Texture)EditorGUILayout.ObjectField("Texture", aToggle.texture, typeof(Texture), true);
                aToggle.color = EditorGUILayout.ColorField("Color", aToggle.color);
            }

            public static void text3Inspector(UIText3 aToggle)
            {
                if (aToggle.debug == true)
                {
                    GUI.enabled = false;
                    EditorGUILayout.ObjectField("Text Mesh", aToggle.textMesh, typeof(TextMesh), true);
                    EditorGUILayout.ObjectField("Mesh Renderer", aToggle.meshRenderer, typeof(MeshRenderer), true);
                    EditorGUILayout.ObjectField("Material", aToggle.material, typeof(Material), true);
                    GUI.enabled = true;  
                }
                string text = EditorGUILayout.TextField("Text", aToggle.text);
                
                    if(text != aToggle.text)
                    {
                        aToggle.textChanged();
                    }
                aToggle.text = text;
                aToggle.font = (Font)EditorGUILayout.ObjectField("Font", aToggle.font, typeof(Font), true);
                aToggle.fontSize = EditorGUILayout.IntField("Font Size", aToggle.fontSize);
                aToggle.fontStyle = (FontStyle)EditorGUILayout.EnumPopup("Font Style", aToggle.fontStyle);
                aToggle.color = EditorGUILayout.ColorField("Font Color", aToggle.color);
            }
            public static void texture3Inspector(UITexture3 aToggle)
            {
                if (aToggle.debug == true)
                {
                    GUI.enabled = false;
                    EditorGUILayout.ObjectField("Mesh Filter", aToggle.meshFilter, typeof(MeshFilter), true);
                    EditorGUILayout.ObjectField("Mesh Renderer", aToggle.meshRenderer, typeof(MeshRenderer), true);
                    GUI.enabled = true;
                }

                aToggle.color = EditorGUILayout.ColorField("Color", aToggle.color);
                aToggle.texture = (Texture)EditorGUILayout.ObjectField("Texture", aToggle.texture, typeof(Texture), true);
            }

            public static UIToggle toggleField(string aContent, UIToggle aToggle)
            {
                return (UIToggle)EditorGUILayout.ObjectField(aContent, aToggle, typeof(UIToggle), true);
            }

            public static UIText2 text2Field(string aContent, UIText2 aToggle)
            {
                return (UIText2)EditorGUILayout.ObjectField(aContent, aToggle, typeof(UIText2), true);
            }
            public static UITexture2 text2Field(string aContent, UITexture2 aToggle)
            {
                return (UITexture2)EditorGUILayout.ObjectField(aContent, aToggle, typeof(UITexture2), true);
            }
            public static UIText3 text2Field(string aContent, UIText3 aToggle)
            {
                return (UIText3)EditorGUILayout.ObjectField(aContent, aToggle, typeof(UIText3), true);
            }
            public static UITexture3 text2Field(string aContent, UITexture3 aToggle)
            {
                return (UITexture3)EditorGUILayout.ObjectField(aContent, aToggle, typeof(UITexture3), true);
            }
            
        }
    }
}