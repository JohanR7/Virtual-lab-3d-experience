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
        if (!HasNaOH)
        {
            Debug.Log("Cannot add Nessler’s reagent before NaOH!");
            return;
        }
        if (HasNessler)
        {
            Debug.Log("Nessler’s reagent is already in the test tube!");
            return;
        }

        HasNessler = true;
        Debug.Log("Nessler’s reagent added to the test tube! Yellow or brown precipitate formed! (NH4+ detected)");
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
