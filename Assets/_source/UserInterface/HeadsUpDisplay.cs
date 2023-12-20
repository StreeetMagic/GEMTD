using UnityEngine;

namespace UserInterface
{
    public class HeadsUpDisplay : MonoBehaviour
    {
        [field: SerializeField] public ChooseTowerPanel ChooseTowerPanel { get; private set; }
        [field: SerializeField] public ThronePanel ThronePanel { get; private set; }

        private void Start()
        {
            ChooseTowerPanel.gameObject.SetActive(false);
        }
    }
}