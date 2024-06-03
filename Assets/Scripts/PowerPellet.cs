using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPellet : Pellet
{
    public float duration;

    public override void Eat()
    {
        GameManager.Instance.PowerPelletEaten(this);
    }
}
