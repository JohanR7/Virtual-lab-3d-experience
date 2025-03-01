using UnityEngine;

public class NesslerContainer : MonoBehaviour
{
    public void TryAddNessler(TestTube testTube)
    {
        if (!testTube.HasNessler)
        {
            testTube.AddNessler();
            Debug.Log("Nessler’s reagent added to test tube!");
        }
    }
}
