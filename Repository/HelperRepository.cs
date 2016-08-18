using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UpDown.Models;
using System.Xml;
using System.Web.Script.Serialization;
using System.Net.Mail;
using System.Net;

namespace UpDown.Repository
{
    public interface IHelperRepository
    {
        void SetSearchItems();
        string GetSearchItems();
        void SendMail(string body);
    }


    public class HelperRepository: BaseRepository, IHelperRepository
    {
        //private DB_84979_udsEntities1 _db;

        //public HelperRepository()
        //{
        //    _db = new DB_84979_udsEntities1();
        //}

        public void SetSearchItems()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Content/xml/search.xml";
            DateTime lastModified = System.IO.File.GetLastWriteTime(path);

            if (lastModified < DateTime.Now.AddHours(-24))
            {
                List<string> SearchItems = new List<string>();
                SearchItems.AddRange(_db.products.Select(x => x.name));
                SearchItems.AddRange(_db.formats.Select(x => x.name));
                SearchItems.AddRange(_db.SubGenres.Select(x => x.subGenreName));
                SearchItems.AddRange(_db.instruments.Select(x => x.name));
                SearchItems.AddRange(_db.effects.Select(x => x.name));

                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(SearchItems);
                System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "Content/data/search.json", json);   
                //string searchStr = "<items>";
                //foreach (string item in SearchItems)
                //{
                //    searchStr += "<item>" + item + "</item>";
                //}
                //searchStr += "</items>";
                //XmlDocument doc = new XmlDocument();
                //doc.LoadXml(searchStr);
                //doc.Save(path);
            }
        }


        public string GetSearchItems()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Content/data/search.json";
            string json = System.IO.File.ReadAllText(path);
            return json;
        }

        public void SendMail(string body)
        {
            string attachFile = "";
            string attachFile2 = "";
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress("updownsound1@gmail.com");
                message.To.Add(new MailAddress("updownsound1@gmail.com"));
                message.Subject = "Cooperation message";
                message.Body = body;
                message.IsBodyHtml = true;

                SmtpClient client = new SmtpClient();
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("updownsound1@gmail.com", "ud12341234");
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(message);
                message.Dispose();
                
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}