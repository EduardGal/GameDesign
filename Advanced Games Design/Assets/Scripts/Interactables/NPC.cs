using UnityEngine;

public class NPC : Interactables
{
    public string[] dialogue;
    public string npcName;

    public override void Interaction()
    {
        //DialogueManager.Instance.AddDialogue(dialogue, npcName);
    }
}