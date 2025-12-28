namespace ProductsOrderWebAPI.Domain.Exceptions
{
    public abstract class ProductException(string message) : Exception(message);

    public class ProductNotFoundException(int id) : ProductException(
        $"Product with id: ${id} was not found"
    );

    public class InvalidProductPriceException(int id) : ProductException(
        $"Product with ${id} has an invalid price"
    );
}
