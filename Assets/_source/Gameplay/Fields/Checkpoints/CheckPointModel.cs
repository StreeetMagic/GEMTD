using Gameplay.Fields.Cells;

namespace Gameplay.Fields.Checkpoints
{
  public class CheckPointModel
  {
    public CheckPointModel(int number, CellModel cellModel)
    {
      Number = number;
      CellModel = cellModel;
    }

    public int Number { get; }
    public CellModel CellModel { get; }
  }
}