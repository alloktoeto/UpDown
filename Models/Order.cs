using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UpDown.Models
{
    public class Order
    {
        // get infor of order
        public double GrossTotal { get; set; }//2.0
        public int InvoiceNumber { get; set; }//0
        public string PaymentStatus { get; set; }//"Completed"
        public string PayerFirstName { get; set; }//"Ilia"
        public double PaymentFee { get; set; }//0.37
        public string BusinessEmail { get; set; }//"updownsound1@gmail.com"
        public string PayerEmail { get; set; }//"gelman9@gmail.com"
        public string TxToken { get; set; }
        public string PayerLastName { get; set; }//"Gelman"
        public string ReceiverEmail { get; set; }//"updownsound1@gmail.com"
        public string ItemName { get; set; }
        public string Currency { get; set; }//"USD"
        public string TransactionId { get; set; }
        public string SubscriberId { get; set; }
        public string Custom { get; set; }
    }
}