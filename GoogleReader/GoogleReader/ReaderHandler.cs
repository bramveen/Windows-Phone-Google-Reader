﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO;
using System.Threading;
using System.Windows.Threading;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Json;
using System.Collections.ObjectModel;
using System.Collections;


namespace GoogleReader
{
    public class ReaderHandler
    {
        private string _username = null;
        private string _password = null;
        private string _sid = null;
        private string _auth = null;
        private string _token = null;
        private Cookie _cookie = null;

        public SyndicationFeed ReadingList = new SyndicationFeed();
        public event InitiatedHandler Initiated;
        public delegate void InitiatedHandler(object sender, EventArgs e);
        public event ReadingListRefreshedHandler ReadingListRefreshed;
        public delegate void ReadingListRefreshedHandler(object sender, EventArgs e);

        
        public ReaderHandler(string username, string password)
        {
            _username = username;
            _password = password;
            getToken();
        }

        //
        public void GetReadingList()
        {
            ReaderCall("http://www.google.com/reader/atom/user/-/state/com.google/reading-list");
        }

        public void GetReadingList(Stream ItemFeed)
        {
                XmlReader r = XmlReader.Create(ItemFeed);
                ReadingList = SyndicationFeed.Load(r);
                ReadingListRefreshed(this, new EventArgs());
        }

        public void GetUnreadCount()
        {
            ReaderCall("http://www.google.com/reader/api/0/unread-count?all=true&output=json&ck=1255643091105&client=scrol");
        }

        public void GetUnreadCount(Stream UnreadFeed)
        {
            JsonObject obj = (JsonObject)JsonObject.Load(UnreadFeed);
            foreach (JsonObject js in obj["unreadcounts"])
            {
                 string id = js["id"];
                 int count = js["count"];
            }
        }

        public void GetSubscriptions()
        {
            ReaderCall("http://www.google.com/reader/api/0/subscription/list?output=json");
        }

        public void GetSubscriptions(Stream Subscriptions)
        {
            JsonObject obj = (JsonObject)JsonObject.Load(Subscriptions);
            foreach (JsonObject js in obj["subscriptions"])
            {
                Subscription sub = new Subscription();
                sub.id = js["id"];
                sub.label = js["label"];     
                
            }          
        }


        private void getToken()
        {
            string requestUrl = string.Format("https://www.google.com/accounts/ClientLogin?service=reader&Email={0}&Passwd={1}", _username, _password);
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(new Uri(requestUrl));
            req.Method = "GET";
            req.BeginGetResponse(new AsyncCallback(ReadCallback), req);
        }

        private void ReadCallback(IAsyncResult result)
        {
            HttpWebRequest request = (HttpWebRequest)result.AsyncState;
            HttpWebResponse respons = (HttpWebResponse)request.EndGetResponse(result);
            using (var stream = respons.GetResponseStream())
            {
                StreamReader r = new StreamReader(stream);
                string resp = r.ReadToEnd();

                int indexSid = resp.IndexOf("SID=") + 4;
                int indexLsid = resp.IndexOf("LSID=");
                int indexAuth = resp.IndexOf("Auth=") + 5;
                _auth = resp.Substring(indexAuth);
                _sid = resp.Substring(indexSid, indexLsid - 5);
            }

            _cookie = new Cookie("SID", _sid, "/", ".google.com");
            HttpWebRequest tokReq = (HttpWebRequest)WebRequest.Create("http://www.google.com/reader/api/0/token");
            tokReq.Method = "GET";
            tokReq.CookieContainer = new CookieContainer();
            string AuthHeader = string.Format("GoogleLogin auth={0}",_auth);
            tokReq.Headers["Authorization"] = AuthHeader;
            tokReq.CookieContainer.Add(new Uri("http://www.google.com"), _cookie);
            tokReq.BeginGetResponse(new AsyncCallback(tokCallback), tokReq);

        }

        private void tokCallback(IAsyncResult result)
        {
            HttpWebRequest request = (HttpWebRequest)result.AsyncState;
            HttpWebResponse respons = (HttpWebResponse)request.EndGetResponse(result);
            using (var stream = respons.GetResponseStream())
            {
                StreamReader r = new StreamReader(stream);
                _token = r.ReadToEnd();
            }
            Initiated(this, new EventArgs());
        }

        private void ReaderCall(string requestUrl)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(new Uri(requestUrl));
            req.CookieContainer = new CookieContainer();
            string AuthHeader = string.Format("GoogleLogin auth={0}", _auth);
            req.Headers["Authorization"] = AuthHeader;
            req.CookieContainer.Add(new Uri("http://www.google.com"), _cookie);
            req.BeginGetResponse(new AsyncCallback(ReaderCallBack), req);

        }

        private void ReaderCallBack(IAsyncResult result)
        {
            Stream sr = null;
            HttpWebRequest request = (HttpWebRequest)result.AsyncState;
            HttpWebResponse respons = (HttpWebResponse)request.EndGetResponse(result);
            sr = respons.GetResponseStream();
            switch (respons.ResponseUri.ToString())
            {
                case "http://www.google.com/reader/atom/user/-/state/com.google/reading-list":
                    GetReadingList(sr);
                    break;
                case "http://www.google.com/reader/api/0/unread-count?all=true&output=json&ck=1255643091105&client=scrol":
                    GetUnreadCount(sr);
                    break;
                case "http://www.google.com/reader/api/0/subscription/list?output=json":
                    GetSubscriptions(sr);
                    break;
            }

            
        }

        public class Subscription
        {
           public string id { get; set; }
           public string label { get; set; }
        }
        
     }
}
