package de.madesign.androidloginkata.app.userhandling;

import java.util.HashMap;

public class UserHandling {

    private HashMap<String, String> credentials = new HashMap<String, String>();

    public boolean login(String username, String password) {
        boolean result = false;

        if (credentials.containsKey(username)) {
            if (credentials.get(username).equals(password)) {
                result = true;
            }
        } else {
            register(username, password);
        }

        return result;
    }

    private void register(final String username, final String password) {
        credentials.put(username, password);
    }
}
