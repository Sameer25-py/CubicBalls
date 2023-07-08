using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class MatchManager : MonoBehaviour
    {
        private Dictionary<string, string> _matchRules = new()
        {
            { "Shuttle", "Pingpong" },
            { "Pingpong", "Tennisball" },
            { "Tennisball", "Baseball" },
            { "Baseball", "Waterpoloball" },
            { "Waterpoloball", "Volleyball" },
            { "Volleyball", "Football" },
            { "Football", "Basketball" },
            { "Basketball", "Rugby" }
        };

        public BallLibrary BallLibrary;

        private void OnEnable()
        {
            Ball.BallCollided += OnBallCollided;
        }

        private void OnBallCollided(string ballName, Vector3 spawnPos)
        {
            ballName = ballName.Replace("(Clone)", "");
            if (_matchRules.ContainsKey(ballName))
            {
                GameObject obj = BallLibrary.InstantiateBall(_matchRules[ballName]);
                obj.transform.position = spawnPos;
                
                GameManager.AddBallToList?.Invoke(obj);
            }
        }
    }
}