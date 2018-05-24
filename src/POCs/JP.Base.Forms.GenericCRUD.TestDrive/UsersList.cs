using System;
using System.Windows.Forms;

namespace TD.Forms.GenericABM.FormTestDrive
{
    public partial class UsersList : UserControl
    {
        public UsersList()
        {
            InitializeComponent();
        }

        public event EventHandler ItemSelected;

        public User SelectedItem
        {
            get
            {
                return new User() { Login = listView1.SelectedItems[0].Text, Password = listView1.SelectedItems[0].SubItems[0].Text };
            }
            set
            {
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    if (value.Login == listView1.Items[i].Text)
                    {
                        listView1.Items[i].Text = value.Login;
                        listView1.Items[i].SubItems[0].Text = value.Password;
                    }
                }
            }
        }

        public void AddUser(User user)
        {
            var item = new ListViewItem(user.Login);
            item.SubItems.Add(user.Password);
            listView1.Items.Add(item);
        }

        public void DeleteSelectedItem()
        {
            if (listView1.SelectedItems.Count > 0)
            {
                listView1.SelectedItems[0].Remove();
            }
        }

        public void LoadAllItems()
        {
            listView1.Items.Clear();
            for (int i = 0; i < 5; i++)
            {
                listView1.Items.Add("Item" + i.ToString()).SubItems.Add("Password" + i.ToString());
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                if (listView1.SelectedItems[0] != null)
                {
                    SelectedItem = new User() { Login = listView1.SelectedItems[0].Text, Password = listView1.SelectedItems[0].SubItems[0].Text };

                    if (ItemSelected != null)
                    {
                        ItemSelected(sender, e);
                    }
                }
            }
        }

        private void UserDetails_Load(object sender, EventArgs e)
        {
            LoadAllItems();
        }
    }
}