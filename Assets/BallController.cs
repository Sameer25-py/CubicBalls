using System;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public class BallController : MonoBehaviour
    {
        public Rigidbody2D Rb2D;
        public Vector3     InitialBallPosition;
        public Vector2     MinMaxXPosition;

        private Camera _camera;


        private void Start()
        {
            Rb2D.gravityScale = 0f;
            Rb2D.position     = InitialBallPosition;
            _camera           = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);

                float xOffSet = Mathf.Clamp(mousePos.x, MinMaxXPosition.x, MinMaxXPosition.y);
                Rb2D.position = new Vector2(xOffSet, Rb2D.position.y);
            }

            if (Input.GetMouseButtonUp(0))
            {
                Rb2D.gravityScale = 1f;
            }
        }
    }
}