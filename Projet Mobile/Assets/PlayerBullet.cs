using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Script;

public class PlayerBullet : MonoBehaviour
{
    public float _moveSpeed = 10.0f;
    private float _destroyTimer = 0.0f;
    void Update()
    {
        if (GameManager.GetManager()._currentBoss != null)
        {
            float distance = Vector3.Distance(GameManager.GetManager()._currentBoss.transform.position, transform.position);
            transform.position = Vector3.Lerp(transform.position, GameManager.GetManager()._currentBoss.transform.position, Time.deltaTime * _moveSpeed);
            if (distance <= 1.0f)
                Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
