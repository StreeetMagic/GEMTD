using Gameplay.Fields.Cells;
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
        private Camera _camera;
        private CellView _highlightedCellView;
        private Material _highlightedMaterial;

        private IGameFactoryService GameFactoryService => ServiceLocator.Instance.Get<IGameFactoryService>();
        private IInputService InputService => ServiceLocator.Instance.Get<IInputService>();

        private void Awake()
        {
            _camera = Camera.main;
            _highlightedMaterial = Resources.Load<Material>(Constants.AssetsPath.Materials.Highlighted);
        }

        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                if (_highlightedCellView != null)
                    _highlightedCellView.UnHighlight();

                return;
            }

            HighlightCellViewByCursor();
            ClickOnCell();
        }

        private void HighlightCellViewByCursor()
        {
            // Ray ray = _camera.ScreenPointToRay(InputService.MousePosition);
            // RaycastHit[] results = Physics.RaycastAll(ray);
            //
            // foreach (RaycastHit hit in results)
            // {
            //     if (hit.transform.TryGetComponent(out CellView cellView) == false)
            //         continue;
            //
            //     if (_highlightedCellView == cellView)
            //         return;
            //
            //     if (_highlightedCellView != null)
            //         _highlightedCellView.UnHighlight();
            //
            //     _highlightedCellView = cellView;
            //     _highlightedCellView.Highlight(_highlightedMaterial);
            //
            //     return;
            // }
        }

        private void ClickOnCell()
        {
            // if (InputService.LeftMouseButtonDown)
            // {
            //     Ray ray = _camera.ScreenPointToRay(InputService.MousePosition);
            //     RaycastHit[] results = Physics.RaycastAll(ray);
            //
            //     foreach (RaycastHit hit in results)
            //     {
            //         if (hit.transform.TryGetComponent(out CellView cellView))
            //         {
            //             CellData celLData = cellView.CelLData;
            //
            //             if (celLData.IsEmpty)
            //             {
            //                 celLData.SetWallData(GameFactoryService.BlockGridFactory.CreateWall(celLData));
            //                 return;
            //             }
            //
            //             return;
            //         }
            //     }
        }

        // if (InputService.RightMouseButtonDown) 
        // {
        //     Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        //     RaycastHit[] results = Physics.RaycastAll(ray);
        //
        //     foreach (RaycastHit hit in results)
        //     {
        //         if (hit.transform.TryGetComponent(out CellView cellView))
        //         {
        //             CellData celLData = cellView.CelLData;
        //
        //             if (celLData.HasWall)
        //             {
        //                 celLData.RemoveWallData();
        //                 return;
        //             }
        //
        //             return;
        //         }
        //     }
        // }
    }
}