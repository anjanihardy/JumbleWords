using Android.App;
using Android.Widget;
using Android.OS;

namespace HelloAnshu
{
    [Activity(Label = "HelloAnshu", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.worldbutton);


            TextView text1= FindViewById<TextView>(Resource.Id.textView2);
            EditText textentry = FindViewById<EditText>(Resource.Id.editText1);


            button.Click += delegate { text1.Text = $"Hello Anshu The Time is {System.DateTime.Now.ToString()} and you enterd {textentry.Text} " ; };
        }
    }
}

