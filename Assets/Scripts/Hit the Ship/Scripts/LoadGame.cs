using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    [SerializeField] private int arcadeIndex = 1;
    [SerializeField] private int endlessIndex = 2;

    public void LoadArcade() {
        SceneManager.LoadSceneAsync(arcadeIndex);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void LoadEndless() {
        SceneManager.LoadSceneAsync(endlessIndex);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
 