using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Headache.TopdownComponent;

namespace Headache.TopdownComponent
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(TopdownTransform))]


    public class TopdownTransformEditor : Editor
    {
        // Hide default Unity gizmos handles
        void OnEnable()
        {
            Tools.hidden = true;
        }

        // Inspector Properties
        public override void OnInspectorGUI()
        {
            
            TopdownTransform tdTransform = target as TopdownTransform;

            // transform inspector field
            EditorGUI.BeginChangeCheck();
            Vector3 position = EditorGUILayout.Vector3Field("Position", tdTransform.position);
            if(EditorGUI.EndChangeCheck())
            {
                //update object position if moving
                Undo.RecordObjects(new Object[] { tdTransform, tdTransform.transform }, "Set Object Position");
                EditorUtility.SetDirty(tdTransform);
                tdTransform.position = position;
            }

            EditorGUI.BeginChangeCheck();
            tdTransform.showFloor = EditorGUILayout.Toggle("Show Floor",tdTransform.showFloor);
            if (EditorGUI.EndChangeCheck())
            {
                SceneView.RepaintAll();
            }



        }

        // Draw custom gizmos handle on the scene
        void OnSceneGUI()
        {
            TopdownTransform tdTransform = target as TopdownTransform;

            // show line on the floor with height if "show Floor" toggle is enabled
            if (tdTransform.showFloor)
                DisplayFloor(tdTransform);

            EditorGUI.BeginChangeCheck();


            if (Selection.gameObjects.Length > 1)
            {
                List<TopdownTransform> tdtransforms = new List<TopdownTransform>();

                // Get every TopDownTransform object selected
                for (int i = 0; i < Selection.gameObjects.Length; i++)
                {
                    TopdownTransform tdtransform = Selection.gameObjects[i].GetComponent<TopdownTransform>();
                    if (tdtransform != null)
                        tdtransforms.Add(tdtransform);
                }

                // Get selected objects position
                Vector3[] positions = new Vector3[tdtransforms.Count];
                for(int i = 0; i < positions.Length; i++)
                {
                    positions[i] = tdtransforms[i].FloorPosition();
                }

                // Get average position of objects;
                Vector3 avgPos = TopdownUtils.VectorsCenter(positions);



                Vector3 handlePos = Handles.PositionHandle(avgPos, Quaternion.identity);

                if (EditorGUI.EndChangeCheck())
                {

                    RecordChanges(tdtransforms.ToArray());  

                        //Undo.RecordObjects(new Object[] { tdTransform, tdTransform.transform }, "Set Object Position");
                    foreach (TopdownTransform tdTrans in tdtransforms)
                    {
                        EditorUtility.SetDirty(tdTrans);
                        tdTrans.position = tdTrans.WorldToTdPosition((handlePos - avgPos)+tdTrans.FloorPosition());
                        Repaint();
                    }
                }
            }
            else
            {
                Vector3 handlePos = Handles.PositionHandle(tdTransform.FloorPosition(), Quaternion.identity);

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObjects(new Object[] { tdTransform, tdTransform.transform }, "Set Object Position");
                    EditorUtility.SetDirty(tdTransform);
                    tdTransform.position = tdTransform.WorldToTdPosition(handlePos);
                    Repaint();
                }
            }
            
        }

        // Display floor position
        void DisplayFloor(TopdownTransform tdtransform)
        {
            // Check if any sprite renderer on the object
            SpriteRenderer sr = tdtransform.GetComponent<SpriteRenderer>();

            // Custom offset
            Vector3 leftOffset, rightOffset;

            // If Sprite Renderer and sprite found, make a custom offset based on sprite size
            if (sr != null && sr.sprite != null)
            {
                leftOffset = new Vector3(-sr.sprite.bounds.extents.x,0);
                leftOffset = tdtransform.transform.TransformVector(leftOffset);

                rightOffset = new Vector3(sr.sprite.bounds.extents.x,0);
                rightOffset = tdtransform.transform.TransformVector(rightOffset);

            }
            else
            {
                leftOffset = Vector3.left;
                rightOffset = Vector3.right;
            }

            // Apply offset

            Vector3 p1 = tdtransform.FloorPosition() + leftOffset;
            Vector3 p2 = tdtransform.FloorPosition() + rightOffset;

            Handles.color = Color.black;
            Handles.DrawLine(p1, p2);

            // Write label position

            Handles.Label(p1 + new Vector3(0, 0.2f, 0) * HandleUtility.GetHandleSize(p1), "Height: " + tdtransform.position.y);
        }
    
        void RecordChanges(TopdownTransform[] tdtransforms)
        {
            Object[] modifiedObjects = new Object[tdtransforms.Length * 2];

            for(int i = 0, j = 0; i < tdtransforms.Length; i ++, j+=2)
            {
                modifiedObjects[j] = tdtransforms[i];
                modifiedObjects[j+1] = tdtransforms[i].transform;
            }
            Undo.RecordObjects(modifiedObjects, "Set Objects Position");

        }

        // Enable default Unity gizmos handles
        void OnDisable()
        {
            Tools.hidden = false;
        }

    }
}
