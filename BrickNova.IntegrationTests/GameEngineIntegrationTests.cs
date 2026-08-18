using BrickNova.Database;
using BrickNova.Entities;
using BrickNova.Game;
using BrickNova.Models;
using System.Drawing;
using System.Windows.Forms;

namespace BrickNova.IntegrationTests;

public class GameEngineIntegrationTests
    : TestEnvironmentSetup
{
    [Fact]
    public void Constructor_ShouldInitializeGameEngine()
    {
        GameEngine gameEngine =
            new GameEngine(
                DatabaseManager
            );

        Assert.NotNull(gameEngine);
    }

    [Fact]
    public void Constructor_ShouldInitializeDefaultGameState()
    {
        GameEngine gameEngine =
            new GameEngine(
                DatabaseManager
            );

        Assert.Equal(
            GameState.Menu,
            gameEngine.CurrentState
        );
    }

    [Fact]
    public void Constructor_ShouldInitializeDefaultLives()
    {
        GameEngine gameEngine =
            new GameEngine(
                DatabaseManager
            );

        Assert.Equal(
            3,
            gameEngine.Lives
        );
    }

    [Fact]
    public void Constructor_ShouldInitializeDefaultScore()
    {
        GameEngine gameEngine =
            new GameEngine(
                DatabaseManager
            );

        Assert.Equal(
            0,
            gameEngine.Score
        );
    }

    [Fact]
    public void Constructor_ShouldInitializeFirstLevel()
    {
        GameEngine gameEngine =
            new GameEngine(
                DatabaseManager
            );

        Assert.Equal(
            1,
            gameEngine.CurrentLevel
        );
    }

    [Fact]
    public void Constructor_ShouldInitializeBall()
    {
        GameEngine gameEngine =
            new GameEngine(
                DatabaseManager
            );

        Assert.NotNull(
            gameEngine.Ball
        );
    }

    [Fact]
    public void Constructor_ShouldInitializePaddle()
    {
        GameEngine gameEngine =
            new GameEngine(
                DatabaseManager
            );

        Assert.NotNull(
            gameEngine.Paddle
        );
    }

    [Fact]
    public void Constructor_ShouldInitializeBricks()
    {
        GameEngine gameEngine =
            new GameEngine(
                DatabaseManager
            );

        Assert.NotNull(
            gameEngine.Bricks
        );
    }

    [Fact]
    public void Constructor_ShouldInitializeAudioManager()
    {
        GameEngine gameEngine =
            new GameEngine(
                DatabaseManager
            );

        Assert.NotNull(
            gameEngine.AudioManager
        );
    }

    [Fact]
    public void StartGameFlow_ShouldTransitionFromMenuToPlaying()
    {
        GameEngine gameEngine =
            new GameEngine(
                DatabaseManager
            );

        Assert.Equal(
            GameState.Menu,
            gameEngine.CurrentState
        );

        gameEngine.HandleKeyDown(
            Keys.N
        );

        gameEngine.UpdateCycle();

        Assert.Equal(
            GameState.Playing,
            gameEngine.CurrentState
        );
    }

    [Fact]
    public void StartGameFlow_ShouldInitializeNewGameCorrectly()
    {
        GameEngine gameEngine =
            new GameEngine(
                DatabaseManager
            );

        Assert.Equal(
            GameState.Menu,
            gameEngine.CurrentState
        );

        gameEngine.HandleKeyDown(
            Keys.N
        );

        gameEngine.UpdateCycle();

        Assert.Equal(
            GameState.Playing,
            gameEngine.CurrentState
        );

        Assert.Equal(
            1,
            gameEngine.CurrentLevel
        );

        Assert.Equal(
            3,
            gameEngine.Lives
        );

        Assert.Equal(
            0,
            gameEngine.Score
        );

        Assert.NotNull(
            gameEngine.Ball
        );

        Assert.NotNull(
            gameEngine.Paddle
        );

        Assert.NotNull(
            gameEngine.Bricks
        );
    }

    [Fact]
    public void NewGameFlow_ShouldStartNewGame()
    {
        GameEngine gameEngine =
            new GameEngine(
                DatabaseManager
            );

        Assert.Equal(
            GameState.Menu,
            gameEngine.CurrentState
        );

        gameEngine.HandleKeyDown(
            Keys.N
        );

        gameEngine.UpdateCycle();

        Assert.Equal(
            GameState.Playing,
            gameEngine.CurrentState
        );

        Assert.Equal(
            1,
            gameEngine.CurrentLevel
        );

        Assert.Equal(
            3,
            gameEngine.Lives
        );

        Assert.Equal(
            0,
            gameEngine.Score
        );

        Assert.Equal(
            new PointF(403, 297),
            gameEngine.Ball.Position
        );

        Assert.Equal(
            new PointF(350, 540),
            gameEngine.Paddle.Position
        );

        Assert.NotEmpty(
            gameEngine.Bricks
        );
    }

    [Fact]
    public void PaddleMovementFlow_ShouldMovePaddleRight()
    {
        GameEngine gameEngine =
            new GameEngine(
                DatabaseManager
            );

        gameEngine.HandleKeyDown(
            Keys.N
        );

        gameEngine.UpdateCycle();

        Assert.Equal(
            GameState.Playing,
            gameEngine.CurrentState
        );

        Point initialPosition =
            gameEngine.Paddle.Position;

        gameEngine.HandleKeyDown(
            Keys.Right
        );

        gameEngine.UpdateCycle();

        gameEngine.HandleKeyDown(
            Keys.Right
        );

        Assert.True(
            gameEngine.Paddle.Position.X >
            initialPosition.X
        );

        Assert.Equal(
            initialPosition.Y,
            gameEngine.Paddle.Position.Y
        );
    }

    [Fact]
    public void PaddleMovementFlow_ShouldMovePaddleLeft()
    {
        GameEngine gameEngine =
            new GameEngine(
                DatabaseManager
            );

        gameEngine.HandleKeyDown(
            Keys.N
        );

        gameEngine.UpdateCycle();

        gameEngine.HandleKeyDown(
            Keys.Right
        );

        gameEngine.UpdateCycle();

        Point positionAfterRight = 
            gameEngine.Paddle.Position;

        gameEngine.HandleKeyUp(
           Keys.Right
       );

        gameEngine.HandleKeyDown(
           Keys.Left
        );
        
        gameEngine.UpdateCycle();

        Assert.True(
           gameEngine.Paddle.Position.X <
           positionAfterRight.X
        );
    }

    [Fact]
    public void BallMovementFlow_ShouldMoveBallAccordingToVelocity()
    {
        GameEngine gameEngine =
            new GameEngine(
                DatabaseManager
            );

        gameEngine.HandleKeyDown(
            Keys.N
        );

        gameEngine.UpdateCycle();

        PointF initialPosition =
            gameEngine.Ball.Position;

        gameEngine.UpdateCycle();

        PointF currentPosition =
            gameEngine.Ball.Position;

        Assert.Equal(
            initialPosition.X + 3,
            currentPosition.X
        );

        Assert.Equal(
            initialPosition.Y - 3,
            currentPosition.Y
        );
    }

    [Fact]
    public void CollisionIntegration_ShouldHandlePaddleCollision()
    {
        GameEngine gameEngine =
            new GameEngine(
                DatabaseManager
            );

        gameEngine.HandleKeyDown(
            Keys.N
        );

        gameEngine.UpdateCycle();

        Assert.Equal(
            GameState.Playing,
            gameEngine.CurrentState
        );

        gameEngine.Ball.Position =
            new PointF(
                gameEngine.Paddle.Position.X + 40,
                gameEngine.Paddle.Position.Y - gameEngine.Ball.Size.Height
            );

        gameEngine.Ball.Velocity =
            new PointF(
                0,
                3
            );

        PointF initialVelocity =
            gameEngine.Ball.Velocity;

        gameEngine.UpdateCycle();

        Assert.True(
            gameEngine.Ball.Velocity.Y < 0
        );

        Assert.True(
            gameEngine.Ball.Velocity.Y !=
            initialVelocity.Y
        );
    }

    [Fact]
    public void CollisionIntegration_ShouldDestroyBrickAndIncreaseScore()
    {
        GameEngine gameEngine =
            new GameEngine(
                DatabaseManager
            );

        gameEngine.HandleKeyDown(
            Keys.N
        );

        gameEngine.UpdateCycle();

        Assert.Equal(
            GameState.Playing,
            gameEngine.CurrentState
        );

        Brick? brick =
            gameEngine.Bricks
                .FirstOrDefault(
                    brick => !brick.IsDestroyed
                );

        Assert.NotNull(brick);

        gameEngine.Ball.Position =
            new PointF(
                brick.Position.X,
                brick.Position.Y
            );

        gameEngine.Ball.Velocity =
            new PointF(
                0,
                3
            );

        int initialScore =
            gameEngine.Score;

        gameEngine.UpdateCycle();

        Assert.True(
            brick.IsDestroyed
        );

        Assert.True(
            gameEngine.Score >
            initialScore
        );
    }

    [Fact]
    public void BrickDestructionFlow_ShouldDestroyBrickAndIncreaseScore()
    {
        GameEngine gameEngine =
            new GameEngine(
                DatabaseManager
            );

        gameEngine.HandleKeyDown(
            Keys.N
        );

        gameEngine.UpdateCycle();

        Assert.Equal(
            GameState.Playing,
            gameEngine.CurrentState
        );

        Brick? brick =
            gameEngine.Bricks
                .FirstOrDefault(
                    brick => !brick.IsDestroyed
                );

        Assert.NotNull(brick);

        int initialScore =
            gameEngine.Score;

        gameEngine.Ball.Position =
            new PointF(
                brick.Position.X +
                brick.Size.Width / 2f -
                gameEngine.Ball.Size.Width / 2f,

                brick.Position.Y +
                brick.Size.Height / 2f -
                gameEngine.Ball.Size.Height / 2f
            );

        gameEngine.Ball.Velocity =
            new PointF(
                0,
                3
            );

        gameEngine.UpdateCycle();

        Assert.True(
            brick.IsDestroyed
        );

        Assert.True(
            gameEngine.Score >
            initialScore
        );
    }

    [Fact]
    public void ScoreUpdateFlow_ShouldIncreaseScoreAfterBrickDestruction()
    {
        GameEngine gameEngine =
            new GameEngine(
                DatabaseManager
            );

        gameEngine.HandleKeyDown(
            Keys.N
        );

        gameEngine.UpdateCycle();

        Assert.Equal(
            GameState.Playing,
            gameEngine.CurrentState
        );

        Brick? brick =
            gameEngine.Bricks
                .FirstOrDefault(
                    brick => !brick.IsDestroyed
                );

        Assert.NotNull(brick);

        int initialScore =
            gameEngine.Score;

        int expectedScore =
            initialScore + brick.Points;

        gameEngine.Ball.Position =
            new PointF(
                brick.Position.X +
                brick.Size.Width / 2f -
                gameEngine.Ball.Size.Width / 2f,

                brick.Position.Y +
                brick.Size.Height / 2f -
                gameEngine.Ball.Size.Height / 2f
            );

        gameEngine.Ball.Velocity =
            new PointF(
                0,
                3
            );

        gameEngine.UpdateCycle();

        Assert.True(
            brick.IsDestroyed
        );

        Assert.Equal(
            expectedScore,
            gameEngine.Score
        );
    }

    [Fact]
    public void LifeLossFlow_ShouldDecreaseLivesAndContinueGame()
    {
        GameEngine gameEngine =
            new GameEngine(
                DatabaseManager
            );

        gameEngine.HandleKeyDown(
            Keys.N
        );

        gameEngine.UpdateCycle();

        Assert.Equal(
            GameState.Playing,
            gameEngine.CurrentState
        );

        int initialLives =
            gameEngine.Lives;

        gameEngine.Ball.Position =
            new PointF(
                gameEngine.Ball.Position.X,
                600
            );

        gameEngine.Ball.Velocity =
            new PointF(
                0,
                3
            );

        gameEngine.UpdateCycle();

        Assert.Equal(
            initialLives - 1,
            gameEngine.Lives
        );

        Assert.Equal(
            GameState.Playing,
            gameEngine.CurrentState
        );

        Assert.Equal(
            new PointF(400, 300),
            gameEngine.Ball.Position
        );

        Assert.Equal(
            new Point(350, 540),
            gameEngine.Paddle.Position
        );
    }

    [Fact]
    public void RestartFlow_ShouldResetGameAndReturnToPlaying()
    {
        GameEngine gameEngine =
            new GameEngine(
                DatabaseManager
            );

        gameEngine.HandleKeyDown(
            Keys.N
        );

        gameEngine.UpdateCycle();

        Assert.Equal(
            GameState.Playing,
            gameEngine.CurrentState
        );

        gameEngine.Ball.Position =
            new PointF(
                500,
                400
            );

        gameEngine.HandleKeyDown(
            Keys.Right
        );

        gameEngine.UpdateCycle();

        gameEngine.HandleKeyUp(
            Keys.Right
        );

        gameEngine.HandleKeyDown(
            Keys.Escape
        );

        gameEngine.UpdateCycle();

        Assert.Equal(
            GameState.Paused,
            gameEngine.CurrentState
        );

        gameEngine.HandleKeyDown(
            Keys.R
        );

        gameEngine.UpdateCycle();

        Assert.Equal(
            GameState.Playing,
            gameEngine.CurrentState
        );

        Assert.Equal(
            3,
            gameEngine.Lives
        );

        Assert.Equal(
            new PointF(403, 297),
            gameEngine.Ball.Position
        );

        Assert.Equal(
            new Point(350, 540),
            gameEngine.Paddle.Position
        );
    }

    [Fact]
    public void PauseFlow_ShouldPauseAndResumeGame()
    {
        GameEngine gameEngine =
            new GameEngine(
                DatabaseManager
            );

        gameEngine.HandleKeyDown(
            Keys.N
        );

        gameEngine.UpdateCycle();

        Assert.Equal(
            GameState.Playing,
            gameEngine.CurrentState
        );

        gameEngine.HandleKeyDown(
            Keys.Escape
        );

        gameEngine.UpdateCycle();

        Assert.Equal(
            GameState.Paused,
            gameEngine.CurrentState
        );

        gameEngine.HandleKeyUp(
            Keys.Escape
        );

        gameEngine.HandleKeyDown(
            Keys.Escape
        );

        gameEngine.UpdateCycle();

        Assert.Equal(
            GameState.Playing,
            gameEngine.CurrentState
        );
    }

    [Fact]
    public void GameOverIntegration_ShouldEnterGameOverAfterFinalLife()
    {
        GameEngine gameEngine =
            new GameEngine(
                DatabaseManager
            );

        bool playerNameRequested = false;

        gameEngine.PlayerNameRequested += () =>
        {
            playerNameRequested = true;
        };

        gameEngine.HandleKeyDown(
            Keys.N
        );

        gameEngine.UpdateCycle();

        Assert.Equal(
            GameState.Playing,
            gameEngine.CurrentState
        );

        Assert.Equal(
            3,
            gameEngine.Lives
        );

        for (int i = 0; i < 3; i++)
        {
            gameEngine.Ball.Position =
                new PointF(
                    gameEngine.Ball.Position.X,
                    600
                );

            gameEngine.Ball.Velocity =
                new PointF(
                    0,
                    3
                );

            gameEngine.UpdateCycle();

            if (i < 2)
            {
                Assert.Equal(
                    GameState.Playing,
                    gameEngine.CurrentState
                );

                Assert.Equal(
                    2 - i,
                    gameEngine.Lives
                );
            }
        }

        Assert.Equal(
            0,
            gameEngine.Lives
        );

        Assert.Equal(
            GameState.GameOver,
            gameEngine.CurrentState
        );

        Assert.True(
            playerNameRequested
        );
    }

    [Fact]
    public void GameOverScorePersistence_ShouldSavePlayerScore()
    {
        GameEngine gameEngine =
           new GameEngine(
               DatabaseManager
           );

        gameEngine.HandleKeyDown(
            Keys.N
        );

        gameEngine.UpdateCycle();

        Assert.Equal(
            GameState.Playing,
            gameEngine.CurrentState
        );

        for (int i = 0; i < 3; i++)
        {
            gameEngine.Ball.Position =
                new PointF(
                    gameEngine.Ball.Position.X,
                    600
                );

            gameEngine.Ball.Velocity =
                new PointF(
                    0,
                    3
                );

            gameEngine.UpdateCycle();
        }

        Assert.Equal(
            GameState.GameOver,
            gameEngine.CurrentState
        );

        const string playerName =
            "IntegrationTestPlayer";

        gameEngine.SavePlayerScore(
            playerName
        );

        List<ScoreRecord> highScores =
            gameEngine.GetHighScores();

        ScoreRecord? savedScore =
            highScores.FirstOrDefault(
                score =>
                    score.PlayerName == playerName
            );

        Assert.NotNull(
            savedScore
        );

        Assert.Equal(
            gameEngine.Score,
            savedScore.Score
        );

        Assert.Equal(
            gameEngine.CurrentLevel,
            savedScore.Level
        );
    }

    [Fact]
    public void PlayerScoreSaveFlow_ShouldPersistPlayerScore()
    {
        GameEngine gameEngine =
           new GameEngine(
               DatabaseManager
           );

        gameEngine.HandleKeyDown(
            Keys.N
        );

        gameEngine.UpdateCycle();

        Assert.Equal(
            GameState.Playing,
            gameEngine.CurrentState
        );

        const string playerName =
            "PlayerScoreIntegrationTest";

        gameEngine.SavePlayerScore(
            playerName
        );

        List<ScoreRecord> highScores =
            gameEngine.GetHighScores();

        ScoreRecord? savedScore =
            highScores.FirstOrDefault(
                score =>
                    score.PlayerName == playerName
            );

        Assert.NotNull(
            savedScore
        );

        Assert.Equal(
            gameEngine.Score,
            savedScore.Score
        );

        Assert.Equal(
            gameEngine.CurrentLevel,
            savedScore.Level
        );
    }

    [Fact]
    public void VictoryIntegration_ShouldEnterVictoryAfterFinalLevelCompletion()
    {
        GameEngine gameEngine =
           new GameEngine(
               DatabaseManager
           );

        bool playerNameRequested = false;

        gameEngine.PlayerNameRequested += () =>
        {
            playerNameRequested = true;
        };

        gameEngine.HandleKeyDown(
            Keys.N
        );

        gameEngine.UpdateCycle();

        Assert.Equal(
            GameState.Playing,
            gameEngine.CurrentState
        );

        const int finalLevel = 50;

        gameEngine.LoadLevel(
            finalLevel
        );

        Assert.Equal(
            finalLevel,
            gameEngine.CurrentLevel
        );

        while (
            gameEngine.Bricks.Any(
                brick => !brick.IsDestroyed
            ))
        {
            Brick brick =
                gameEngine.Bricks.First(
                    brick => !brick.IsDestroyed
                );

            gameEngine.Ball.Position =
                new PointF(
                    brick.Position.X +
                    brick.Size.Width / 2f -
                    gameEngine.Ball.Size.Width / 2f,

                    brick.Position.Y +
                    brick.Size.Height / 2f -
                    gameEngine.Ball.Size.Height / 2f
                );

            gameEngine.Ball.Velocity =
                new PointF(
                    0,
                    3
                );

            gameEngine.UpdateCycle();
        }

        Assert.Equal(
            GameState.Victory,
            gameEngine.CurrentState
        );

        Assert.True(
            playerNameRequested
        );
    }

    [Fact]
    public void LoadCompletionFlow_ShouldAdvanceToNextLevel()
    {
        GameEngine gameEngine =
           new GameEngine(
               DatabaseManager
           );

        gameEngine.HandleKeyDown(
            Keys.N
        );

        gameEngine.UpdateCycle();

        Assert.Equal(
            GameState.Playing,
            gameEngine.CurrentState
        );

        Assert.Equal(
            1,
            gameEngine.CurrentLevel
        );

        Brick lastBrick =
            gameEngine.Bricks.First(
                brick => !brick.IsDestroyed
            );

        foreach (Brick brick in gameEngine.Bricks)
        {
            if (brick != lastBrick &&
                !brick.IsDestroyed)
            {
                brick.Destroy();
            }
        }

        Assert.False(
            lastBrick.IsDestroyed
        );

        gameEngine.Ball.Position =
            new PointF(
                lastBrick.Position.X,
                lastBrick.Position.Y
            );

        gameEngine.Ball.Velocity =
            new PointF(
                0,
                3
            );

        gameEngine.UpdateCycle();

        Assert.Equal(
            2,
            gameEngine.CurrentLevel
        );

        Assert.Equal(
            GameState.Playing,
            gameEngine.CurrentState
        );

        Assert.NotEmpty(
            gameEngine.Bricks
        );

        Assert.Contains(
            gameEngine.Bricks,
            brick => !brick.IsDestroyed
        );
    }

    [Fact]
    public void LevelTransitionIntegration_ShouldLoadNextLevelAndResetObjects()
    {
        GameEngine gameEngine =
           new GameEngine(
               DatabaseManager
           );

        gameEngine.HandleKeyDown(
            Keys.N
        );

        gameEngine.UpdateCycle();

        Assert.Equal(
            GameState.Playing,
            gameEngine.CurrentState
        );

        Assert.Equal(
            1,
            gameEngine.CurrentLevel
        );

        Brick lastBrick =
            gameEngine.Bricks.First(
                brick => !brick.IsDestroyed
            );

        foreach (Brick brick in gameEngine.Bricks)
        {
            if (brick != lastBrick &&
                !brick.IsDestroyed)
            {
                brick.Destroy();
            }
        }

        gameEngine.Ball.Position =
            new PointF(
                lastBrick.Position.X,
                lastBrick.Position.Y
            );

        gameEngine.Ball.Velocity =
            new PointF(
                0,
                3
            );

        gameEngine.Paddle.MoveRight();

        Assert.NotEqual(
            new Point(350, 540),
            gameEngine.Paddle.Position
        );

        gameEngine.UpdateCycle();

        Assert.Equal(
            2,
            gameEngine.CurrentLevel
        );

        Assert.Equal(
            GameState.Playing,
            gameEngine.CurrentState
        );

        Assert.Equal(
            new PointF(400, 300),
            gameEngine.Ball.Position
        );

        Assert.Equal(
            new Point(350, 540),
            gameEngine.Paddle.Position
        );

        Assert.NotEmpty(
            gameEngine.Bricks
        );

        Assert.Contains(
            gameEngine.Bricks,
            brick => !brick.IsDestroyed
        );
    }

    [Fact]
    public void FinalLevelIntegration_ShouldEnterVictoryAfterCompletingFinalLevel()
    {
        GameEngine gameEngine =
           new GameEngine(
               DatabaseManager
           );

        bool playerNameRequested = false;

        gameEngine.PlayerNameRequested += () =>
        {
            playerNameRequested = true;
        };

        gameEngine.HandleKeyDown(
            Keys.N
        );

        gameEngine.UpdateCycle();

        Assert.Equal(
            GameState.Playing,
            gameEngine.CurrentState
        );

        const int finalLevel = 50;

        gameEngine.LoadLevel(
            finalLevel
        );

        Assert.Equal(
            finalLevel,
            gameEngine.CurrentLevel
        );

        Brick lastBrick =
            gameEngine.Bricks.First(
                brick => !brick.IsDestroyed
            );

        foreach (Brick brick in gameEngine.Bricks)
        {
            if (brick != lastBrick &&
                !brick.IsDestroyed)
            {
                brick.Destroy();
            }
        }

        gameEngine.Ball.Position =
            new PointF(
                lastBrick.Position.X,
                lastBrick.Position.Y
            );

        gameEngine.Ball.Velocity =
            new PointF(
                0,
                3
            );

        gameEngine.UpdateCycle();

        Assert.Equal(
            finalLevel,
            gameEngine.CurrentLevel
        );

        Assert.Equal(
            GameState.Victory,
            gameEngine.CurrentState
        );

        Assert.True(
            playerNameRequested
        );
    }

    [Fact]
    public void GameProgressSaveIntegration_ShouldPersistProgressAfterLifeLoss()
    {
        GameEngine gameEngine =
           new GameEngine(
               DatabaseManager
           );

        gameEngine.HandleKeyDown(
            Keys.N
        );

        gameEngine.UpdateCycle();

        Assert.Equal(
            GameState.Playing,
            gameEngine.CurrentState
        );

        Assert.Equal(
            3,
            gameEngine.Lives
        );

        Assert.Equal(
            1,
            gameEngine.CurrentLevel
        );

        gameEngine.Ball.Position =
            new PointF(
                gameEngine.Ball.Position.X,
                600
            );

        gameEngine.Ball.Velocity =
            new PointF(
                0,
                3
            );

        gameEngine.UpdateCycle();

        Assert.Equal(
            2,
            gameEngine.Lives
        );

        GameProgressRepository repository =
            new GameProgressRepository(
                DatabaseManager
            );

        GameProgress? progress =
            repository.LoadProgress();

        Assert.NotNull(
            progress
        );

        Assert.Equal(
            gameEngine.CurrentLevel,
            progress.CurrentLevel
        );

        Assert.Equal(
            gameEngine.Score,
            progress.Score
        );

        Assert.Equal(
            gameEngine.Lives,
            progress.Lives
        );
    }

    [Fact]
    public void GameProgressLoadIntegration_ShouldRestoreSavedProgress()
    {
        DatabaseManager databaseManager =
            TestDataInitializer.CreateInitializedDatabase(
                out string databasePath
            );

        GameProgress progress =
            new GameProgress
            {
                Id = 1,
                CurrentLevel = 10,
                Score = 1500,
                Lives = 2,
                UpdatedAt = DateTime.Now
            };

        GameProgressRepository repository =
            new GameProgressRepository(
                databaseManager
            );

        repository.SaveProgress(
            progress
        );

        GameEngine gameEngine =
            new GameEngine(
                databaseManager
            );

        gameEngine.HandleKeyDown(
            Keys.C
        );

        gameEngine.UpdateCycle();

        Assert.Equal(
            GameState.Playing,
            gameEngine.CurrentState
        );

        Assert.Equal(
            10,
            gameEngine.CurrentLevel
        );

        Assert.Equal(
            1500,
            gameEngine.Score
        );

        Assert.Equal(
            2,
            gameEngine.Lives
        );

        Assert.NotEmpty(
            gameEngine.Bricks
        );
    }

    [Fact]
    public void ContinueFlowIntegration_ShouldRestoreSavedGame()
    {
        DatabaseManager databaseManager =
            TestDataInitializer.CreateInitializedDatabase(
                out string databasePath
            );

        try
        {
            TestDataInitializer.CreateProgress(
                databaseManager,
                level: 10,
                score: 1500,
                lives: 2
            );

            GameProgressRepository repository =
                new GameProgressRepository(
                    databaseManager
                );

            GameProgress? savedProgress =
                repository.LoadProgress();

            Assert.NotNull(
                savedProgress
            );

            Assert.Equal(
                10,
                savedProgress.CurrentLevel
            );

            Assert.Equal(
                1500,
                savedProgress.Score
            );

            Assert.Equal(
                2,
                savedProgress.Lives
            );

            GameEngine gameEngine =
                new GameEngine(
                    databaseManager
                );

            gameEngine.HandleKeyDown(
                Keys.C
            );

            gameEngine.UpdateCycle();

            Assert.Equal(
                GameState.Playing,
                gameEngine.CurrentState
            );

            Assert.Equal(
                10,
                gameEngine.CurrentLevel
            );

            Assert.Equal(
                1500,
                savedProgress.Score
            );

            Assert.Equal(
                2,
                savedProgress.Lives
            );

            Assert.NotEmpty(
                gameEngine.Bricks
            );
        }
        finally
        {
            TestEnvironment.CleanupDatabase(
                databasePath
            );
        }
    }

    [Fact]
    public void ContinueWithoutProgress_ShouldRemainInMenu()
    {
        DatabaseManager databaseManager =
            TestDataInitializer.CreateInitializedDatabase(
                out string databasePath
            );

        try
        {
            GameEngine gameEngine =
                new GameEngine(
                    databaseManager
                );

            bool continueFailed =
                false;

            gameEngine.ContinueFailed += () =>
            {
                continueFailed = true;
            };

            Assert.Equal(
                GameState.Menu,
                gameEngine.CurrentState
            );

            gameEngine.HandleKeyDown(
                Keys.C
            );

            gameEngine.UpdateCycle();

            Assert.Equal(
                GameState.Menu,
                gameEngine.CurrentState
            );

            Assert.True(
                continueFailed
            );
        }
        finally
        {
            TestEnvironment.CleanupDatabase(
                databasePath
            );
        }
    }

    [Fact]
    public void ResetProgressIntegration_ShouldDeleteSavedProgress()
    {
        DatabaseManager databaseManager =
            TestDataInitializer.CreateInitializedDatabase(
                out string databasePath
            );

        try
        {
            TestDataInitializer.CreateProgress(
                databaseManager,
                level: 10,
                score: 1500,
                lives: 2
            );

            GameProgressRepository repository =
                new GameProgressRepository(
                    databaseManager
                );

            GameProgress? savedProgress =
                repository.LoadProgress();

            Assert.NotNull(
                savedProgress
            );

            GameEngine gameEngine =
                new GameEngine(
                    databaseManager
                );

            gameEngine.ResetProgress();

            GameProgress? deletedProgress = 
                repository.LoadProgress();

            Assert.Null(
                deletedProgress
            );

            Assert.Equal(
                GameState.Menu,
                gameEngine.CurrentState
            );
        }
        finally
        {
            TestEnvironment.CleanupDatabase(
                databasePath
            );
        }
    }

    [Fact]
    public void ScorePersistenceIntegration_ShouldSaveAndLoadPlayerScore()
    {
        DatabaseManager databaseManager =
            TestDataInitializer.CreateInitializedDatabase(
                out string databasePath
            );

        try
        {
            GameEngine gameEngine =
                new GameEngine(
                    databaseManager
                );

            gameEngine.NewGame();

            gameEngine.SavePlayerScore(
                "TestPlayer"
            );

            List<ScoreRecord> highScores =
                gameEngine.GetHighScores();

            Assert.NotEmpty(
                highScores
            );

            ScoreRecord savedScore =
                highScores.First(
                    score => score.PlayerName == "TestPlayer"
                );

            Assert.Equal(
                "TestPlayer",
                savedScore.PlayerName
            );

            Assert.Equal(
                gameEngine.Score,
                savedScore.Score
            );

            Assert.Equal(
                gameEngine.CurrentLevel,
                savedScore.Level
            );
        }
        finally
        {
            TestEnvironment.CleanupDatabase(
                databasePath
            );
        }
    }

    [Fact]
    public void HighScoresIntegration_ShouldReturnSavedHighScores()
    {
        DatabaseManager databaseManager =
            TestDataInitializer.CreateInitializedDatabase(
                out string databasePath
            );

        try
        {
            GameEngine gameEngine =
                new GameEngine(
                    databaseManager
                );

            ScoreRepository repository =
                new ScoreRepository(
                    databaseManager
                );

            repository.SaveScore(
                new ScoreRecord
                {
                    PlayerName = "PlayerOne",
                    Score = 500,
                    Level = 3,
                    CreatedAt = DateTime.Now
                }
            );

            repository.SaveScore(
                new ScoreRecord
                {
                    PlayerName = "PlayerTwo",
                    Score = 1500,
                    Level = 10,
                    CreatedAt = DateTime.Now
                }
            );

            repository.SaveScore(
                new ScoreRecord
                {
                    PlayerName = "PlayerThree",
                    Score = 1000,
                    Level = 7,
                    CreatedAt = DateTime.Now
                }
            );

            List<ScoreRecord> highScores =
                gameEngine.GetHighScores();

            Assert.Equal(
                3,
                highScores.Count
            );

            Assert.Equal(
                "PlayerTwo",
                highScores[0].PlayerName
            );

            Assert.Equal(
                1500,
                highScores[0].Score
            );

            Assert.Equal(
                "PlayerThree",
                highScores[1].PlayerName
            );

            Assert.Equal(
                1000,
                highScores[1].Score
            );

            Assert.Equal(
                "PlayerOne",
                highScores[2].PlayerName
            );

            Assert.Equal(
                500,
                highScores[2].Score
            );
        }
        finally
        {
            TestEnvironment.CleanupDatabase(
                databasePath
            );
        }
    }

    [Fact]
    public void SettingsPersistenceIntegration_ShouldSaveAndLoadAudioSettings()
    {
        DatabaseManager databaseManager =
           TestDataInitializer.CreateInitializedDatabase(
               out string databasePath
           );

        try
        {
            GameEngine gameEngine =
                new GameEngine(
                    databaseManager
                );

            gameEngine.AudioManager.MasterVolume = 0.35f;
            gameEngine.AudioManager.SoundEnabled = false;

            gameEngine.SaveAudioSettings();

            GameEngine restoredGameEngine =
                new GameEngine(
                    databaseManager
                );

            Assert.Equal(
                0.35f,
                restoredGameEngine.AudioManager.MasterVolume
            );

            Assert.False(
                restoredGameEngine.AudioManager.SoundEnabled
            );
        }
        finally
        {
            TestEnvironment.CleanupDatabase(
                databasePath
            );
        }
    }

    [Fact]
    public void ApplicationRestartPersistence_ShouldRestoreSavedData()
    {
        DatabaseManager databaseManager =
            TestDataInitializer.CreateInitializedDatabase(
                out string databasePath
            );

        try
        {
            GameEngine firstGameEngine =
                new GameEngine(
                    databaseManager
                );

            TestDataInitializer.CreateProgress(
                databaseManager,
                level: 15,
                score: 2500,
                lives: 2
            );

            firstGameEngine.AudioManager.MasterVolume = 0.4f;
            firstGameEngine.AudioManager.SoundEnabled = false;

            firstGameEngine.SaveAudioSettings();

            ScoreRepository scoreRepository =
                new ScoreRepository(
                    databaseManager
                );

            scoreRepository.SaveScore(
                new ScoreRecord
                {
                    PlayerName = "RestartPlayer",
                    Score = 2500,
                    Level = 15,
                    CreatedAt = DateTime.Now
                }
            );

            GameEngine restartedGameEngine =
                new GameEngine(
                    databaseManager
                );

            GameProgressRepository progressRepository =
                new GameProgressRepository(
                    databaseManager
                );

            GameProgress? restoredProgress =
                progressRepository.LoadProgress();

            Assert.NotNull(
                restoredProgress
            );

            Assert.Equal(
                15,
                restoredProgress.CurrentLevel
            );

            Assert.Equal(
                2500,
                restoredProgress.Score
            );

            Assert.Equal(
                2,
                restoredProgress.Lives
            );

            Assert.Equal(
                0.4f,
                restartedGameEngine.AudioManager.MasterVolume
            );

            Assert.False(
                restartedGameEngine.AudioManager.SoundEnabled
            );

            List<ScoreRecord> highScores =
                restartedGameEngine.GetHighScores();

            ScoreRecord savedScore =
                highScores.First(
                    score => score.PlayerName == "RestartPlayer"
                );

            Assert.Equal(
                2500,
                savedScore.Score
            );

            Assert.Equal(
                15,
                savedScore.Level
            );
        }
        finally
        {
            TestEnvironment.CleanupDatabase(
                databasePath
            );
        }
    }

    [Fact]
    public void AudioManagerIntegration_ShouldApplyAndPersistAudioSettings()
    {
        DatabaseManager databaseManager =
            TestDataInitializer.CreateInitializedDatabase(
                out string databasePath
            );

        try
        {
            GameEngine gameEngine =
                new GameEngine(
                    databaseManager
                );

            Assert.NotNull(
                gameEngine.AudioManager
            );

            gameEngine.AudioManager.MasterVolume = 0.65f;
            gameEngine.AudioManager.SoundEnabled = false;

            Assert.Equal(
                0.65f,
                gameEngine.AudioManager.MasterVolume
            );

            Assert.False(
                gameEngine.AudioManager.SoundEnabled
            );

            gameEngine.SaveAudioSettings();

            GameEngine restartedGameEngine =
                new GameEngine(
                    databaseManager
                );

            Assert.Equal(
                0.65f,
                restartedGameEngine.AudioManager.MasterVolume
            );

            Assert.False(
                restartedGameEngine.AudioManager.SoundEnabled
            );
        }
        finally
        {
            TestEnvironment.CleanupDatabase(
                databasePath
            );
        }
    }

    [Fact]
    public void SoundEnabledIntegration_ShouldPersistSoundState()
    {
        DatabaseManager databaseManager =
            TestDataInitializer.CreateInitializedDatabase(
                out string databasePath
            );

        try
        {
            GameEngine gameEngine =
                new GameEngine(
                    databaseManager
                );

            Assert.True(
                gameEngine.AudioManager.SoundEnabled
            );

            gameEngine.AudioManager.SoundEnabled = false;

            Assert.False(
                gameEngine.AudioManager.SoundEnabled
            );

            gameEngine.SaveAudioSettings();

            GameEngine restartedGameEngine =
                new GameEngine(
                    databaseManager
                );

            Assert.False(
                restartedGameEngine.AudioManager.SoundEnabled
            );

            restartedGameEngine.AudioManager.SoundEnabled = true;

            restartedGameEngine.SaveAudioSettings();

            GameEngine restoredGameEngine =
                new GameEngine(
                    databaseManager
                );

            Assert.True(
                restartedGameEngine.AudioManager.SoundEnabled
            );
        }
        finally
        {
            TestEnvironment.CleanupDatabase(
                databasePath
            );
        }
    }

    [Fact]
    public void MasterVolumeIntegration_ShouldPersistVolume()
    {
        DatabaseManager databaseManager =
            TestDataInitializer.CreateInitializedDatabase(
                out string databasePath
            );

        try
        {
            GameEngine gameEngine =
                new GameEngine(
                    databaseManager
                );

            gameEngine.AudioManager.MasterVolume = 0.25f;

            Assert.Equal(
                0.25f,
                gameEngine.AudioManager.MasterVolume
            );

            gameEngine.SaveAudioSettings();

            GameEngine restartedGameEngine =
                new GameEngine(
                    databaseManager
                );

            Assert.Equal(
                0.25f,
                restartedGameEngine.AudioManager.MasterVolume
            );

            restartedGameEngine.AudioManager.MasterVolume = 0.85f;

            Assert.Equal(
                0.85f,
                restartedGameEngine.AudioManager.MasterVolume
            );

            restartedGameEngine.SaveAudioSettings();

            GameEngine restoredGameEngine =
                new GameEngine(
                    databaseManager
                );

            Assert.Equal(
                0.85f,
                restartedGameEngine.AudioManager.MasterVolume
            );
        }
        finally
        {
            TestEnvironment.CleanupDatabase(
                databasePath
            );
        }
    }

    [Fact]
    public void GameplaySoundFlow_ShouldProcessGameplayWithAudioManager()
    {
        DatabaseManager databaseManager =
            TestDataInitializer.CreateInitializedDatabase(
                out string databasePath
            );

        try
        {
            GameEngine gameEngine =
                new GameEngine(
                    databaseManager
                );

            Assert.NotNull(
                gameEngine.AudioManager
            );

            gameEngine.AudioManager.SoundEnabled = true;

            gameEngine.NewGame();

            Assert.Equal(
                GameState.Playing,
                gameEngine.CurrentState
            );

            Assert.True(
                gameEngine.AudioManager.SoundEnabled
            );

            gameEngine.HandleKeyDown(
                Keys.Left
            );

            gameEngine.UpdateCycle();

            gameEngine.HandleKeyUp(
                Keys.Left
            );

            gameEngine.UpdateCycle();

            Assert.Equal(
                GameState.Playing,
                gameEngine.CurrentState
            );
        }
        finally
        {
            TestEnvironment.CleanupDatabase(
                databasePath
            );
        }
    }

    [Fact]
    public void GameStateSoundFlow_ShouldHandleAudioAcrossGameStates()
    {
        DatabaseManager databaseManager =
            TestDataInitializer.CreateInitializedDatabase(
                out string databasePath
            );

        try
        {
            GameEngine gameEngine =
                new GameEngine(
                    databaseManager
                );

            gameEngine.AudioManager.SoundEnabled = true;

            gameEngine.NewGame();

            Assert.Equal(
                GameState.Playing,
                gameEngine.CurrentState
            );

            Assert.True(
                gameEngine.AudioManager.SoundEnabled
            );

            gameEngine.HandleKeyDown(
                Keys.Escape
            );

            gameEngine.UpdateCycle();

            Assert.Equal(
                GameState.Paused,
                gameEngine.CurrentState
            );

            Assert.True(
                gameEngine.AudioManager.SoundEnabled
            );

            gameEngine.HandleKeyDown(
                Keys.Escape
            );

            gameEngine.UpdateCycle();

            Assert.Equal(
                GameState.Playing,
                gameEngine.CurrentState
            );

            Assert.True(
                gameEngine.AudioManager.SoundEnabled
            );
        }
        finally
        {
            TestEnvironment.CleanupDatabase(
                databasePath
            );
        }
    }

    [Fact]
    public void FullStartGameScenario_ShouldStartAndRunGame()
    {
        DatabaseManager databaseManager =
            TestDataInitializer.CreateInitializedDatabase(
                out string databasePath
            );

        try
        {
            GameEngine gameEngine =
                new GameEngine(
                    databaseManager
                );

            Assert.Equal(
                GameState.Menu,
                gameEngine.CurrentState
            );

            Assert.Equal(
                1,
                gameEngine.CurrentLevel
            );

            Assert.Equal(
                3,
                gameEngine.Lives
            );

            Assert.Equal(
                0,
                gameEngine.Score
            );

            Assert.NotEmpty(
                gameEngine.Bricks
            );

            gameEngine.HandleKeyDown(
                Keys.N
            );

            gameEngine.UpdateCycle();

            Assert.Equal(
                GameState.Playing,
                gameEngine.CurrentState
            );

            Assert.Equal(
                1,
                gameEngine.CurrentLevel
            );

            Assert.Equal(
                3,
                gameEngine.Lives
            );

            Assert.Equal(
                0,
                gameEngine.Score
            );

            Assert.NotEmpty(
                gameEngine.Bricks
            );

            float initialPaddleX =
                gameEngine.Paddle.Position.X;

            gameEngine.HandleKeyDown(
                Keys.Right
            );

            gameEngine.UpdateCycle();

            gameEngine.HandleKeyUp(
                Keys.Right
            );

            float updatedPaddleX =
                gameEngine.Paddle.Position.X;

            Assert.True(
                updatedPaddleX >= initialPaddleX
            );

            PointF initialBallPosition =
                gameEngine.Ball.Position;

            gameEngine.UpdateCycle();

            PointF updatedBallPosition =
                gameEngine.Ball.Position;

            Assert.NotEqual(
                initialBallPosition,
                updatedBallPosition
            );

            Assert.Equal(
                GameState.Playing,
                gameEngine.CurrentState
            );
        }
        finally
        {
            TestEnvironment.CleanupDatabase(
                databasePath
            );
        }
    }

    [Fact]
    public void FullGameplayScenario_ShouldProcessCompleteGameplayFlow()
    {
        DatabaseManager databaseManager =
            TestDataInitializer.CreateInitializedDatabase(
                out string databasePath
            );

        try
        {
            GameEngine gameEngine =
                new GameEngine(
                    databaseManager
                );

            gameEngine.NewGame();

            Assert.Equal(
                GameState.Playing,
                gameEngine.CurrentState
            );

            Assert.Equal(
                1,
                gameEngine.CurrentLevel
            );

            Assert.Equal(
                3,
                gameEngine.Lives
            );

            Assert.Equal(
                0,
                gameEngine.Score
            );

            Assert.NotEmpty(
                gameEngine.Bricks
            );

            float initialPaddleX =
                gameEngine.Paddle.Position.X;

            gameEngine.HandleKeyDown(
                Keys.Right
            );

            gameEngine.UpdateCycle();

            gameEngine.HandleKeyUp(
                Keys.Right
            );

            Assert.True(
                gameEngine.Paddle.Position.X >=
                initialPaddleX
            );

            PointF initialBallPosition =
                gameEngine.Ball.Position;

            gameEngine.UpdateCycle();

            Assert.NotEqual(
                initialBallPosition,
                gameEngine.Ball.Position
            );

            Brick brick =
                gameEngine.Bricks.First(
                    brick => !brick.IsDestroyed
                );

            int brickPoints =
                brick.Points;

            brick.Destroy();

            gameEngine.Ball.Position =
                new PointF(
                    brick.Position.X,
                    brick.Position.Y
                );

            gameEngine.Ball.Velocity =
                new PointF(
                    0,
                    3
                );

            gameEngine.UpdateCycle();

            Assert.Equal(
                GameState.Playing,
                gameEngine.CurrentState
            );

            Assert.True(
                gameEngine.Score >= 0
            );

            gameEngine.Ball.Position =
                new PointF(
                    gameEngine.Ball.Position.X,
                    600
                );

            gameEngine.Ball.Velocity =
                new PointF(
                    0,
                    -10
                );

            gameEngine.UpdateCycle();

            Assert.Equal(
                GameState.Playing,
                gameEngine.CurrentState
            );

            Assert.Equal(
                2,
                gameEngine.Lives
            );

            Assert.NotEmpty(
                gameEngine.Bricks
            );
        } 
        finally
        {
            TestEnvironment.CleanupDatabase(
                databasePath
            );
        }
    }

    [Fact]
    public void FullLoseLifeScenario_ShouldReachGameOver()
    {
        DatabaseManager databaseManager =
            TestDataInitializer.CreateInitializedDatabase(
                out string databasePath
            );

        try
        {
            GameEngine gameEngine =
                new GameEngine(
                    databaseManager
                );

            gameEngine.NewGame();

            Assert.Equal(
                GameState.Playing,
                gameEngine.CurrentState
            );

            Assert.Equal(
                3,
                gameEngine.Lives
            );

            Assert.Equal(
                0,
                gameEngine.Score
            );

            gameEngine.Ball.Position =
                new PointF(
                    gameEngine.Ball.Position.X,
                    600
                );

            gameEngine.Ball.Velocity =
                new PointF(
                    0,
                    -10
                );

            gameEngine.UpdateCycle();

            Assert.Equal(
                2,
                gameEngine.Lives
            );

            Assert.Equal(
                GameState.Playing,
                gameEngine.CurrentState
            );

            Assert.Equal(
                new PointF(400, 300),
                gameEngine.Ball.Position
            );

            Assert.Equal(
                new Point(350, 540),
                gameEngine.Paddle.Position
            );

            gameEngine.Ball.Position =
                new PointF(
                    gameEngine.Ball.Position.X,
                    600
                );

            gameEngine.Ball.Velocity =
                new PointF(
                    0,
                    -10
                );

            gameEngine.UpdateCycle();

            Assert.Equal(
                1,
                gameEngine.Lives
            );

            Assert.Equal(
                GameState.Playing,
                gameEngine.CurrentState
            );

            gameEngine.Ball.Position =
                new PointF(
                    gameEngine.Ball.Position.X,
                    600
                );

            gameEngine.Ball.Velocity =
                new PointF(
                    0,
                    -10
                );

            gameEngine.UpdateCycle();

            Assert.Equal(
                0,
                gameEngine.Lives
            );

            Assert.Equal(
                GameState.GameOver,
                gameEngine.CurrentState
            );

            Assert.NotEmpty(
                gameEngine.Bricks
            );
        }
        finally
        {
            TestEnvironment.CleanupDatabase(
                databasePath
            );
        }
    }

    [Fact]
    public void FullGameOverScenario_ShouldProcessCompleteGameOverFlow()
    {
        DatabaseManager databaseManager =
            TestDataInitializer.CreateInitializedDatabase(
                out string databasePath
            );

        try
        {
            GameEngine gameEngine =
                new GameEngine(
                    databaseManager
                );

            bool playerNameRequested = false;

            gameEngine.PlayerNameRequested += () =>
            {
                playerNameRequested = true;
            };

            gameEngine.NewGame();

            Assert.Equal(
                GameState.Playing,
                gameEngine.CurrentState
            );

            Assert.Equal(
                3,
                gameEngine.Lives
            );

            Assert.Equal(
                0,
                gameEngine.Score
            );

            LoseBall(gameEngine);

            Assert.Equal(
                2,
                gameEngine.Lives
            );

            Assert.Equal(
                GameState.Playing,
                gameEngine.CurrentState
            );

            LoseBall(gameEngine);

            Assert.Equal(
                1,
                gameEngine.Lives
            );

            Assert.Equal(
                GameState.Playing,
                gameEngine.CurrentState
            );

            LoseBall(gameEngine);

            Assert.Equal(
                0,
                gameEngine.Lives
            );

            Assert.Equal(
                GameState.GameOver,
                gameEngine.CurrentState
            );

            Assert.True(
                playerNameRequested
            );

            Assert.Empty(
                gameEngine.GetHighScores()
            );
        }
        finally
        {
            TestEnvironment.CleanupDatabase(
                databasePath
            );
        }
    }

    [Fact]
    public void FullVictoryScenario_ShouldCompleteGameAndEnterVictoryState()
    {
        DatabaseManager databaseManager =
            TestDataInitializer.CreateInitializedDatabase(
                out string databasePath
            );

        try
        {
            GameEngine gameEngine =
                new GameEngine(
                    databaseManager
                );

            gameEngine.NewGame();

            Assert.Equal(
                GameState.Playing,
                gameEngine.CurrentState
            );

            Assert.Equal(
                1,
                gameEngine.CurrentLevel
            );

            Assert.Equal(
                3,
                gameEngine.Lives
            );

            gameEngine.LoadLevel(50);

            Assert.Equal(
                50,
                gameEngine.CurrentLevel
            );

            Assert.Equal(
                GameState.Playing,
                gameEngine.CurrentState
            );

            Assert.NotEmpty(
                gameEngine.Bricks
            );

            Brick finalBrick =
                gameEngine.Bricks.First(
                    brick => !brick.IsDestroyed
                );

            foreach (Brick brick in gameEngine.Bricks)
            {
                if (brick != finalBrick &&
                    !brick.IsDestroyed)
                {
                    brick.Destroy();
                }
            }

            gameEngine.Ball.Position =
                new PointF(
                    finalBrick.Position.X,
                    finalBrick.Position.Y
                );

            gameEngine.Ball.Velocity =
                new PointF(
                    0,
                    3
                );

            gameEngine.UpdateCycle();

            Assert.Equal(
                GameState.Victory,
                gameEngine.CurrentState
            );

            Assert.Equal(
                50,
                gameEngine.CurrentLevel
            );

            Assert.Equal(
                3,
                gameEngine.Lives
            );

            GameProgressRepository repository =
                new GameProgressRepository(
                    databaseManager
                );

            GameProgress? progress =
                repository.LoadProgress();

            Assert.Null(progress);
        }
        finally
        {
            TestEnvironment.CleanupDatabase(
                databasePath
            );
        }
    }

    [Fact]
    public void FullContinueScenario_ShouldRestoreCompleteSavedGame()
    {
        DatabaseManager databaseManager =
            TestDataInitializer.CreateInitializedDatabase(
                out string databasePath
            );

        try
        {
            TestDataInitializer.CreateProgress(
                databaseManager,
                level: 25,
                score: 7500,
                lives: 2
            );

            GameProgressRepository repository =
                new GameProgressRepository(
                    databaseManager
                );

            GameProgress? savedProgress = 
                repository.LoadProgress();

            Assert.NotNull(
                savedProgress
            );

            Assert.Equal(
                25,
                savedProgress.CurrentLevel
            );

            Assert.Equal(
                7500,
                savedProgress.Score
            );

            Assert.Equal(
                2,
                savedProgress.Lives
            );

            GameEngine gameEngine =
                new GameEngine(
                    databaseManager
                );

            Assert.Equal(
                GameState.Menu,
                gameEngine.CurrentState
            );

            gameEngine.HandleKeyDown(
                Keys.C
            );

            gameEngine.UpdateCycle();

            Assert.Equal(
                GameState.Playing,
                gameEngine.CurrentState
            );

            Assert.Equal(
                25,
                gameEngine.CurrentLevel
            );

            Assert.Equal(
                7500,
                gameEngine.Score
            );

            Assert.Equal(
                2,
                gameEngine.Lives
            );

            Assert.NotEmpty(
                gameEngine.Bricks
            );

            Assert.Contains(
                gameEngine.Bricks,
                brick => !brick.IsDestroyed
            );
        }
        finally
        {
            TestEnvironment.CleanupDatabase(
                databasePath
            );
        }
    }

    [Fact]
    public void FullHighScoreScenario_ShouldSaveAndLoadHighScore()
    {
        DatabaseManager databaseManager =
            TestDataInitializer.CreateInitializedDatabase(
                out string databasePath
            );

        try
        {
            GameEngine gameEngine =
                new GameEngine(
                    databaseManager
                );

            gameEngine.NewGame();

            Assert.Equal(
                GameState.Playing,
                gameEngine.CurrentState
            );

            gameEngine.LoadLevel(1);

            gameEngine.Ball.Position =
                new PointF(
                    gameEngine.Ball.Position.X,
                    600
                );

            gameEngine.Ball.Velocity =
                new Point(
                    0,
                    -10
                );

            for (int i = 0; i < 3; i++)
            {
                gameEngine.UpdateCycle();

                if (gameEngine.CurrentState == 
                    GameState.GameOver)
                {
                    break;
                }

                gameEngine.Ball.Position =
                    new PointF(
                        gameEngine.Ball.Position.X,
                        600
                    );

                gameEngine.Ball.Velocity =
                    new PointF(
                        0,
                        -10
                    );
            }

            Assert.Equal(
                GameState.GameOver,
                gameEngine.CurrentState
            );

            gameEngine.SavePlayerScore(
                "IntegrationTestPlayer"
            );

            List<ScoreRecord> scores =
                gameEngine.GetHighScores();

            Assert.NotEmpty(
                scores
            );

            ScoreRecord savedScore =
                scores.First(
                    score =>
                        score.PlayerName ==
                        "IntegrationTestPlayer"
                );

            Assert.Equal(
                "IntegrationTestPlayer",
                savedScore.PlayerName
            );

            Assert.Equal(
                gameEngine.Score,
                savedScore.Score
            );

            Assert.Equal(
                gameEngine.CurrentLevel,
                savedScore.Level
            );

            GameEngine restartedGameEngine =
                new GameEngine(
                    databaseManager
                );

            List<ScoreRecord> persistedScores =
                restartedGameEngine.GetHighScores();

            Assert.Contains(
                persistedScores,
                score =>
                    score.PlayerName ==
                    "IntegrationTestPlayer" &&
                    score.Score ==
                    savedScore.Score &&
                    score.Level ==
                    savedScore.Level
                );
        }
        finally
        {
            TestEnvironment.CleanupDatabase(
                databasePath
            );
        }
    }

    [Fact]
    public void FullSettingsScenario_ShouldPersistAndRestoreSettings()
    {
        DatabaseManager databaseManager =
            TestDataInitializer.CreateInitializedDatabase(
                out string databasePath
            );

        try
        {
            GameEngine gameEngine =
                new GameEngine(
                    databaseManager
                );

            gameEngine.AudioManager.SoundEnabled =
                false;

            gameEngine.AudioManager.MasterVolume =
                0.35f;

            gameEngine.SaveAudioSettings();

            Assert.False(
                gameEngine.AudioManager.SoundEnabled
            );

            Assert.Equal(
                0.35f,
                gameEngine.AudioManager.MasterVolume
            );

            GameEngine restartedGameEngine =
                new GameEngine(
                    databaseManager
                );

            Assert.False(
                restartedGameEngine.AudioManager.SoundEnabled
            );

            Assert.Equal(
                0.35f,
                restartedGameEngine.AudioManager.MasterVolume
            );

            restartedGameEngine.AudioManager.SoundEnabled =
                true;

            restartedGameEngine.AudioManager.MasterVolume =
                0.8f;

            restartedGameEngine.SaveAudioSettings();

            GameEngine finalGameEngine =
                new GameEngine(
                    databaseManager
                );

            Assert.True(
                finalGameEngine.AudioManager.SoundEnabled
            );

            Assert.Equal(
                0.8f,
                finalGameEngine.AudioManager.MasterVolume
            );
        }
        finally
        {
            TestEnvironment.CleanupDatabase(
                databasePath
            );
        }
    }

    [Fact]
    public void FullApplicationRestartScenario_ShouldRestorePersistentData()
    {
        DatabaseManager databaseManager =
            TestDataInitializer.CreateInitializedDatabase(
                out string databasePath
            );

        try
        {
            GameEngine firstGameEngine =
                new GameEngine(
                    databaseManager
                );

            firstGameEngine.NewGame();

            Assert.Equal(
                GameState.Playing,
                firstGameEngine.CurrentState
            );

            GameProgressRepository progressRepository =
                new GameProgressRepository(
                    databaseManager
                );

            GameProgress progress =
                new GameProgress
                {
                    Id = 1,
                    CurrentLevel = 15,
                    Score = 3500,
                    Lives = 2,
                    UpdatedAt = DateTime.Now
                };

            progressRepository.SaveProgress(
                progress
            );

            firstGameEngine.AudioManager.SoundEnabled =
                false;

            firstGameEngine.AudioManager.MasterVolume =
                0.45f;

            firstGameEngine.SaveAudioSettings();

            firstGameEngine.SavePlayerScore(
                "RestartTestPlayer"
            );

            firstGameEngine.Stop();

            GameEngine restartedGameEngine =
                new GameEngine(
                    databaseManager
                );

            Assert.False(
                restartedGameEngine.AudioManager.SoundEnabled
            );

            Assert.Equal(
                0.45f,
                restartedGameEngine.AudioManager.MasterVolume
            );

            GameProgress? restoredProgress =
                progressRepository.LoadProgress();

            Assert.NotNull(
                restoredProgress
            );

            Assert.Equal(
                15,
                restoredProgress.CurrentLevel
            );

            Assert.Equal(
                3500,
                restoredProgress.Score
            );

            Assert.Equal(
                2,
                restoredProgress.Lives
            );

            List<ScoreRecord> highScores =
                restartedGameEngine.GetHighScores();

            Assert.Contains(
                highScores,
                score =>
                    score.PlayerName ==
                    "RestartTestPlayer"
                );
        }
        finally
        {
            TestEnvironment.CleanupDatabase(
                databasePath
            );
        }
    }

    private static void LoseBall(
        GameEngine gameEngine)
    {
        gameEngine.Ball.Position =
            new PointF(
                gameEngine.Ball.Position.X,
                600);

        gameEngine.Ball.Velocity =
            new PointF(
                0,
                -10
            );

        gameEngine.UpdateCycle();
    }
}
