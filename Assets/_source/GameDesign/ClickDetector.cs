using System;
using Gameplay.Fields.Cells;
using Gameplay.Fields.Walls.WallPlacers;
using Games;
using Infrastructure.Services.Inputs;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Zenject;

namespace GameDesign
{
  public class ClickDetector : MonoBehaviour
  {
    private Camera _camera;
    private CellView _highlightedCellView;
    private Material _highlightedMaterial;

    private IInputService _inputService;
    public WallPlacerConfig WallPlacerConfig;

    [Inject]
    public void Construct(IInputService inputService)
    {
      _inputService = inputService;

      if (_inputService == null)
        throw new NullReferenceException(nameof(_inputService));
    }

    private void Awake()
    {
      _camera = Camera.main;
      _highlightedMaterial = Resources.Load<Material>(Constants.AssetsPath.Materials.Highlighted);
      Resources.Load<Material>(Constants.AssetsPath.Materials.Painted);
    }

    private void Update()
    {
      if (Keyboard.current.spaceKey.wasPressedThisFrame)
      {
        Time.timeScale = Time.timeScale == 0
          ? 1
          : 0;
      }

      if (UnHighlightCells())
        return;

      HighlightCellViewByCursor();
    }

    private bool UnHighlightCells()
    {
      if (EventSystem.current.IsPointerOverGameObject())
      {
        if (_highlightedCellView != null)
          _highlightedCellView.UnHighlight();

        return true;
      }

      return false;
    }

    private void HighlightCellViewByCursor()
    {
      Ray ray = _camera.ScreenPointToRay(_inputService.MousePosition);
      // ReSharper disable once Unity.PreferNonAllocApi
      RaycastHit[] results = Physics.RaycastAll(ray);

      foreach (RaycastHit hit in results)
      {
        if (hit.transform.TryGetComponent(out CellView cellView) == false)
          continue;

        if (_highlightedCellView == cellView)
          return;

        if (_highlightedCellView != null)
          _highlightedCellView.UnHighlight();

        _highlightedCellView = cellView;
        _highlightedCellView.Highlight(_highlightedMaterial);

        return;
      }
    }
  }
}