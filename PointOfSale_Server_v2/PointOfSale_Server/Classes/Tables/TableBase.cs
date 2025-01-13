using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using PointOfSale_Server.Classes.Managers;
using PointOfSale_Server.Classes.Messages;
using System.Net.Sockets;
using System.Threading;

namespace PointOfSale_Server.Classes
{
    public enum TableStatus
    {
        TABLE_OPEN,
        TABLE_OCCUPIED,
        FOOD_COOKING, //Kitchen Application
        ORDER_PLACED,
        FOOD_DELIVERED,
        READY_FOR_PAYMENT
    }

    public class TableBase
    {
        public int TableNumber { get; private set; }

        public FoodOrder Order = null;

        protected bool IsSelected = false;

        public TableStatus CurrentStatus;

        public Button TableButton;

        protected System.Windows.Forms.Timer TenSecondTimer;

        protected Bitmap TableOpen, TableOrdered, TableWaiting, TableFoodDelivered, TablePaymentReady, TableSelected, TableOccupied;

        public TableBase(int TableNum, Button InTableButton)
        {
            TableNumber = TableNum;
            this.TableButton = InTableButton;
            CurrentStatus = TableStatus.TABLE_OPEN;
            SetupImages();
            TenSecondTimer = new System.Windows.Forms.Timer();
            TenSecondTimer.Interval = 10000;
            TenSecondTimer.Stop();
            TenSecondTimer.Tick += OnTimerTick;
        }

        virtual protected void SetupImages()
        {
        }

        public void SelectTable()
        {
            TableButton.BackgroundImage = TableSelected;
        }

        public void UnselectTable()
        {
            UpdateStatus();
        }

        public void SeatCustomer()
        {
            CurrentStatus = TableStatus.TABLE_OCCUPIED;
            UpdateStatus();
        }

        public void PlaceOrder(FoodOrder InOrder)
        {
            Order = InOrder;
            CurrentStatus = TableStatus.ORDER_PLACED;
            UpdateStatus();

            OrderUpdateMessage orderUpdateMessage = new OrderUpdateMessage(Order);
            MessageManager.SendMessageToAllClients(orderUpdateMessage.BuildMessage());
        }

        //Kitchen Application
        public void FoodCooking()
        {
            CurrentStatus = TableStatus.FOOD_COOKING;
            UpdateStatus();
        }

        public void DeliverFood()
        {
            CurrentStatus = TableStatus.FOOD_DELIVERED;
            UpdateStatus();
            TenSecondTimer.Start();
        }

        public void PaymentAccepted()
        {
            SaleManager.AddOrder(Order);
            CurrentStatus = TableStatus.TABLE_OPEN;
            UpdateStatus();
            Order = null;
        }

        public void UpdateStatus()
        {
            switch(CurrentStatus)
            {
                case TableStatus.TABLE_OPEN:
                    TableButton.BackgroundImage = TableOpen;
                    break;
                case TableStatus.TABLE_OCCUPIED:
                    TableButton.BackgroundImage = TableOccupied;
                    break;
                case TableStatus.FOOD_COOKING:    //Kitchen Application
                    TableButton.BackgroundImage = TableWaiting;
                    break;
                case TableStatus.ORDER_PLACED:
                    TableButton.BackgroundImage = TableOrdered;
                    break;
                case TableStatus.FOOD_DELIVERED:
                    TableButton.BackgroundImage = TableFoodDelivered;
                    break;
                case TableStatus.READY_FOR_PAYMENT:
                    TableButton.BackgroundImage = TablePaymentReady;
                    break;
            }

            UpdateTableStatusMessage TableStatusMessage = new UpdateTableStatusMessage(TableNumber, CurrentStatus);
            MessageManager.SendMessageToAllClients(TableStatusMessage.BuildMessage());
        }

        public void SendStatusUpdateToClient(Socket Client)
        {
            UpdateTableStatusMessage TableStatusMessage = new UpdateTableStatusMessage(TableNumber, CurrentStatus);
            MessageManager.SendMessageToClient(TableStatusMessage.BuildMessage(), Client);

            if(Order != null)
            {
                Thread.Sleep(20);
                OrderUpdateMessage orderUpdateMessage = new OrderUpdateMessage(Order);
                MessageManager.SendMessageToClient(orderUpdateMessage.BuildMessage(), Client);
            }
        }

        public void ForceSetTableStatus(TableStatus NewStatus)
        {
            CurrentStatus = NewStatus;
            UpdateStatus();
        }

        protected void OnTimerTick(object sender, EventArgs e)
        {
            TenSecondTimer.Stop();
            CurrentStatus = TableStatus.READY_FOR_PAYMENT;
            UpdateStatus();
        }

        public void OnOrderChanged()
        {

        }

    }
}
