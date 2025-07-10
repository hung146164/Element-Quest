using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuaiBayController : QuaiController
{
    [SerializeField] Transform Bullet;
    protected override void CheckJump()
    {

    }
    protected virtual void ShootSwordBullet()
    {
        Instantiate(Bullet, transform.position, Quaternion.identity).gameObject.SetActive(true);
    }

}
