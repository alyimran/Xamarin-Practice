using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace XamarinSearchFilter.BLL
{
    public class ContactsAdapter : RecyclerView.Adapter,IFilterable
    {
        private Context context;
        private List<Contacts> contactList;
        private ContactsAdapterListener listener;
        private JavaLangHolder _contactListHolder;

        public ContactsAdapter(Context context, List<Contacts> contactList, ContactsAdapterListener listener)
        {
            this.context = context;
            this.listener = listener;
            this.contactList = contactList;
            _contactListHolder = new JavaLangHolder();
            _contactListHolder.ContactListFiltered = contactList;
        }

        public override int ItemCount => _contactListHolder.ContactListFiltered.Count;

        public Filter Filter => new MyFilterClass(contactList, _contactListHolder);

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            MyViewHolder holderr = holder as MyViewHolder;
             Contacts contact = _contactListHolder.ContactListFiltered[position];
            holderr.name.Text = contact.name;
            holderr.phone.Text = contact.phone;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context)
               .Inflate(Resource.Layout.user_row_item, parent, false);

            return new MyViewHolder(itemView);
        }

        public class MyViewHolder:RecyclerView.ViewHolder,View.IOnClickListener
        {
            public TextView name, phone;
            public ImageView thumbnail;
            public MyViewHolder(View view):base(view)
            {
                name = view.FindViewById<TextView>(Resource.Id.name);
                phone = view.FindViewById<TextView>(Resource.Id.phone);
                thumbnail = view.FindViewById<ImageView>(Resource.Id.thumbnail);
            }

            public void OnClick(View v)
            {
                
               // listener.onContactSelected(contactListFiltered.get(getAdapterPosition()));
            }
        }
    }

    public class JavaLangHolder : Java.Lang.Object
    {
        public List<Contacts> ContactListFiltered { get; set; }
    }

    public class MyFilterClass : Filter
    {
        public List<Contacts> contactList;
        private JavaLangHolder _contactListHolder;
        public MyFilterClass(List<Contacts> contactList_P, JavaLangHolder contactListHolder)
        {
            contactList = contactList_P;
            _contactListHolder = contactListHolder;
        }

        protected override FilterResults PerformFiltering(ICharSequence constraint)
        {
            System.String charString = constraint.ToString();
            if (string.IsNullOrEmpty(charString))
            {
                _contactListHolder.ContactListFiltered = contactList;
            }
            else
            {
                List<Contacts> filteredList = new List<Contacts>();
                foreach (Contacts row in contactList)
                {

                    // name match condition. this might differ depending on your requirement
                    // here we are looking for name or phone number match
                    if (row.name.ToLower().Contains(charString.ToLower()) || row.phone.Contains(charString))
                    {
                        filteredList.Add(row);
                    }
                }

                _contactListHolder.ContactListFiltered = filteredList;
            }

            FilterResults filterResults = new FilterResults();
            filterResults.Values = _contactListHolder;
            return filterResults;
        }
        protected override void PublishResults(ICharSequence constraint, FilterResults results)
        {
            var obj = (JavaLangHolder)results.Values;

            _contactListHolder.ContactListFiltered = obj.ContactListFiltered;

            MainActivity.mAdapter.NotifyDataSetChanged();
        }
    }

    public interface ContactsAdapterListener
    {
        void onContactSelected(Contacts contact);
    }
}
   
