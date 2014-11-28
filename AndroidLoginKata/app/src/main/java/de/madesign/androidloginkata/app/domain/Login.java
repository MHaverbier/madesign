package de.madesign.androidloginkata.app.domain;

import de.madesign.androidloginkata.app.Doorman;
import de.madesign.androidloginkata.app.SloganCollection;
import de.madesign.androidloginkata.app.model.PersonalizedSlogan;
import rx.functions.Action1;

/*
* Business logic integration for login
*/
public class Login {
    private Doorman doorman;
    private SloganCollection sloganCollection;

    public Login() {
        doorman = new Doorman();
        sloganCollection = new SloganCollection();
    }

    public void login(String name, String password, Action1<PersonalizedSlogan> loginSucceeded, Action1<String> loginFailed) {
        doorman.validateUser(name, password,
            user -> {
                String sloganOfTheDay = sloganCollection.selectSlogan(user.isFullAge());
                PersonalizedSlogan personalizedSlogan =
                    new PersonalizedSlogan(user, sloganOfTheDay);
                loginSucceeded.call(personalizedSlogan);
            },
            () -> loginFailed.call("Du kommst hier nicht rein!"));
    }
}
