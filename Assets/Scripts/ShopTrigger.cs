using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    public enum SHOP
    { 
        MEAT,
        LENO
    }

    public SHOP shop;
    public bool showShop;

    void Update()
    {
        if (showShop)
            switch (shop)
            { 
                case SHOP.MEAT:
                    GameManager.manager.UIM.ShowShopMeat();
                    return;

                case SHOP.LENO:
                    GameManager.manager.UIM.ShowShopLeno();
                    return;
            }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.transform.parent.gameObject == GameManager.manager.player.character.gameObject)
           showShop = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.parent.gameObject == GameManager.manager.player.character.gameObject)
        { 
            showShop =false;
            GameManager.manager.UIM.DisableAllCanvas();
        }
    }
}
