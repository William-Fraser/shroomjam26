using UnityEngine;

public class ShopManager : MonoBehaviour
{
    //should be more like park management?
    //maybe park tier is handled by GreyLeno and other stuff by Meat
    //linked to UIManager and UpgradeManager
    //prize wheel in GreyLeno section

    public UpgradeManager upgradeM;

    void Start()
    {
        upgradeM = GetComponent<UpgradeManager>();
    }

    //Grey Leno stuff
    // New Rides, New Staff, Better Rides, Date

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
