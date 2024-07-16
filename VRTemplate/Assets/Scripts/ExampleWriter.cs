using UnityEngine;

public class ExampleWriter : MonoBehaviour
{
    [SerializeField] private int numberOne = 10;
    [SerializeField] private int numberTwo = 2;

    void Start()
    {
        // Assuming DataManager.Instance already exists and is set up
        DataManager.Instance.AddSubject("Physics", "Newton's Laws of Motion");
        DataManager.Instance.AddSubject("Physics", "Quantum Mechanics");

        DataManager.Instance.AddSubject("Feedback", "Question one is marked with a " + numberOne.ToString());
        DataManager.Instance.AddSubject("Feedback", "Question two is marked with a " + numberTwo.ToString());

        DataManager.Instance.WriteSubjectsToFile();
    }

}
