using System;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Com.Google.Android.Flexbox;
using Java.Lang;

namespace FlexboxQsPlayground.Playground
{
    public class FlexItemEditFragment : DialogFragment
    {
        private static readonly string FLEX_ITEM_KEY = "flex_item";

        private static readonly string VIEW_INDEX_KEY = "view_index";

        private string ALIGN_SELF_AUTO;

        private string ALIGN_SELF_FLEX_START;

        private string ALIGN_SELF_FLEX_END;

        private string ALIGN_SELF_CENTER;

        private string ALIGN_SELF_BASELINE;

        private string ALIGN_SELF_STRETCH;

        private int mViewIndex;

        private IFlexItem mFlexItem;

        /**
		 * Instance of a {@link FlexItem} being edited. At first it's created as another instance from
		 * the {@link #mFlexItem} because otherwise changes before clicking the ok button will be
		 * reflected if the {@link #mFlexItem} is changed directly.
		 */
        private IFlexItem mFlexItemInEdit;

        private Action<IFlexItem, int> mFlexItemChangedListener;

        private Context mContext;

        public static FlexItemEditFragment newInstance(IFlexItem flexItem, int viewIndex)
        {
            FlexItemEditFragment fragment = new FlexItemEditFragment();
            Bundle args = new Bundle();
            args.PutParcelable(FLEX_ITEM_KEY, flexItem);
            args.PutInt(VIEW_INDEX_KEY, viewIndex);
            fragment.Arguments = (args);
            return fragment;
        }

        public override void OnAttach(Context context)
        {
            base.OnAttach(context);
            mContext = context;
        }

        public override void OnDetach()
        {
            mContext = null;
            base.OnDetach();
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ;
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Honeycomb)
            {
                SetStyle(DialogFragment.StyleNormal, Android.Resource.Style.ThemeMaterialLightDialog);
            }
            else if (Build.VERSION.SdkInt >= BuildVersionCodes.Honeycomb)
            {
                SetStyle(DialogFragment.StyleNormal, Android.Resource.Style.ThemeHoloLightDialog);
            }
            else
            {
                SetStyle(DialogFragment.StyleNormal, Android.Resource.Style.ThemeDialog);
            }
            Bundle args = Arguments;
            mFlexItem = (IFlexItem)args.GetParcelable(FLEX_ITEM_KEY);
            mFlexItemInEdit = CreateNewFlexItem(mFlexItem);
            mViewIndex = args.GetInt(VIEW_INDEX_KEY);

            var activity = Activity;
            ALIGN_SELF_AUTO = activity.GetString(Resource.String.auto);
            ALIGN_SELF_FLEX_START = activity.GetString(Resource.String.flex_start);
            ALIGN_SELF_FLEX_END = activity.GetString(Resource.String.flex_end);
            ALIGN_SELF_CENTER = activity.GetString(Resource.String.center);
            ALIGN_SELF_BASELINE = activity.GetString(Resource.String.baseline);
            ALIGN_SELF_STRETCH = activity.GetString(Resource.String.stretch);
        }

        public override Android.Views.View OnCreateView(Android.Views.LayoutInflater inflater, Android.Views.ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment_flex_item_edit, container, false);
            Dialog.SetTitle((mViewIndex + 1).ToString());

            var orderTextInput = (TextInputLayout)view.FindViewById(Resource.Id.input_layout_order);
            var orderEdit = (EditText)view.FindViewById(Resource.Id.edit_text_order);
            orderEdit.Text = ((mFlexItem.Order).ToString());
            //orderEdit.addTextChangedListener(
            //new FlexEditTextWatcher(orderTextInput, new IntegerInputValidator(),
            //Resource.String.must_be_integer));
            if (mFlexItem is FlexboxLayoutManager.LayoutParams)
            {
                // Order is not enabled in FlexboxLayoutManager
                orderEdit.Enabled = (false);
            }

            TextInputLayout flexGrowInput = (TextInputLayout)view
                    .FindViewById(Resource.Id.input_layout_flex_grow);
            EditText flexGrowEdit = (EditText)view.FindViewById(Resource.Id.edit_text_flex_grow);
            flexGrowEdit.Text = ((mFlexItem.FlexGrow).ToString());
            //flexGrowEdit.addTextChangedListener(
            //new FlexEditTextWatcher(flexGrowInput, new NonNegativeDecimalInputValidator(),
            //Resource.String.must_be_non_negative_float));

            TextInputLayout flexShrinkInput = (TextInputLayout)view
                    .FindViewById(Resource.Id.input_layout_flex_shrink);
            EditText flexShrinkEdit = (EditText)view.FindViewById(
                    Resource.Id.edit_text_flex_shrink);
            flexShrinkEdit.Text = ((mFlexItem.FlexShrink).ToString());
            //flexShrinkEdit.addTextChangedListener(
            //new FlexEditTextWatcher(flexShrinkInput, new NonNegativeDecimalInputValidator(),
            //Resource.String.must_be_non_negative_float));

            TextInputLayout flexBasisPercentInput = (TextInputLayout)
                    view.FindViewById(Resource.Id.input_layout_flex_basis_percent);
            EditText flexBasisPercentEdit = (EditText)view.FindViewById(
                    Resource.Id.edit_text_flex_basis_percent);
            if (mFlexItem.FlexBasisPercent != FlexboxLayout.LayoutParams.InterfaceConsts.FlexBasisPercentDefault)
            {
                flexBasisPercentEdit.Text = ((System.Math.Round(mFlexItem.FlexBasisPercent * 100)).ToString());
            }
            else
            {
                flexBasisPercentEdit.Text = ((int)mFlexItem.FlexBasisPercent).ToString();
            }
            //flexBasisPercentEdit.addTextChangedListener(
            //new FlexEditTextWatcher(flexBasisPercentInput, new FlexBasisPercentInputValidator(),
            //Resource.String.must_be_minus_one_or_non_negative_integer));

            TextInputLayout widthInput = (TextInputLayout)view.FindViewById(Resource.Id.input_layout_width);
            EditText widthEdit = (EditText)view.FindViewById(Resource.Id.edit_text_width);
            widthEdit.Text = Util.pixelToDp(mContext, mFlexItem.Width).ToString();
            //widthEdit.addTextChangedListener(
            //new FlexEditTextWatcher(widthInput, new DimensionInputValidator(),
            //Resource.String.must_be_minus_one_or_minus_two_or_non_negative_integer));

            TextInputLayout heightInput = (TextInputLayout)view.FindViewById(Resource.Id.input_layout_height);
            EditText heightEdit = (EditText)view.FindViewById(Resource.Id.edit_text_height);
            heightEdit.Text = (Util.pixelToDp(mContext, mFlexItem.Height)).ToString();
            //heightEdit.addTextChangedListener(
            //new FlexEditTextWatcher(heightInput, new DimensionInputValidator(),
            //Resource.String.must_be_minus_one_or_minus_two_or_non_negative_integer));

            TextInputLayout minWidthInput = (TextInputLayout)view
                    .FindViewById(Resource.Id.input_layout_min_width);
            EditText minWidthEdit = (EditText)view.FindViewById(Resource.Id.edit_text_min_width);
            minWidthEdit.Text = ((Util.pixelToDp(mContext, mFlexItem.MinWidth))).ToString();
            //minWidthEdit.addTextChangedListener(
            //new FlexEditTextWatcher(minWidthInput, new FixedDimensionInputValidator(),
            //Resource.String.must_be_non_negative_integer));

            TextInputLayout minHeightInput = (TextInputLayout)view.FindViewById(Resource.Id.input_layout_min_height);
            EditText minHeightEdit = (EditText)view.FindViewById(Resource.Id.edit_text_min_height);
            minHeightEdit.Text = ((Util.pixelToDp(mContext, mFlexItem.MinHeight))).ToString();
            //minHeightEdit.addTextChangedListener(
            //new FlexEditTextWatcher(minHeightInput, new FixedDimensionInputValidator(),
            //Resource.String.must_be_non_negative_integer));

            TextInputLayout maxWidthInput = (TextInputLayout)view.FindViewById(Resource.Id.input_layout_max_width);
            EditText maxWidthEdit = (EditText)view.FindViewById(Resource.Id.edit_text_max_width);
            maxWidthEdit.Text = (Util.pixelToDp(mContext, mFlexItem.MaxWidth)).ToString();
            //maxWidthEdit.addTextChangedListener(
            //new FlexEditTextWatcher(maxWidthInput, new FixedDimensionInputValidator(),
            //Resource.String.must_be_non_negative_integer));

            TextInputLayout maxHeightInput = (TextInputLayout)view.FindViewById(Resource.Id.input_layout_max_height);
            EditText maxHeightEdit = (EditText)view.FindViewById(Resource.Id.edit_text_max_height);
            maxHeightEdit.Text = (Util.pixelToDp(mContext, mFlexItem.MaxHeight)).ToString();
            //maxHeightEdit.addTextChangedListener(
            //new FlexEditTextWatcher(maxHeightInput, new FixedDimensionInputValidator(),
            //Resource.String.must_be_non_negative_integer));

            setNextFocusesOnEnterDown(orderEdit, flexGrowEdit, flexShrinkEdit, flexBasisPercentEdit,
                    widthEdit, heightEdit, minWidthEdit, minHeightEdit, maxWidthEdit, maxHeightEdit);

            Spinner alignSelfSpinner = (Spinner)view.FindViewById(Resource.Id.spinner_align_self);
            ArrayAdapter arrayAdapter = ArrayAdapter.CreateFromResource(Activity, Resource.Array.array_align_self, Resource.Layout.spinner_item);
            alignSelfSpinner.Adapter = (arrayAdapter);
            //    alignSelfSpinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            //    @Override


            //    public void onItemSelected(AdapterView<?> parent, View view, int position, long id)
            //    {
            //        String selected = parent.getItemAtPosition(position).toString();
            //        if (selected.equals(ALIGN_SELF_AUTO))
            //        {
            //            mFlexItemInEdit.setAlignSelf(AlignSelf.AUTO);
            //        }
            //        else if (selected.equals(ALIGN_SELF_FLEX_START))
            //        {
            //            mFlexItemInEdit.setAlignSelf(AlignItems.FLEX_START);
            //        }
            //        else if (selected.equals(ALIGN_SELF_FLEX_END))
            //        {
            //            mFlexItemInEdit.setAlignSelf(AlignItems.FLEX_END);
            //        }
            //        else if (selected.equals(ALIGN_SELF_CENTER))
            //        {
            //            mFlexItemInEdit.setAlignSelf(AlignItems.CENTER);
            //        }
            //        else if (selected.equals(ALIGN_SELF_BASELINE))
            //        {
            //            mFlexItemInEdit.setAlignSelf(AlignItems.BASELINE);
            //        }
            //        else if (selected.equals(ALIGN_SELF_STRETCH))
            //        {
            //            mFlexItemInEdit.setAlignSelf(AlignItems.STRETCH);
            //        }
            //    }

            //    @Override


            //    public void onNothingSelected(AdapterView<?> parent)
            //    {
            //        // No op
            //    }
            //});

            CheckBox wrapBeforeCheckBox = (CheckBox)view.FindViewById(Resource.Id.checkbox_wrap_before);
            wrapBeforeCheckBox.Checked = (mFlexItem.WrapBefore);
            //    wrapBeforeCheckBox.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            //        @Override
            //        public void onCheckedChanged(CompoundButton buttonView, boolean isChecked)
            //    {
            //        mFlexItemInEdit.setWrapBefore(isChecked);
            //    }
            //});
            int alignSelfPosition = arrayAdapter.GetPosition(alignSelfAsString(mFlexItem.AlignSelf));
            alignSelfSpinner.SetSelection(alignSelfPosition);

            //        view.FindViewById(Resource.Id.button_cancel).setOnClickListener(new View.OnClickListener() {
            //            @Override
            //            public void onClick(View v)
            //    {
            //        copyFlexItemValues(mFlexItem, mFlexItemInEdit);
            //        dismiss();
            //    }
            //});
            //        Button okButton = (Button) view.FindViewById(Resource.Id.button_ok);
            //okButton.setOnClickListener(new View.OnClickListener() {
            //            @Override
            //            public void onClick(View v)
            //{
            //    if (orderTextInput.isErrorEnabled() || flexGrowInput.isErrorEnabled() ||
            //            flexBasisPercentInput.isErrorEnabled() || widthInput.isErrorEnabled() ||
            //            heightInput.isErrorEnabled() || minWidthInput.isErrorEnabled() ||
            //            minHeightInput.isErrorEnabled() || maxWidthInput.isErrorEnabled() ||
            //            maxHeightInput.isErrorEnabled())
            //    {
            //        Toast.makeText(getActivity(), Resource.String.invalid_values_exist, Toast.LENGTH_SHORT)
            //                .show();
            //        return;
            //    }
            //    if (mFlexItemChangedListener != null)
            //    {
            //        copyFlexItemValues(mFlexItemInEdit, mFlexItem);
            //        mFlexItemChangedListener.onFlexItemChanged(mFlexItem, mViewIndex);
            //    }
            //    dismiss();
            //}
            //});
            return view;
        }

        public void setFlexItemChangedListener(Action<IFlexItem, int> flexItemChangedListener)
        {
            mFlexItemChangedListener = flexItemChangedListener;
        }

        private void setNextFocusesOnEnterDown(params TextView[] textViews)
        {
            // This can be done by setting android:nextFocus* as in 
            // https://developer.android.com/training/keyboard-input/navigation.html
            // But it requires API level 11 as a minimum sdk version. To support the lower level
            // devices,
            // doing it programmatically.
            for (int i = 0; i < textViews.Length; i++)
            {
                int index = i;
                //        textViews[index].setOnEditorActionListener(new TextView.OnEditorActionListener() {
                //                @Override


                //                public boolean onEditorAction(TextView v, int actionId, KeyEvent event)
                //        {
                //            if (actionId == EditorInfo.IME_ACTION_NEXT ||
                //                    actionId == EditorInfo.IME_ACTION_DONE ||
                //                    (actionId == EditorInfo.IME_NULL
                //                            && event.getAction() == KeyEvent.ACTION_DOWN
                //                            && event.getKeyCode() == KeyEvent.KEYCODE_ENTER)) {
                //        if (index + 1 < textViews.length)
                //        {
                //            textViews[index + 1].requestFocus();
                //        }
                //        else if (index == textViews.length - 1)
                //        {
                //            InputMethodManager inputMethodManager
                //                    = (InputMethodManager)getActivity()
                //                    .getSystemService(Context.INPUT_METHOD_SERVICE);
                //            inputMethodManager.hideSoftInputFromWindow(v.getWindowToken(), 0);
                //        }
                //    }
                //    return true;
                //}
                //});

                //            // Suppress the key focus change by KeyEvent.ACTION_UP of the enter key
                //            textViews[index].setOnKeyListener(new View.OnKeyListener() {
                //                @Override
                //                public boolean onKey(View v, int keyCode, KeyEvent event)
                //{
                //    return keyCode == KeyEvent.KEYCODE_ENTER
                //            && event.getAction() == KeyEvent.ACTION_UP;
                //}
                //});
            }

        }

        private string alignSelfAsString(int alignSelf)
        {
            switch (alignSelf)
            {
                case AlignSelf.Auto:
                    return ALIGN_SELF_AUTO;
                case AlignItems.FlexStart:
                    return ALIGN_SELF_FLEX_START;
                case AlignItems.FlexEnd:
                    return ALIGN_SELF_FLEX_END;
                case AlignItems.Center:
                    return ALIGN_SELF_CENTER;
                case AlignItems.Baseline:
                    return ALIGN_SELF_BASELINE;
                case AlignItems.Stretch:
                    return ALIGN_SELF_STRETCH;
                default:
                    return ALIGN_SELF_AUTO;
            }
        }

        //private class FlexEditTextWatcher implements TextWatcher
        //{

        //    TextInputLayout mTextInputLayout;

        //    InputValidator mInputValidator;

        //        int mErrorMessageId;

        //    FlexEditTextWatcher(TextInputLayout textInputLayout,
        //                InputValidator inputValidator, int errorMessageId) {
        //        mTextInputLayout = textInputLayout;
        //        mInputValidator = inputValidator;
        //        mErrorMessageId = errorMessageId;
        //    }

        //    @Override


        //        public void beforeTextChanged(CharSequence s, int start, int count, int after)
        //{
        //    // No op
        //}

        //@Override
        //        public void onTextChanged(CharSequence s, int start, int before, int count)
        //{
        //    if (mInputValidator.isValidInput(s))
        //    {
        //        mTextInputLayout.setErrorEnabled(false);
        //        mTextInputLayout.setError("");
        //    }
        //    else
        //    {
        //        mTextInputLayout.setErrorEnabled(true);
        //        mTextInputLayout.setError(getActivity().getResources()
        //                .GetString(mErrorMessageId));
        //    }
        //}

        //@Override
        //        public void afterTextChanged(Editable editable)
        //{
        //if (mTextInputLayout.isErrorEnabled() || TextUtils.isEmpty(editable) ||
        //        !mInputValidator.isValidInput(editable.toString()))
        //{
        //    return;
        //}
        //Number value;
        //switch (mTextInputLayout.getId())
        //{
        //    case Resource.Id.input_layout_flex_grow:
        //    case Resource.Id.input_layout_flex_shrink:
        //        try
        //        {
        //            value = Float.valueOf(editable.toString());
        //        }
        //        catch (NumberFormatException | NullPointerException ignore) {
        //            return;
        //        }
        //        break;
        //        default:
        //                try
        //        {
        //            value = Integer.valueOf(editable.toString());
        //        }
        //        catch (NumberFormatException | NullPointerException ignore) {
        //            return;
        //        }
        //        }

        //        switch (mTextInputLayout.Id)
        //        {
        //            case Resource.Id.input_layout_order:
        //                if (!(mFlexItemInEdit instanceof FlexboxLayoutManager.LayoutParams)) {
        //                    mFlexItemInEdit.setOrder(value.intValue());
        //                }
        //                break;
        //            case Resource.Id.input_layout_flex_grow:
        //                mFlexItemInEdit.setFlexGrow(value.floatValue());
        //                break;
        //            case Resource.Id.input_layout_flex_shrink:
        //                mFlexItemInEdit.setFlexShrink(value.floatValue());
        //                break;
        //            case Resource.Id.input_layout_width:
        //                mFlexItemInEdit.setWidth(Util.dpToPixel(mContext, value.intValue()));
        //                break;
        //            case Resource.Id.input_layout_height:
        //                mFlexItemInEdit.setHeight(Util.dpToPixel(mContext, value.intValue()));
        //                break;
        //            case Resource.Id.input_layout_flex_basis_percent:
        //                if (value.intValue() != FlexboxLayout.LayoutParams.FLEX_BASIS_PERCENT_DEFAULT)
        //                {
        //                    mFlexItemInEdit.setFlexBasisPercent((float)(value.intValue() / 100.0));
        //                }
        //                else
        //                {
        //                    mFlexItemInEdit.setFlexBasisPercent(FlexItem.FLEX_BASIS_PERCENT_DEFAULT);
        //                }
        //                break;
        //            case Resource.Id.input_layout_min_width:
        //                mFlexItemInEdit.setMinWidth(Util.dpToPixel(mContext, value.intValue()));
        //                break;
        //            case Resource.Id.input_layout_min_height:
        //                mFlexItemInEdit.setMinHeight(Util.dpToPixel(mContext, value.intValue()));
        //                break;
        //            case Resource.Id.input_layout_max_width:
        //                mFlexItemInEdit.setMaxWidth(Util.dpToPixel(mContext, value.intValue()));
        //                break;
        //            case Resource.Id.input_layout_max_height:
        //                mFlexItemInEdit.setMaxHeight(Util.dpToPixel(mContext, value.intValue()));
        //                break;
        //        }
        //        }
        //}

        private IFlexItem CreateNewFlexItem(IFlexItem item)
        {
            if (item is FlexboxLayout.LayoutParams)
            {
                IFlexItem newItem = new FlexboxLayout.LayoutParams(item.Width, item.Height);
                copyFlexItemValues(item, newItem);
                return newItem;
            }
            else if (item is FlexboxLayoutManager.LayoutParams)
            {
                IFlexItem newItem = new FlexboxLayoutManager.LayoutParams(item.Width,
                                                                         item.Height);
                copyFlexItemValues(item, newItem);
                return newItem;
            }
            throw new IllegalArgumentException("Unknown FlexItem: " + item);
        }

        private void copyFlexItemValues(IFlexItem @from, IFlexItem to)
        {
            if (!(@from is FlexboxLayoutManager.LayoutParams))
            {
                to.Order = @from.Order;
            }
            to.FlexGrow = @from.FlexGrow;
            to.FlexShrink = @from.FlexShrink;
            to.FlexBasisPercent = @from.FlexBasisPercent;
            to.Height = @from.Height;
            to.Width = @from.Width;
            to.MaxHeight = @from.MaxHeight;
            to.MinHeight = @from.MinHeight;
            to.MaxWidth = @from.MaxWidth;
            to.MinWidth = @from.MinWidth;
            to.AlignSelf = @from.AlignSelf;
            to.WrapBefore = @from.WrapBefore;
        }
    }
}
