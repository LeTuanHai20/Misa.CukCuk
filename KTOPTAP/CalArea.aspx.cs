using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KTOPTAP
{
    public partial class CalArea : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["allow"] != null)
            {
                txtName.Text = "Xin chào " + Session["name"].ToString();
            }
            else
            {
                Response.Write("<script>alert('Bạn chưa đăng nhập!')</script>");
                Response.Redirect("Login.aspx");
            }

        }
    }
}