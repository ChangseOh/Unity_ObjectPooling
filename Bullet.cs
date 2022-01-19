using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
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
        Invoke("DestroyBullet", 1f);
    }
    public void DestroyBullet()
    {
        ObjectPool.ReturnObject(0, this);
    }
}
