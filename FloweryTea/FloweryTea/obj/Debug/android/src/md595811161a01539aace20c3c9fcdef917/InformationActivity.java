package md595811161a01539aace20c3c9fcdef917;


public class InformationActivity
	extends android.support.v7.app.AppCompatActivity
	implements
		mono.android.IGCUserPeer,
		android.view.View.OnClickListener,
		com.google.android.gms.tasks.OnCompleteListener,
		com.facebook.FacebookCallback
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onBackPressed:()V:GetOnBackPressedHandler\n" +
			"n_onClick:(Landroid/view/View;)V:GetOnClick_Landroid_view_View_Handler:Android.Views.View/IOnClickListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onComplete:(Lcom/google/android/gms/tasks/Task;)V:GetOnComplete_Lcom_google_android_gms_tasks_Task_Handler:Android.Gms.Tasks.IOnCompleteListenerInvoker, Xamarin.GooglePlayServices.Tasks\n" +
			"n_onCancel:()V:GetOnCancelHandler:Xamarin.Facebook.IFacebookCallbackInvoker, Xamarin.Facebook\n" +
			"n_onError:(Lcom/facebook/FacebookException;)V:GetOnError_Lcom_facebook_FacebookException_Handler:Xamarin.Facebook.IFacebookCallbackInvoker, Xamarin.Facebook\n" +
			"n_onSuccess:(Ljava/lang/Object;)V:GetOnSuccess_Ljava_lang_Object_Handler:Xamarin.Facebook.IFacebookCallbackInvoker, Xamarin.Facebook\n" +
			"";
		mono.android.Runtime.register ("FloweryTea.Activities.Account.InformationActivity, FloweryTea, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", InformationActivity.class, __md_methods);
	}


	public InformationActivity ()
	{
		super ();
		if (getClass () == InformationActivity.class)
			mono.android.TypeManager.Activate ("FloweryTea.Activities.Account.InformationActivity, FloweryTea, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void onBackPressed ()
	{
		n_onBackPressed ();
	}

	private native void n_onBackPressed ();


	public void onClick (android.view.View p0)
	{
		n_onClick (p0);
	}

	private native void n_onClick (android.view.View p0);


	public void onComplete (com.google.android.gms.tasks.Task p0)
	{
		n_onComplete (p0);
	}

	private native void n_onComplete (com.google.android.gms.tasks.Task p0);


	public void onCancel ()
	{
		n_onCancel ();
	}

	private native void n_onCancel ();


	public void onError (com.facebook.FacebookException p0)
	{
		n_onError (p0);
	}

	private native void n_onError (com.facebook.FacebookException p0);


	public void onSuccess (java.lang.Object p0)
	{
		n_onSuccess (p0);
	}

	private native void n_onSuccess (java.lang.Object p0);

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
