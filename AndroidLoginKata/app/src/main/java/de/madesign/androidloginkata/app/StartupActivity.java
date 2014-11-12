package de.madesign.androidloginkata.app;

import android.os.Bundle;
import com.google.inject.Inject;
import roboguice.activity.RoboFragmentActivity;

public class StartupActivity extends RoboFragmentActivity {
    @Inject
    private Interactions interactions;

    @Override
    public void onCreate(final Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        interactions.start();
    }
}
