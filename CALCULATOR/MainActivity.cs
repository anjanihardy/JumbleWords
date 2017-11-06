using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Views.InputMethods;
using Android.Views;
using Android.Content;


namespace CALCULATOR
{
    [Activity(Label = "CALCULATOR", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        RadioGroup RGCalc;
        EditText number1;
        EditText number2;
        TextView tvoutput;
        RatingBar myratingBar;
        TextView tvrating;
  
  
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RequestWindowFeature(WindowFeatures.NoTitle);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button calcBtn = FindViewById<Button>(Resource.Id.Calculate);
            number1 = FindViewById<EditText>(Resource.Id.no1);
            number2 = FindViewById<EditText>(Resource.Id.no2);
            RGCalc = FindViewById<RadioGroup>(Resource.Id.CalcSelect);


            tvoutput = FindViewById<TextView>(Resource.Id.tvOutput);

            calcBtn.Click += delegate { calcu(); };
            number1.Click += delegate { number1.Text = ""; };
            number2.Click += delegate { number2.Text = ""; };

         
            var sumbitButton = FindViewById<Button>(Resource.Id.button2);
         
          
            myratingBar  = FindViewById<RatingBar>(Resource.Id.ratingBar1);
            myratingBar.NumStars = 7;
            sumbitButton.Click += delegate { rate(); };
          
        }



        public void calcu()
        {
            RadioButton radioButton = FindViewById<RadioButton>(RGCalc.CheckedRadioButtonId);
            if (radioButton.Text == "Subtraction")
            {
                tvoutput.Text = sub(Convert.ToInt32(number1.Text), Convert.ToInt32(number2.Text)).ToString();
           
            }
            if (radioButton.Text == "Addition")
            {
                tvoutput.Text = sum(Convert.ToInt32(number1.Text), Convert.ToInt32(number2.Text)).ToString();

            }

            if (radioButton.Text == "Multiplication")
            {
                tvoutput.Text = multiply(Convert.ToInt32(number1.Text), Convert.ToInt32(number2.Text)).ToString();

            }

            if (radioButton.Text == "Division")
            {
                tvoutput.Text = divide(Convert.ToInt32(number1.Text), Convert.ToInt32(number2.Text)).ToString();

            }
        }

        public int sum(int x, int y)
        {
            return x + y;

        }

        public int sub(int x, int y)

        {
            return x - y;
        }


        public int multiply(int a, int s) => a * s;
        public int divide(int a, int s) => a / s;

        public void rate()
        {
            tvrating = FindViewById<TextView>(Resource.Id.textView3);
            tvrating.Text = myratingBar.Rating.ToString();
        }
      /*  public override bool OnTouchEvent(MotionEvent e)
        {
            InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
            imm.HideSoftInputFromWindow(WindowToken, 0);
            return base.OnTouchEvent(e);
        }*/
    } 
                                                                          
}                                                                                                                                   

