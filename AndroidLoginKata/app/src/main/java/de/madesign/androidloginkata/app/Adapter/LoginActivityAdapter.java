package de.madesign.androidloginkata.app.Adapter;

import android.content.Context;
import android.content.Intent;
import com.google.inject.Inject;
import de.madesign.androidloginkata.app.LoginActivity;
import roboguice.inject.ContextSingleton;

@ContextSingleton
public class LoginActivityAdapter {

    @Inject
    private Context context;
    private LoginActivity loginActivity;

    @Inject
    public LoginActivityAdapter() {
    }

    public LoginActivityAdapter(final LoginActivity loginActivity) {
        this.loginActivity = loginActivity;
    }

    public void show() {
        Intent intent = new Intent(context, LoginActivity.class);
        context.startActivity(intent);
    }

    public void displayError(String message) {
        loginActivity.displayError(message);
    }
}
