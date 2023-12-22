using UnityEngine;

namespace UserInterface.HeadsUpDisplays
{
    public class HeadsUpDisplayView : MonoBehaviour
    {
        public ChooseTowerPanelView ChooseTowerPanelView { get; private set; }
        public ThronePanelView ThronePanelView { get; private set; }

        private void Awake()
        {
            ChooseTowerPanelView = GetComponentInChildren<ChooseTowerPanelView>();
            ThronePanelView = GetComponentInChildren<ThronePanelView>();
        }
        
        private void Start()
        {
            ChooseTowerPanelView.gameObject.SetActive(false);
        }
    }
}