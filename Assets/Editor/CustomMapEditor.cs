using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MechanicFever
{
    public enum EditMode
    {
        CollisionMode,
        PropMode
    };

    [CustomEditor(typeof(Map))]
    public class CustomMapEditor : Editor
    {
        private Map map { get { return target as Map; } }

        public static bool ShowGrid = true;

        public static EditMode EditMode = EditMode.CollisionMode;

        private TabsBlock tabs;

        void CollisionMode() => EditMode = EditMode.CollisionMode;

        void PropMode() => EditMode = EditMode.PropMode;

        private void OnEnable()
        {
            tabs = new TabsBlock(new Dictionary<string, System.Action>()
            {
                {"Collision", CollisionMode},
                {"Props", PropMode}
            });
            tabs.SetCurrentMethod(map.lastTab);
        }

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
                GenerateMap(map);

            Undo.RecordObject(this.map, "Map");
            tabs.Draw();
            if (GUI.changed)
                this.map.lastTab = tabs.curMethodIndex;

            EditorUtility.SetDirty(this.map);

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

                        GUIStyle normalStyle = new GUIStyle("popup")
                        {
                            fontStyle = FontStyle.Normal,
                            fontSize = 5,
                            fixedWidth = 35,
                            fixedHeight = 35
                        };

                        GUIStyle obstacleStyle = new GUIStyle(normalStyle)
                        {
                            fontStyle = FontStyle.BoldAndItalic
                        };
                        obstacleStyle.normal.textColor = Color.red;

                        GUIStyle obstacleTypeStyle = new GUIStyle(normalStyle)
                        {
                            fontSize = 12,
                            fontStyle = FontStyle.BoldAndItalic,
                            fixedWidth = 35,
                            fixedHeight = 35
                        };

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
                                Node n = map.Grid[x].Columns[z];

                                EditorGUILayout.BeginHorizontal(rowStyle);
                                GUIStyle styleToUse = normalStyle;

                                switch (n.CollisionType)
                                {
                                    case CollisionType.Obstacle:
                                        styleToUse = obstacleStyle;
                                        break;
                                    case CollisionType.Occupied:
                                        styleToUse = occupiedStyle;
                                        break;
                                }

                                EditorGUILayout.BeginVertical(columnStyle);

                                if(EditMode == EditMode.CollisionMode)
                                {
                                    n.SetCollisionType((CollisionType)EditorGUILayout.EnumPopup(n.CollisionType, styleToUse));
                                    n.SetPosition(x, 0, z);
                                }

                                EditorGUILayout.EndHorizontal();

                                if (EditMode == EditMode.PropMode)
                                {
                                    if (n.CollisionType == CollisionType.Obstacle)
                                        n.SetProp(EditorGUILayout.IntField(n.Prop, obstacleTypeStyle));
                                }

                                EditorGUILayout.EndVertical();
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