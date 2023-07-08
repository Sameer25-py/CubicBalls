using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class Ball : MonoBehaviour
    {
        public static Action<string, Vector3> BallCollided;
        public        bool                    HasCollided = false;
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (HasCollided) return;
            if (gameObject.name.Contains("Rugby")) return;
            if (!col.collider.CompareTag("Ball") || col.collider.gameObject.name != gameObject.name) return;
            HasCollided = true;
            col.collider.gameObject.GetComponent<Ball>()
                .HasCollided = true;
            BallCollided?.Invoke(gameObject.name, col.transform.position);
            Destroy(gameObject);
            Destroy(col.collider.gameObject);
        }
    }
}