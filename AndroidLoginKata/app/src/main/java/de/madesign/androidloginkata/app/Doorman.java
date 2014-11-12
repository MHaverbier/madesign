package de.madesign.androidloginkata.app;

import de.madesign.androidloginkata.app.model.User;

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

    public User validateUser(final String name, final String password) {
        if (name.startsWith("x")) {
            return new User("Horst", 21);
        } else if(name.startsWith("u")) {
            return null;
        } else {
            return new User("Chantalle", 14);
        }
    }
}
