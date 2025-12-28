namespace ProductsOrderWebAPI.Domain.Exceptions
{
    public class OrderException(string message) : Exception(message);

    public class OrderNotFoundException(int id) : OrderException(
        $"Order with id: {id} was not found"    
    );

    public class OrderWithNoProductsException() : OrderException(
        "Order needs to have at least one Product in its list"
    );
}
