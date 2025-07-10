using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesBall : Trap
{
    [SerializeField] Transform Left;
    [SerializeField] Transform Right;
    bool GoRight = true;
    [SerializeField] float speed;

    protected override void ApplyTrapEffect()
    {
        HealthSystemPlayer.instance.ChangeHealth(damageTrap, false);
    }

    private void FixedUpdate()
    {
        MoveSpike();
    }

    void MoveSpike()
    {
        if (GoRight)
        {
            transform.position = Vector2.MoveTowards(transform.position, Right.position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, Right.position) < 0.1f)
            {
                GoRight = false;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, Left.position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, Left.position) < 0.1f)
            {
                GoRight = true;
            }
        }
    }

}




