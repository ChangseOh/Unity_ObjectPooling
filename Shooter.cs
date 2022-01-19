using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    private Camera mainCam;

    float cnt;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        cnt = 0.07f;
    }

    // Update is called once per frame
    void Update()
    {
        cnt -= Time.deltaTime;
        if (Input.GetMouseButton(0) && cnt <= 0f)
        {
            cnt = 0.07f;
            RaycastHit hitResult;
            var phray = Physics.Raycast(mainCam.ScreenPointToRay(Input.mousePosition), out hitResult);
            if (phray)
            {
                int kind = Random.Range(0, 2);
                var bullet = ObjectPool.GetObject(kind);//Instantiate(bulletPrefab).GetComponent<Bullet>();
                if (!bullet)
                    return;

                var direction = new Vector3(hitResult.point.x, transform.position.y, hitResult.point.z) - transform.position;
                bullet.transform.position = direction.normalized;
                if (kind == 0)
                {
                    bullet.GetComponent<Bullet>().Shoot(direction.normalized * 0.3f);
                }
                else
                {
                    bullet.GetComponent<Coin>().Shoot(direction.normalized * 0.2f);
                }

            }
        }
    }
}
