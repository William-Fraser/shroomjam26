using UnityEngine;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour
{
    //builds/attractions
    //characters
    public delegate void IncreaseSpeed();
    public static event IncreaseSpeed IncreaseS;

    public delegate void DecreaseTime();
    public static event DecreaseTime DecreaseT;

    public delegate void LowerTragedy();
    public static event LowerTragedy LowerT;

    //shop
    //park
    public List<Task> newAttractions;
    public List<HelperControl> newHelpers;
    public VisitorControl visitorPrefab;

    void Start()
    {
        for (int i = 0; i < newAttractions.Count; i++)
        {
            newAttractions[i].gameObject.SetActive(false);
            GameManager.manager.taskM.RemoveTask(newAttractions[i].transform.position, newAttractions[i]);
        }

        for (int i = 0; i < newHelpers.Count; i++)
        {
            newHelpers[i].gameObject.SetActive(false);
            GameManager.manager.player.RemoveHelper(newHelpers[i]);
        }
    }

    public int IncreasePrice(int price)
    {
        price = price * 2;
        return price;  
    }

    public void NewRide()
    {
        // reveal new pre-placed rides
        newAttractions[0].gameObject.SetActive(true);
        GameManager.manager.taskM.AddTask(newAttractions[0].transform.position, newAttractions[0]);
        newAttractions.RemoveAt(0);
    }

    public void NewStaff()
    {
        // reveal new staff
        newHelpers[0].gameObject.SetActive(true);
        newHelpers.RemoveAt(0);
    }

    public void BetterRides()
    {
        // increase happiness and divide sadness for rides
        GameManager.manager.taskM.ShopAttractionMultiplier++;
    }

    public void Date()
    {
        // UNDERTALE REFERENCE????>?>?>???
        GameManager.manager.UIM.UndertaleRef();
    }

    //Meat stuff
    // Faster Movement, faster timer, Lower Tragedy rate, More Guests

    public void FasterMovement()
    {
        // increases movement speed for everyone
        if (IncreaseS != null)
            IncreaseS();
        else Debug.LogError("nullEvent");
    }

    public void FasterTime()
    {
        // increase all time everywhere
        if (DecreaseT != null)
            DecreaseT();
        else Debug.LogError("nullEvent");
        // stop this madness after this event
    }

    public void LowerTragedyRate()
    {
        // decrease visitor trash and lost
        if (LowerT != null)
            LowerT();
        else Debug.LogError("nullEvent");
    }

    public void MoreGuests()
    {
        //spawn in 3 guests
        for (int i=0; i < 3; i++)
        Instantiate(visitorPrefab, 
            GameManager.manager.player.character.transform.position, 
            GameManager.manager.player.character.transform.rotation);
    }
}
