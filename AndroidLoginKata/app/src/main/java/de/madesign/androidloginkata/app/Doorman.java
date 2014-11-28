package de.madesign.androidloginkata.app;

import de.madesign.androidloginkata.app.model.User;
import rx.functions.Action0;
import rx.functions.Action1;

import java.util.Date;

public class Doorman {
    public User determineUser() {
        User result = null;

        Date d = new Date();

        if (d.getTime() % 2 == 0) {
            result = new User("Kevin", 21);
        } else {
            result = new User("Melanie", 14);
        }

        return result;
    }

    public void validateUser(final String name, final String password, Action1<User> success, Action0 failed ) {
        if (name.startsWith("x")) {
            success.call(new User("Horst", 21));
        } else if(name.startsWith("u")) {
            failed.call();
        } else {
            success.call(new User("Chantalle", 14));
        }
    }
}
