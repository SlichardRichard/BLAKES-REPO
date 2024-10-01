using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public bool isPaused = false;

    public GameObject pauseMenu;
    public Image healthBar;
    public TextMeshProUGUI AmmoCounter;
    public PlayerController playerData;
    //public GameObject GunData;

    // Start is called before the first frame update
    void Start()
    {
        playerData = GameObject.Find("Player").GetComponent<PlayerController>();
        //GunData = GameObject.Find("weapon1").GetComponent<GameObject>();
    }



    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp((float)playerData.Health / (float)playerData.maxHealth, 0, 1);
        //AmmoCounter.text = "Ammo" + GunData.


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                pauseMenu.SetActive(true);

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                
                Time.timeScale = 0;

                isPaused = true;

            }

            else
                Resume();
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);

        Time.timeScale = 1;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        isPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadLevel(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    public void RestartLevel()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }






}
