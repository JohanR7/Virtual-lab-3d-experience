using UnityEngine;

public class TestTube : MonoBehaviour
{
    public bool HasSalt { get; private set; } = false;
    public bool HasNaOH { get; private set; } = false;
    public bool HasNessler { get; private set; } = false;

    public string saltType = "";

    public void AddSalt(string salt)
    {
        if (!HasSalt)
        {
            HasSalt = true;
            saltType = salt;
            Debug.Log(salt + " added to the test tube!");
        }
        else
        {
            Debug.Log("Salt already added: " + saltType);
        }
    }

    public void AddNaOH()
    {
        if (!HasNaOH)
        {
            HasNaOH = true;
            Debug.Log("NaOH added to the test tube!");
        }
        else
        {
            Debug.Log("NaOH is already in the test tube!");
        }
    }

    public void AddNessler()
    {
        if (HasNaOH && !HasNessler) // Ensures Nessler is added only after NaOH
        {
            HasNessler = true;
            Debug.Log("Nessler’s reagent added to the test tube!.Yellow or brown precipitate formed! (NH4+ detected)");

            // Check if NH4Cl is present
            if (HasSalt)
            {
                Debug.Log("Test tube contains salt: " + saltType);
                if (saltType.Trim().Equals("NH4Cl", System.StringComparison.OrdinalIgnoreCase))
                {
                    Debug.Log("Yellow or brown precipitate formed! (NH4+ detected)");
                }
                else
                {
                    Debug.Log("No NH4+ detected. The salt is: " + saltType);
                }
            }
            else
            {
                Debug.Log("No salt present in the test tube!");
            }
        }
        else if (!HasNaOH)
        {
            Debug.Log("Cannot add Nessler’s reagent before NaOH!");
        }
        else
        {
            Debug.Log("Nessler’s reagent is already in the test tube!");
        }
    }
    void CheckForReaction()
{
    ReactionHandler reactionHandler = FindObjectOfType<ReactionHandler>();
    if (reactionHandler != null)
    {
        reactionHandler.CheckReaction(this); // Pass the test tube instance
    }
}

}
