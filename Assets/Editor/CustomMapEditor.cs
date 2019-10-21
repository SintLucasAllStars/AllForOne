using UnityEditor;
using UnityEngine;

namespace MechanicFever
{
    [CustomEditor(typeof(Map))]
    public class CustomMapEditor : Editor
    {
        public static bool ShowGrid = true;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("_sizeX"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_sizeZ"));

            EditorGUILayout.PropertyField(serializedObject.FindProperty("_tile"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_obstacles"));

            serializedObject.ApplyModifiedProperties();

            Map map = (Map)target;

            GameObject gameObject = Selection.activeGameObject;

            EditorGUILayout.Space();

            if (GUILayout.Button("Generate Map"))
            {
                GenerateMap(map);
            }

            if (map.Grid[0] != null)
            {
                ShowGrid = EditorGUILayout.Foldout(ShowGrid, "Map (Size: [" + map.Grid.Length + ", " + map.Grid[0].Columns.Length + "])");
                if (ShowGrid)
                {
                    EditorGUI.indentLevel++;
                    for (ushort i = 0; i < 1; i++)
                    {
                        EditorGUI.indentLevel = 0;

                        GUIStyle tableStyle = new GUIStyle("box")
                        {
                            padding = new RectOffset(10, 10, 10, 10)
                        };
                        tableStyle.margin.left = 32;

                        GUIStyle columnStyle = new GUIStyle
                        {
                            fixedWidth = 40
                        };

                        GUIStyle rowStyle = new GUIStyle
                        {
                            fixedHeight = 40
                        };

                        GUIStyle cornerLabelStyle = new GUIStyle
                        {
                            fixedWidth = 35,
                            alignment = TextAnchor.MiddleRight,
                            fontStyle = FontStyle.BoldAndItalic,
                            fontSize = 14
                        };
                        cornerLabelStyle.padding.top = -5;

                        GUIStyle normalStyle = new GUIStyle("popup")
                        {
                            fontStyle = FontStyle.Normal,
                            fontSize = 5,
                            fixedWidth = 35,
                            fixedHeight = 35
                        };

                        GUIStyle wallStyle = new GUIStyle(normalStyle)
                        {
                            fontStyle = FontStyle.BoldAndItalic
                        };
                        wallStyle.normal.textColor = Color.red;

                        GUIStyle occupiedStyle = new GUIStyle(normalStyle)
                        {
                            fontStyle = FontStyle.BoldAndItalic
                        };
                        occupiedStyle.normal.textColor = Color.yellow;

                        EditorGUILayout.BeginHorizontal(tableStyle);
                        for (int x = 0; x < map.Grid.Length; x++)
                        {
                            EditorGUILayout.BeginVertical(columnStyle);
                            for (int z = 0; z < map.Grid[x].Columns.Length; z++)
                            {
                                EditorGUILayout.BeginHorizontal(rowStyle);
                                GUIStyle styleToUse = normalStyle;

                                switch (map.Grid[x].Columns[z].CollisionType)
                                {
                                    case CollisionType.Obstacle_01:
                                        styleToUse = wallStyle;
                                        break;
                                    case CollisionType.Obstacle_02:
                                        styleToUse = wallStyle;
                                        break;
                                    case CollisionType.Occupied:
                                        styleToUse = occupiedStyle;
                                        break;
                                }

                                map.Grid[x].Columns[z].SetCollisionType((CollisionType)EditorGUILayout.EnumPopup(map.Grid[x].Columns[z].CollisionType, styleToUse));
                                map.Grid[x].Columns[z].SetPosition(x, 0, z);

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
            if (map.Grid.Length != map.SizeX)
                map.SetMap(new MapX[map.SizeX]);
            for (int x = 0; x < map.Grid.Length; x++)
            {
                map.Grid[x] = new MapX
                {
                    Columns = new Node[map.SizeZ]
                };
                for (int z = 0; z < map.Grid[x].Columns.Length; z++)
                {
                    map.Grid[x].Columns[z] = new Node(x, z, 0);
                }
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}