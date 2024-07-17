using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MultipleChoiceQuestion : MonoBehaviour
{
    [SerializeField, Tooltip("The TMP_Text component that displays the question text.")]
    private TMP_Text _questionText; // Text component for the question

    private string _answer = "Unanswered"; // Tracks the selected answer

    // Sets the answer based on the text given
    public void SetAnswer(TMP_Text targetText)
    {
        // Check if the button is null
        if (targetText == null)
        {
            Debug.LogWarning($"Trying to assign the answer to the text, but the button has not been found on script '{GetType().Name}' on object '{gameObject.name}'");
            return;
        }

        // If target text is not null assign the new text
        if (targetText != null)
        {
            _answer = targetText.text; // Set the answer to the target text
        }
        else
        {
            Debug.LogWarning($"Trying to assign the answer to the text, but TMP_Text component has not been found on script '{GetType().Name}' on object '{targetText.name}'");
        }
    }

    // Finalizes the question and submits the answer
    public void FinishQuestion()
    {
        // Check if an answer has been selected
        if (_answer == "Unanswered")
        {
            Debug.LogWarning($"Trying to finish the question, but the question has not been answered yet on script '{GetType().Name}' on object '{gameObject.name}'");
            return;
        }

        // Get the subject name from the question text or fallback to the GameObject name
        string subjectName = _questionText != null ? _questionText.text : gameObject.name;

        // Add the subject and answer to the DataManager
        DataManager.Instance.AddSubject(subjectName, $"{_questionText.text} has been answered with: {_answer}");

        // Destroy the GameObject after processing
        Destroy(gameObject);
    }
}
