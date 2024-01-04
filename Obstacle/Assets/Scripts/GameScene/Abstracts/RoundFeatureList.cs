public class ExtraLifeRoundFeature : RoundFeatureBase {
  HealthSystem playerHealthSystem;

  public ExtraLifeRoundFeature(int initialPrice, float priceScaleFactor, HealthSystem playerHealthSystem)
    : base(initialPrice, priceScaleFactor) {
    this.playerHealthSystem = playerHealthSystem;
  }

  public override void Execute() {
    playerHealthSystem.Heal(1);
    ScoreHandle.Instance.DecreaseEggCount(price);
  }
}

public class ExtraJumpRoundFeature : RoundFeatureBase {
  PlayerJump playerJump;

  public ExtraJumpRoundFeature(int initialPrice, float priceScaleFactor, PlayerJump playerJump)
    : base(initialPrice, priceScaleFactor) {
    this.playerJump = playerJump;
  }

  public override void Execute() {
    playerJump.IncreaseMaxJumpCount(1);
    ScoreHandle.Instance.DecreaseEggCount(price);
  }
}

public class LaserRoundFeature : RoundFeatureBase {
  PlayerLaser playerLaser;

  public LaserRoundFeature(int initialPrice, float priceScaleFactor, PlayerLaser playerLaser)
    : base(initialPrice, priceScaleFactor) {
    this.playerLaser = playerLaser;
  }

  public override void Execute() {
    playerLaser.DecreaseCooldown(1f);
    ScoreHandle.Instance.DecreaseEggCount(price);
  }
}