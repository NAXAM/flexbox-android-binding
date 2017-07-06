using System;
using Android.Content;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V7.Preferences;
using Android.Views;
using Android.Widget;
using Com.Google.Android.Flexbox;

namespace FlexboxQsPlayground.Playground
{
    public class FragmentHelper
    {
        private static readonly String DEFAULT_WIDTH = "120";

        private static readonly String DEFAULT_HEIGHT = "80";

        private String ROW;

        private String COLUMN;

        private String ROW_REVERSE;

        private String COLUMN_REVERSE;

        private String NOWRAP;

        private String WRAP;

        private String WRAP_REVERSE;

        private String FLEX_START;

        private String FLEX_END;

        private String CENTER;

        private String BASELINE;

        private String STRETCH;

        private String SPACE_BETWEEN;

        private String SPACE_AROUND;

        private MainActivity mActivity;

        private IFlexContainer mFlexContainer;

        private ISharedPreferences mSharedPreferences;


       public FragmentHelper(MainActivity mainActivity, IFlexContainer flexContainer)
        {
            mActivity = mainActivity;
            mFlexContainer = flexContainer;
        }

        public void initializeViews()
        {
            mSharedPreferences = PreferenceManager.GetDefaultSharedPreferences(mActivity);
            initializeStringResources();
            NavigationView navigationView = (NavigationView)mActivity.FindViewById(Resource.Id.nav_view);
            if (navigationView != null)
            {
                navigationView.SetNavigationItemSelectedListener(mActivity);
                IMenu navigationMenu = navigationView.Menu;
                initializeFlexDirectionSpinner(navigationMenu);
                initializeFlexWrapSpinner(navigationMenu);
                initializeJustifyContentSpinner(navigationMenu);
                initializeAlignItemsSpinner(navigationMenu);
                initializeAlignContentSpinner(navigationMenu);
            }
        }

        private void initializeStringResources()
        {
            ROW = mActivity.GetString(Resource.String.row);
            COLUMN = mActivity.GetString(Resource.String.column);
            ROW_REVERSE = mActivity.GetString(Resource.String.row_reverse);
            COLUMN_REVERSE = mActivity.GetString(Resource.String.column_reverse);
            NOWRAP = mActivity.GetString(Resource.String.nowrap);
            WRAP = mActivity.GetString(Resource.String.wrap);
            WRAP_REVERSE = mActivity.GetString(Resource.String.wrap_reverse);
            FLEX_START = mActivity.GetString(Resource.String.flex_start);
            FLEX_END = mActivity.GetString(Resource.String.flex_end);
            CENTER = mActivity.GetString(Resource.String.center);
            BASELINE = mActivity.GetString(Resource.String.baseline);
            STRETCH = mActivity.GetString(Resource.String.stretch);
            SPACE_BETWEEN = mActivity.GetString(Resource.String.space_between);
            SPACE_AROUND = mActivity.GetString(Resource.String.space_around);
        }

        /**
		 * Sets the attributes for a {@link FlexItem} based on the stored default values in
		 * the SharedPreferences.
		 *
		 * @param flexItem the FlexItem instance
		 * @return a FlexItem instance, which attributes from the SharedPreferences are updated
		 */
        IFlexItem setFlexItemAttributes(IFlexItem flexItem)
        {
            flexItem.Width = (Util.dpToPixel(mActivity,
                    readPreferenceAsInteger(mActivity.GetString(Resource.String.new_width_key),
                            DEFAULT_WIDTH)));
            flexItem.Height = (Util.dpToPixel(mActivity,
                    readPreferenceAsInteger(mActivity.GetString(Resource.String.new_height_key),
                            DEFAULT_HEIGHT)));
            // Order is not supported in the FlexboxLayoutManager
            if (!(flexItem is FlexboxLayoutManager.LayoutParams))
            {
                flexItem.Order = (
                        readPreferenceAsInteger(mActivity.GetString(Resource.String.new_flex_item_order_key),
                                "1"));
            }
            flexItem.FlexGrow = (
                    readPreferenceAsFloat(mActivity.GetString(Resource.String.new_flex_grow_key), "0.0"));
            flexItem.FlexShrink = (
                    readPreferenceAsFloat(mActivity.GetString(Resource.String.new_flex_shrink_key), "1.0"));
            int flexBasisPercent = readPreferenceAsInteger(
                    mActivity.GetString(Resource.String.new_flex_basis_percent_key), "-1");
            flexItem.FlexBasisPercent = (
                    flexBasisPercent == -1 ? -1 : (float)(flexBasisPercent / 100.0));
            return flexItem;
        }

        private int readPreferenceAsInteger(String key, String defValue)
        {
            if (mSharedPreferences.Contains(key))
            {
                return int.Parse(mSharedPreferences.GetString(key, defValue));
            }
            else
            {
                return int.Parse(defValue);
            }
        }

        private float readPreferenceAsFloat(String key, String defValue)
        {
            if (mSharedPreferences.Contains(key))
            {
                return float.Parse(mSharedPreferences.GetString(key, defValue));
            }
            else
            {
                return float.Parse(defValue);
            }
        }

        private void initializeSpinner(
            int currentValue,
            int menuItemId,
            IMenu navigationMenu,
            int arrayResourceId,
            EventHandler<AdapterView.ItemSelectedEventArgs> itemSelected,
            Func<int, string> converter)
        {
            Spinner spinner = (Spinner)MenuItemCompat.GetActionView(navigationMenu.FindItem(menuItemId));
            ArrayAdapter adapter = ArrayAdapter.CreateFromResource(mActivity, arrayResourceId, Resource.Layout.spinner_item);
            spinner.Adapter = (adapter);
            spinner.ItemSelected -= itemSelected;
            spinner.ItemSelected += itemSelected;
            String selectedAsString = converter.Invoke(currentValue);
            int position = adapter.GetPosition(selectedAsString);
            spinner.SetSelection(position);
        }

        private void initializeFlexDirectionSpinner(IMenu navigationMenu)
        {
            initializeSpinner(
                mFlexContainer.FlexDirection,
                Resource.Id.menu_item_flex_direction,
                navigationMenu,
                Resource.Array.array_flex_direction,
                (object sender, AdapterView.ItemSelectedEventArgs e) =>
                {
                    int flexDirection = FlexDirection.Row;
                    var selected = e.Parent.GetItemAtPosition(e.Position).ToString();
                    if (selected.Equals(ROW))
                    {
                        flexDirection = FlexDirection.Row;
                    }
                    else if (selected.Equals(ROW_REVERSE))
                    {
                        flexDirection = FlexDirection.RowReverse;
                    }
                    else if (selected.Equals(COLUMN))
                    {
                        flexDirection = FlexDirection.Column;
                    }
                    else if (selected.Equals(COLUMN_REVERSE))
                    {
                        flexDirection = FlexDirection.ColumnReverse;
                    }
                    mFlexContainer.FlexDirection = (flexDirection);
                }, (arg) =>
                {
                    switch (arg)
                    {
                        case FlexDirection.Row:
                            return ROW;
                        case FlexDirection.RowReverse:
                            return ROW_REVERSE;
                        case FlexDirection.Column:
                            return COLUMN;
                        case FlexDirection.ColumnReverse:
                            return COLUMN_REVERSE;
                        default:
                            return ROW;
                    }
                }
            );
        }

        private void initializeFlexWrapSpinner(IMenu navigationMenu)
        {
            initializeSpinner(
                mFlexContainer.FlexWrap,
                Resource.Id.menu_item_flex_wrap,
                navigationMenu,
                Resource.Array.array_flex_wrap,
                (sender, e) =>
                {

                    int flexWrap = FlexWrap.Nowrap;
                    String selected = e.Parent.GetItemAtPosition(e.Position).ToString();
                    if (selected.Equals(NOWRAP))
                    {
                        flexWrap = FlexWrap.Nowrap;
                    }
                    else if (selected.Equals(WRAP))
                    {
                        flexWrap = FlexWrap.Wrap;
                    }
                    else if (selected.Equals(WRAP_REVERSE))
                    {
                        flexWrap = FlexWrap.WrapReverse;
                    }

                    if (mFlexContainer is FlexboxLayoutManager &&
                        flexWrap == FlexWrap.WrapReverse)
                    {
                        Toast.MakeText(mActivity,
                                Resource.String.wrap_reverse_not_supported,
                                       ToastLength.Short).Show();
                    }
                    else
                    {
                        mFlexContainer.FlexWrap = (flexWrap);
                    }
                },
                (arg) =>
            {
                switch (arg)
                {
                    case FlexWrap.Nowrap:
                        return NOWRAP;
                    case FlexWrap.Wrap:
                        return WRAP;
                    case FlexWrap.WrapReverse:
                        return WRAP_REVERSE;
                    default:
                        return NOWRAP;
                }
            });
        }

        private void initializeJustifyContentSpinner(IMenu navigationMenu)
        {
            initializeSpinner(
                mFlexContainer.JustifyContent,
                Resource.Id.menu_item_justify_content,
                navigationMenu,
                Resource.Array.array_justify_content,
                (sender, e) =>
                {

                    int justifyContent = JustifyContent.FlexStart;
                    String selected = e.Parent.GetItemAtPosition(e.Position).ToString();
                    if (selected.Equals(FLEX_START))
                    {
                        justifyContent = JustifyContent.FlexStart;
                    }
                    else if (selected.Equals(FLEX_END))
                    {
                        justifyContent = JustifyContent.FlexEnd;
                    }
                    else if (selected.Equals(CENTER))
                    {
                        justifyContent = JustifyContent.Center;
                    }
                    else if (selected.Equals(SPACE_BETWEEN))
                    {
                        justifyContent = JustifyContent.SpaceBetween;
                    }
                    else if (selected.Equals(SPACE_AROUND))
                    {
                        justifyContent = JustifyContent.SpaceAround;
                    }
                    mFlexContainer.JustifyContent = (justifyContent);
                }
                ,
                (arg) =>
            {
                switch (arg)
                {
                    case JustifyContent.FlexStart:
                        return FLEX_START;
                    case JustifyContent.FlexEnd:
                        return FLEX_END;
                    case JustifyContent.Center:
                        return CENTER;
                    case JustifyContent.SpaceAround:
                        return SPACE_AROUND;
                    case JustifyContent.SpaceBetween:
                        return SPACE_BETWEEN;
                    default:
                        return FLEX_START;
                }
            });
        }

        private void initializeAlignItemsSpinner(IMenu navigationMenu)
        {
            initializeSpinner(
                mFlexContainer.AlignItems,
                Resource.Id.menu_item_align_items,
                navigationMenu,
                Resource.Array.array_align_items,
                (sender, e) =>
                {

                    int alignItems = AlignItems.Stretch;
                    String selected = e.Parent.GetItemAtPosition(e.Position).ToString();
                    if (selected.Equals(FLEX_START))
                    {
                        alignItems = AlignItems.FlexStart;
                    }
                    else if (selected.Equals(FLEX_END))
                    {
                        alignItems = AlignItems.FlexEnd;
                    }
                    else if (selected.Equals(CENTER))
                    {
                        alignItems = AlignItems.Center;
                    }
                    else if (selected.Equals(BASELINE))
                    {
                        alignItems = AlignItems.Baseline;
                    }
                    else if (selected.Equals(STRETCH))
                    {
                        alignItems = AlignItems.Stretch;
                    }
                    mFlexContainer.AlignItems = (alignItems);
                }
                ,
                (arg) =>
            {
                switch (arg)
                {
                    case AlignItems.FlexStart:
                        return FLEX_START;
                    case AlignItems.FlexEnd:
                        return FLEX_END;
                    case AlignItems.Center:
                        return CENTER;
                    case AlignItems.Baseline:
                        return BASELINE;
                    case AlignItems.Stretch:
                        return STRETCH;
                    default:
                        return STRETCH;
                }

            });
        }

        private void initializeAlignContentSpinner(IMenu navigationMenu)
        {
            initializeSpinner(
                mFlexContainer.AlignContent,
                Resource.Id.menu_item_align_content,
                navigationMenu,
                Resource.Array.array_align_content,
                (sender, e) =>
            {
                int alignContent = AlignContent.Stretch;
                String selected = e.Parent.GetItemAtPosition(e.Position).ToString();
                if (selected.Equals(FLEX_START))
                {
                    alignContent = AlignContent.FlexStart;
                }
                else if (selected.Equals(FLEX_END))
                {
                    alignContent = AlignContent.FlexEnd;
                }
                else if (selected.Equals(CENTER))
                {
                    alignContent = AlignContent.Center;
                }
                else if (selected.Equals(SPACE_BETWEEN))
                {
                    alignContent = AlignContent.SpaceBetween;
                }
                else if (selected.Equals(SPACE_AROUND))
                {
                    alignContent = AlignContent.SpaceAround;
                }
                else if (selected.Equals(STRETCH))
                {
                    alignContent = AlignContent.Stretch;
                }

                if (mFlexContainer is FlexboxLayoutManager)
                {
                    Toast.MakeText(
                            mActivity,
                            Resource.String.align_content_not_supported,
                            ToastLength.Short).Show();
                }
                else
                {
                    mFlexContainer.AlignContent = (alignContent);
                }
            }
                ,
                arg =>

        {
            switch (arg)
            {
                case AlignContent.FlexStart:
                    return FLEX_START;
                case AlignContent.FlexEnd:
                    return FLEX_END;
                case AlignContent.Center:
                    return CENTER;
                case AlignContent.SpaceBetween:
                    return SPACE_BETWEEN;
                case AlignContent.SpaceAround:
                    return SPACE_AROUND;
                case AlignContent.Stretch:
                    return STRETCH;
                default:
                    return STRETCH;
            }
        });
        }

    }
}
