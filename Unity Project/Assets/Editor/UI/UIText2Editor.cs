using UnityEngine;
using UnityEditor;
using System.Collections;

namespace OnLooker
{
    namespace UI
    {

        [CustomEditor(typeof(UIText2))]
        public class UIText2Editor : Editor
        {
            public override void OnInspectorGUI()
            {
                DrawDefaultInspector();

                UIText2 inspected = (UIText2)target;

                EditorGUILayout.BeginVertical();
                inspected.debug = EditorGUILayout.Toggle("Debug", inspected.debug);
                inspected.setDefault(true);

                UIEditorUtilities.toggleInspector(inspected);
                //UIEditorUtilities.toggle2Inspector(inspected);
                Rect bounds = inspected.bounds;
                Vector2 position = EditorGUILayout.Vector2Field("Position", new Vector2(bounds.x, bounds.y));
                if (inspected.debug == true)
                {
                    GUI.enabled = false;
                    EditorGUILayout.RectField("Scaled Bounds", inspected.scaledBounds);
                    GUI.enabled = true;
                }
                inspected.uniformScale = EditorGUILayout.Toggle("Uniform Scale", inspected.uniformScale);
                if (inspected.uniformScale == false)
                {
                    inspected.scale = EditorGUILayout.Vector2Field("Scale", inspected.scale);
                }
                else
                {
                    Vector2 scale = inspected.scale;
                    scale.x = EditorGUILayout.FloatField("Scale", inspected.scale.x);
                    inspected.scale = scale;
                }
                UIEditorUtilities.text2Inspector(inspected);

                inspected.setBounds(position.x, position.y, bounds.width, bounds.height);

                //GUIText text = inspected.guiText;
                //Vector2 position = text.pixelOffset;
                //position.x = inspected.scaledBounds.x;
                //position.y = inspected.scaledBounds.y;
                //text.pixelOffset = position;

                if (inspected.debug == true)
                {
                    inspected.showBounds();
                    GUITexture guiTexture = inspected.guiTexture;
                    guiTexture.pixelInset = inspected.scaledBounds;
                }
                else
                {
                    inspected.hideBounds();
                }

                EditorGUILayout.EndVertical();


            }
            
        }
    }
}