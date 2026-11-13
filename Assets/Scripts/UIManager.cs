using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Canvas Pause;
    public Canvas ShopMeat;
    public Canvas ShopLeno;
    public Canvas LenoDate;

    #region MainMenu

    public void StartGame()
    { 
        //Begins Game
    }

    public void LoadGame()
    { 
        //Starts Game and Loads Saved Data
    }

    // could put settings here too?

    public void QuitGame()
    { 
        // stops program
    }
    #endregion

    #region PauseMenu

    public void Resume()
    { 
        //resumes game, GM unpause
    }

    public void Settings()
    { 
        //opens settings UI
    }

    public void Cheat()
    { 
        //gives 999999999 money
    }

    public void ReturnToMainMenu()
    { 
        // saves, closes pausemenu and changes scene to main menu
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
