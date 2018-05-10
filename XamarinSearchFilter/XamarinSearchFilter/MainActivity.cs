using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using System.Collections.Generic;
using XamarinSearchFilter.BLL;

namespace XamarinSearchFilter
{
    [Activity(Label = "XamarinSearchFilter", MainLauncher = true,Theme ="@style/Theme.AppCompat.Light.NoActionBar")]
    public class MainActivity : AppCompatActivity,ContactsAdapterListener,SearchView.IOnQueryTextListener
    {
        
        private RecyclerView recyclerView;
        private List<Contacts> contactList = new List<Contacts>();
        public static ContactsAdapter mAdapter;
        private SearchView searchView;
        public MainActivity()
        {
           

        }
        public void onContactSelected(Contacts contact)
        {
           
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_main);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
           SupportActionBar.SetTitle(Resource.String.toolbar_title);

            recyclerView = FindViewById<RecyclerView>(Resource.Id.recycler_view);
            contactList = new List<Contacts>();
            //searchView = FindViewById<SearchView>(Resource.Id.action_search);
            //searchView.SetOnQueryTextListener(this);
            contactList.Add(new Contacts { name = "Ali", phone = "03476029722" });
            contactList.Add(new Contacts { name = "Hamza", phone = "03476029722" });
            contactList.Add(new Contacts { name = "ahmad", phone = "03476029722" });
            contactList.Add(new Contacts { name = "waqas", phone = "03476029722" });
            contactList.Add(new Contacts { name = "shahid", phone = "03476029722" });
            contactList.Add(new Contacts { name = "hamad", phone = "03476029722" });
            contactList.Add(new Contacts { name = "yaseen", phone = "03476029722" });

            mAdapter = new ContactsAdapter(this, contactList, this);
            recyclerView.SetLayoutManager(new LinearLayoutManager(ApplicationContext));
            recyclerView.SetAdapter(mAdapter);



        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);

            // Associate searchable configuration with the SearchView
            SearchManager searchManager = (SearchManager)GetSystemService(Context.SearchService);
            searchView = (SearchView)menu.FindItem(Resource.Id.action_search)
                    .ActionView;
            searchView.SetSearchableInfo(searchManager
                    .GetSearchableInfo(ComponentName));
            searchView.MaxWidth = int.MaxValue;
            searchView.SetOnQueryTextListener(this);

            return true;
        }
        public bool OnQueryTextChange(string newText)
        {
            
            mAdapter.Filter.InvokeFilter(newText);
            return false;
        }

        public bool OnQueryTextSubmit(string query)
        {
            mAdapter.Filter.InvokeFilter(query);
            return false;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            //noinspection SimplifiableIfStatement
            if (id == Resource.Id.action_search)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }
    }

      
    }


