using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletVitri : MonoBehaviour
{
    public Transform EnemyPosition;
    void Start()
    {
        Move_skill();
    }
    void Move_skill()
    {
        Vector3 SpawnPos=EnemyPosition.position;
        SpawnPos.y += 2;
        transform.position=SpawnPos;
        AudioManager.instance.PlaySFX("Xuctuattack");

    }
    void DestroySkill()
    {
        Destroy(gameObject);
    }
}
