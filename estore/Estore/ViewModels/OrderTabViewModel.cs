namespace Estore.ViewModels
{
	public static class OrderTabViewModel
	{
        public static OrderTabStep[] OrderTabViewList = new OrderTabStep[] {
            new OrderTabStep() {  Step = OrderTabStep.OrderTabStepEnum.Address, Title = "Адрес", Url = "/checkout/address" }, 
            new OrderTabStep() {  Step = OrderTabStep.OrderTabStepEnum.Billing, Title = "Оплата", Url = "/checkout/billing" }, 
            new OrderTabStep() {  Step = OrderTabStep.OrderTabStepEnum.Review, Title = "Подтверждение", Url = "/checkout/review" }
        };
    }

    public class OrderTabStep {
        public enum OrderTabStepEnum { Address, Billing, Review }
        public OrderTabStepEnum Step { get; set;}

        public string Title {get; set; } = null!;

        public string Url { get; set; } = null!;
    }
}