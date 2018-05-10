package md548bbb3352ae0731b49cd32eb97c0f449;


public class JavaLangHolder
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("XamarinSearchFilter.BLL.JavaLangHolder, XamarinSearchFilter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", JavaLangHolder.class, __md_methods);
	}


	public JavaLangHolder ()
	{
		super ();
		if (getClass () == JavaLangHolder.class)
			mono.android.TypeManager.Activate ("XamarinSearchFilter.BLL.JavaLangHolder, XamarinSearchFilter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
