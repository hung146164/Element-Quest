using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSkillPrefab : MonoBehaviour
{
    public float velocitySkill;
    public Transform PlayerPosition;
    AudioManager audioManager;
    private void Start()
    {
        audioManager = GameObject.FindWithTag("audio").GetComponent<AudioManager>();
        Move_skill();
    }

    void Move_skill()
    {
        AudioManager.instance.PlaySFX("Fire");

        float direction = Mathf.Sign(PlayerPosition.localScale.x);

        // Chỉnh localScale của đối tượng cùng chiều với Player
        Vector3 newScale = transform.localScale;
        newScale.x = Mathf.Abs(newScale.x) * direction;
        transform.localScale = newScale;

        // Thiết lập vận tốc cho Rigidbody2D
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(direction * velocitySkill, 0);
    }
    void DestroySkill()
    {
        Destroy(gameObject);
    }
}
