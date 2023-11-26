namespace Games
{
    public class Constants
    {
        public class Scenes
        {
            public const string InitialScene = "00_Initial";
            public const string MainMenuScene = "01_MainMenu";
            public const string Gameloop = "02_Gameloop";
        }

        public class AssetsPath
        {
            public class Prefabs
            {
                public const string BlockGrid = "BlockGrid";
                public const string Cell = "Cell";
                public const string Block = "Block";
                public const string Checkpoint = "Checkpoint";
                public const string Wall = "Wall";
            }

            public class Configs
            {
                public const string GameConfig = "GameConfigSO";
                public const string BlockGridConfig = "BlockGridConfigSO";
                public const string CheckpointsConfig = "CheckpointsConfigSO";
                public const string MapWallsConfig = "MapWallsConfigSO";
                public const string StartingLabyrinthConfig = "StartingLabyrinthConfigSO";
            }

            public class Materials
            {
                public const string Highlighted = "Highlighted";
            }
        }
    }
}