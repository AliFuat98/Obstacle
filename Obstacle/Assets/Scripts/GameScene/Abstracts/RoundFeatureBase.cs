public abstract class RoundFeatureBase : IRoundFeature {
  protected int price;
  protected float priceScaleFactor;

  public RoundFeatureBase(int initialPrice, float priceScaleFactor) {
    price = initialPrice;
    this.priceScaleFactor = priceScaleFactor;
  }

  public bool CanPay(int eggCount) {
    return eggCount >= price;
  }

  public abstract void Execute();
  public abstract bool IsPossible();

  public string GetPriceText() {
    return $"{price} Egg";
  }

  public void UpdatePrice() {
    price += (int)(price * priceScaleFactor);
  }
}