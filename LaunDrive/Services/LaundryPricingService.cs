namespace LaunDrive.Services
{
    public class LaundryPricingService
    {
        public const decimal WashPrice = 15m;
        public const decimal IronPrice = 10m;
        public const decimal FoldPrice = 5m;
        public const decimal DeliveryFee = 30m;

        public static decimal CalculateTotal(bool wash, bool iron, bool fold, int numberOfPieces)
        {
            if (numberOfPieces <= 0)
            {
                throw new ArgumentException("Number of pieces must be greater than zero.");
            }
            if (!wash && !iron && !fold)
            {
                throw new ArgumentException("Select at least one laundry service.");
            }

            decimal pricePerPiece = 0m;
            
            if (wash)
            {
                pricePerPiece += WashPrice;
            }
            if (iron)
            {
                pricePerPiece += IronPrice;
            }
            if(fold)
            {
                pricePerPiece += FoldPrice;
            }

            decimal total = (pricePerPiece * numberOfPieces) + DeliveryFee;

            return total;
        }
        
        }
}
