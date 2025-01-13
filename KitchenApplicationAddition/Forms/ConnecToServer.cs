using KitchenApplicationAddition.Classes.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KitchenApplicationAddition.Classes.Managers;

namespace KitchenApplicationAddition.Forms
{
    public partial class ConnecToServer : Form
    {
        public ConnecToServer()
        {
            InitializeComponent();
        }

        private void ConnectBtn_Click(object sender, EventArgs e)
        {
            if (!MessageManager.ConnectToServer(this.IpAddressTextBox.Text))
            {
                MessageBox.Show("Unable to establish connection to server.");
                this.DialogResult = DialogResult.None;
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
