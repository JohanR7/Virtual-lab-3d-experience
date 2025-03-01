using UnityEngine;

public class ReactionHandler : MonoBehaviour
{
    public void CheckReaction(TestTube testTube)
    {
        if (testTube.HasSalt && testTube.saltType == "NH4Cl")
        {
            if (testTube.HasNaOH && testTube.HasNessler)
            {
                Debug.Log("Yellow/Brown precipitate formed: NH4⁺ confirmed!");
                // Later, replace this with particle effects
            }
            else if (testTube.HasNaOH)
            {
                Debug.Log("NaOH added. Now add Nessler’s reagent for NH4⁺ test.");
            }
        }
    }
}
