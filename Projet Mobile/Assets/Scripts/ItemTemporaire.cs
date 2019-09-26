using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script;

public class ItemTemporaire : Item
{
    public enum TYPE
    {
        SLOW,
        TOURELLE,
        POISON,
        ACCELERATION
    }

    public TYPE type;
    public float time;
    public float value;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ActiveItem();
        }
    }

    void ActiveItem()
    {
        switch (type)
        {
            case TYPE.SLOW:
                //GameManager.GetManager()._currentBoss Slow boss
                break;
            case TYPE.TOURELLE:
                //Spawn Tourelle
                break;
            case TYPE.POISON:
                GameManager.GetManager()._player.SetupPoison(time, value);
                break;
            case TYPE.ACCELERATION:
<<<<<<< Updated upstream
                GameManager.GetManager()._player.SetupAcc(time, value);
                    break;
=======
                GameManager.GetManager()._player.SetupAcc(time,value);
                break;
>>>>>>> Stashed changes
            default:
                break;
        }
    }
}
