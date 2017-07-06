using System;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace FlexboxQsPlayground.Playground
{
    public class FlexItemViewHolder : RecyclerView.ViewHolder
    {
        TextView mTextView;
        
        public FlexItemViewHolder(View itemView) : base(itemView)
        {
            mTextView = itemView.FindViewById<TextView>(Resource.Id.textview);
        }

        public void Bind(RecyclerView.LayoutParams lp) {
            var pos = AdapterPosition;

            mTextView.Text = (pos + 1).ToString();
            mTextView.SetBackgroundResource(Resource.Drawable.flex_item_background);
            mTextView.Gravity = GravityFlags.Center;
            mTextView.LayoutParameters = lp;
        }
    }
}
