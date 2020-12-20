using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentGravity : MonoBehaviour
{

    Vector3 localGravity;
    List<GameObject> triggers;
    private void Start() {
        localGravity = LEVELDATA.instance.LevelGravity;
        triggers = new List<GameObject>();
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().velocity += localGravity * Time.fixedDeltaTime * 9.81f;
    }

    private void OnTriggerEnter(Collider other) {

        if(other.gameObject.tag == "localgravity")
        {
            triggers.Add(other.gameObject);
            localGravity = other.transform.up;
        }
        
    }

    private void OnTriggerExit(Collider other) {

        // I was running into issues where the player removed a block
        // before the present left it, causing the list to be invalid.
        // This validates the list.
        triggers.RemoveAll(item => item == null);


        triggers.Remove(other.gameObject);
        if(triggers.Count == 0)
        {
            localGravity = LEVELDATA.instance.LevelGravity;
        }
        else
        {
            localGravity = triggers[triggers.Count-1].transform.up;
        }
    }
}
