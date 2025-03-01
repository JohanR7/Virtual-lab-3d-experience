using UnityEngine;

public class NaOHContainer : MonoBehaviour
{
    public void TryAddNaOH(TestTube testTube)
    {
        if (!testTube.HasNaOH)
        {
            testTube.AddNaOH();
            Debug.Log("NaOH added to test tube!");
        }
    }
}
