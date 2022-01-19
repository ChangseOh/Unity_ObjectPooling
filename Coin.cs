using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Vector3 direction;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction);
    }
    public void Shoot(Vector3 direction)
    {
        this.direction = direction;
        Invoke("DestroyCoin", 2f);
    }
    public void DestroyCoin()
    {
        ObjectPool.ReturnObject(1, this);
    }
}
