using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script;

public class ItemPassif : Item
{
    public enum TYPE
    {
        ATTAQUE,
        SPEED_ATTAQUE,
        DOUBLE_ATTAQUE,
        BERSERK,
        NUDITE,
        CHANCE,
        SUPERCAPSULE,
        SHIELD,
        RETURN
    }

    public TYPE type;
    public float value;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.GetManager()._player.ActiveItem(type,value);
        }
    }

    


}
