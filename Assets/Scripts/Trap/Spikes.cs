using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : Trap
{
    protected override void ApplyTrapEffect()
    {
        HealthSystemPlayer.instance.ChangeHealth(damageTrap, false);
    }
}
