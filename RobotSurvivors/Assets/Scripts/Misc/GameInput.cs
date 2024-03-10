using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInput : MonoBehaviour
{
    private PlayerControls playerControls;

    void Awake()
    {
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

    public float GetInstantAttackDirectionCommand()
    {
        return playerControls.Player.InstantAttackDirection.ReadValue<float>();
    }

    public bool PauseMenuButtonPressed()
    {
        return playerControls.Player.Pause.IsPressed();
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

    public void ToggleFullScreen(bool value)
    {
        Screen.fullScreen = !Screen.fullScreen;
        Debug.Log("Fullscreen is " + Screen.fullScreen + " value is" + value);
    }

    public void ChangeResolution(int value)
    {
        Vector2 resolution = ResolutionDropdown.resolutionNumbers[value];
        Debug.Log(resolution);
        Screen.SetResolution((int)resolution.x, (int)resolution.y, Screen.fullScreen);
    }
}
