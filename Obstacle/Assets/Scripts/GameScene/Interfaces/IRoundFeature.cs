public interface IRoundFeature {

  bool CanPay(int eggCount);

  void Execute();
  bool IsPossible();

  void UpdatePrice();

  string GetPriceText();
}