//using System;
//using System.Collections;
//using Android.Support.V4.App;
//using Android.Support.V7.Widget;
//using Com.Google.Android.Flexbox;

//namespace FlexboxQsPlayground.Playground
//{
//    public class RecyclerViewFragment : Fragment
//    {

//        private static readonly String FLEX_ITEMS_KEY = "flex_items";

//        public static RecyclerViewFragment newInstance()
//        {
//            return new RecyclerViewFragment();
//        }

//        private FlexItemAdapter mAdapter;

//        public override Android.Views.View OnCreateView(Android.Views.LayoutInflater inflater, Android.Views.ViewGroup container, Android.OS.Bundle savedInstanceState)
//        {

//            return inflater.Inflate(Resource.Layout.fragment_recyclerview, container, false);
//        }

//        public override void OnViewCreated(Android.Views.View view, Android.OS.Bundle savedInstanceState)
//        {
//            base.OnViewCreated(view, savedInstanceState);

//            RecyclerView recyclerView = (RecyclerView)view.FindViewById(Resource.Id.recyclerview);
//            MainActivity activity = (MainActivity)Activity;
//             FlexboxLayoutManager flexboxLayoutManager = new FlexboxLayoutManager(activity);
//            recyclerView.SetLayoutManager(flexboxLayoutManager);
//            if (mAdapter == null)
//            {
//                mAdapter = new FlexItemAdapter(activity, flexboxLayoutManager);
//            }
//            recyclerView.SetAdapter(mAdapter);
//            if (savedInstanceState != null)
//            {
//                IList layoutParams = savedInstanceState.GetParcelableArrayList(FLEX_ITEMS_KEY);
//                for (int i = 0; i < layoutParams.Count; i++)
//                {
//                    mAdapter.Ad(layoutParams.get(i));
//                }
//                mAdapter.notifyDataSetChanged();
//            }
//             FragmentHelper fragmentHelper = new FragmentHelper(activity, flexboxLayoutManager);
//            fragmentHelper.InitializeViews();

//            FloatingActionButton addFab = (FloatingActionButton)activity.FindViewById(
//                    Resource.Id.add_fab);
//            if (addFab != null)
//            {
//                addFab.setOnClickListener(new View.OnClickListener() {
//                @Override


//                public void onClick(View view)
//                {
//                    FlexboxLayoutManager.LayoutParams lp = new FlexboxLayoutManager.LayoutParams(
//                            ViewGroup.LayoutParams.WRAP_CONTENT,
//                            ViewGroup.LayoutParams.WRAP_CONTENT);
//                    fragmentHelper.setFlexItemAttributes(lp);
//                    mAdapter.addItem(lp);
//                }
//            });
//        }
//        FloatingActionButton removeFab = (FloatingActionButton)activity.FindViewById(
//                Resource.Id.remove_fab);
//        if (removeFab != null) {
//            removeFab.setOnClickListener(new View.OnClickListener() {
//                @Override
//                public void onClick(View v)
//        {
//            if (mAdapter.getItemCount() == 0)
//            {
//                return;
//            }
//            mAdapter.removeItem(mAdapter.getItemCount() - 1);
//        }
//    });
//        }
//    }

//    @Override
//    public void onSaveInstanceState(Bundle outState)
//{
//    super.onSaveInstanceState(outState);
//    outState.putParcelableArrayList(FLEX_ITEMS_KEY, new ArrayList<>(mAdapter.getItems()));
//}
//    }
//}
