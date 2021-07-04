﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<HighscoreEntry> highscoreEntryList;
    private List<Transform> highscoreEntryTransformList;

    public Text GameScoreText;

    private void Awake()
    {

        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        AddHighscoreEntry(EndScreen.score);

        GameScoreText.text = "SCORE: " + EndScreen.score.ToString().PadLeft(5, '0');

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.hsEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 20.0f;

        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString = rank.ToString() + ")";
        entryTransform.Find("posText").GetComponent<Text>().text = rankString;

        int score = highscoreEntry.score;
        entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();

        transformList.Add(entryTransform);
    }

    private void AddHighscoreEntry(int score)
    {
        Highscores highscores;
        // Create highscore entry
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score };

        // Load saved highscores

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        if (jsonString == "")
        {
            highscores = new Highscores();
            highscores.hsEntryList = new List<HighscoreEntry>();
            for(int i = 0; i < 10; i++)
            {
                HighscoreEntry hse = new HighscoreEntry { score = 0 };
                highscores.hsEntryList.Add(hse);
            }
        }
        else
        {
            highscores = JsonUtility.FromJson<Highscores>(jsonString);
        }

        // Add new entry to highscores
        highscores.hsEntryList.Add(highscoreEntry);

        // Sort the list
        for (int i = 0; i < highscores.hsEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.hsEntryList.Count; j++)
            {
                if (highscores.hsEntryList[j].score > highscores.hsEntryList[i].score)
                {
                    // Swap
                    HighscoreEntry temp = highscores.hsEntryList[i];
                    highscores.hsEntryList[i] = highscores.hsEntryList[j];
                    highscores.hsEntryList[j] = temp;
                }
            }
        }

        while(highscores.hsEntryList.Count > 10)
        {
            highscores.hsEntryList.RemoveAt(highscores.hsEntryList.Count - 1);
        }

        // Save updated highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    /* Represents a single high score entry */
    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
//        public string name;
    }

    private class Highscores
    {
        public List<HighscoreEntry> hsEntryList;
    }
}
