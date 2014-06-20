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
        [CustomEditor(typeof(UIEvent2D))]
        public class UIEvent2DEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                DrawDefaultInspector();
                UIEvent2D inspected = (UIEvent2D)target;
                if(inspected == null)
                {
                    return;
                }
                //Initialize the UIEvent2D to make sure it has the proper components
                inspected.initialize();
                EditorGUILayout.BeginVertical();
                    //Draw Required Objects
                    EditorGUILayout.ObjectField("Texture", inspected.textureComponent, typeof(GUITexture), true);
                    EditorGUILayout.ObjectField("Text", inspected.textComponent, typeof(GUIText), true);
                    EditorGUILayout.ObjectField("Debug", inspected.debugTextureComponent, typeof(GUITexture), true);
                    //Draw the two flags
                    inspected.debugMode = EditorGUILayout.Toggle("Debug Mode", inspected.debugMode);
                    inspected.clampContent = EditorGUILayout.Toggle("Clamp Content", inspected.clampContent);
                    //If not clamping content,  give the user the option of setting the text bounds
                    if (inspected.clampContent == false)
                    {
                        inspected.bounds = EditorGUILayout.RectField("Bounds", inspected.bounds);
                        inspected.textBounds = EditorGUILayout.Vector2Field("Text Bounds", inspected.textBounds);
                        inspected.textureBounds = EditorGUILayout.RectField("Texture Bounds", inspected.textureBounds);


                        inspected.textComponent.pixelOffset = inspected.textBounds;
                        inspected.textureComponent.pixelInset = inspected.textureBounds;
                    }
                    else//Other wise clamp the pixel offsets and give the user the option to only set the bounds
                    {
                        inspected.bounds = EditorGUILayout.RectField("Bounds",inspected.bounds);

                        inspected.textComponent.pixelOffset = new Vector2(inspected.bounds.x, inspected.bounds.y);
                        inspected.textureComponent.pixelInset = inspected.bounds;
                    }
                    //Check debugMode flag and handle it
                    if (inspected.debugMode == false)
                    {
                        inspected.hideDebug();
                    }
                    else
                    {
                        inspected.showDebug();
                        inspected.debugTextureComponent.pixelInset = inspected.bounds;
                    }
                    
                    //Sprite Properties
                    EditorGUILayout.Space();
                    EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.Space();
                        GUILayout.Label("Sprite Properties");
                    EditorGUILayout.EndHorizontal();

                    inspected.text = EditorGUILayout.TextField("Text", inspected.text);
                    inspected.font = (Font)EditorGUILayout.ObjectField("Font", inspected.font, typeof(Font), true);
                    inspected.fontSize = EditorGUILayout.IntField("Font Size", inspected.fontSize);
                    inspected.fontStyle = (FontStyle)EditorGUILayout.EnumPopup("Font Style", inspected.fontStyle);
                    inspected.fontColor = EditorGUILayout.ColorField("Font Color", inspected.fontColor);
                    inspected.textureColor = EditorGUILayout.ColorField("Texture Color", inspected.textureColor);    
                    inspected.texture = (Texture)EditorGUILayout.ObjectField("Texture", inspected.texture, typeof(Texture), true);
                    
                    

                EditorGUILayout.EndVertical();

            }
        }
    }
}