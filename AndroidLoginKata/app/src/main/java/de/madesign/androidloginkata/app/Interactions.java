package de.madesign.androidloginkata.app;

import com.google.inject.Inject;
import de.madesign.androidloginkata.app.Adapter.LoginActivityAdapter;
import de.madesign.androidloginkata.app.Adapter.SpruchActivityAdapter;
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

    //private AndroidBinder binder;

    @Inject
    public Interactions() {
        doorman = new Doorman();
        sloganCollection = new SloganCollection();
        //binder = new AndroidBinder();
        //binder.bind(contextWLANProvider.WLANActive, status -> onWlanStatusChanged(status.getData()));
    }

    public void start(){
        // spike implementation to show we are still logged in; as determineUser does not
        // deliver null atm, we don't check for it and don't do loginActivityAdapter.show.
        User user = doorman.determineUser();
        String sloganOfTheDay = sloganCollection.selectSlogan(user.isFullAge());
        PersonalizedSlogan personalizedSlogan =
            new PersonalizedSlogan(user, sloganOfTheDay);
        spruchActivityAdapter.show(personalizedSlogan);
    }

    public void logout() {
        loginActivityAdapter.show();
    }

    @Override
    protected void finalize() throws Throwable {
        super.finalize();
        //binder.unbindAll();
    }
}
