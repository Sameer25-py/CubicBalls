using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class Loader : MonoBehaviour
    {
        private Image _loaderFillImage;

        private void OnEnable()
        {
            _loaderFillImage = GetComponent<Image>();
        }

        private IEnumerator Start()
        {
            float elapsedTime = 0f;
            while (elapsedTime < 1f)
            {
                elapsedTime                 += Time.deltaTime / 2f;
                _loaderFillImage.fillAmount =  elapsedTime;
                yield return null;
            }

            SceneManager.LoadScene("Gameplay");
        }
    }
}