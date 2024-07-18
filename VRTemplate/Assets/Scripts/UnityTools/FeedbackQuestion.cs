using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FeedbackQuestion : MonoBehaviour
{
    [SerializeField] private Slider _questionSlider;
    [SerializeField] private TMP_Text _questionText;

    private string _subjectName;

    private void Start()
    {
        SetSubjectName();
        DataManager.Instance.AddSubject(_subjectName, "X");
    }

    private void SetSubjectName()
    {
        _subjectName = _questionText != null ? _questionText.text : gameObject.name;
    }

    public void FinishQuestion()
    {
        if (_questionSlider == null) return;

        // Determine the subject name based on the text if available, otherwise use the gameObject name
        string subjectName = _questionText != null ? _questionText.text : gameObject.name;

        // Get the slider value as a float
        float rating = _questionSlider.value;

        // Add the subject and rating to the DataManager in the desired format
        DataManager.Instance.ReplaceSubject(subjectName.ToString(), rating.ToString());
        Destroy(gameObject);
    }
}
