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

    public void show() {
        Intent intent = new Intent(context, LoginActivity.class);
        context.startActivity(intent);
    }
}
