package md595811161a01539aace20c3c9fcdef917;


public class ForgotPasswordActivity
	extends android.support.v7.app.AppCompatActivity
	implements
		mono.android.IGCUserPeer,
		android.view.View.OnClickListener,
		com.google.android.gms.tasks.OnCompleteListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onClick:(Landroid/view/View;)V:GetOnClick_Landroid_view_View_Handler:Android.Views.View/IOnClickListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onComplete:(Lcom/google/android/gms/tasks/Task;)V:GetOnComplete_Lcom_google_android_gms_tasks_Task_Handler:Android.Gms.Tasks.IOnCompleteListenerInvoker, Xamarin.GooglePlayServices.Tasks\n" +
			"";
		mono.android.Runtime.register ("FloweryTea.Activities.Account.ForgotPasswordActivity, FloweryTea, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ForgotPasswordActivity.class, __md_methods);
	}


	public ForgotPasswordActivity ()
	{
		super ();
		if (getClass () == ForgotPasswordActivity.class)
			mono.android.TypeManager.Activate ("FloweryTea.Activities.Account.ForgotPasswordActivity, FloweryTea, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


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
