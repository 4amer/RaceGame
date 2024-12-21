using UI.Manager;
using UnityEngine;

namespace UI
{
    public class BaseWindow : MonoBehaviour
    {
        [SerializeField] private WindowsTypes _type = WindowsTypes.None;

        public WindowsTypes Type => _type;

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}