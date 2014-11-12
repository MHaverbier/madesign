package de.madesign.androidloginkata.app.Adapter;

import android.content.Context;
import android.content.Intent;
import com.google.inject.Inject;
import de.madesign.androidloginkata.app.SpruchActivity;
import de.madesign.androidloginkata.app.model.PersonalizedSlogan;
import roboguice.inject.ContextSingleton;

@ContextSingleton
public class SpruchActivityAdapter {

    @Inject
    private Context context;

    public void show(final PersonalizedSlogan personalizedSlogan) {
        Intent intent = new Intent(context, SpruchActivity.class);
        intent.putExtra("personalizedSlogan.name", personalizedSlogan.getName());
        intent.putExtra("personalizedSlogan.slogan", personalizedSlogan.getSlogan());
        context.startActivity(intent);
    }
}
