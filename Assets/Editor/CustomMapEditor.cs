using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace FowlPlay
{
    [CustomEditor(typeof(Map))]
    public class CustomMapEditor : Editor
    {
        public static bool ShowGrid = true;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            GUI.enabled = false;
            SerializedProperty prop = serializedObject.FindProperty("m_Script");
            EditorGUILayout.PropertyField(prop, true, new GUILayoutOption[0]);
            GUI.enabled = true;

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("_sizeX"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_sizeY"));

            serializedObject.ApplyModifiedProperties();

            Map map = (Map)target;

            GameObject gameObject = Selection.activeGameObject;

            EditorGUILayout.Space();

            GenerateMap(map);

            if (map.Grid[0, 0] != null)
            {
                ShowGrid = EditorGUILayout.Foldout(ShowGrid, "Map (Size: [" + map.Grid.GetLength(0) + ", " + map.Grid.GetLength(1) + "])");
                if (ShowGrid)
                {
                    GUIStyle tableStyle = new GUIStyle("box")
                    {
                        padding = new RectOffset(10, 10, 10, 10)
                    };
                    tableStyle.margin.left = 32;

                    GUIStyle columnStyle = new GUIStyle
                    {
                        fixedWidth = 105
                    };

                    GUIStyle rowStyle = new GUIStyle
                    {
                        fixedHeight = 22
                    };

                    GUIStyle cornerLabelStyle = new GUIStyle
                    {
                        fixedWidth = 42,
                        alignment = TextAnchor.MiddleRight,
                        fontStyle = FontStyle.BoldAndItalic,
                        fontSize = 14
                    };
                    cornerLabelStyle.padding.top = -5;

                    GUIStyle normalStyle = new GUIStyle("popup")
                    {
                        fontStyle = FontStyle.Normal,
                        fontSize = 12
                    };

                    GUIStyle obstacleStyle = new GUIStyle(normalStyle)
                    {
                        fontStyle = FontStyle.BoldAndItalic
                    };
                    obstacleStyle.normal.textColor = Color.red;

                    GUIStyle occupiedStyle = new GUIStyle(normalStyle)
                    {
                        fontStyle = FontStyle.BoldAndItalic
                    };
                    occupiedStyle.normal.textColor = Color.yellow;

                    EditorGUI.indentLevel++;
                    for (ushort i = 0; i < 1; i++)
                    {
                        EditorGUI.indentLevel = 0;

                        EditorGUILayout.BeginHorizontal(tableStyle);
                        for (int x = 0; x < map.Grid.GetLength(0); x++)
                        {
                            EditorGUILayout.BeginVertical(columnStyle);
                            for (int y = 0; y < map.Grid.GetLength(1); y++)
                            {
                                EditorGUILayout.BeginHorizontal(rowStyle);
                                GUIStyle styleToUse = normalStyle;

                                switch (map.Grid[x, y].CollisionType)
                                {
                                    case CollisionType.Obstacle:
                                        styleToUse = obstacleStyle;
                                        break;
                                    case CollisionType.Occupied:
                                        styleToUse = occupiedStyle;
                                        break;
                                }

                                map.Grid[x, y].SetCollisionType((CollisionType)EditorGUILayout.EnumPopup(map.Grid[x, y].CollisionType, styleToUse));

                                EditorGUILayout.EndHorizontal();
                            }
                            EditorGUILayout.EndVertical();
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                }
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void GenerateMap(Map map)
        {
            Node[,] oldMap = map.Grid;
            map.SetMap(map.SizeX, map.SizeY);
            for (int x = 0; x < map.Grid.GetLength(0); x++)
            {
                for (int y = 0; y < map.Grid.GetLength(1); y++)
                {
                    if (x < oldMap.GetLength(0) && y < oldMap.GetLength(1) && oldMap[x, y] != null)
                        map.Grid[x, y] = oldMap[x, y];
                    else
                        map.Grid[x, y] = new Node(x, y, 0);
                }
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
