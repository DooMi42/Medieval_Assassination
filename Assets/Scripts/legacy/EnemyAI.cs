using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public EnemyVision vision;
    // Start is called before the first frame update
    void Start()
    {
        vision.angry = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (vision.angry == true)
        {

        }
    }

}