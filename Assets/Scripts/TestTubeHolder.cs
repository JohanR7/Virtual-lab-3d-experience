using UnityEngine;

public class TestTubeHolder : MonoBehaviour
{
    public Transform[] holderSlots; // Assign slots in the Inspector
    private bool[] slotOccupied;

    void Start()
    {
        slotOccupied = new bool[holderSlots.Length];
    }

    public bool TryPlaceTestTube(GameObject testTube)
    {
        for (int i = 0; i < holderSlots.Length; i++)
        {
            if (!slotOccupied[i]) // Find an empty slot
            {
                // Place the test tube in the slot
                testTube.transform.position = holderSlots[i].position;
                testTube.transform.rotation = holderSlots[i].rotation;
                testTube.transform.parent = holderSlots[i]; // Attach to holder
                slotOccupied[i] = true;

                // Disable physics so it stays in place
                Rigidbody rb = testTube.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true;
                }

                Debug.Log("Test tube placed in holder slot " + i);
                return true;
            }
        }

        Debug.Log("No empty slots available.");
        return false;
    }

    public void FreeSlot(GameObject testTube)
    {
        for (int i = 0; i < holderSlots.Length; i++)
        {
            if (holderSlots[i] == testTube.transform.parent) // Check if the test tube is in this slot
            {
                slotOccupied[i] = false; // Free the slot
                Debug.Log("Slot " + i + " freed.");
                return;
            }
        }
    }
}