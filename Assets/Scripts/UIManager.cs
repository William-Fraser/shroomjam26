using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Canvas Pause;
    public Canvas ShopMeat;
    public Canvas ShopLeno;
    public Canvas LenoDate;

    public Slider Volume;
    public AudioSource sound;

    [Space(20)]
    public TextMeshProUGUI Multiplier;
    public TextMeshProUGUI Money;

    void Start()
    {
        Time.timeScale = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        { 
            Pause.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        sound.volume = Volume.value;

        Multiplier.text = $"Mult: {GameManager.manager.multM.meterCurrent}/{GameManager.manager.multM.meterMax}";
        Money.text = $"${GameManager.manager.player.money}$";
    }

    #region MainMenu

    // could put settings here too?

    
    #endregion

    #region PauseMenu

    public void EnterGame()
    { 
        //resumes game, GM unpause
        Pause.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void Settings()
    { 
        //opens settings UI
    }

    public void Cheat()
    { 
        //gives 999999999 money
    }

    public void QuitGame()
    {
        // stops program
        Application.Quit();
    }
    #endregion

    #region Settings

    // I forget what to put here...

    //sound
    //brightness?
    //maybe put sound right in the menus

    #endregion

    #region CanvasControl

    public void ShowShopMeat()
    { 
        ShopMeat.gameObject.SetActive(true);
        
    }

    public void ShowShopLeno()
    {
        ShopLeno.gameObject.SetActive(true);
    }

    public void UndertaleRef()
    {
        LenoDate.gameObject.SetActive(true);
    }

    public void DisableAllCanvas()
    { 
        Pause.gameObject.SetActive(false);
        ShopMeat.gameObject.SetActive(false);
        ShopLeno.gameObject.SetActive(false);
        LenoDate.gameObject.SetActive(false);
    }

    #endregion
}
