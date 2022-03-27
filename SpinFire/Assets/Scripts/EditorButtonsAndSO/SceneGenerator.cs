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
        EditorSceneManager.SaveScene(newLoaction, "Assets/Scenes/Testing.unity");
        GameObject MainCamera = Resources.Load("Main Camera") as GameObject;
        //Camera.main.clearFlags = CameraClearFlags.SolidColor;
        GameObject Aderyn = Resources.Load("Player") as GameObject;
        GameObject CanvasHUD = Resources.Load("can_HUD") as GameObject;
        GameObject Ground = Resources.Load("Ground") as GameObject;
        GameObject Respawn = Resources.Load("Respawn") as GameObject;
        GameObject Radar = Resources.Load("Radar") as GameObject;
        GameObject GameManager = Resources.Load("GAME") as GameObject;
        GameObject eventSystem = Resources.Load("EventSystem") as GameObject;
        GameObject Gas = Resources.Load("Gas") as GameObject;
        GameObject deadZone = Resources.Load("DeadZone") as GameObject;
        Instantiate(GameManager, Vector3.zero, Quaternion.identity);
        Instantiate(CanvasHUD, Vector3.zero, Quaternion.identity);
        Instantiate(MainCamera);
        Instantiate(Aderyn, Vector3.zero, Quaternion.identity);
        Instantiate(Respawn, Vector3.zero, Quaternion.identity);
        Instantiate(Ground, Vector3.down * 0.3f, Quaternion.identity);
        Instantiate(Radar, Vector3.right * 3f, Quaternion.identity);
        Instantiate(Gas, Vector3.left * 3f, Quaternion.identity);
        Instantiate(deadZone, Vector3.down * 2f, Quaternion.identity);
        Instantiate(eventSystem);
        //Instantiate(EventSystem)

    }
}
