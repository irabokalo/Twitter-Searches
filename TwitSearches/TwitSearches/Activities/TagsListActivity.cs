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
using Android.Views.InputMethods;

namespace TwitSearches.Activities
{
    [Activity(Label = "Twitter searches")]
    public class TagsListActivity: Activity
    { 
        ListView listView;
        ArrayAdapter<string> adapter;
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
            adapter = new ArrayAdapter<string>(this, Resource.Layout.list_item, tags);
            listView.Adapter = adapter;

            saveTagButton.Click += OnSaveTagClick;

            listView.ItemClick += listView_ItemClick;
            listView.ItemLongClick += ShowPopupMenu;          
        }

        protected override void OnStart()
        {
            base.OnStart();
            var parentContainer = FindViewById<LinearLayout>(Resource.Id.parent);
            parentContainer.RequestFocus();
        }

        void OnSaveTagClick(object sender, EventArgs e)
        {
            string tag = textTag.Text;
            string query = textQuery.Text;
            ap.saveTag(tag, query);
            adapter.Add(tag);
            adapter.NotifyDataSetChanged();
            textTag.Text = "";
            textQuery.Text = "";
            InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
            imm.HideSoftInputFromWindow(textTag.WindowToken, Android.Views.InputMethods.HideSoftInputFlags.None);
            Toast.MakeText(this, "Tag has been added", ToastLength.Short).Show();
        }

        void listView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var tag = (string)listView.GetItemAtPosition(e.Position);
            var query = ap.getQuery(tag);
            var uri = Android.Net.Uri.Parse("https://www.google.com/search?q=" + query);
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
                        ShowAlertDialog(e.Position);
                        break;
                }
            };

            menu.Show();
        }

        private void ShowAlertDialog(int tagIndex)
        {
            AlertDialog.Builder dialog = new AlertDialog.Builder(this);
            AlertDialog alert = dialog.Create();
            alert.SetTitle("Confirmation");
            alert.SetMessage("Are you sure you want to delete this tag?");
            alert.SetButton("DELETE", (c, ev) =>
            {
                DeleteTag(tagIndex);
            });
            alert.SetButton2("CANCEL", (c, ev) => { });
            alert.Show();
        }

        public void EditTag(int tagIndex)
        {
            var tag = (string)listView.GetItemAtPosition(tagIndex);
            var activity2 = new Intent(this, typeof(EditTagActivity));
            activity2.PutExtra("MyData", tag);
            StartActivity(activity2);

        }

        public void ShareTag(int tagIndex)
        {
            //TODO: IMPLEMENT
        }

        public void DeleteTag(int tagIndex)
        {
            var tag = (string) listView.GetItemAtPosition(tagIndex);
            ap.deleteTag(tag);
            adapter.Remove(tag);
            adapter.NotifyDataSetChanged();
        }
    }
}