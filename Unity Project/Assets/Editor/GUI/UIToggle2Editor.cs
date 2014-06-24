using UnityEngine;
using UnityEditor;
using System.Collections;

namespace OnLooker
{
    namespace UI
    {
        //Custom editor for UIEvent2D
        //The purpose of this editor script is to make the UIEvent2D easier 
        //to use instead of searching through dozens of subobjects to modify a few values.
        //This class unifies as much as possible of the subobject data.
        [CustomEditor(typeof(UIToggle2))]
        public class UIToggle2Editor : Editor
        {
            public override void OnInspectorGUI()
            {
                DrawDefaultInspector();
                UIToggle2 inspected = (UIToggle2)target;
                if(inspected == null)
                {
                    return;
                }
                //Initialize the UIEvent2D to make sure it has the proper components
                inspected.initialize();
                EditorGUILayout.BeginVertical();
                    //Draw Required Objects
                    //EditorGUILayout.ObjectField("Texture", inspected.textureComponent, typeof(GUITexture), true);
                    //EditorGUILayout.ObjectField("Text", inspected.textComponent, typeof(GUIText), true);
                    //EditorGUILayout.ObjectField("Debug", inspected.debugTextureComponent, typeof(GUITexture), true);
                    //Draw the two flags
                    inspected.debugMode = EditorGUILayout.Toggle("Debug Mode", inspected.debugMode);
                    inspected.scaleFont = EditorGUILayout.Toggle("Scale Font", inspected.scaleFont);

                    EditorGUILayout.Space();
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.Space();
                    GUILayout.Label("Size and Scale");
                    EditorGUILayout.EndHorizontal();
                    //Control Size
                    Rect bounds = EditorGUILayout.RectField("Size", inspected.size);
                    Vector2 textPosition = EditorGUILayout.Vector2Field("Text Position", inspected.textBounds);
                    Rect textureBounds = EditorGUILayout.RectField("Texture Bounds", inspected.textureBounds);
                    inspected.uniformScale = EditorGUILayout.Toggle("Uniform Scale", inspected.uniformScale);
                    if (inspected.uniformScale == true)
                    {
                        float lscale = EditorGUILayout.FloatField("Scale", inspected.scale.x);
                        if (lscale < 0.0f)
                        {
                            lscale = 0.0f;
                        }
                        Vector2 scale = inspected.scale;
                        scale.Set(lscale, lscale);
                        inspected.scale = scale;
                    }
                    else
                    {
                        inspected.scale = EditorGUILayout.Vector2Field("Scale", inspected.scale);
                    }

                    //Sprite Properties
                    EditorGUILayout.Space();
                    EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.Space();
                        GUILayout.Label("Sprite Properties");
                    EditorGUILayout.EndHorizontal();
                    inspected.trapDoubleClick = EditorGUILayout.Toggle("Trap Double Click", inspected.trapDoubleClick);
                    inspected.text = EditorGUILayout.TextField("Text", inspected.text);
                    inspected.font = (Font)EditorGUILayout.ObjectField("Font", inspected.font, typeof(Font), true);
                    int fontSize = EditorGUILayout.IntField("Font Size", inspected.fontSize);
                    inspected.fontStyle = (FontStyle)EditorGUILayout.EnumPopup("Font Style", inspected.fontStyle);
                    inspected.fontColor = EditorGUILayout.ColorField("Font Color", inspected.fontColor);
                    inspected.textureColor = EditorGUILayout.ColorField("Texture Color", inspected.textureColor);    
                    inspected.texture = (Texture)EditorGUILayout.ObjectField("Texture", inspected.texture, typeof(Texture), true);
                EditorGUILayout.EndVertical();

                

                inspected.setBounds(bounds.x, bounds.y, bounds.width, bounds.height);
                inspected.setTextBounds(textPosition.x, textPosition.y, fontSize);
                inspected.setTextureBounds(textureBounds.x, textureBounds.y, textureBounds.width, textureBounds.height);
                //Check debugMode flag and handle it
                if (inspected.debugMode == false)
                {
                    inspected.hideDebug();
                }
                else
                {
                    inspected.showDebug();
                    if (Application.isPlaying == false)
                    {
                        inspected.debugTextureComponent.pixelInset = inspected.bounds;
                    }
                }
            
            }
        }
    }
}