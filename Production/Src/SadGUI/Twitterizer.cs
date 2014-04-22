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


namespace Twitter
{
    public class ConsumerToken
    {
        public ConsumerToken() { /* Filler!!*/ }
        public string ConsumerKey { get; set; }
        public string consumerSecret { get; set; }
    }

    public class Twitterizer
    {
        private static Twitterizer instance;

        private TwitterService twitterService;

        private List<string> Tweets;

        public bool Active { private get; set; }

        private Twitterizer()
        {
            Tweets = new List<string>();
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

            Instance.Active = true;

            OAuthAccessToken AccessTokenAuth;
            ConsumerToken ClientUserToken;

            if (File.Exists(FilePath))
            {
                using (var xml = new XmlTextReader(Path.Combine(Environment.CurrentDirectory, FilePath)))
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
        }

        public static IEnumerator<string> GetEnumerator()
        {
            foreach(var Tweet in Instance.Tweets)
            {
                yield return Tweet;
            }
        }

        public static void SendTweet(string[] Words)
        {
            Words = Words.Where(w => w != Words[0]).ToArray();
            string Tweet = string.Join(" ", Words);
            SendTweet(Tweet);
        }

        public static void SendTweet(string Tweet)
        {
            if (Instance.Active == false)
            {
                return;
            }
            
            Tweet = String.Format("[{0}]: {1}", DateTime.Now.ToLongTimeString(), Tweet);

            if (Tweet.Length > 140 || Tweet.Length <= 0)
            {
                Console.WriteLine("ERROR!  Your tweet must be between 1 and 140 characters!");
                return;
            }

            TwitterStatus Result = Instance.twitterService.SendTweet(new SendTweetOptions { Status = Tweet });
            if (Result != null)
            {
                Instance.Tweets.Add(Tweet);
            }
            else
            {
                Console.WriteLine("ERROR!  The tweet didn't send for some reason...");
            }
        }

        public static void SendTweetWithMedia(string Tweet, Stream img)
        {
            if (Instance.Active == false)
            {
                return;
            }

            Tweet = String.Format("[{0}]: {1}", DateTime.Now.ToLongTimeString(), Tweet);

            if (Tweet.Length > 140 || Tweet.Length <= 0)
            {
                Console.WriteLine("ERROR!  Your tweet must be between 1 and 140 characters!");
                return;
            }

            //MemoryStream ms = new MemoryStream();
            //img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            //ms.Seek(0, SeekOrigin.Begin);
            Dictionary<string, Stream> Images = new Dictionary<string, Stream> { { "MyPicture", img } };
            SendTweetWithMediaOptions Options = new SendTweetWithMediaOptions();
            Options.Status = Tweet;
            Options.Images = Images;
            TwitterStatus Result = Instance.twitterService.SendTweetWithMedia(Options);
            if (Result != null)
            {
                Instance.Tweets.Add(Tweet);
            }
            else
            {
                Console.WriteLine("ERROR!  The tweet didn't send for some reason...");
            }
        }
    }
}
