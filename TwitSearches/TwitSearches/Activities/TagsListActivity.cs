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
    [Activity(Label = "Twitter searches")]
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
            saveTagButton = FindViewById<Button>(Resource.Id.save);
            textQuery = FindViewById<EditText>(Resource.Id.inputQuery);
            textTag = FindViewById<EditText>(Resource.Id.inputTag);
            listView.Adapter =  new ArrayAdapter<string>(this, Resource.Layout.list_item, tags);

            saveTagButton.Click += OnSaveTagClick;

            listView.ItemClick += listView_ItemClick;
            listView.ItemLongClick += ShowPopupMenu;          
        }

        void OnSaveTagClick(object sender, EventArgs e)
        {
            string tag = textTag.Text;
            string query = textQuery.Text;

            ap.saveTag(tag, query);
            Toast.MakeText(this, "Done", ToastLength.Short);
        }

        void listView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var tag = (string)listView.GetItemAtPosition(e.Position);
            var uri = Android.Net.Uri.Parse("https://www.google.com/search?q=" + tag);
            var intent = new Intent(Intent.ActionView, uri);
            StartActivity(intent);
        }

        private void ShowPopupMenu(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            PopupMenu menu = new PopupMenu(this, (View)sender);
            menu.Inflate(Resource.Menu.popupMenu);
            menu.MenuItemClick += (s, arg) =>
            {
                switch (arg.Item.TitleFormatted.ToString())
                {
                    case "Share":
                        ShareTag(e.Position);
                        break;
                    case "Edit":
                        EditTag(e.Position);
                        break;
                    case "Delete":
                        DeleteTag(e.Position);
                        break;
                }
            };

            menu.Show();
        }

        public void EditTag(int tagIndex)
        {
            //TODO: IMPLEMENT
        }

        public void ShareTag(int tagIndex)
        {
            //TODO: IMPLEMENT
        }

        public void DeleteTag(int tagIndex)
        {
            //TODO: IMPLEMENT
        }
    }
}