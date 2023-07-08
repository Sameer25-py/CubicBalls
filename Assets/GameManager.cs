using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour
    {
        public BallLibrary    BallLibrary;
        public BallController BallController;

        public GameObject PauseMenu, InGameUI, StatsMenu, StatsMenuInGame, Settings, MainMenu;

        public        TMP_Text           IngameScore, HighScore, HighScore2;
        private       List<GameObject>   _spawnedBalls = new();
        public static Action<GameObject> AddBallToList;

        public static bool EnableVibration = true;
        public static bool EnableSound     = true;

        private int _score = 0;

        public AudioSource AudioSource;

        private void UpdateScore()
        {
            _score           += 2;
            IngameScore.text =  _score.ToString();
            HighScore.text   =  _score.ToString();
            HighScore2.text  =  _score.ToString();
        }

        private void ResetScore()
        {
            _score           = 0;
            IngameScore.text = "0";
            HighScore.text   = "0";
            HighScore2.text  = "0";
        }

        private void OnEnable()
        {
            BallController.FingerLift += OnFingerLift;
            AddBallToList             += OnAddBallToListCalled;
        }

        private void CheckForGameEnd()
        {
            if (_spawnedBalls.Count == 1) return;
            foreach (GameObject ball in _spawnedBalls)
            {
                if (ball)
                {
                    if (ball.transform.position.y >= 3.1f)
                    {
                        GameEnd();
                        break;
                    }
                }
            }
        }

        private void GameEnd()
        {
            BallController.enabled = false;
            StatsMenu.SetActive(true);
        }

        private void OnAddBallToListCalled(GameObject obj)
        {
            _spawnedBalls.Add(obj);
            UpdateScore();
            CheckForGameEnd();

            if (EnableVibration)
            {
                Handheld.Vibrate();
            }
        }

        private void OnFingerLift()
        {
            Invoke(nameof(SpawnBall), 1f);
        }

        public void StartGame()
        {
            MainMenu.SetActive(false);
            StatsMenuInGame.SetActive(false);
            StatsMenu.SetActive(false);
            ResetScore();
            InGameUI.SetActive(true);
            foreach (GameObject ball in _spawnedBalls.Where(ball => ball))
            {
                Destroy(ball);
            }

            SpawnBallWithoutChecking();
        }

        private void SpawnBall()
        {
            GameObject newBall = BallLibrary.InstantiateRandomBall();
            BallController.AssignBall(newBall);
            BallController.enabled = true;
            _spawnedBalls.Add(newBall);
            CheckForGameEnd();
        }

        private void SpawnBallWithoutChecking()
        {
            GameObject newBall = BallLibrary.InstantiateRandomBall();
            BallController.AssignBall(newBall);
            BallController.enabled = true;
            _spawnedBalls.Add(newBall);
        }

        public void PauseGame()
        {
            BallController.enabled = false;
            PauseMenu.SetActive(true);
        }

        public void ResumeGame()
        {
            BallController.enabled = true;
            PauseMenu.SetActive(false);
        }

        public void GoToHome()
        {
            StatsMenu.SetActive(false);
            Settings.SetActive(false);
            MainMenu.SetActive(true);
            StatsMenuInGame.SetActive(false);
        }

        public void ShowStats()
        {
            StatsMenu.SetActive(true);
        }

        public void ShowStatsInGame()
        {
            StatsMenuInGame.SetActive(true);
        }

        public void BackToPauseMenu()
        {
            StatsMenuInGame.SetActive(false);
        }

        public void ShowSettings()
        {
            if (Settings.activeSelf)
            {
                Settings.SetActive(false);
            }
            else
            {
                Settings.SetActive(true);
            }
        }

        public void ToggleVibration(bool state)
        {
            EnableVibration = state;
        }

        public void ToogleSound(bool state)
        {   
            EnableSound      = state;
            AudioSource.mute = !EnableSound;

        }
    }
}