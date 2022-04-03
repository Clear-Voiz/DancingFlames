using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneGenerator : MonoBehaviour
{
    [MenuItem("Generate/Scenography")]
    static void Sandboxer()
    {
        var currentScene = EditorSceneManager.GetActiveScene();
        EditorSceneManager.SaveScene(currentScene);
        var newLoaction = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        EditorSceneManager.SaveScene(newLoaction, "Assets/Scenes/Testing"+EditorSceneManager.sceneCountInBuildSettings+".unity");
        GameObject MainCamera = Resources.Load("Main Camera") as GameObject;
        //Camera.main.clearFlags = CameraClearFlags.SolidColor;
        //GameObject Aderyn = Resources.Load("Player") as GameObject;
        Object Aderyn = Resources.Load("Player");
        GameObject CanvasHUD = Resources.Load("can_HUD") as GameObject;
        GameObject Ground = Resources.Load("Ground") as GameObject;
        GameObject Respawn = Resources.Load("Respawn") as GameObject;
        GameObject Radar = Resources.Load("Radar") as GameObject;
        GameObject GameManager = Resources.Load("GAME") as GameObject;
        GameObject eventSystem = Resources.Load("EventSystem") as GameObject;
        GameObject Gas = Resources.Load("Gas") as GameObject;
        GameObject deadZone = Resources.Load("DeadZone") as GameObject;
        GameObject Enix = Resources.Load("Square") as GameObject;
        PrefabUtility.InstantiatePrefab(GameManager, newLoaction);
        PrefabUtility.InstantiatePrefab(CanvasHUD, newLoaction);
        PrefabUtility.InstantiatePrefab(MainCamera, newLoaction);
        PrefabUtility.InstantiatePrefab(Respawn, newLoaction);
        var temp = PrefabUtility.InstantiatePrefab(Ground, newLoaction) as GameObject;
        temp.transform.position = Vector3.down * 0.3f;
        temp = PrefabUtility.InstantiatePrefab(Radar, newLoaction) as GameObject;
        temp.transform.position = Vector3.right * 3f;
        temp = PrefabUtility.InstantiatePrefab(Gas, newLoaction) as GameObject;
        temp.transform.position = Vector3.left * 3f;
        temp = PrefabUtility.InstantiatePrefab(deadZone, newLoaction) as GameObject;
        temp.transform.position = Vector3.down * 2f;
        PrefabUtility.InstantiatePrefab(eventSystem, newLoaction);
        PrefabUtility.InstantiatePrefab(Aderyn, newLoaction);
        PrefabUtility.InstantiatePrefab(Enix, newLoaction);
    }
}
