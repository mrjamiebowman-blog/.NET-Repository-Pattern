namespace MrJB.KafkaModels.Models.Orders
{
    public class Order : Checkout
    {
        public int? OrderId { get; set; }
    }
}
