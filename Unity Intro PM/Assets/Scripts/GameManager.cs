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
    

    // Start is called before the first frame update
    void Start()
    {
        playerData = GameObject.Find("Player").GetComponent<PlayerController>();
        
    }



    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex > 0)
        {

        
            healthBar.fillAmount = Mathf.Clamp((float)playerData.Health / (float)playerData.maxHealth, 0, 1);
            
            
            if(GameObject.Find("WeaponSlot").GetComponentInChildren<Gun>() != null) 
            {
                AmmoCounter.text = "Ammo: " + playerData.currentWeapon.GetComponent<Gun>().currentAmmo;
            }
            

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
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneID);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }






}
