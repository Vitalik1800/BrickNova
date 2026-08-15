using BrickNova.Models;

namespace BrickNova.Tests;

public class ScoreTests
{
    [Fact]
    public void ScoreRecord_ShouldInitializeProperties()
    {
        ScoreRecord record = new ScoreRecord
        {
            PlayerName = "Player",
            Score = 1000,
            Level = 5,
            CreatedAt = DateTime.Now
        };

        Assert.Equal("Player", record.PlayerName);
        Assert.Equal(1000, record.Score);
        Assert.Equal(5, record.Level);
    }

    [Fact]
    public void ScoreRecord_ShouldStorePlayerName()
    {
        ScoreRecord record = new ScoreRecord
        {
            PlayerName = "Vitaly"
        };

        Assert.Equal(
            "Vitaly",
            record.PlayerName
        );
    }

    [Fact]
    public void ScoreRecord_ShouldStoreScore()
    {
        ScoreRecord record = new ScoreRecord
        {
            Score = 500
        };

        Assert.Equal(
            500,
            record.Score
        );
    }

    [Fact]
    public void ScoreRecord_ShouldStoreLevel()
    {
        ScoreRecord record = new ScoreRecord
        {
            Level = 10
        };

        Assert.Equal(
            10,
            record.Level
        );
    }

    [Fact]
    public void AddPoints_ShouldIncreaseScore()
    {
        int currentScore = 100;
        int points = 50;

        int result =
            ScoreCalculator.AddPoints(
                currentScore,
                points
            );

        Assert.Equal(
            150,
            result
        );
    }

    [Fact]
    public void AddPoints_ShouldReturnSameScore_WhenPointsAreZero()
    {
        int currentScore = 500;

        int result =
            ScoreCalculator.AddPoints(
                currentScore,
                0
            );

        Assert.Equal(
            500,
            result
        );
    }

    [Fact]
    public void AddPoints_ShouldSupportMultipleScoreUpdates()
    {
        int score = 0;

        score = ScoreCalculator.AddPoints(score, 100);
        score = ScoreCalculator.AddPoints(score, 200);
        score = ScoreCalculator.AddPoints(score, 50);

        Assert.Equal(
            350,
            score
        );
    }

    [Fact]
    public void AddPoints_ShouldIncrementScoreCorrectly()
    {
        int score = 0;

        score = ScoreCalculator.AddPoints(score, 100);

        Assert.Equal(100, score);

        score = ScoreCalculator.AddPoints(score, 50);

        Assert.Equal(150, score);

        score = ScoreCalculator.AddPoints(score, 25);

        Assert.Equal(175, score);
    }

    [Fact]
    public void AddPoints_ShouldAccumulateBrickPoints()
    {
        int score = 500;

        int brickPoints1 = 50;
        int brickPoints2 = 100;
        int brickPoints3 = 200;

        score = ScoreCalculator.AddPoints(
            score,
            brickPoints1
        );

        score = ScoreCalculator.AddPoints(
            score,
            brickPoints2
        );

        score = ScoreCalculator.AddPoints(
            score,
            brickPoints3
        );

        Assert.Equal(850, score);
    }
}
