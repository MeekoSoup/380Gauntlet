using General;
using UnityEngine;

public class InstructionRemover : Singleton<InstructionRemover>
{
    public GameObject[] elements;

    public static void SetPlayerCount(int players)
    {
        // Turn off elements in order for each active player
        int i = 0;
        for (; i < players; i++)
        {
            if (Instance.elements[i])
                Instance.elements[i].SetActive(false);
        }
        // turn it back on for all the rest
        for (int j = i; j < 4; j++)
        {
            if (Instance.elements[j])
                Instance.elements[j].SetActive(true);
        }
    }
}
