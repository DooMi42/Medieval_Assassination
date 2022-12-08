using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public WeaponController wc;
    public GameObject hitParticle;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && wc.isAttacking)
        {
            print("attacked" + other.name);

            //Add a hitparticle that spawns blood when enemy is hit

        }
    }
}
