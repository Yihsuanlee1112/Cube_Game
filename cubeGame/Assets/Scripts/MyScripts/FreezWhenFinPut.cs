using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezWhenFinPut : MonoBehaviour
{
    public Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Build"))
        {
            print("fff");
            FreezPosition();
        }
    }
    public void FreezPosition()
    {
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }
}
