using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance;

    [Header("UI引用")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    [Header("打字效果速度")]
    public float typeSpeed = 0.03f;

    private string[] currentLines;
    private int lineIndex;
    private bool isTyping;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        // 正在打字时按空格/鼠标直接显示完
        if (isTyping)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                StopAllCoroutines();
                dialogueText.text = currentLines[lineIndex];
                isTyping = false;
            }
            return;
        }

        // 下一句
        if (dialoguePanel.activeSelf &&
            (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
        {
            NextLine();
        }
    }

    // 开始对话
    public void ShowDialogue(string[] lines)
    {
        currentLines = lines;
        lineIndex = 0;
        dialoguePanel.SetActive(true);
        NextLine();
    }

    // 下一句
    void NextLine()
    {
        if (lineIndex >= currentLines.Length)
        {
            EndDialogue();
            return;
        }

        StartCoroutine(TypeText(currentLines[lineIndex]));
        lineIndex++;
    }


    IEnumerator TypeText(string line)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }
        isTyping = false;
    }

    // 结束对话
    void EndDialogue()
    {
        dialoguePanel.SetActive(false);
    }
}
