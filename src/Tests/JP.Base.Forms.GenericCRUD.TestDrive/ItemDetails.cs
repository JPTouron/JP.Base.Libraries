using System.Windows.Forms;

namespace TD.Forms.GenericABM.FormTestDrive
{
    public partial class ItemDetails : UserControl
    {
        public ItemDetails()
        {
            InitializeComponent();
        }

        public string Item
        {
            get
            {
                return textBox1.Text;
            }
            set
            {
                textBox1.Text = value;
            }
        }
    }
}