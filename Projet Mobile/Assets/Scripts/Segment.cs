using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour
{

    public int ID;
    public int difficulty;
    public bool isTransition;

    public float Lenght;
    public Vector3 endY;
    public Vector3 startY;
    

    private Transform myItem;

    private void Start()
    {
       
    }

    public void Spawn(List<Item> items = null)
    {
        myItem = GetComponentInChildren<Item>().transform;
        if (items != null)
        {
            int raretéMax = 0;
            foreach (Item item in items)
            {
                raretéMax += item.rareté;
            }

            int random = Random.Range(1, raretéMax);
            raretéMax = 0;
            int i = 0;
            while (raretéMax<random)
            {
                raretéMax += items[i].rareté;
                i++;
            }

            GameObject transform = Instantiate(items[i - 1].gameObject, myItem.position, Quaternion.identity);
            transform.transform.parent = this.transform;
            Destroy(myItem.gameObject);
            myItem = transform.transform;
        }
        gameObject.SetActive(true);
    }

    public void DeSpawn()
    {
        gameObject.SetActive(false);
    }
}
