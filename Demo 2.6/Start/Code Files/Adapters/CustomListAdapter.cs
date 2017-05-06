using System.Collections.Generic;

using Android.App;
using Android.Views;
using Android.Widget;
using GPSImageTag.Core.Models;

namespace GPSImageTag.DroidNative.Adapters
{
    public class CusotmListAdapter : BaseAdapter<Photo>
    {
        Activity context;
        List<Photo> list;

        public CusotmListAdapter(Activity _context, List<Photo> _list)
            : base()
        {
            this.context = _context;
            this.list = _list;
        }

        public override int Count
        {
            get { return list.Count; }
        }

        public override Photo this[int index]
        {
            get
            {
                return list[index];
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            // re-use an existing view, if one is available
            // otherwise create a new one
            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.ListRowLayout, parent, false);

            Photo item = this[position];
            view.FindViewById<TextView>(Resource.Id.Name).Text = item.Name;
   
            using (var imageView = view.FindViewById<ImageView>(Resource.Id.Thumbnail))
            {
                string url = Android.Text.Html.FromHtml(item.Uri).ToString();

                //Download and display image
                Koush.UrlImageViewHelper.SetUrlDrawable(imageView,
                    url, Resource.Drawable.Placeholder);
            }
            return view;
        }
    }
}