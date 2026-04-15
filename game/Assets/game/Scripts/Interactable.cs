using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("对话内容")]
    [TextArea] public string[] dialogueLines;

    private bool isInRange;

    void Update()
    {
        // 在范围 + 按E
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            DialogueUI.Instance.ShowDialogue(dialogueLines);
        }
    }

    // 玩家进入范围
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isInRange = true;
    }

    // 玩家离开范围
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            isInRange = false;
    }
}
