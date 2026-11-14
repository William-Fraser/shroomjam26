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
    private PlayerController player;

    void Start()
    {
        upgradeM = GetComponent<UpgradeManager>();
        player = GameManager.manager.player;
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
    public String OriginalButtonTextTragedy;
    public String OriginalButtonTextGuests;

    public bool CheckFundsAndPay(int price)
    {
        if (player.money < price) return false;
        else
        {
            player.DecreaseMoney(price);
            return true;
        }
        
    }

    public void FixButtons()
    {
        ButtonTextRides.text = OriginalButtonTextRides + "\n" + PriceRides;
        ButtonTextStaff.text = OriginalButtonTextStaff + "\n" + PriceStaff;
        ButtonTextRides2.text = OriginalButtonTextRides2 + "\n" + PriceRides2;
        ButtonTextMove.text = OriginalButtonTextMove + "\n" + PriceMove;
        ButtonTextTime.text = OriginalButtonTextTime + "\n" + PriceTime;
        ButtonTextTragegy.text = OriginalButtonTextTragedy + "\n" + PriceTragegy;
        ButtonTextGuests.text = OriginalButtonTextGuests +"\n"+ PriceGuests;
    }

    public void NewRides()
    {
        if (!CheckFundsAndPay(PriceRides)) return;
        // reveal new pre-placed rides
        upgradeM.NewRide();
        PriceRides = upgradeM.IncreasePrice(PriceRides);
        FixButtons();
    }

    public void NewStaff()
    {
        if (!CheckFundsAndPay(PriceStaff)) return;
        // reveal new staff
        upgradeM.NewStaff();
        PriceStaff = upgradeM.IncreasePrice(PriceStaff);
        FixButtons();
    }

    public void BetterRides()
    {
        if (!CheckFundsAndPay(PriceRides2)) return;
        // 2x happiness and divide sadness for rides
        
        upgradeM.BetterRides();
        PriceRides2 = upgradeM.IncreasePrice(PriceRides2);
        FixButtons();
    }

    public void Date()
    {
        // UNDERTALE REFERENCE????>?>?>???
        upgradeM.Date();
    }

    //Meat stuff
    // Faster Movement, faster timer, Lower Tragedy rate, More Guests

    public void FasterMovement()
    {
        if (!CheckFundsAndPay(PriceMove)) return;
        // increases movement speed for everyone
        upgradeM.FasterMovement();
        PriceMove = upgradeM.IncreasePrice(PriceMove);
        FixButtons();
    }

    public void FasterTime()
    {
        if (!CheckFundsAndPay(PriceTime)) return;
        // increase all time everywhere
        upgradeM.FasterTime();
        PriceTime = upgradeM.IncreasePrice(PriceTime);
        FixButtons();
    }

    public void LowerTragedyRate()
    {
        if (!CheckFundsAndPay(PriceTragegy)) return;
        // decrease visitor trash and lost
        upgradeM.LowerTragedyRate();
        PriceTragegy = upgradeM.IncreasePrice(PriceTragegy);
        FixButtons();
    }

    public void MoreGuests()
    {
        if (!CheckFundsAndPay(PriceGuests)) return;
        // add more guests 3 per level?
        upgradeM.MoreGuests();
        PriceGuests = upgradeM.IncreasePrice(PriceGuests);
        FixButtons();
    }
}
