using System;
using System.Collections.Generic;
using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Com.Google.Android.Flexbox;

namespace FlexboxQsPlayground.Playground
{
    public class FlexboxLayoutFragment : Fragment
    {
        static readonly string FLEX_ITEMS_KEY = "flex_items_key";

        IFlexContainer mFlexContainer;

        private FlexboxLayoutFragment()
        {
        }

        public static FlexboxLayoutFragment newInstance()
        {
            return new FlexboxLayoutFragment();
        }

        public override Android.Views.View OnCreateView(Android.Views.LayoutInflater inflater, Android.Views.ViewGroup container, Android.OS.Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragment_flexboxlayout, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            var activity = (MainActivity)Activity;
            mFlexContainer = view.FindViewById<FlexboxLayout>(Resource.Id.flexbox_layout);

            var fragmentHelper = new FragmentHelper(activity, mFlexContainer);
            fragmentHelper.initializeViews();

            if (savedInstanceState != null) {
                var flexItems = savedInstanceState.GetParcelableArrayList(FLEX_ITEMS_KEY) ?? new List<IParcelable>(0);
                mFlexContainer.RemoveAllViews();

                for (int i = 0; i < flexItems.Count; i++)
                {
                    var textView = CreateBaseFlexItemTextView(activity, i);

                    textView.LayoutParameters = ((FlexboxLayout.LayoutParams)flexItems[i]);
                    mFlexContainer.AddView(textView);
                }
            } else {
                for (int i = 0; i < mFlexContainer.FlexItemCount; i++)
                {
                    
                }
            }
        }

        public override void OnSaveInstanceState(Android.OS.Bundle outState)
        {
            base.OnSaveInstanceState(outState);

            var cache = new List<IParcelable>(mFlexContainer.FlexItemCount);
            for (int i = 0; i < mFlexContainer.FlexItemCount; i++)
            {
                var view = mFlexContainer.GetFlexItemAt(i);
                cache.Add((IFlexItem)view.LayoutParameters);
            }

            outState.PutParcelableArrayList(FLEX_ITEMS_KEY, cache);
        }

        TextView CreateBaseFlexItemTextView(Context context, int index)
        {
            TextView textView = new TextView(context);
            textView.SetBackgroundResource(Resource.Drawable.flex_item_background);
            textView.Text = ((index + 1).ToString());
            textView.Gravity = (GravityFlags.Center);
            return textView;
        }
    }
}
