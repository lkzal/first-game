using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance;

    public GameObject dialoguePanel;
    public Image headImage;
    public TextMeshProUGUI dialogueText;
    public float typeSpeed = 0.04f;

    private string[] lines;
    private int lineIndex;
    private bool isTyping;

    void Awake()
    {
        Instance = this;
        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (!dialoguePanel.activeSelf) return;

        if (isTyping)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                StopAllCoroutines();
                dialogueText.text = lines[lineIndex - 1];
                isTyping = false;
            }
            return;
        }

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            // 按空格/鼠标时，先结束对话，同时清空输入缓存
            Input.ResetInputAxes();
            NextLine();
        }
    }

    public void StartDialogue(Interactable npc)
    {
        lines = npc.dialogueLines;
        lineIndex = 0;

        if (npc.npcHead != null)
            headImage.sprite = npc.npcHead;

        dialoguePanel.SetActive(true);
        NextLine();
    }

    void NextLine()
    {
        if (lineIndex >= lines.Length)
        {
            EndDialogue();
            return;
        }

        StopAllCoroutines();
        StartCoroutine(TypeText(lines[lineIndex]));
        lineIndex++;
    }

    IEnumerator TypeText(string str)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char c in str)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }
        isTyping = false;
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        PlayerMovement3D.canMove = true;
    }
}
