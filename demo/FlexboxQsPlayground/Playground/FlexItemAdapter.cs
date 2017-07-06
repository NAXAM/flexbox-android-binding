using System;
using System.Collections.Generic;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Com.Google.Android.Flexbox;

namespace FlexboxQsPlayground.Playground
{
    public class FlexItemAdapter : RecyclerView.Adapter
    {
        readonly AppCompatActivity activity;
        readonly FlexboxLayoutManager layoutManager;

        List<FlexboxLayoutManager.LayoutParams> mLayoutParams;

        public FlexItemAdapter(AppCompatActivity activity, FlexboxLayoutManager layoutManager)
        {
            this.layoutManager = layoutManager;
            this.activity = activity;

            mLayoutParams = new List<FlexboxLayoutManager.LayoutParams>(0);
        }

        public override int ItemCount => mLayoutParams.Count;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is FlexItemViewHolder vh) {
                //TODO click event on itemView

                vh.Bind(mLayoutParams[position]);
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.FromContext(activity)
                                         .Inflate(Resource.Layout.viewholder_flex_item, parent, false);

            return new FlexItemViewHolder(itemView);
        }
    }
}
