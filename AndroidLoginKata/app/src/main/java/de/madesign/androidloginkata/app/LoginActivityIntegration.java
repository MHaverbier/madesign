package de.madesign.androidloginkata.app;

import com.google.inject.Inject;
import de.madesign.androidloginkata.app.Adapter.SpruchActivityAdapter;
import de.madesign.androidloginkata.app.model.PersonalizedSlogan;
import roboguice.inject.ContextSingleton;

@ContextSingleton
public class LoginActivityIntegration {
    @Inject
    private SpruchActivityAdapter spruchActivityAdapter;
    private Doorman doorman;
    private SloganCollection sloganCollection;
    public LoginActivity loginActivity;

    @Inject
    public LoginActivityIntegration() {
        doorman = new Doorman();
        sloganCollection = new SloganCollection();
    }

    public void login(String name, String password) {
        doorman.validateUser(name, password,
            user -> {
                String sloganOfTheDay = sloganCollection.selectSlogan(user.isFullAge());
                PersonalizedSlogan personalizedSlogan =
                    new PersonalizedSlogan(user, sloganOfTheDay);
                spruchActivityAdapter.show(personalizedSlogan);
            },
            () -> loginActivity.onError("Du kommst hier nicht rein!"));
    }
}
