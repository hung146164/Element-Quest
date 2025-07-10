using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class HitBox : MonoBehaviour
{
    [SerializeField]Transform Player;
    [SerializeField]float DameHitbox=10;
    [SerializeField]float TangDamelenSoLan;
    Rigidbody2D rb;
    protected string ownTag;
    private void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        ownTag=Player.tag;
    }
    public void SetDameHitBox(float dameHitBox)
    {
        DameHitbox=dameHitBox*TangDamelenSoLan;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag!= ownTag)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if(PlayerAttack.instance.isparry)
                {
                    if(CompareTag("Bullet"))
                    {
                        rb.velocity = -rb.velocity;
                        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 180);
                        ownTag = "Player";
                    }
                }
                else
                {
                    HealthSystemPlayer.instance.ChangeHealth(DameHitbox, false);
                }
            }
            else if(collision.gameObject.CompareTag("enemy"))
            {
                collision.GetComponent<HealthSystemEnemy>().ChangeHealth(DameHitbox, false);
            }

        }


    } 
}
