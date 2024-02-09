using Gameplay.Fields.Towers.Shooters;
using Gameplay.Fields.Towers.TargetDetectors;
using UnityEngine;

namespace Gameplay.Fields.Towers
{
  public class TowerView : MonoBehaviour
  {
    [SerializeField] private Transform _meshModel;

    private MeshRenderer _meshRenderer;

    private SingleProjectileShooterView _shooterView;
    private TargetDetetcorView _targetDetetcorView;
    public TowerModel TowerModel { get; private set; }
    public Material Material { get; private set; }

    public void Init(TowerModel towerModel, Material material)
    {
      TowerModel = towerModel;
      Material = material;

      _meshRenderer = GetComponentInChildren<MeshRenderer>();
      _meshRenderer.material = Material;

      _shooterView.Init(TowerModel.Shooter);
      _targetDetetcorView.Init(TowerModel.TargetDetetcor);
    }

    public void Awake()
    {
      _shooterView = GetComponentInChildren<SingleProjectileShooterView>();
      _targetDetetcorView = GetComponentInChildren<TargetDetetcorView>();
    }

    public void SetScale(Vector3 scale)
    {
      _meshModel.transform.localScale = scale;
    }
  }
}