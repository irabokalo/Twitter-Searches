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
    [Activity(Label = "EditTagActivity")]
    public class EditTagActivity : Activity
    {
        Button cancelButton;
        Button saveTagButton;
        EditText textTag;
        EditText textQuery;
        AppPreferences ap;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.editTags);
            Context mContext = Android.App.Application.Context;
            ap = new AppPreferences(mContext);
            var tags = ap.getAllTags().Keys.ToList();


            saveTagButton = FindViewById<Button>(Resource.Id.saveEdit);
            textQuery = FindViewById<EditText>(Resource.Id.inputQueryEdit);
            textTag = FindViewById<EditText>(Resource.Id.inputTagEdit);
            cancelButton = FindViewById<Button>(Resource.Id.cancelEdit);

            string text = Intent.GetStringExtra("MyData") ?? "Data not available";

            var tag = tags.Find(x => x == text);
            var query = ap.getQuery(tag);

            textQuery.Text = query;
            textTag.Text = tag;

            saveTagButton.Click += OnSaveTagClick;
            cancelButton.Click += OnCancelClick;
        }

        private void OnCancelClick(object sender, EventArgs e)
        {
            StartActivity(typeof(TagsListActivity));
        }

        private void OnSaveTagClick(object sender, EventArgs e)
        {
            string tag = textTag.Text;
            string query = textQuery.Text;
            ap.saveTag(tag, query);
            InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
            imm.HideSoftInputFromWindow(textTag.WindowToken, Android.Views.InputMethods.HideSoftInputFlags.None);
            Toast.MakeText(this, "Tag has been changed", ToastLength.Short).Show();
            StartActivity(typeof(TagsListActivity));
        }
    }
}