using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class Sound : MonoBehaviour
    {
        private Toggle _toggle;

        private void OnEnable()
        {
            _toggle      = GetComponent<Toggle>();
            _toggle.isOn = GameManager.EnableSound;
        }
    }
}