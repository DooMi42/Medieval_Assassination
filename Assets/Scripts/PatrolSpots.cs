using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolSpots : MonoBehaviour
{

    ///<summary>
    ///pistää patrol piste napit veks pelin aloitettua
    ///</summary>

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
}
