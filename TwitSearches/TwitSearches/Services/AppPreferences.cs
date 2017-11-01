using Android.Content;
using Android.Preferences;
using System;
using System.Collections.Generic;

public class AppPreferences
{
    private ISharedPreferences mSharedPrefs;
    private ISharedPreferencesEditor mPrefsEditor;
    private Context mContext;


    public AppPreferences(Context context)
    {
        this.mContext = context;
        mSharedPrefs = PreferenceManager.GetDefaultSharedPreferences(mContext);
        mPrefsEditor = mSharedPrefs.Edit();
    }

    public void saveTag(string tag, string value)
    {
        mPrefsEditor.PutString(tag, value);
        mPrefsEditor.Commit();
    }

    public void deleteTag(string key)
    {
        mPrefsEditor.Remove(key);
        mPrefsEditor.Commit();
    }

    public string getQuery(string tag)
    {
        return mSharedPrefs.GetString(tag,"");
    }

    public IDictionary<string, object> getAllTags ()
    {
        return mSharedPrefs.All;
    }
}