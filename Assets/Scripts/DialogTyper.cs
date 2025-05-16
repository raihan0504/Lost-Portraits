using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class DialogTyper : MonoBehaviour
{
    public TextMeshProUGUI dialogText;
    public float typingSpeed = 0.05f;

    private Coroutine typingCoroutine;

    public void StartTyping(string line)
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeLine(line));
    }

    IEnumerator TypeLine(string line)
    {
        dialogText.text = "";

        foreach (char letter in line)
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void ClearText()
    {
        dialogText.text = "";
    }
}
