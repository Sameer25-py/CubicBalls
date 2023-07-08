using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class BallController : MonoBehaviour
    {
        public Rigidbody2D Rb2D;
        public Vector3     InitialBallPosition;
        public Vector2     MinMaxXPosition;

        private Camera _camera;

        public static Action FingerLift;

        public void AssignBall(GameObject obj)
        {
            Rb2D              = obj.GetComponent<Rigidbody2D>();
            Rb2D.gravityScale = 0f;
            Rb2D.position     = InitialBallPosition;
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Rb2D == null) return;
            if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject(0))
            {
                Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);

                float xOffSet = Mathf.Clamp(mousePos.x, MinMaxXPosition.x, MinMaxXPosition.y);
                Rb2D.position = new Vector2(xOffSet, Rb2D.position.y);
            }

            if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject(0))
            {
                Rb2D.gravityScale = 1f;
                Rb2D              = null;
                FingerLift?.Invoke();
            }
        }
    }
}