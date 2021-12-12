using System.Web;
namespace dms.Utilities
{
    public class UserData
    {

        public int ClientId { get; set; }
        public UserData()
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session["ClientId"] != null)
            {
                ClientId = (int)HttpContext.Current.Session["ClientId"];
            }
           
        }
    }
}