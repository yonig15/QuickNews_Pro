namespace QuickNews.Model
{
	public class M_User : BaseModel			
	{
		public string UserId { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public virtual List<M_Category> Interests { get; set; }
	}
}