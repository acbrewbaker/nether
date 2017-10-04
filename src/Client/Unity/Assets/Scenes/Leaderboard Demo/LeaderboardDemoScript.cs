﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Azure.Functions;
using RestClient;
using Tacticsoft;
using Nether;

namespace NetherDemo {
  public class LeaderboardDemoScript : MonoBehaviour {
    [Header("Unity UI")]
    [SerializeField]
    private InputField inputPlayer;

    [SerializeField]
    private InputField inputScore;
    [SerializeField]
    private Text outputText;

    [Header("Nether")]
    [SerializeField]
    private LeaderboardManager leaderboardManager;

    // Use this for initialization
    void Start() {
      if (leaderboardManager == null ||
          inputPlayer == null ||
          inputScore == null ||
          outputText == null) {
        Debug.LogError("Inspector elements need to be linked up to game objects in hierarchy.");
        return;
      }
    }

    // Update is called once per frame
    void Update() {

    }

    #region Demo UI

    public void ClickLeaderboard() {
      if (!leaderboardManager.IsLeaderboardOpen()){
        outputText.text = "Loading...";
      } else {
        outputText.text = "";
      }
      leaderboardManager.ToggleLeaderboard();
    }

    public void ClickSubmit() {
      if (string.IsNullOrEmpty(inputPlayer.text)) {
        outputText.text = "Please enter a player name!";
        return;
      }
      if (string.IsNullOrEmpty(inputScore.text)) {
        outputText.text = "Please enter a score!";
        return;
      }

      double score = 0;
      Double.TryParse(inputScore.text, out score);

      outputText.text = "Submitting...";
      leaderboardManager.SubmitScore(inputPlayer.text, score);
    }

    #endregion

    #region Event handlers (optional)

    void OnEnable()
    {
        LeaderboardManager.OnLoadLeaderboardSuccess += OnLoadLeaderboardSuccess;
        LeaderboardManager.OnLoadLeaderboardFail += OnLoadLeaderboardFail;
        LeaderboardManager.OnSubmitScoreSuccess += OnSubmitScoreSuccess;
        LeaderboardManager.OnSubmitScoreFail += OnSubmitScoreFail;
    }


    void OnDisable()
    {
        LeaderboardManager.OnLoadLeaderboardSuccess -= OnLoadLeaderboardSuccess;
        LeaderboardManager.OnLoadLeaderboardFail -= OnLoadLeaderboardFail;
        LeaderboardManager.OnSubmitScoreSuccess -= OnSubmitScoreSuccess;
        LeaderboardManager.OnSubmitScoreFail -= OnSubmitScoreFail;
    }
    private void OnLoadLeaderboardSuccess(LeaderboardItem[] scores) {
      outputText.text = "Loaded " + scores.Length + " scores.";
    }

    private void OnLoadLeaderboardFail() {
      outputText.text = "Failed to load scores.";
    }

    private void OnSubmitScoreSuccess() {
      outputText.text = "Successfully submitted score.";
    }

    private void OnSubmitScoreFail() {
      outputText.text = "Failed to submit score.";
    }

    #endregion
  }
}
