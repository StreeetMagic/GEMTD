namespace Games
{
  public class Constants
  {
    public class Ids
    {
      public const string InitialSceneName = nameof(InitialSceneName);
    }

    public class Scenes
    {
      public const string InitialScene = "00_Initial";
      public const string MainMenuScene = "01_MainMenu";
      public const string Gameloop = "02_Gameloop";
      public const string Prototype = "99_Prototype";
    }

    public class AssetsPath
    {
      public class Prefabs
      {
        public const string CoroutineRunner = nameof(CoroutineRunner);
        public const string Field = nameof(Field);
        public const string Cell = nameof(Cell);
        public const string Block = nameof(Block);
        public const string Checkpoint = nameof(Checkpoint);
        public const string Wall = nameof(Wall);
        public const string Tower = nameof(Tower);
        public const string Projectile = nameof(Projectile);
        public const string Enemy = nameof(Enemy);
        public const string HeadsUpDisplay = nameof(HeadsUpDisplay);
        public const string Throne = nameof(Throne);
      }

      //TODO сделать через ассет провайдер
      public class Materials
      {
        public const string Highlighted = nameof(Highlighted);
        public const string Painted = nameof(Painted);
        public const string BMaterial = nameof(BMaterial);
        public const string DMaterial = nameof(DMaterial);
        public const string EMaterial = nameof(EMaterial);
        public const string GMaterial = nameof(GMaterial);
        public const string PMaterial = nameof(PMaterial);
        public const string QMaterial = nameof(QMaterial);
        public const string RMaterial = nameof(RMaterial);
        public const string YMaterial = nameof(YMaterial);
      }
    }
  }
}