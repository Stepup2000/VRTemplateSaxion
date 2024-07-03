using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private static DataManager instance;
    private string currentFileName;
    private Dictionary<string, List<string>> subjectsDictionary = new Dictionary<string, List<string>>();

    public static DataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DataManager>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("DataWriter");
                    instance = singletonObject.AddComponent<DataManager>();
                }
            }
            return instance;
        }
    }

    [SerializeField] private string _title = "PlaytestData";
    [SerializeField] private string _folderName = "PlaytestData";

    private void Awake()
    {
        // Ensure there's only one instance of this class
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void CreateNewFile()
    {
        string dateTimeString = DateTime.Now.ToString("dd-MM-yyyy_HHmm");
        currentFileName = $"{dateTimeString}.csv"; // Change file extension to CSV

        // Define the folder name
        string folderName = "PlaytestData";

        // Combine the paths to create the full directory path
        string directoryPath = Path.Combine(Application.dataPath, folderName);

        // Check if the directory exists; if not, create it
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        // Combine the full file path including the folder
        string filePath = Path.Combine(directoryPath, currentFileName);

        int suffix = 1;
        while (File.Exists(filePath))
        {
            currentFileName = $"{dateTimeString}_{suffix}.csv"; // Change file extension to CSV
            filePath = Path.Combine(directoryPath, currentFileName);
            suffix++;
        }

        File.WriteAllText(filePath, "");
        Debug.Log($"Created new file: {currentFileName}");
        Debug.Log($"File Path: {filePath}");
    }



    // Method to add information about a subject to the dictionary
    public void AddSubject(string subjectName, string sentence)
    {
        if (!subjectsDictionary.ContainsKey(subjectName))
        {
            subjectsDictionary.Add(subjectName, new List<string>());
        }

        subjectsDictionary[subjectName].Add(sentence);
    }

    // Method to write all subjects to the file
    public void WriteSubjectsToFile()
    {
        if (subjectsDictionary.Count == 0)
        {
            Debug.LogWarning("No subjects to write.");
            return;
        }

        CreateNewFile();

        // Prepare the content to write to CSV
        string content = "";
        content += $"\"{_title}\"\n\n"; // Enclose title in quotes for CSV

        foreach (var kvp in subjectsDictionary)
        {
            content += $"\"Question: {kvp.Key}\",\n"; // Subject name as the key

            foreach (var sentence in kvp.Value)
            {
                // Split sentence into two parts: before the last space and after it
                int lastSpaceIndex = sentence.LastIndexOf(' ');
                if (lastSpaceIndex != -1)
                {
                    string firstPart = sentence.Substring(0, lastSpaceIndex);
                    string secondPart = sentence.Substring(lastSpaceIndex + 1);

                    // Format each part in quotes and separated by comma
                    content += $"\"{firstPart}\",\"{secondPart}\"\n";
                }
                else
                {
                    // If no space found, write the whole sentence in one column
                    content += $"\"{sentence}\",\n";
                }
            }

            content += "\n"; // Add an empty line between subjects for readability in CSV
        }

        // Write content to file
        string directoryPath = Path.Combine(Application.dataPath, _folderName);
        string filePath = Path.Combine(directoryPath, currentFileName);

        File.AppendAllText(filePath, content);
        Debug.Log("Subjects written to file.");
    }

    private void OnApplicationQuit()
    {
        WriteSubjectsToFile();
    }

}
