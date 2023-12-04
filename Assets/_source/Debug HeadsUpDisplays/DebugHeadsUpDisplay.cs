using InfastuctureCore.ServiceLocators;
using InfastuctureCore.Services.StateMachineServices;
using Infrastructure.GameLoopStateMachines;
using Infrastructure.GameLoopStateMachines.States;
using UnityEngine;
using UnityEngine.UI;

namespace Debug_HeadsUpDisplays
{
    public class DebugHeadsUpDisplay : MonoBehaviour
    {
        [SerializeField] private Button _finishPlacingWalls;
        [SerializeField] private Button _button2;
        [SerializeField] private Button _button3;

        private void Start()
        {
            _finishPlacingWalls.onClick.AddListener(GameLoopStateMachine.Get<PlaceWallsState>().FinishPlacingTowers);
        }

        private IStateMachineService<GameLoopStateMachineData> GameLoopStateMachine => ServiceLocator.Instance.Get<IStateMachineService<GameLoopStateMachineData>>();
    }
}