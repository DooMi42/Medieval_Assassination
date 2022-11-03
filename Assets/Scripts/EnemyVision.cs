using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
///<summary>
/// skript vihollisen näkökentälle
/// laittaa vihollisen näön pois näkyvistä pelin alussa
/// tekee vihollisesta vihaisen, kun pelaaja menee näkö cone meshiin (tba)
///</summary>

    public bool angry = false;
    private MeshRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<MeshRenderer>();    
        rend.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        // onko pelihahmo alueella?
        if (other.CompareTag("Player"))
        {
            angry = true;
        }
    }
}
