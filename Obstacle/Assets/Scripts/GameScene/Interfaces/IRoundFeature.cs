public interface IRoundFeature {

  bool CanPay(int eggCount);

  void Execute();

  void UpdatePrice();

  string GetPriceText();
}