using Gameplay.Fields.Cells;
using Gameplay.Walls.WallPlacers;
using Games;
using InfastuctureCore.ServiceLocators;
using Infrastructure.Services.GameFactoryServices;
using Infrastructure.Services.InputServices;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameDesign
{
    public class ClickDetector : MonoBehaviour
    {
        public LevelDesignTools LevelDesignTools;
        public WallPlacerConfig WallPlacerConfig;

        public GameDesignMode GameDesignMode => LevelDesignTools.GameDesignMode;

        private Camera _camera;
        private CellView _highlightedCellView;
        private Material _highlightedMaterial;
        private Material _paintedMaterial;

        private IGameFactoryService GameFactoryService => ServiceLocator.Instance.Get<IGameFactoryService>();
        private IInputService InputService => ServiceLocator.Instance.Get<IInputService>();

        private void Awake()
        {
            _camera = Camera.main;
            _highlightedMaterial = Resources.Load<Material>(Constants.AssetsPath.Materials.Highlighted);
            _paintedMaterial = Resources.Load<Material>(Constants.AssetsPath.Materials.Painted);
        }

        private void Update()
        {
            if (UnHighlightCells())
                return;

            switch (GameDesignMode)
            {
                case GameDesignMode.StartingWallsPlacing:
                    PlaceWall(out CellModel _);
                    RemoveWall(out CellModel _);
                    break;

                case GameDesignMode.PaintingBlocks:
                    PaintBlock();
                    break;

                case GameDesignMode.HighlightingCells:
                    HighlightCellViewByCursor();
                    break;

                case GameDesignMode.WallPacersSetup:
                    PlaceWallForPlacer();
                    RemoveWallForPlacer();
                    break;
            }
        }

        private void PlaceWallForPlacer()
        {
            if (PlaceWall(out var cellData))
            {
                WallPlacerConfig.AddPlacedTower(cellData.CoordinatesValues);
            }
        }

        private void RemoveWallForPlacer()
        {
            if (RemoveWall(out var removedCellData))
            {
                WallPlacerConfig.RemovePlacedTower(removedCellData.CoordinatesValues);
            }
        }

        private void PaintBlock()
        {
            if (InputService.LeftMouseButtonIsPressed)
            {
                Ray ray = _camera.ScreenPointToRay(InputService.MousePosition);
                // ReSharper disable once Unity.PreferNonAllocApi
                RaycastHit[] results = Physics.RaycastAll(ray);

                foreach (RaycastHit hit in results)
                {
                    if (hit.transform.TryGetComponent(out CellView cellView))
                    {
                        cellView.PaintBlock(_paintedMaterial);
                        return;
                    }
                }
            }

            if (InputService.RightMouseButtonIsPressed)
            {
                Ray ray = _camera.ScreenPointToRay(InputService.MousePosition);
                // ReSharper disable once Unity.PreferNonAllocApi
                RaycastHit[] results = Physics.RaycastAll(ray);

                foreach (RaycastHit hit in results)
                {
                    if (hit.transform.TryGetComponent(out CellView cellView))
                    {
                        cellView.UnHighlight();
                        return;
                    }
                }
            }
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
            Ray ray = _camera.ScreenPointToRay(InputService.MousePosition);
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

        private bool PlaceWall(out CellModel cellModel)
        {
            cellModel = null;

            if (InputService.LeftMouseButtonWasPressedThisFrame)
            {
                Ray ray = _camera.ScreenPointToRay(InputService.MousePosition);
                // ReSharper disable once Unity.PreferNonAllocApi
                RaycastHit[] results = Physics.RaycastAll(ray);

                foreach (RaycastHit hit in results)
                {
                    if (hit.transform.TryGetComponent(out CellView cellView))
                    {
                        CellModel celLModel = cellView.CelLModel;

                        if (celLModel.IsEmpty)
                        {
                            celLModel.SetWallData(GameFactoryService.FieldFactory.CreateWallData());
                            cellModel = celLModel;
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool RemoveWall(out CellModel cellModel)
        {
            if (InputService.RightMouseButtonWasPressedThisFrame)
            {
                Ray ray = _camera.ScreenPointToRay(InputService.MousePosition);
                // ReSharper disable once Unity.PreferNonAllocApi
                RaycastHit[] results = Physics.RaycastAll(ray);

                foreach (RaycastHit hit in results)
                {
                    if (hit.transform.TryGetComponent(out CellView cellView))
                    {
                        CellModel celLModel = cellView.CelLModel;

                        if (celLModel.HasWall)
                        {
                            celLModel.RemoveWallData();
                            cellModel = celLModel;
                            return true;
                        }
                    }
                }
            }

            cellModel = null;
            return false;
        }
    }
}