using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanDuoitheo : MonoBehaviour
{
    public float velocitySkill;
    public Transform PlayerPosition;
    public Transform EnemyPosition;
    private void Start()
    {
        MoveSkill();
        AudioManager.instance.PlaySFX("BossFire");
    }
    void MoveSkill()
    {

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 moveDir = -(PlayerPosition.position - EnemyPosition.position).normalized * velocitySkill;
        rb.velocity = new Vector2(moveDir.x, moveDir.y);

        float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    void DestroySkill()
    {
        Destroy(gameObject);
        StartCoroutine(UnloadUnusedAssets());
    }
    private IEnumerator UnloadUnusedAssets()
    {
        yield return Resources.UnloadUnusedAssets();
        Debug.Log("Đã giải phóng tài nguyên không sử dụng.");
    }
}   
