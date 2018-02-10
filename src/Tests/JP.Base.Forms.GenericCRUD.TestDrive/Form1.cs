using JP.Base.Forms.GenericCRUD.Forms;
using System;
using System.Windows.Forms;

namespace TD.Forms.GenericABM.FormTestDrive
{
    public partial class Form1 : CRUDForm
    {
        private views viewing;

        public Form1()
        {
            InitializeComponent();
        }

        private enum views
        {
            Users,
            Items
        }

        private void DisplayViews(views view)
        {
            if (view == views.Users)
            {
                usersList1.Visible = true;
                userDetails1.Visible = true;
                itemsList1.Visible = false;
                itemDetails1.Visible = false;
            }
            else
            {
                userDetails1.Visible = false;
                usersList1.Visible = false;
                itemsList1.Visible = true;
                itemDetails1.Visible = true;
            }
            viewing = view;
        }

        private void Form1_DeletingItem()
        {
            if (viewing == views.Items)
            {
                itemsList1.DeleteSelectedItem();
            }
            else
            {
                usersList1.DeleteSelectedItem();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadTreeNodes();
            DisplayViews(views.Items);
            itemsList1.ItemSelected += new EventHandler(itemsList1_ItemSelected);

            usersList1.ItemSelected += new EventHandler(usersList1_ItemSelected);

            SavingItem += new JP.Base.Forms.GenericCRUD.EventHandlers.SavingItemHandler(Form1_SavingItem);
            DeletingItem += new JP.Base.Forms.GenericCRUD.EventHandlers.DeletingItemHandler(Form1_DeletingItem);
            SelectedNode += new JP.Base.Forms.GenericCRUD.EventHandlers.NodeSelectedHandler(Form1_SelectedNode);
        }

        private void Form1_SavingItem(bool newItem, ref bool cancel)
        {
            if (viewing == views.Items)
            {
                if (!newItem)
                {
                    itemsList1.SelectedItem = itemDetails1.Item;
                }
                else
                {
                    itemsList1.AddItem(itemDetails1.Item);
                }
            }
            else
            {
                if (!newItem)
                {
                    usersList1.SelectedItem = userDetails1.User;
                }
                else
                {
                    usersList1.AddUser(userDetails1.User);
                }
            }
        }

        private void Form1_SelectedNode(object sender, TreeViewEventArgs e)
        {
            if (Enum.IsDefined(typeof(views), e.Node.Text))
                DisplayViews((views)Enum.Parse(typeof(views), e.Node.Text));
        }

        private void itemsList1_ItemSelected(object sender, EventArgs e)
        {
            itemDetails1.Item = itemsList1.SelectedItem;
        }

        private void LoadTreeNodes()
        {
            TreeNode parent = TViewCategorias.Nodes.Add("Entidades");
            parent.Nodes.Add("Items");
            parent.Nodes.Add("Users");
        }

        private void usersList1_ItemSelected(object sender, EventArgs e)
        {
            userDetails1.User = usersList1.SelectedItem;
        }
    }
}