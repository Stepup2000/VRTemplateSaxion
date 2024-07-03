using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FeedbackQuestion : MonoBehaviour
{
    [SerializeField] private Slider _questionSlider;
    [SerializeField] private TMP_Text _questionText;

    public void FinishQuestion()
    {
        if (_questionSlider == null) return;

        // Determine the subject name based on the text if available, otherwise use the gameObject name
        string subjectName = _questionText != null ? _questionText.text : gameObject.name;

        // Get the slider value as a float
        float rating = _questionSlider.value;

        // Add the subject and rating to the DataManager in the desired format
        DataManager.Instance.AddSubject(subjectName.ToString(), "This question is marked with a " + rating.ToString());
        Destroy(gameObject);
    }
}
