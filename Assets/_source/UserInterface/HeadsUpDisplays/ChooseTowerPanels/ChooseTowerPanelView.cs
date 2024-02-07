using System.Collections.Generic;
using System.Linq;
using Gameplay.Fields.Cells;
using Gameplay.Fields.Towers;
using Infrastructure;
using Infrastructure.Services.CurrentDatas;
using Infrastructure.Services.StateMachines;
using Infrastructure.Services.StateMachines.GameLoopStateMachines;
using Infrastructure.Services.StateMachines.GameLoopStateMachines.States;
using Infrastructure.Services.StaticDataServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UserInterface.HeadsUpDisplays.ChooseTowerPanels.ButtonPanels;
using Zenject;

namespace UserInterface.HeadsUpDisplays.ChooseTowerPanels
{
  public class ChooseTowerPanelView : MonoBehaviour
  {
    public List<Button> DowngradeButtons { get; private set; }
    public List<Button> PlacedTowerButtons { get; private set; }
    public List<Button> SingleMergeTowerButtons { get; private set; }
    public List<Button> DoubleMergeTowerButtons { get; private set; }

    private IStateMachine<IGameLoopState> _gameLoopStateMachine;
    private ICurrentDataService _currentDataService;
    private IStaticDataService _staticDataService;

    [Inject]
    public void Construct(IStateMachine<IGameLoopState> gameLoopStateMachine, ICurrentDataService currentDataService, IStaticDataService staticDataService)
    {
      _gameLoopStateMachine = gameLoopStateMachine;
      _currentDataService = currentDataService;
      _staticDataService = staticDataService;
    }

    private void Awake()
    {
      DowngradeButtons =
        GetComponentInChildren<DowngradeButtonsView>()
          .GetComponentsInChildren<Button>()
          .ToList();

      PlacedTowerButtons =
        GetComponentInChildren<PlacedButtonsView>()
          .GetComponentsInChildren<Button>()
          .ToList();

      SingleMergeTowerButtons =
        GetComponentInChildren<SingleMergeButtonsView>()
          .GetComponentsInChildren<Button>()
          .ToList();

      DoubleMergeTowerButtons =
        GetComponentInChildren<DoubleMergeButtonsView>()
          .GetComponentsInChildren<Button>()
          .ToList();
    }

    public void OnChooseTowerStateEntered(List<Vector2Int> wallsCoordinates)
    {
      DisableMergeButtons();
      gameObject.SetActive(true);

      for (int i = 0; i < PlacedTowerButtons.Count; i++)
        InitButton(PlacedTowerButtons[i], _currentDataService.FieldModel.CellsContainerModel.GetCellModelByCoordinates(wallsCoordinates[i]));

      CheckTowers(wallsCoordinates);
    }

    private void CheckTowers(List<Vector2Int> wallsCoordinates)
    {
      GetCellModels(wallsCoordinates, out CellModel[] cells, out Dictionary<TowerType, int> towerTypes);

      CheckDowngrades(wallsCoordinates, towerTypes);

      if (CheckSimilar(wallsCoordinates, towerTypes))
        return;

      CheckMerges(towerTypes, cells);
    }

    private void CheckMerges(Dictionary<TowerType, int> towerTypes, CellModel[] cells)
    {
      foreach (KeyValuePair<TowerType, int> pair in towerTypes)
      {
        if (pair.Value > 1)
        {
          for (int i = 0; i < cells.Length; i++)
          {
            if (cells[i].TowerModel.Type != pair.Key)
              continue;

            if (_staticDataService.TowersConfig.GetTowerValues(pair.Key).SingleMergeType == TowerType.None)
              continue;

            Button singleMergeTowerButton = SingleMergeTowerButtons[i];
            singleMergeTowerButton.gameObject.SetActive(true);
            singleMergeTowerButton.GetComponentInChildren<TextMeshProUGUI>().text = _staticDataService.TowersConfig.GetTowerValues(pair.Key).SingleMergeType.ToString();
            singleMergeTowerButton.onClick.RemoveAllListeners();

            singleMergeTowerButton.onClick.AddListener(() =>
            {
              cells[i].Upgrade(_staticDataService.TowersConfig.GetTowerValues(pair.Key).SingleMergeType, cells[i].TowerModel.Level + 1);
              _gameLoopStateMachine.Get<ChooseTowerState>().ConfirmTower(cells[i], () => _gameLoopStateMachine.Enter<EnemyMoveState>());
              gameObject.SetActive(false);
            });
          }
        }

        if (pair.Value <= 3)
          continue;

        for (int i = 0; i < cells.Length; i++)
        {
          if (cells[i].TowerModel.Type != pair.Key)
            continue;

          if (_staticDataService.TowersConfig.GetTowerValues(cells[i].TowerModel.Type).DoubleMergeType == TowerType.None)
            continue;

          Button doubleMergeTowerButton = DoubleMergeTowerButtons[i];
          doubleMergeTowerButton.gameObject.SetActive(true);
          doubleMergeTowerButton.GetComponentInChildren<TextMeshProUGUI>().text = _staticDataService.TowersConfig.GetTowerValues(cells[i].TowerModel.Type).DoubleMergeType.ToString();
          doubleMergeTowerButton.onClick.RemoveAllListeners();

          doubleMergeTowerButton.onClick.AddListener(() =>
          {
            cells[i].Upgrade(_staticDataService.TowersConfig.GetTowerValues(cells[i].TowerModel.Type).DoubleMergeType, cells[i].TowerModel.Level + 2);
            _gameLoopStateMachine.Get<ChooseTowerState>().ConfirmTower(cells[i], () => _gameLoopStateMachine.Enter<EnemyMoveState>());
            gameObject.SetActive(false);
          });
        }
      }
    }

    private void CheckDowngrades(List<Vector2Int> wallsCoordinates, Dictionary<TowerType, int> towerTypes)
    {
      //throw new NotImplementedException();
    }

    private bool CheckSimilar(List<Vector2Int> wallsCoordinates, Dictionary<TowerType, int> towerTypes) =>
      towerTypes.Count == wallsCoordinates.Count;

    private void GetCellModels(List<Vector2Int> wallsCoordinates, out CellModel[] cells, out Dictionary<TowerType, int> towerTypes)
    {
      cells = new CellModel[wallsCoordinates.Count];
      towerTypes = new Dictionary<TowerType, int>();

      for (int i = 0; i < wallsCoordinates.Count; i++)
      {
        Vector2Int vector = wallsCoordinates[i];
        cells[i] = _currentDataService.FieldModel.CellsContainerModel.GetCellModelByCoordinates(vector);

        TowerType towerType = cells[i].TowerModel.Type;

        if (towerTypes.ContainsKey(towerType))
        {
          towerTypes[towerType]++;
        }
        else
        {
          towerTypes.Add(towerType, 1);
        }
      }
    }

    private void InitButton(Button towerButton, CellModel cellModel)
    {
      towerButton.GetComponentInChildren<TextMeshProUGUI>().text = cellModel.TowerModel.Type.ToString();

      towerButton.onClick.RemoveAllListeners();
      towerButton.onClick.AddListener(() =>
      {
        _gameLoopStateMachine.Get<ChooseTowerState>().ConfirmTower(cellModel, () => _gameLoopStateMachine.Enter<EnemyMoveState>());
        gameObject.SetActive(false);
      });
    }

    private void DisableMergeButtons()
    {
      for (int i = 0; i < SingleMergeTowerButtons.Count; i++)
      {
        DowngradeButtons[i].gameObject.SetActive(false);
        SingleMergeTowerButtons[i].gameObject.SetActive(false);
        DoubleMergeTowerButtons[i].gameObject.SetActive(false);
      }
    }
  }
}