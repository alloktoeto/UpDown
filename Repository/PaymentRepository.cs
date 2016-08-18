using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UpDown.Models;
using System.IO;
using System.Text;
using System.Web.Configuration;

namespace UpDown.Repository
{
    public class PaymentRepository: BaseRepository
    {
        public void SetTransaction(Order order, string tx)
        {
            transaction trans = new transaction();

            trans.BusinessEmail = order.BusinessEmail;
            trans.Currency = order.Currency;
            trans.Custom = order.Custom;
            trans.GrossTotal = (decimal)order.GrossTotal;
            trans.InvoiceNumber = order.InvoiceNumber;
            trans.ItemName = order.ItemName;
            trans.PayerEmail = order.PayerEmail;
            trans.PayerFirstName = order.PayerFirstName;
            trans.PayerLastName = order.PayerLastName;
            trans.PaymentFee = (decimal)order.PaymentFee;
            trans.PaymentStatus = order.PaymentStatus;
            trans.ReceiverEmail = order.ReceiverEmail;
            trans.SubscriberId = order.SubscriberId;
            trans.TransactionId = order.TransactionId;
            trans.CreateDate = DateTime.Now;
            trans.tx = tx;

            _db.transactions.AddObject(trans);
            _db.SaveChanges();
        }

        public void SetDownLoads(List<ProductViewModel> products, user user)
        {
            foreach (var product in products)
	        {
                foreach (int format in product.FormatsSelectedIdList)
                {

                    download dl = null;// new download();
                    dl = _db.downloads.SingleOrDefault(x => x.format == format && x.productId == product.Id && x.userID == user.userID);
                    if (dl!=null)
                    {
                        dl.count += 5;
                        dl.date = DateTime.Now;
                    }
                    else
                    {
                        dl = new download();
                        dl.date = DateTime.Now;
                        dl.count = 5;
                        dl.format = format;
                        dl.productId = product.Id;
                        dl.userID = user.userID;
                        dl.link = EncriptString("product=" + product.Id + "&format=" + format + "&date=" + dl.date + "&user=" + user.userID);
                        _db.downloads.AddObject(dl);
                    }
                    
                }
	        }
            _db.SaveChanges();
        }

        public List<download> GetDownloads()
        {
            return _db.downloads.ToList();
        }

        public void ReduceCountDownload(int downId)
        {
            var down = _db.downloads.FirstOrDefault(x => x.downloadID == downId);
            down.count--;
            _db.SaveChanges();
        }

        private string EncriptString(string plaintext)
        {
            string password = WebConfigurationManager.AppSettings["encryptKey"];
            //string plaintext = "qwerty";
            ////Console.WriteLine("Your encrypted string is:");
            string encryptedstring = StringCipher.Encrypt(plaintext, password);

            ////Console.WriteLine("Your decrypted string is:");
            //string decryptedstring = StringCipher.Decrypt(encryptedstring, password);
            return encryptedstring;
            
        }

        public string DecryptString(string encryptedstring)
        {
            string password = WebConfigurationManager.AppSettings["encryptKey"];
            string decryptedstring = StringCipher.Decrypt(encryptedstring, password);
            return decryptedstring;
        }
    }
}