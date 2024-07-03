using System;

namespace Web_Form
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void submitBtn_Click(object sender, EventArgs e)
        {
            namePrint.Text = $"Name : {name.Text}";
            emailPrint.Text = $"Email : {email.Text}";
        }
    }
}