using UnityEngine;

public class BunsenBurner : MonoBehaviour
{
    public void HeatTestTube(TestTube testTube)
    {
        if (testTube.HasSalt && testTube.saltType == "NH4Cl")
        {
            Debug.Log("White fumes detected! Presence of Cl- confirmed.");
            // Later, add particle effects here
        }
        else
        {
            Debug.Log("No visible reaction.");
        }
    }
}
