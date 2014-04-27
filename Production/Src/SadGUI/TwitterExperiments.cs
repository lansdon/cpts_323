using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SadGUI
{
    public class TwitterExperiments: ViewModelBase
    {
        public TwitterExperiments()
        {
            SpamCommand = new DelegateCommand(Spam);
        }

        public void Spam()
        {
            Twitterizer.SendTweet("SPAM!!!!!");
            Console.WriteLine(Twitterizer.GetTweetCount);
        }

        public ICommand SpamCommand { get; set; }
    }
}
