using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace TwitSearches.Activities
{
    [Activity(Label = "TagsListActivity")]
    public class TagsListActivity: Activity
    { 
        ListView listView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.tags);
            Context mContext = Android.App.Application.Context;
            AppPreferences ap = new AppPreferences(mContext);

            ap.saveTag("ira", "https://stackoverflow.com/questions/26668509/how-do-i-use-sharedpreferences-in-xamarin-android");
            ap.saveTag("ira1", "https://stackoverflow.com/questions/26668509/how-do-i-use-sharedpreferences-in-xamarin-android");
            var tags = ap.getAllTags().Keys.ToList();

            listView = FindViewById<ListView>(Resource.Id.List); // get reference to the ListView in the layout

            // populate the listview with data
            listView.Adapter =  new ArrayAdapter<string>(this, Resource.Layout.list_item, tags);
            //  listView.ItemClick += OnListItemClick;  // to be defined

        }
        //protected override void OnListItemClick(ListView l, View v, int position, long id)
        //{
        //    // Ось тут Улян додаси віконце де будуть ті запити хендлитись

        //}
    }
}