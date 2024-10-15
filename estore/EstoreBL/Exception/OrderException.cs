namespace Estore.BL
{
	public class OrderException : Exception
    {
		public OrderException(string errorMessage): base(errorMessage){
			
		}
	}
}

