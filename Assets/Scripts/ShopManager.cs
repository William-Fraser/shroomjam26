using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;

public class ShopManager : MonoBehaviour
{
    //should be more like park management?
    //maybe park tier is handled by GreyLeno and other stuff by Meat
    //linked to UIManager and UpgradeManager
    //prize wheel in GreyLeno section

    public UpgradeManager upgradeM;
    private PlayerController player = GameManager.manager.player;

    void Start()
    {
        upgradeM = GetComponent<UpgradeManager>();
        FixButtons();
    }

    //Grey Leno stuff
    // New Rides, New Staff, Better Rides, Date
    public int PriceRides = 10;
    public int PriceStaff = 10;
    public int PriceRides2 = 10;
    //Meat
    public int PriceMove = 10;
    public int PriceTime = 10;
    public int PriceTragegy = 10;
    public int PriceGuests = 10;

    //leno
    public TextMeshProUGUI ButtonTextRides;
    public TextMeshProUGUI ButtonTextStaff;
    public TextMeshProUGUI ButtonTextRides2;
    //Meat
    public TextMeshProUGUI ButtonTextMove;
    public TextMeshProUGUI ButtonTextTime;
    public TextMeshProUGUI ButtonTextTragegy;
    public TextMeshProUGUI ButtonTextGuests;



    public String OriginalButtonTextRides;
    public String OriginalButtonTextStaff;
    public String OriginalButtonTextRides2;
    public String OriginalButtonTextMove;
    public String OriginalButtonTextTime;
    public String OriginalButtonTextTragegy;
    public String OriginalButtonTextGuests;


    public void PayAndFixButton(int price)
    {

        FixButtons();
    }

    public void FixButtons()
    { 
    
    }

    public void NewRides()
    { 
        // reveal new pre-placed rides
    }

    public void NewStaff()
    { 
        // reveal new staff
    }

    public void BetterRides()
    { 
        // 2x happiness and divide sadness for rides
    }

    public void Date()
    { 
        // UNDERTALE REFERENCE????>?>?>???
    }

    //Meat stuff
    // Faster Movement, faster timer, Lower Tragedy rate, More Guests

    public void FasterMovement()
    { 
        // increases movement speed for everyone
    }

    public void FasterTime()
    { 
        // increase all time everywhere
    }

    public void LowerTragedyRate()
    { 
        // decrease visitor trash and lost
    }

    public void MoreGuests()
    { 
        // add more guests 3 per level?
    }
}
