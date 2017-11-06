using Android.App;
using Android.Widget;
using Android.OS;
using System;
using System.Collections;
using System.Text;
using Android.Media;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Android.Net;
using Android.Service;
using Android.Content;






namespace JumbleWords
{
	
    [Activity(Label = "JumbleWords", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        bool WordBankUpdated = false;
      
        int JumbleWordSequnence = 0;
        ArrayList WordBank;
        int scorecount=0;

        TextView jumble;
        TextView hint;
		MediaPlayer _player;
        HttpWebResponse r;



		protected override void OnCreate(Bundle savedInstanceState)
        {



            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
       
            jumble = FindViewById<TextView>(Resource.Id.myjumble);
            hint = FindViewById<TextView> (Resource.Id.hint);

			Button submitbutton = FindViewById<Button>(Resource.Id.scorebutton);



			var connectivityManager = (ConnectivityManager)Application.Context.GetSystemService(Context.ConnectivityService);
			var activeNetworkInfo = connectivityManager.ActiveNetworkInfo;
            bool isonlien = activeNetworkInfo.IsConnected;
            bool isWifi = activeNetworkInfo.Type == ConnectivityType.Wifi;


           





			WordBank = new ArrayList();

            WordMeaning myfirstwordmeaning;
            myfirstwordmeaning = new WordMeaning();
            myfirstwordmeaning.word = "Small";
            myfirstwordmeaning.meaning = "Chotu";

            WordBank.Add(myfirstwordmeaning);
            WordBank.Add(new WordMeaning("Small", "Chotu"));
            WordBank.Add(new WordMeaning("Bucket", "Balti"));
            WordBank.Add(new WordMeaning("name", "naam"));





          //  jumble.Text = x;
                jumble.Click += delegate {
                jumble.Text = $"Correct The Word ->  {jumbleme()}";
            };


			EditText answer = FindViewById<EditText>(Resource.Id.AnswerInput);

			answer.Click += delegate
            {
                answer.Text = "";
            };

     
			
			submitbutton.Click += async (object sender, EventArgs e) => {
				Scoreclick(sender, e);
			}; 
        }

       
       string jumbleme()
        {
			WordMeaning _wordMeaning = new WordMeaning();
            _wordMeaning = (WordMeaning)WordBank[JumbleWordSequnence];


            hint.Text = _wordMeaning.meaning;

            Char[] WordChar = _wordMeaning.word.ToCharArray(); 
            int _WordLength=  WordChar.Length;
            int CharCounter = 1;
            string finalJumbledWord="";
			ArrayList RandomIntegerList = new ArrayList();
          
            while (CharCounter <= _WordLength)
            {
				Random Jumbler = new Random();
                int JumblerInt = Jumbler.Next(0,_WordLength);
                if (!RandomIntegerList.Contains(JumblerInt))
                {
                   RandomIntegerList.Add(JumblerInt);

                    finalJumbledWord = finalJumbledWord + WordChar[JumblerInt].ToString();
                    CharCounter++;
                    if (finalJumbledWord == _wordMeaning.word)
					{

                        CharCounter = 1;
                        RandomIntegerList = null;
                        RandomIntegerList = new ArrayList();
                        finalJumbledWord = "";
					}
                }


            }


            return finalJumbledWord;
        }


        private async void  Scoreclick(object sender, EventArgs e)
        {

    
  
			TextView score = FindViewById<TextView>(Resource.Id.score);
         

			
			EditText answer = FindViewById<EditText>(Resource.Id.AnswerInput);
          
            string JumbleAnswer = answer.Text;
			if (JumbleWordSequnence >= WordBank.Count - 1)
			{
				JumbleWordSequnence = 0;
			}

            WordMeaning CurrentWordMeaningToJumble = new WordMeaning();
            CurrentWordMeaningToJumble = (WordMeaning)WordBank[JumbleWordSequnence];
           
            if (JumbleAnswer.ToLower() == CurrentWordMeaningToJumble.word.ToLower())
            {
                _player = MediaPlayer.Create(this, Resource.Raw.a);
                _player.Start();
                scorecount = scorecount + 1;
                JumbleWordSequnence  = JumbleWordSequnence + 1;
                CurrentWordMeaningToJumble = (WordMeaning)WordBank[JumbleWordSequnence];
				jumble.Text = $"GREAT!! NEXT WORD ->  {jumbleme()}";
                score.SetBackgroundColor((Android.Graphics.Color.Green));
				score.SetTextColor((Android.Graphics.Color.White));
                hint.Text = CurrentWordMeaningToJumble.meaning;
			
               

            }
            else
            {
                CurrentWordMeaningToJumble = (WordMeaning)WordBank[JumbleWordSequnence];
				_player = MediaPlayer.Create(this, Resource.Raw.c);
				_player.Start();
                jumble.Text = $"Sorry!! NEXT Hint ->  {jumbleme()}";
				score.SetBackgroundColor((Android.Graphics.Color.Red));
				score.SetTextColor((Android.Graphics.Color.White));
				hint.Text = CurrentWordMeaningToJumble.meaning;

            }

           
            score.Text = scorecount.ToString();
            if (WordBankUpdated == false)
            {
                string x = await Callweb().ConfigureAwait(false);

                WordBankUpdated = true;
            }
		}

		public async Task<string>  Callweb()
		{

            string url = "https://docs.google.com/spreadsheets/d/e/2PACX-1vSJ9hsU4J_VxpoDyVwz8dyd-AVrOFtJP0ZHfxVIcHch7SiD8W4DvJSvJ7CSGDdEXDl5tzRlbNLpEAlF/pub?gid=0&single=true&output=csv";
			Task<HttpWebResponse> l = Getdata(url);
		r= await l;
			System.IO.Stream dataStream = r.GetResponseStream();
			// Open the stream using a StreamReader for easy access.  
			StreamReader reader = new StreamReader(dataStream);
			// Read the content.  
			string responseFromServer = reader.ReadToEnd();

            string[] xline = responseFromServer.Split('\n');

            foreach (string x in xline)
            {
                string[] y = x.Split(',');


                WordBank.Add(new WordMeaning(y[0].ToString().Replace('\r',' ').TrimEnd(),y[1].ToString().Replace('\r', ' ').TrimEnd()));

            }

            return responseFromServer;

		}



		public async Task<HttpWebResponse> Getdata(string url)
		{

			string html = string.Empty;
			//string url = "https://docs.google.com/spreadsheets/d/e/2PACX-1vSbW9uernqKbuBlDxAslODpERm0K4GJ3QP2ORBDQ_14i5zeSKPBffoMB8J9zo7qKxJMPtKFQZELv34-/pub?gid=1909603269&single=true&output=csv";

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

			var response = await request.GetResponseAsync();
			return (HttpWebResponse)response;
		}

		public void csvtoarraylist(string x)
        {
            


        }
      
    }
    public class WordMeaning
    {
        public WordMeaning()
        {}
        public WordMeaning(string _word,string _meaning)
                {
            word = _word;
            meaning = _meaning;

                }

       public string word;
       public string meaning;

    }


    public static class myweb
    {

		public static async Task<string> SendRequest(this string url)
		{
			using (var wc = new WebClient())
			{
				var bytes = await wc.DownloadDataTaskAsync(url);
				using (var reader = new StreamReader(new MemoryStream(bytes)))
					return await reader.ReadToEndAsync();
			}
		}




      



	}

}

