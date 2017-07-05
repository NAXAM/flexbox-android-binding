using System;
using Android.Graphics.Drawables;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Com.Google.Android.Flexbox;

namespace FlexboxQs.CatGallery
{
    public class CatViewHolder : RecyclerView.ViewHolder
    {
        ImageView imgCat;

        public CatViewHolder(View itemView) : base(itemView)
        {
            imgCat = itemView.FindViewById<ImageView>(Resource.Id.imageview);
        }

        public void Bind(Drawable drawable) {
            imgCat.SetImageDrawable(drawable);

            ViewGroup.LayoutParams lp = imgCat.LayoutParameters;

            if (lp is FlexboxLayoutManager.LayoutParams flp) {
                flp.FlexGrow = 1.0f;
            }
        }
    }
}
