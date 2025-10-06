using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem; // New Input System

[System.Serializable]
public struct DialogueLine
{
    public string speaker;    // Character name
    [TextArea] public string text;   // Dialogue text
}

public class Dialogue : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI nameText;
    public DialogueLine[] dialogueLines;
    public float textSpeed;

    public Attack attack;
    public AIChase movement;

    private int index = 0;
    private bool isTyping = false;

    void Start()
    {
        if (attack != null)
            attack.enabled = false;

        if (movement != null)
            movement.enabled = false;


        textComponent.text = string.Empty;
        StartDialogue();
    }

    void StartDialogue()
    {
        if (dialogueLines == null || dialogueLines.Length == 0)
        {
            Debug.LogWarning("Dialogue lines are empty!");
            textComponent.text = "";
            if (nameText != null) nameText.text = "";
            return;
        }
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        textComponent.text = "";
        if (nameText != null)
            nameText.text = dialogueLines[index].speaker;

        foreach (char c in dialogueLines[index].text.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;

    }

    void Update()
    {
        if (dialogueLines == null || dialogueLines.Length == 0) return;
        if (index >= dialogueLines.Length) return;


        if (Keyboard.current.fKey.wasPressedThisFrame)
        {

            if (isTyping)
            {
                StopAllCoroutines();
                textComponent.text = dialogueLines[index].text;
                isTyping = false;

            }
            else
            {
                NextLine();
            }
        }
    }

    void NextLine()
    {

        if (index < dialogueLines.Length - 1)
        {
            index++;
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }
    void EndDialogue()
    {
        if (attack != null)
            attack.enabled = true;

        if (movement != null)
            movement.enabled = true;

        gameObject.SetActive(false); // Hide panel when done
    }
}

