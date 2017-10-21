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
        Button saveTagButton;
        EditText textTag;
        EditText textQuery;
        AppPreferences ap;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.tags);
            Context mContext = Android.App.Application.Context;
            ap = new AppPreferences(mContext);

            ap.saveTag("ira", "https://stackoverflow.com/questions/26668509/how-do-i-use-sharedpreferences-in-xamarin-android");
            ap.saveTag("ira1", "https://stackoverflow.com/questions/26668509/how-do-i-use-sharedpreferences-in-xamarin-android");
            var tags = ap.getAllTags().Keys.ToList();

            listView = FindViewById<ListView>(Resource.Id.List); 
            saveTagButton = FindViewById<Button>(Resource.Id.saveTag);
            textQuery = FindViewById<EditText>(Resource.Id.inputQuery);
            textTag = FindViewById<EditText>(Resource.Id.inputTag);
            listView.Adapter =  new ArrayAdapter<string>(this, Resource.Layout.list_item, tags);


            saveTagButton.Click += OnSaveTagClick;
        }

        void OnSaveTagClick(object sender, EventArgs e)
        {
            string tag = textTag.Text;
            string query = textQuery.Text;

            ap.saveTag(tag, query);
            Toast.MakeText(this, "Done", ToastLength.Short);
      
        }

        //protected override void OnListItemClick(ListView l, View v, int position, long id)
        //{
        //    // Ось тут Улян додаси віконце де будуть ті запити хендлитись

        //}
    }
}