using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("提示文字")]
    public string tipText;

    [Header("对话内容")]
    public Sprite npcHead;
    [TextArea] public string[] dialogueLines;

    [Header("任务")]
    public bool giveTask;
    public string taskName;

    private bool inRange;

    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            TipUI.Instance.HideTip();

            DialogueUI.Instance.StartDialogue(this);
            PlayerMovement3D.canMove = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;
            TipUI.Instance.ShowTip(tipText);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            TipUI.Instance.HideTip();
        }
    }
}
