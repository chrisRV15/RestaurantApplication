using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KitchenApplicationAddition.Forms;
using KitchenApplicationAddition.Classes.Managers;
using KitchenApplicationAddition.Classes.Messages;
using KitchenApplicationAddition.Classes;

namespace KitchenApplicationAddition
{
    public partial class Form1 : Form
    {
        private List<FoodOrder> orderList = new List<FoodOrder>();

        public Form1()
        {
            InitializeComponent();
            MessageManager.ServerStatusChangedDelegate += OnServerStatusChanged;
            MessageManager.OnOrderUpdatedMessageRecieved += OnOrderUpdateMessageRecieved;
        }

        private void OnOrderUpdateMessageRecieved(MessageBase Message)
        {
            OrderUpdateMessage orderUpdatedMessage = (OrderUpdateMessage)Message;
            if (this.TableOrderListBox1.InvokeRequired)
            {
                // If the current thread is not the UI thread, invoke the method on the UI thread.
                this.TableOrderListBox1.Invoke(new Action(() =>
                {
                    this.TableOrderListBox1.DataSource = orderUpdatedMessage.FullOrder.Order;
                }));
            }
            if (this.Order1Table.InvokeRequired)
            {
                this.Order1Table.Invoke(new Action(() =>
                {
                    this.Order1Table.Text = orderUpdatedMessage.FullOrder.TableNumber.ToString();
                }));
            }
        }


        private void ConnectToServerBtn_Click(object sender, EventArgs e)
        {
            ConnecToServer connectToServerForm = new ConnecToServer();
            connectToServerForm.ShowDialog();
        }


        private void OnServerStatusChanged(bool ServerStatus)
        {
            if (ServerStatusBtn.IsDisposed) return;
            if (ServerStatus)
            {
                ServerStatusBtn.Invoke(new MethodInvoker(delegate { ServerStatusBtn.Text = "Server: Connected"; }));
                ServerStatusBtn.Invoke(new MethodInvoker(delegate { ServerStatusBtn.BackColor = Color.Green; }));
                Console.WriteLine("You have been connected to the server!");

            }
            else
            {
                //ServerStatusBtn.Invoke(new MethodInvoker(delegate { ServerStatusBtn.Text = "Server: Disconnected"; }));
                //ServerStatusBtn.Invoke(new MethodInvoker(delegate { ServerStatusBtn.BackColor = Color.Red; }));
            }
        }

        private void UpdateOrder1()
        {
        }

        private void Order1CompleteBtn_Click(object sender, EventArgs e)
        {
            this.TableOrderListBox1.DataSource = null;
            this.Order1Table.Text = null;
        }
    }
}
