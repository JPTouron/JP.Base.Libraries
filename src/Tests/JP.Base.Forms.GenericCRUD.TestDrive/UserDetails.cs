using System.Windows.Forms;

namespace TD.Forms.GenericABM.FormTestDrive
{
    public partial class UserDetails : UserControl
    {
        public UserDetails()
        {
            InitializeComponent();
        }

        public string Login { get { return txtLogin.Text; } set { txtLogin.Text = value; } }
        public string Password { get { return txtPwd.Text; } set { txtPwd.Text = value; } }

        public User User { get { return new User() { Login = txtLogin.Text, Password = txtPwd.Text }; } set { txtLogin.Text = value.Login; txtPwd.Text = value.Password; } }
    }
}