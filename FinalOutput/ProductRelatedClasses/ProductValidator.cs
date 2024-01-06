
namespace FinalOutput.ProductRelatedClasses
{
    public class ProductValidator
    {
        public static bool CheckIfProductQuantityIsZero(Product product)
        {
            return product.ProductQuantity == 0;
        }

        public static bool CheckIfProductIsInStock(Product selectedProduct, int inputproductQuantity)
        {
            return selectedProduct.ProductQuantity >= inputproductQuantity;
        }
    }
}
