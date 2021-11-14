using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class LevelEditor : EditorWindow
{
#if UNITY_EDITOR
    bool spawnMultipleObjects;

    bool WallSpawningEnabled;
    bool floorSpawningEnabled;

    bool foward;
    bool right;
    bool left;
    bool backwards;

    bool fowardFloor;
    bool rightFloor;
    bool leftFloor;
    bool backwardsFloor;

    Object wall;
    Object doorway;
    Object outsideFloor;
    Object insidefloor;

    Vector2 scrollPos;

    public enum gameObjectTypes
    {
        wall = 0,
        doorway = 1,
    }
    public gameObjectTypes typeWallObject;

    public enum FloorObjectTypes
    {
        insideFloor = 0,
        ousideFloor = 1
    }
    public FloorObjectTypes typeFloorObject;



    [MenuItem("Tools/levelEditor")]
    public static void ShowMyEditor()
    {
        // This method is called when the user selects the menu item in the Editor
        EditorWindow wnd = GetWindow<LevelEditor>();
        wnd.titleContent = new GUIContent("Level Editor");
    }

    public void OnGUI()
    {
        spawnMultipleObjects = EditorGUILayout.Toggle("spawn multiple objects at once", spawnMultipleObjects);

        GUILayout.Space(10);

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width(position.width), GUILayout.Height(position.height - 50));

        #region wall and doorway
        GUILayout.Label("wall/doorway spawning", EditorStyles.largeLabel);
        GUILayout.Space(10);

        WallSpawningEnabled = EditorGUILayout.BeginToggleGroup("spawn walls Around Object", WallSpawningEnabled);
        GUILayout.Label("wall", EditorStyles.boldLabel);
        wall = EditorGUILayout.ObjectField(wall, typeof(GameObject), false);
        GUILayout.Label("door way", EditorStyles.boldLabel);
        doorway = EditorGUILayout.ObjectField(doorway, typeof(GameObject), false);

        GUILayout.Space(25);
        GUILayout.Label("which object would you like to spawn", EditorStyles.boldLabel);
        typeWallObject = (gameObjectTypes)EditorGUILayout.EnumPopup("type:", typeWallObject);

        GUILayout.Space(25);

        GUILayout.Label("selected object should spawn on the +z axis wall", EditorStyles.boldLabel);
        foward = EditorGUILayout.Toggle("Spawn +z", foward);
        if (foward && !spawnMultipleObjects)
        {
            backwards = false;
            right = false;
            left = false;
        }
        GUILayout.Label("selected object should spawn on the +x axis wall", EditorStyles.boldLabel);
        right = EditorGUILayout.Toggle("Spawn +x", right);
        if (right && !spawnMultipleObjects)
        {
            backwards = false;
            foward = false;
            left = false;
        }
        GUILayout.Label("selected object should spawn on the -x axis wall", EditorStyles.boldLabel);
        left = EditorGUILayout.Toggle("Spawn -x", left);
        if (left && !spawnMultipleObjects)
        {
            backwards = false;
            right = false;
            foward = false;
        }
        GUILayout.Label("selected object should spawn on the -z axis wall", EditorStyles.boldLabel);
        backwards = EditorGUILayout.Toggle("Spawn -z", backwards);
        if (backwards && !spawnMultipleObjects)
        {
            foward = false;
            right = false;
            left = false;
        }
        EditorGUILayout.EndToggleGroup();

        GUILayout.Space(10);

        #endregion

        #region floor
        GUILayout.Label("floor", EditorStyles.largeLabel);
        GUILayout.Space(10);

        floorSpawningEnabled = EditorGUILayout.BeginToggleGroup("spawn floors Around Object", floorSpawningEnabled);
        GUILayout.Label("outside floor", EditorStyles.boldLabel);
        outsideFloor = EditorGUILayout.ObjectField(outsideFloor, typeof(GameObject), false);
        GUILayout.Label("inside floor", EditorStyles.boldLabel);
        insidefloor = EditorGUILayout.ObjectField(insidefloor, typeof(GameObject), false);

        GUILayout.Space(25);
        GUILayout.Label("which object would you like to spawn", EditorStyles.boldLabel);
        typeFloorObject = (FloorObjectTypes)EditorGUILayout.EnumPopup("type:", typeFloorObject);

        GUILayout.Space(25);

        GUILayout.Label("selected object should spawn on the +z axis", EditorStyles.boldLabel);
        fowardFloor = EditorGUILayout.Toggle("Spawn +z", fowardFloor);
        GUILayout.Label("selected object should spawn on the +x axis", EditorStyles.boldLabel);
        rightFloor = EditorGUILayout.Toggle("Spawn +x", rightFloor);
        GUILayout.Label("selected object should spawn on the -x axis", EditorStyles.boldLabel);
        leftFloor = EditorGUILayout.Toggle("Spawn -x", leftFloor);
        GUILayout.Label("selected object should spawn on the -z axis", EditorStyles.boldLabel);
        backwardsFloor = EditorGUILayout.Toggle("Spawn -z", backwardsFloor);
        EditorGUILayout.EndToggleGroup();
        #endregion


        GUILayout.Space(50);
        GUILayout.Label("Add Selected Objects", EditorStyles.largeLabel);

        if (GUILayout.Button("generate"))
        {
            spawnObjects();
        }
        EditorGUILayout.EndScrollView();
    }

    private float offSetCalculation(bool positive, bool negative, float offsetValue)
    {
        float value = 0;
        if (positive)
        {
            value = offsetValue;
        }
        else if (negative)
        {
            value = -offsetValue;
        }
        return value;
    }

    private void spawnObjects()
    {
        GameObject parentObject;
        if (Selection.activeGameObject != null)
        {
            parentObject = Selection.activeGameObject;

            float x = 0;
            float z = 0;

            #region wall and door spawning
            if (WallSpawningEnabled)
            {
                GameObject spawnObject;
                switch ((int)typeWallObject)
                {
                    case 0:
                        spawnObject = (GameObject)wall;
                        break;
                    case 1:
                        spawnObject = (GameObject)doorway;
                        break;
                    default:
                        spawnObject = new GameObject();
                        break;
                }

                Vector3 wallOffSet;
                Quaternion rotation;

                float y;
                switch (typeWallObject)
                {
                    case gameObjectTypes.wall:
                        y = 4.25f;
                        break;
                    case gameObjectTypes.doorway:
                        y = 5;
                        break;
                    default:
                        y = 0;
                        Debug.LogWarning("y offset calculation defaulted");
                        break;
                }


                if (spawnMultipleObjects)
                {
                    if (foward)
                    {
                        wallOffSet = new Vector3(0, y, 4);
                        rotation = Quaternion.identity;

                        Instantiate(spawnObject, parentObject.transform.position + wallOffSet, rotation, parentObject.transform);
                    }
                    if (backwards)
                    {
                        wallOffSet = new Vector3(0, y, -4);
                        rotation = Quaternion.identity;

                        Instantiate(spawnObject, parentObject.transform.position + wallOffSet, rotation, parentObject.transform);
                    }
                    if (right)
                    {
                        wallOffSet = new Vector3(4, y, 0);
                        rotation = Quaternion.Euler(0, 90, 0);

                        Instantiate(spawnObject, parentObject.transform.position + wallOffSet, rotation, parentObject.transform);
                    }
                    if (left)
                    {
                        wallOffSet = new Vector3(-4, y, 0);
                        rotation = Quaternion.Euler(0, 90, 0);

                        Instantiate(spawnObject, parentObject.transform.position + wallOffSet, rotation, parentObject.transform);
                    }
                }
                else
                {
                    z = offSetCalculation(foward, backwards, 4);
                    x = offSetCalculation(right, left, 4);
                    wallOffSet = new Vector3(x, y, z);
                    rotation = Quaternion.identity;
                    if (right || left)
                    {
                        rotation = Quaternion.Euler(0, 90, 0);
                    }

                    Instantiate(spawnObject, parentObject.transform.position + wallOffSet, rotation, parentObject.transform);
                }
            }
            #endregion

            #region floor spawning
            if (floorSpawningEnabled)
            {
                GameObject spawnObject;
                switch ((int)typeFloorObject)
                {
                    case 0:
                        spawnObject = (GameObject)insidefloor;
                        break;
                    case 1:
                        spawnObject = (GameObject)outsideFloor;
                        break;
                    default:
                        spawnObject = new GameObject();
                        break;
                }

                Vector3 wallOffSet;
                Quaternion rotation;
                if (spawnObject.CompareTag("Untagged") && parentObject.CompareTag("insideFlooring") || spawnObject.CompareTag("insideFlooring") && parentObject.CompareTag("Untagged"))
                {
                    if (fowardFloor)
                    {
                        wallOffSet = new Vector3(0, 0, 7.5f);
                        rotation = Quaternion.identity;

                        Instantiate(spawnObject, parentObject.transform.position + wallOffSet, rotation, parentObject.transform.parent);
                    }
                    if (backwardsFloor)
                    {
                        wallOffSet = new Vector3(0, 0, -7.5f);
                        rotation = Quaternion.identity;

                        Instantiate(spawnObject, parentObject.transform.position + wallOffSet, rotation, parentObject.transform.parent);
                    }
                    if (rightFloor)
                    {
                        wallOffSet = new Vector3(7.5f, 0, 0);
                        rotation = Quaternion.Euler(0, 90, 0);

                        Instantiate(spawnObject, parentObject.transform.position + wallOffSet, rotation, parentObject.transform.parent);
                    }
                    if (leftFloor)
                    {
                        wallOffSet = new Vector3(-7.5f, 0, 0);
                        rotation = Quaternion.Euler(0, 90, 0);

                        Instantiate(spawnObject, parentObject.transform.position + wallOffSet, rotation, parentObject.transform.parent);
                    }
                }
                else
                {
                    if (fowardFloor)
                    {
                        wallOffSet = new Vector3(0, 0, 8);
                        rotation = Quaternion.identity;

                        Instantiate(spawnObject, parentObject.transform.position + wallOffSet, rotation, parentObject.transform.parent);
                    }
                    if (backwardsFloor)
                    {
                        wallOffSet = new Vector3(0, 0, -8);
                        rotation = Quaternion.identity;

                        Instantiate(spawnObject, parentObject.transform.position + wallOffSet, rotation, parentObject.transform.parent);
                    }
                    if (rightFloor)
                    {
                        wallOffSet = new Vector3(8, 0, 0);
                        rotation = Quaternion.Euler(0, 90, 0);

                        Instantiate(spawnObject, parentObject.transform.position + wallOffSet, rotation, parentObject.transform.parent);
                    }
                    if (leftFloor)
                    {
                        wallOffSet = new Vector3(-8, 0, 0);
                        rotation = Quaternion.Euler(0, 90, 0);

                        Instantiate(spawnObject, parentObject.transform.position + wallOffSet, rotation, parentObject.transform.parent);
                    }
                }
            }
            #endregion
        }
        else
        {
            Debug.LogWarning("Please select a gameObject to spawn from");
        }
    }
#endif
}
