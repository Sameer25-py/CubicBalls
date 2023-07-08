using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class BallLibrary : MonoBehaviour
    {
        public List<GameObject> Balls;

        public GameObject InstantiateBall(string ballName)
        {
            return (from ball in Balls
                where ball.name == ballName
                select Instantiate(ball)).FirstOrDefault();
        }

        public GameObject InstantiateRandomBall()
        {
            return Instantiate(Balls[Random.Range(0, Balls.Count)]);
        }
    }
}