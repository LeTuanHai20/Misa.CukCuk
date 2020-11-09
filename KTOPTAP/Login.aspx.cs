using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KTOPTAP
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            if(username != "")
            {
                try
                {
                    string path = Server.MapPath("~\\Login");
                    string file = Path.Combine(path, username + ".txt");
                    StreamReader reader = new StreamReader(file);
                    string stroredPass = reader.ReadLine();

                    SHA256 sha256 = SHA256.Create();
                    byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(txtPassword.Text.Trim()));
                    string ecryptedPass = BitConverter.ToString(bytes);
                    if (ecryptedPass == stroredPass)
                    {
                        Session["name"] = username;
                        Session["allow"] = true;
                        Response.Redirect("CalArea.aspx");
                    }
                    else
                    {
                        Response.Write("<script>alert('User Name or password is invalid !')</script>");
                    }
                }
                catch (Exception)
                {
                    Response.Write("<script>alert('Bạn vui lòng đăng ký trước!')</script>");
                }
                
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            if(username != "" && password != "")
            {
                try
                {
                    string path = Server.MapPath("~\\Login");
                    String file = Path.Combine(path, username + ".txt");
                    StreamWriter writer = new StreamWriter(file);

                    SHA256 sha256 = SHA256.Create();
                    byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(txtPassword.Text.Trim()));
                    string ecryptedPass = BitConverter.ToString(bytes);

                    writer.WriteLine(ecryptedPass);
                    writer.Close();
                    Response.Write("<script>alert('Đăng kí thành công')</script>");
                }
                catch (Exception)
                {
                    Response.Write("<script>alert('Không thể lưu thông tin đăng kí')</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Tên đăng nhập và mật khẩu không hợp lê')</script>");
            }
        }
    }
}