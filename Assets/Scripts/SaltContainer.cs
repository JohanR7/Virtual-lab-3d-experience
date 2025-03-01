using UnityEngine;

public class SaltContainer : MonoBehaviour
{
    public string saltType = "NH4Cl"; // Set the type of salt in Inspector

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("TestTubeLayer")) 
        {
            TestTube testTube = other.GetComponent<TestTube>();
            if (testTube != null && !testTube.HasSalt) // Only add salt if it's empty
            {
                testTube.AddSalt(saltType);
                Debug.Log(saltType + " added to the test tube!");
            }
        }
    }
}
