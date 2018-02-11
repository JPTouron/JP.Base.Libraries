using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using JP.Base.MVP.Implementation;

namespace MVPWithLog4Net
{
    public partial class Form1 : Form, IFormView
    {
        private FormPresenter presenter;

        public Form1()
        {
            InitializeComponent();

            NotifyUIDisplayErrorMessage += Form1_NotifyUIDisplayErrorMessage;
            NotifyUISetBusyState += Form1_NotifyUISetBusyState;
            NotifyUISetNormalState += Form1_NotifyUISetNormalState;
            NotifyUIValidationErrors += Form1_NotifyUIValidationErrors;

            presenter = new FormPresenter(this);
        }

        public Model Model
        {
            get;
            set;
        }

        public event EventHandler NotifyTerminating;

        public event NotifyDisplayErrorMessageHandler NotifyUIDisplayErrorMessage;

        public event EventHandler NotifyUISetBusyState;

        public event EventHandler NotifyUISetNormalState;

        public event EventHandler<IDictionary<int, string>> NotifyUIValidationErrors;

        public void DisplayErrorMessage(string message, bool shouldDisableView)
        {
            MessageBox.Show($"message:{message}.\r\n\r\n shouldDisableView:{shouldDisableView}");
        }

        public void SetBusyState()
        {
            toolStripStatusLabel1.Text = $"Status: Busy";
        }

        public void SetNormalState()
        {
            toolStripStatusLabel1.Text = $"Status: Normal";
        }

        public void TerminateView()
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Model = new Model { id = Id.Text, name = ModelName.Text, value = Value.Text };
            presenter.CreateNewItem();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Model = new Model { id = Id.Text, name = ModelName.Text, value = Value.Text };
            presenter.UpdateItem(false);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Form1_NotifyUIDisplayErrorMessage(string message, bool shouldDisableView)
        {
            DisplayErrorMessage(message, shouldDisableView);
        }

        private void Form1_NotifyUISetBusyState(object sender, EventArgs e)
        {
            SetBusyState();
        }

        private void Form1_NotifyUISetNormalState(object sender, EventArgs e)
        {
            SetNormalState();
        }

        private void Form1_NotifyUIValidationErrors(object sender, IDictionary<int, string> e)
        {
            toolStripStatusLabel1.Text = "Model Errors: " + e.Select(x => x.Value).Aggregate((i, j) => i + ", " + j);
        }
    }
}