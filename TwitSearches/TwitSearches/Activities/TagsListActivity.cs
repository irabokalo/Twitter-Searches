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
    public class TagsListActivity : ListActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Context mContext = Android.App.Application.Context;
            AppPreferences ap = new AppPreferences(mContext);


            ap.saveTag("ira", "https://stackoverflow.com/questions/26668509/how-do-i-use-sharedpreferences-in-xamarin-android");
            ap.saveTag("ira1", "https://stackoverflow.com/questions/26668509/how-do-i-use-sharedpreferences-in-xamarin-android");
            var tags = ap.getAllTags().Keys.ToList();
       

            ListAdapter = new ArrayAdapter<string>(this, Resource.Layout.list_item, tags);

            ListView.TextFilterEnabled = true;

        }
        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            // Ось тут Улян додаси віконце де будуть ті запити хендлитись

        }
    }
}