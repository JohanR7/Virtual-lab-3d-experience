using UnityEngine;

public class NesslerContainer : MonoBehaviour
{
    public void TryAddNessler(TestTube testTube)
    {
        if (!testTube.HasNessler)
        {
            testTube.AddNessler();
            Debug.Log("Nessler’s reagent added to the test tube! Yellow or brown precipitate formed! (NH4+ detected)");
        }
    }
}
