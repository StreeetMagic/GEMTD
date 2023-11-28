using UnityEngine;
using UnityEngine.UI;

namespace Debug_HeadsUpDisplays
{
    public class DebugHeadsUpDisplay : MonoBehaviour
    {
        [SerializeField] private Button _button1;
        [SerializeField] private Button _button2;
        [SerializeField] private Button _button3;

        private void Start()
        {
            _button1.onClick.AddListener(() => { Debug.Log("Clicked1"); });
            _button2.onClick.AddListener(() => { Debug.Log("Clicked2"); });
            _button3.onClick.AddListener(() => { Debug.Log("Clicked3"); });
        }
    }
}