package de.madesign.androidloginkata.app;

import com.google.inject.Inject;
import de.madesign.androidloginkata.app.Adapter.LoginActivityAdapter;
import de.madesign.androidloginkata.app.Adapter.SpruchActivityAdapter;
import de.madesign.androidloginkata.app.model.PersonalizedSlogan;
import de.madesign.androidloginkata.app.model.User;
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

    @Inject
    public Interactions() {
        doorman = new Doorman();
        sloganCollection = new SloganCollection();
    }

    public void start(){
        User user = doorman.determineUser();
        String sloganOfTheDay = sloganCollection.selectSlogan(user.isFullAge());
        PersonalizedSlogan personalizedSlogan =
            new PersonalizedSlogan(user.getName(), sloganOfTheDay);
        spruchActivityAdapter.show(personalizedSlogan);
    }

    public void logout() {
        loginActivityAdapter.show();
    }

    public void login(String name, String password, Action1<String> action1) {
        User user = doorman.validateUser(name, password);
        if (user == null){
            action1.call("Du kommst hier nicht rein!");
            return;
        }
        String sloganOfTheDay = sloganCollection.selectSlogan(user.isFullAge());
        PersonalizedSlogan personalizedSlogan =
            new PersonalizedSlogan(user.getName(), sloganOfTheDay);
        spruchActivityAdapter.show(personalizedSlogan);
    }
}
