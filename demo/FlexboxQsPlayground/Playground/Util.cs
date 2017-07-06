using System;
using Android.Content;
using Android.Util;

namespace FlexboxQsPlayground.Playground
{
    public static class Util
    {
		/**
	   * Convert pixel to dp. Preserve the negative value as it's used for representing
	   * MATCH_PARENT(-1) and WRAP_CONTENT(-2).
	   * Ignore the round error that might happen in dividing the pixel by the density.
	   *
	   * @param context the context
	   * @param pixel   the value in pixel
	   * @return the converted value in dp
	   */
		public static int pixelToDp(Context context, int pixel)
		{
			DisplayMetrics displayMetrics = context.Resources.DisplayMetrics;
			return (int)(pixel < 0 ? pixel : Math.Round(pixel / displayMetrics.Density));
		}

		/**
		 * Convert dp to pixel. Preserve the negative value as it's used for representing
		 * MATCH_PARENT(-1) and WRAP_CONTENT(-2).
		 *
		 * @param context the context
		 * @param dp      the value in dp
		 * @return the converted value in pixel
		 */
		public static int dpToPixel(Context context, int dp)
		{
			DisplayMetrics displayMetrics = context.Resources.DisplayMetrics;
			return (int)(dp < 0 ? dp : Math.Round(dp * displayMetrics.Density));
		}
    }
}
