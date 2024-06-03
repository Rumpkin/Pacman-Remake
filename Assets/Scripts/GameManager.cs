using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int Score { get; private set; }

    public static GameObject Pacman;

    public static GameObject Blinky;
    public static GameObject Pinky;
    public static GameObject Inky;
    public static GameObject Clyde;

    public int wave { get; private set; }
    public bool pacmanScaryMode { get; private set; }
    

    private void Awake()
    {        
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }
    
    void Start()
    {
        pacmanScaryMode = false;

        DontDestroyOnLoad(gameObject);

        Score = 0;
        wave = 0;

        SceneManager.LoadScene(1);

        Invoke("StartGame", 5);
    }

    public void PelletEaten(Pellet pellet)
    {
        Score += pellet.Points;
        Destroy(pellet.gameObject);
    }

    public void PowerPelletEaten(Pellet pellet)
    {
        PelletEaten(pellet);
        TogglePacmanScaryMode();
    }

    public void StartGame()
    {
        Blinky.GetComponent<Movement>().SetFrozen(false);
        Pinky.GetComponent<Movement>().SetFrozen(false);
        Inky.GetComponent<Movement>().SetFrozen(false);
        Clyde.GetComponent<Movement>().SetFrozen(false);

        Pacman.GetComponent<Movement>().SetFrozen(false);
        Pacman.GetComponent<pacmanMovementController>().started = true;

        wave = 0;
        CancelInvoke("NextWave");

        NextWave();
    }

    public void TogglePacmanScaryMode()
    {
        pacmanScaryMode = true;
        StartFrightened();
        CancelInvoke("ResetPacmanScaryMode");
        Invoke("ResetPacmanScaryMode", 10);
    }

    public void ResetPacmanScaryMode()
    {
        pacmanScaryMode = false;
    }

    public void WipeOut()
    {
        Blinky.GetComponent<Movement>().SetFrozen(true);
        Pinky.GetComponent<Movement>().SetFrozen(true);
        Inky.GetComponent<Movement>().SetFrozen(true);
        Clyde.GetComponent<Movement>().SetFrozen(true);

        Pacman.GetComponent<Movement>().SetFrozen(true);
        Invoke("DisableGhosts", 1f);
        Invoke("RoomReset", 3.2f);
    }

    public void DisableGhosts()
    {
        Pacman.transform.rotation = new Quaternion(0, 0, 0, 0);

        Blinky.GetComponent<Movement>().enabled = false;
        Pinky.GetComponent<Movement>().enabled = false;
        Inky.GetComponent<Movement>().enabled = false;
        Clyde.GetComponent<Movement>().enabled = false;
    }

    public void RoomReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Invoke("StartGame", 5);
    }

    public void StartScatter(bool switchDirection = true)
    {
        Blinky.GetComponent<GhostMovement>().SwitchState(GhostMovement.state.Scatter, switchDirection);
        Pinky.GetComponent<GhostMovement>().SwitchState(GhostMovement.state.Scatter, switchDirection);
        Inky.GetComponent<GhostMovement>().SwitchState(GhostMovement.state.Scatter, switchDirection);
        Clyde.GetComponent<GhostMovement>().SwitchState(GhostMovement.state.Scatter, switchDirection);
    }

    public void StartChase()
    {
        Blinky.GetComponent<GhostMovement>().SwitchState(GhostMovement.state.Chase);
        Pinky.GetComponent<GhostMovement>().SwitchState(GhostMovement.state.Chase);
        Inky.GetComponent<GhostMovement>().SwitchState(GhostMovement.state.Chase);
        Clyde.GetComponent<GhostMovement>().SwitchState(GhostMovement.state.Chase);
    }

    public void StartFrightened()
    {
        Blinky.GetComponent<GhostMovement>().SwitchState(GhostMovement.state.Frightened);
        Pinky.GetComponent<GhostMovement>().SwitchState(GhostMovement.state.Frightened);
        Inky.GetComponent<GhostMovement>().SwitchState(GhostMovement.state.Frightened);
        Clyde.GetComponent<GhostMovement>().SwitchState(GhostMovement.state.Frightened);
    }

    public void NextWave()
    {
        switch (wave)
        {
            case 0:
                //scatter for 7
                Debug.Log("Scatter");
                StartScatter(false);                                
                Invoke("NextWave", 7);
                break;

            case 1:
                //chase for 20
                Debug.Log("Chase");
                StartChase();
                Invoke("NextWave", 20);
                break;

            case 2:
                //scatter for 7
                Debug.Log("Scatter");
                StartScatter();
                Invoke("NextWave", 7);
                break;

            case 3:
                //chase for 20
                Debug.Log("Chase");
                StartChase();
                Invoke("NextWave", 20);
                break;

            case 4:
                //scatter for 5
                Debug.Log("Scatter");
                StartScatter();
                Invoke("NextWave", 5);
                break;

            case 5:
                //chase for 20
                Debug.Log("Chase");
                StartChase();
                Invoke("NextWave", 20);
                break;

            case 6:
                //scatter for 5
                Debug.Log("Scatter");
                StartScatter();
                Invoke("NextWave", 5);
                break;

            case 7:
                //chase for indefidentily
                Debug.Log("Chase");
                StartChase();                
                break;
        }

        wave++;
    }
}
