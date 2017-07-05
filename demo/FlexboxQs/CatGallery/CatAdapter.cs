using System;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;

namespace FlexboxQs.CatGallery
{
    public class CatAdapter : RecyclerView.Adapter
    {
        static int[] cat_res = new [] {
			Resource.Drawable.cat_1,
			Resource.Drawable.cat_2,
			Resource.Drawable.cat_3,
			Resource.Drawable.cat_4,
			Resource.Drawable.cat_5,
			Resource.Drawable.cat_6,
			Resource.Drawable.cat_7,
			Resource.Drawable.cat_8,
			Resource.Drawable.cat_9,
			Resource.Drawable.cat_10,
			Resource.Drawable.cat_11,
			Resource.Drawable.cat_12,
			Resource.Drawable.cat_13,
			Resource.Drawable.cat_14,
			Resource.Drawable.cat_15,
			Resource.Drawable.cat_16,
			Resource.Drawable.cat_17,
			Resource.Drawable.cat_18,
			Resource.Drawable.cat_19
        };
        Context context;

        public CatAdapter(Context context)
        {
            this.context = context;
        }

        public override int ItemCount => cat_res.Length * 4;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is CatViewHolder cvh) {
                var d = context.Resources.GetDrawable(cat_res[position % cat_res.Length], null);    
                cvh.Bind(d);
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemView = LayoutInflater.FromContext(parent.Context)
                                         .Inflate(Resource.Layout.viewholder_cat, parent, false);
            
            return new CatViewHolder(itemView);
        }
    }
}
