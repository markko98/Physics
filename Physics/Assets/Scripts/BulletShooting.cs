using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooting : MonoBehaviour
{
    // PROJECTILE/BULLET PREFAB
    public GameObject bulletGo;
    private PlayerController playerController;
    
    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerController.onFire += Fire;
    }

    private void Fire()
    {
        Instantiate(bulletGo, transform.position, Quaternion.identity);
    }
}
