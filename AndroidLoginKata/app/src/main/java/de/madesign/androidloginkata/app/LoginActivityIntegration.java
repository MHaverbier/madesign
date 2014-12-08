package de.madesign.androidloginkata.app;

import com.google.inject.Inject;
import de.madesign.androidloginkata.app.Adapter.LoginActivityAdapter;
import de.madesign.androidloginkata.app.Adapter.SpruchActivityAdapter;
import de.madesign.androidloginkata.app.model.PersonalizedSlogan;
import roboguice.inject.ContextSingleton;

@ContextSingleton
public class LoginActivityIntegration {
    @Inject
    private SpruchActivityAdapter spruchActivityAdapter;
    @Inject
    private Doorman doorman;
    @Inject
    private SloganCollection sloganCollection;

    public void login(LoginActivityAdapter loginActivityAdapter, String name, String password) {
        doorman.validateUser(name, password,
            user -> {
                String sloganOfTheDay = sloganCollection.selectSlogan(user.isFullAge());
                PersonalizedSlogan personalizedSlogan =
                    new PersonalizedSlogan(user, sloganOfTheDay);
                spruchActivityAdapter.show(personalizedSlogan);
            },
            () -> loginActivityAdapter.displayError("Du kommst hier nicht rein!"));
    }
}
