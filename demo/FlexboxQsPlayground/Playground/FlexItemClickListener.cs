 using System;
using Android.Support.V7.App;
using Android.Views;
using Com.Google.Android.Flexbox;

namespace FlexboxQsPlayground.Playground
{
    public class FlexItemClickListener : Java.Lang.Object, View.IOnClickListener
	{
		static readonly String EDIT_DIALOG_TAG = "edit_dialog_tag";

		private int mViewIndex;

		private AppCompatActivity mActivity;
		private Action<IFlexItem, int> mFlexItemChangedListener;

		public FlexItemClickListener(AppCompatActivity activity, Action<IFlexItem, int> listener,
			int viewIndex)
		{
			mActivity = activity;
			mFlexItemChangedListener = listener;
			mViewIndex = viewIndex;
		}

        public void OnClick(View v)
		{
			FlexItemEditFragment fragment = FlexItemEditFragment.newInstance((IFlexItem)v.LayoutParameters, mViewIndex);
			fragment.setFlexItemChangedListener(mFlexItemChangedListener);
			fragment.Show(mActivity.SupportFragmentManager, EDIT_DIALOG_TAG);
        }
    }
}
