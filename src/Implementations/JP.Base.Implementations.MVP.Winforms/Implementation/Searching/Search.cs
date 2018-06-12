using System;
using System.Windows.Forms;

namespace JP.Base.Implementations.MVP.Winforms.Implementation.Searching
{
    public partial class Search : UserControl
    {
        public Search()
        {
            InitializeComponent();
        }

        public string InputText { get { return textBox1.Text; } }

        public event NotifySearchRequestHandler NotifySearchRequest;

        private void OnButton1Click(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void OnTextBox1KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformSearch();
            }
        }

        private void PerformSearch()
        {
            if (NotifySearchRequest != null)
            {
                NotifySearchRequest(textBox1.Text);
            }
        }
    }
}