using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    HashSet<GameObject> gravities;
    void Start()
    {
        // only need to do this once for the global gravity of the scene
        GravityController.instance = new GravityController();
        gravities = new HashSet<GameObject>();
        localGravity = new Vector3(Mathf.Infinity, 0f);

        Cursor.lockState = CursorLockMode.Locked;
    }

    public Vector3 localGravity;
    float verticalMoveForce = 10f;
    float horizontalMoveForce = 10f;

    float mouseVertical = 0f;
    float mouseMaxVertical = 45f;
    float mouseMinVertical = -40f;

    private void FixedUpdate()
    {
        if (Input.GetAxis("Vertical") != 0f)
        {
            transform.GetComponent<Rigidbody>().AddForce(transform.forward * Input.GetAxis("Vertical") * verticalMoveForce);
        }

        if (Input.GetAxis("Horizontal") != 0f)
        {
            // Rotate on horizontal rather than strafe.
            // transform.Rotate(Vector3.up, Input.GetAxis("Horizontal") * horizontalMoveForce);
            transform.GetComponent<Rigidbody>().AddForce(transform.right * Input.GetAxis("Horizontal") * horizontalMoveForce);
        }

        if(localGravity.Equals(new Vector3(Mathf.Infinity, 0f)))
        {
            transform.GetComponent<Rigidbody>().velocity += GravityController.instance.globalGravity * Time.fixedDeltaTime;
        }
        else
        {
            transform.GetComponent<Rigidbody>().velocity += localGravity * Time.fixedDeltaTime;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.GetComponent<Rigidbody>().AddForce(transform.up * 300f);
        }

    }

    public GameObject playerCamera;
    public GameObject cameraPoint;

    //public float ayyLmao = 10f;

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKey(KeyCode.Keypad8))
        {
            topsyTurvy(Vector3.forward);
        }
        if (Input.GetKey(KeyCode.Keypad4))
        {
            topsyTurvy(Vector3.left);
        }
        if (Input.GetKey(KeyCode.Keypad6))
        {
            topsyTurvy(Vector3.right);
        }
        if (Input.GetKey(KeyCode.Keypad2))
        {
            topsyTurvy(Vector3.back);
        }
        if (Input.GetKey(KeyCode.Keypad9))
        {
            topsyTurvy(Vector3.up);
        }
        if (Input.GetKey(KeyCode.Keypad3))
        {
            topsyTurvy(Vector3.down);
        }
        */

        if (Input.GetAxis("Mouse X") != 0f)
        {
            transform.Rotate(0f, Input.GetAxis("Mouse X"), 0f);
        }
        if (Input.GetAxis("Mouse Y") != 0f)
        {
            mouseVertical += Input.GetAxis("Mouse Y");
            mouseVertical = Mathf.Clamp(mouseVertical, mouseMinVertical, mouseMaxVertical);
            cameraPoint.transform.localRotation = Quaternion.Euler(mouseVertical, 0f, 0f);
        }

        // Move the camera closer to the player so that it doesn't go through walls.
        RaycastHit wallHit;
        if(Physics.Raycast(transform.position, transform.forward * -1, out wallHit, 5f))
        {
            playerCamera.transform.localPosition = new Vector3(playerCamera.transform.localPosition.x, playerCamera.transform.localPosition.y, (-1 * wallHit.distance));
        }
        else
        {
            playerCamera.transform.localPosition = new Vector3(playerCamera.transform.localPosition.x, playerCamera.transform.localPosition.y, -5f);
        }

        // Drawline

        /*
        float vSpeed = Mathf.Sin(Mathf.Deg2Rad * mouseVertical) * -1 * ayyLmao;
        float hSpeed = Mathf.Cos(Mathf.Deg2Rad * mouseVertical) * -1 * ayyLmao;

        lr.positionCount = 30;

        for(int a = 0; a < lr.positionCount; a++)
        {
            float vComp = transform.position.y + (vSpeed * a * 0.1f) + (0.5f * -9.81f * Mathf.Pow(a * 0.1f, 2));
            float hComp = (hSpeed * a * 0.1f);

            lr.SetPosition(a, transform.position + new Vector3(hComp, vComp));
        }
        */
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<varLocalGravity>() != null)
        {
            if( ! gravities.Contains(other.gameObject))
            {
                gravities.Add(other.gameObject);
            }
        }

        recalculateLocalGravity();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<varLocalGravity>() != null)
        {
            if ( gravities.Contains(other.gameObject))
            {
                gravities.Remove(other.gameObject);
            }
        }

        recalculateLocalGravity();
    }

    void recalculateLocalGravity()
    {
        /*
        Vector3 olddown = gravityForce;
        gravityForce = newTurn * 9.81f;

        if(olddown.Equals(gravityForce))
        {
            return;
        }

        transform.rotation = Quaternion.LookRotation(Vector3.Cross(transform.forward, gravityForce), -1 * gravityForce);
        */

        localGravity = Vector3.zero;

        if (gravities.Count > 0)
        {
            foreach(GameObject lg in gravities)
            {
                localGravity += lg.GetComponent<varLocalGravity>().localGravity;
            }
        }
        else
        {
            localGravity = new Vector3(Mathf.Infinity, 0f);
        }

    }
}
