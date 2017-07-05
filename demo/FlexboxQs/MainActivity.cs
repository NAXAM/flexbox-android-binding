using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Com.Google.Android.Flexbox;
using FlexboxQs.CatGallery;

namespace FlexboxQs
{
    [Activity(Label = "FlexboxQs", MainLauncher = true, Icon = "@mipmap/ic_launcher", Theme = "@style/AppTheme.NoActionBar")]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            var lstCats = FindViewById<RecyclerView>(Resource.Id.recyclerview);
            var layout = new FlexboxLayoutManager(this);
            layout.FlexWrap = FlexWrap.Wrap;
            layout.FlexDirection = FlexDirection.Row;
            layout.AlignItems = AlignItems.Stretch;
            var adapter = new CatAdapter(this);

            lstCats.SetLayoutManager(layout);
            lstCats.SetAdapter(adapter);
        }
    }
}

