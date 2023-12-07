namespace Games
{
    public class Constants
    {
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
                public const string Field = nameof(Field);
                public const string Cell = nameof(Cell);
                public const string Block = nameof(Block);
                public const string Checkpoint = nameof(Checkpoint);
                public const string Wall = nameof(Wall);
                public const string Tower = nameof(Tower);
                public const string Projectile = nameof(Projectile);
            }

            //TODO сделать через ассет провайдер
            public class Materials
            {
                public const string Highlighted = nameof(Highlighted);
                public const string Painted = nameof(Painted);
            }
        }
    }
}