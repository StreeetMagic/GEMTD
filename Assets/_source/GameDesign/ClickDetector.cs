using Gameplay.Fields.Cells;
using Games;
using InfastuctureCore.ServiceLocators;
using Infrastructure.Services.GameFactoryServices;
using Infrastructure.Services.InputServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace GameDesign
{
    public class ClickDetector : MonoBehaviour
    {
        public LevelDesignTools LevelDesignTools;
        
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
                case GameDesignMode.WallsPlacing:
                    PlaceWall();
                    break;

                case GameDesignMode.PaintingBlocks:
                    PaintBlock();
                    break;

                case GameDesignMode.HighlightingCells:
                    HighlightCellViewByCursor();
                    break;
            }
        }

        private void PaintBlock()
        {
            if (InputService.LeftMouseButtonIsPressed)
            {
                Ray ray = _camera.ScreenPointToRay(InputService.MousePosition);
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

        private void PlaceWall()
        {
            if (InputService.LeftMouseButtonIsPressed)
            {
                Ray ray = _camera.ScreenPointToRay(InputService.MousePosition);
                RaycastHit[] results = Physics.RaycastAll(ray);

                foreach (RaycastHit hit in results)
                {
                    if (hit.transform.TryGetComponent(out CellView cellView))
                    {
                        CellData celLData = cellView.CelLData;

                        if (celLData.IsEmpty)
                        {
                            celLData.SetWallData(GameFactoryService.BlockGridFactory.CreateWall(celLData));
                            return;
                        }

                        return;
                    }
                }
            }

            if (InputService.RightMouseButtonIsPressed)
            {
                Ray ray = _camera.ScreenPointToRay(InputService.MousePosition);
                RaycastHit[] results = Physics.RaycastAll(ray);

                foreach (RaycastHit hit in results)
                {
                    if (hit.transform.TryGetComponent(out CellView cellView))
                    {
                        CellData celLData = cellView.CelLData;

                        if (celLData.HasWall)
                        {
                            celLData.RemoveWallData();
                            return;
                        }

                        return;
                    }
                }
            }
        }
    }
}