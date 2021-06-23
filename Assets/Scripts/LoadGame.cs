using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    [SerializeField] private int gameIndex = 1;

    public void LoadGameScene() {
        SceneManager.LoadSceneAsync(gameIndex);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
 