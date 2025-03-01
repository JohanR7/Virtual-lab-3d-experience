using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    public GameObject player;
    public Transform holdPos; // Position where object is held
    public float pickUpRange = 2f; // Pickup distance
    public float moveSpeed = 15f; // Movement speed for smooth pickup
    public float rotationSensitivity = 8f; // Adjusted for smoother rotation
    private GameObject heldObj;
    private Rigidbody heldObjRb;
    private bool isHolding = false;
    private int originalLayer;
    private bool canDrop = true; // Controls if dropping is allowed

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Pick up/drop logic
        {
            if (isHolding)
                DropObject();
            else
                TryPickUp();
        }

        if (isHolding)
        {
            MoveObjectSmooth();
            RotateObject();

            if (Input.GetMouseButtonDown(1)) // Right-click to place in holder
            {
                TryPlaceInHolder();
            }

            if (Input.GetKeyDown(KeyCode.F)) // Press F to interact with salt container
            {
                TryAddSalt();
                TryAddReagent();
            }
            if (Input.GetKeyDown(KeyCode.H)) // Press H to heat near burner
{
    TryHeatTestTube();
}

        }
    }

    void TryPickUp()
    {
        RaycastHit hit;
        if (Physics.SphereCast(Camera.main.transform.position, 0.4f, Camera.main.transform.forward, out hit, pickUpRange))
        {
            if (hit.transform.CompareTag("canPickUp"))
            {
                PickUpObject(hit.transform.gameObject);
            }
        }
    }

    void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>())
        {
            // Check if the object is being picked up from a holder
            TestTubeHolder holder = pickUpObj.transform.parent?.GetComponentInParent<TestTubeHolder>();
            if (holder != null)
            {
                holder.FreeSlot(pickUpObj); // Free the slot
            }

            heldObj = pickUpObj;
            heldObjRb = pickUpObj.GetComponent<Rigidbody>();
            originalLayer = heldObj.layer;
            heldObj.layer = LayerMask.NameToLayer("Ignore Raycast"); // Prevents interference
            heldObjRb.isKinematic = true;
            heldObj.transform.position = holdPos.position;
            heldObj.transform.rotation = holdPos.rotation; // Align instantly
            heldObj.transform.parent = holdPos;
            isHolding = true;
            canDrop = true; // Reset canDrop when picking up
        }
    }

    void DropObject()
    {
        ResetObject();
        isHolding = false;
    }

    void ResetObject()
    {
        if (heldObj != null)
        {
            heldObj.layer = originalLayer;
            heldObjRb.isKinematic = false;
            heldObj.transform.parent = null;
            heldObj = null;
        }
    }

    void MoveObjectSmooth()
    {
        if (heldObj != null)
        {
            Vector3 targetPosition = holdPos.position;
            heldObj.transform.position = Vector3.Lerp(heldObj.transform.position, targetPosition, Time.deltaTime * moveSpeed);
            heldObj.transform.rotation = Quaternion.Lerp(heldObj.transform.rotation, holdPos.rotation, Time.deltaTime * moveSpeed);
        }
    }

    void RotateObject()
    {
        if (Input.GetKey(KeyCode.R)) // Hold R to rotate
        {
            canDrop = false; // Prevent dropping while rotating
            float xRot = Input.GetAxis("Mouse X") * rotationSensitivity;
            float yRot = Input.GetAxis("Mouse Y") * rotationSensitivity;

            // Rotate around camera orientation for better control
            heldObj.transform.Rotate(Camera.main.transform.up, -xRot, Space.World);
            heldObj.transform.Rotate(Camera.main.transform.right, yRot, Space.World);
        }
        else
        {
            canDrop = true; // Allow dropping when not rotating
        }
    }

    void TryPlaceInHolder()
    {
        Collider[] colliders = Physics.OverlapSphere(heldObj.transform.position, 2f); // Detect nearby objects
        foreach (Collider col in colliders)
        {
            TestTubeHolder holder = col.GetComponent<TestTubeHolder>();
            if (holder != null)
            {
                bool placed = holder.TryPlaceTestTube(heldObj);
                if (placed)
                {
                    isHolding = false;
                    heldObj = null; // Remove reference after placing
                }
                return;
            }
        }
    }

    void TryAddSalt()
    {
        if (heldObj != null)
        {
            Collider[] colliders = Physics.OverlapSphere(heldObj.transform.position, 1f); // Check nearby objects
            foreach (Collider col in colliders)
            {
                SaltContainer saltContainer = col.GetComponent<SaltContainer>();
                if (saltContainer != null)
                {
                    TestTube testTube = heldObj.GetComponent<TestTube>();
                    if (testTube != null && !testTube.HasSalt)
                    {
                        testTube.AddSalt(saltContainer.saltType);
                        Debug.Log(saltContainer.saltType + " added to the test tube!");
                        return;
                    }
                }
            }
        }
    }
    void TryHeatTestTube()
{
    if (heldObj != null)
    {
        Collider[] colliders = Physics.OverlapSphere(heldObj.transform.position, 1f); // Check nearby objects
        foreach (Collider col in colliders)
        {
            BunsenBurner burner = col.GetComponent<BunsenBurner>();
            if (burner != null)
            {
                TestTube testTube = heldObj.GetComponent<TestTube>();
                if (testTube != null)
                {
                    burner.HeatTestTube(testTube);
                    return;
                }
            }
        }
    }
}
void TryAddReagent()
{
    if (heldObj != null)
    {
        Collider[] colliders = Physics.OverlapSphere(heldObj.transform.position, 1f);
        foreach (Collider col in colliders)
        {
            NaOHContainer naohContainer = col.GetComponent<NaOHContainer>();
            NesslerContainer nesslerContainer = col.GetComponent<NesslerContainer>();

            TestTube testTube = heldObj.GetComponent<TestTube>();

            if (testTube != null)
            {
                if (naohContainer != null)
                {
                    naohContainer.TryAddNaOH(testTube);
                    return;
                }
                else if (nesslerContainer != null)
                {
                    nesslerContainer.TryAddNessler(testTube);
                    return;
                }
            }
        }
    }
}



   
}