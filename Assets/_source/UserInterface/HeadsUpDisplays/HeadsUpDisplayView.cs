using UnityEngine;

namespace UserInterface.HeadsUpDisplays
{
    public class HeadsUpDisplayView : MonoBehaviour
    {
        [field: SerializeField] public ChooseTowerPanelView ChooseTowerPanelView { get; private set; }
        [field: SerializeField] public ThronePanelView ThronePanelView { get; private set; }

        private void Start()
        {
            ChooseTowerPanelView.gameObject.SetActive(false);
        }
    }
}