using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInput : MonoBehaviour
{
    private PlayerControls playerControls;

    //public static GameInput instance;

    void Awake()
    {
        /*if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }*/

        playerControls = new PlayerControls();
        playerControls.Enable();    
    }

    public Vector3 GetMovementVectorNormalized()
    {
        return (Vector3)playerControls.Player.Move.ReadValue<Vector2>().normalized;
    }

    public float GetFireCommand()
    {
        return playerControls.Player.Fire.ReadValue<float>();
    }
    
    public float GetZoom()
    {
        return playerControls.Player.CameraZoom.ReadValue<float>();
    }

    public void ReloadScene()
    {
        string nameOfTheScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(nameOfTheScene);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("QUIT!");
    }
}
