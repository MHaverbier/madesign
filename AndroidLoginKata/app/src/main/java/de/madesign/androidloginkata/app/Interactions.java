package de.madesign.androidloginkata.app;

import com.google.inject.Inject;
import de.madesign.androidloginkata.app.Adapter.LoginActivityAdapter;
import de.madesign.androidloginkata.app.Adapter.SpruchActivityAdapter;
import de.madesign.androidloginkata.app.Adapter.WlanAdapter;
import de.madesign.androidloginkata.app.model.PersonalizedSlogan;
import de.madesign.androidloginkata.app.model.User;
import ma.bindings.android.AndroidBinder;
import roboguice.inject.ContextSingleton;
import rx.functions.Action1;

@ContextSingleton
public class Interactions {
    private Doorman doorman;
    private SloganCollection sloganCollection;

    @Inject
    private LoginActivityAdapter loginActivityAdapter;
    @Inject
    private SpruchActivityAdapter spruchActivityAdapter;

    private AndroidBinder binder;

    @Inject
    public Interactions() {
        doorman = new Doorman();
        sloganCollection = new SloganCollection();
        binder = new AndroidBinder();
        //binder.bind(wlanAdapter.wlanStatusChangedEvent, status -> onWlanStatusChanged(status.getData()));
    }

    public void start(){
        User user = doorman.determineUser();
        String sloganOfTheDay = sloganCollection.selectSlogan(user.isFullAge());
        PersonalizedSlogan personalizedSlogan =
            new PersonalizedSlogan(user, sloganOfTheDay);
        spruchActivityAdapter.show(personalizedSlogan);
    }

    public void logout() {
        loginActivityAdapter.show();
    }

    @Inject
    WlanAdapter wlanAdapter;
    Action1<Boolean> wlanAction;

    public void setCallBackFromLoginActivity(Action1<Boolean> action1) {
        wlanAction = action1;
    }

    private void onWlanStatusChanged(boolean status) {
        if (wlanAction != null) {
            wlanAction.call(status);
        }
    }

    @Override
    protected void finalize() throws Throwable {
        super.finalize();
        binder.unbindAll();
    }
}
