using Hammock;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using TweetSharp;
using SadGUI;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Windows.Data;
using System.Windows;


namespace SadGUI
{
    public class ConsumerToken
    {
        public ConsumerToken() { /* Filler!!*/ }
        public string ConsumerKey { get; set; }
        public string consumerSecret { get; set; }
    }

    public class TweetInfo
    {
        public TweetInfo(string _Tweet = "", Stream _Img = null)
        {
            this.Text = _Tweet;
            this.Img = _Img;
        }
        public string Text { get; private set; }
        public Stream Img { get; private set; }
    }

    public class Twitterizer: ViewModelBase
    {
        private static Twitterizer instance;
        private TwitterService twitterService;
        private Queue<TweetInfo> Tweet_Que;
        public BackgroundWorker commandThread { get; set; }

        public static bool Active { private get; set; }

        private Twitterizer()
        {
            _Tweets = new ObservableCollection<string>();
            Tweet_Que = new Queue<TweetInfo>();
        }

        public static Twitterizer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Twitterizer();
                }
                return instance;
            }
        }

        public static void Init(string FilePath = "")
        {
            if (FilePath == "")
            {
                Console.WriteLine("ERROR!  No filepath specified!");
                return;
            }

            OAuthAccessToken AccessTokenAuth;
            ConsumerToken ClientUserToken;

            //FilePath = Path.Combine(Environment.CurrentDirectory, FilePath);

            if (File.Exists(FilePath))
            {
                using (var xml = new XmlTextReader(FilePath))
                {
                    XmlSerializer AccessToken = new XmlSerializer(typeof(OAuthAccessToken));
                    XmlSerializer UserToken = new XmlSerializer(typeof(ConsumerToken));

                    xml.ReadStartElement("Begin");

                    ClientUserToken = (ConsumerToken)UserToken.Deserialize(xml);
                    AccessTokenAuth = (OAuthAccessToken)AccessToken.Deserialize(xml);

                    xml.ReadEndElement();

                    xml.Close();
                }
            }
            else
            {
                Console.WriteLine("ERROR!  Could not open file: " + FilePath);
                return;
            }

            TwitterClientInfo twitterClientInfo = new TwitterClientInfo();
            twitterClientInfo.ConsumerKey = ClientUserToken.ConsumerKey;
            twitterClientInfo.ConsumerSecret = ClientUserToken.consumerSecret;

            Instance.twitterService = new TwitterService(twitterClientInfo);

            Instance.twitterService.AuthenticateWith(AccessTokenAuth.Token, AccessTokenAuth.TokenSecret);

            //Handle our threading.
            Instance.commandThread = new BackgroundWorker();
            Instance.commandThread.WorkerReportsProgress = false;
            Instance.commandThread.WorkerSupportsCancellation = false;
            Instance.commandThread.DoWork += new DoWorkEventHandler(ProcessQue);

            Application.Current.Dispatcher.Invoke(() => BindingOperations.EnableCollectionSynchronization(Instance._Tweets, new object()));
        }

        public static IEnumerator<string> GetEnumerator()
        {
            foreach(var Tweet in Instance._Tweets)
            {
                yield return Tweet;
            }
        }

        public ObservableCollection<string> _Tweets
        {
            get;
            set;
        }

        public static void SendTweet(string[] Words, Stream img = null)
        {
            Words = Words.Where(w => w != Words[0]).ToArray();
            string Tweet = string.Join(" ", Words);
            SendTweet(Tweet);
        }

        public static void SendTweet(string Tweet, Stream img = null)
        {
            if (Active == false)
            {
                return;
            }

            if (Instance.twitterService == null)
            {
                return;
            }

            Tweet = String.Format("[{0}]: {1}", DateTime.Now.ToString("hh:mm:ss:ffff"), Tweet);

            if (Tweet.Length > 140 || Tweet.Length <= 0)
            {
                Console.WriteLine("ERROR!  Your tweet must be between 1 and 140 characters!");
                return;
            }

            Instance.Tweet_Que.Enqueue(new TweetInfo(Tweet, img));

            RunBackgroundworkder();
        }

        public static int GetTweetCount
        {
            get
            {
                return Instance.Tweet_Que.Count();
            }
            set
            {
                Instance.OnPropertyChanged("GetTweetCount");
            }
        }

        private static void RunBackgroundworkder()
        {
            if (Instance.commandThread.IsBusy == false)
                Instance.commandThread.RunWorkerAsync();
        }

        public static void ProcessQue(object sender, DoWorkEventArgs args)
        {
            Console.WriteLine("In");

            //Instance.commandThread.DoWork += new DoWorkEventHandler(delegate(object o, DoWorkEventArgs args)
            //{
            //    BackgroundWorker b = o as BackgroundWorker;
                while (Instance.Tweet_Que.Count() > 0)
                {
                    var Tweet = Instance.Tweet_Que.Dequeue();
                    TwitterStatus Result = null;
                    if (Tweet.Img == null)
                    {
                        Result = Instance.twitterService.SendTweet(new SendTweetOptions { Status = Tweet.Text });
                    }
                    else
                    {
                        Dictionary<string, Stream> Images = new Dictionary<string, Stream> { { "MyPicture", Tweet.Img } };
                        SendTweetWithMediaOptions Options = new SendTweetWithMediaOptions();
                        Options.Status = Tweet.Text;
                        Options.Images = Images;

                        Result = Instance.twitterService.SendTweetWithMedia(Options);
                    }
                    if (Result != null)
                    {
                        Instance._Tweets.Insert(0, Tweet.Text);
                    }
                    else
                    {
                        Console.WriteLine("ERROR!  The tweet didn't send for some reason...");
                    }
                    Thread.Sleep(10);
                }
            //});

            // what to do when worker completes its task (notify the user)
            Console.WriteLine("Out");
        }
    }
}
