package de.madesign.androidloginkata.app;

import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import com.google.inject.Inject;
import roboguice.activity.RoboFragmentActivity;
import roboguice.inject.ContentView;
import roboguice.inject.InjectView;

@ContentView(R.layout.activity_login)
public class LoginActivity extends RoboFragmentActivity {

    @InjectView(R.id.username)
    private EditText usernameView;

    @InjectView(R.id.password)
    private EditText passwordView;

    @InjectView(R.id.login)
    private Button loginButton;

    @InjectView(R.id.error)
    private TextView errorView;

    @Inject
    private LoginActivityIntegration loginActivityIntegration;

    @Override public void onCreate(final Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        loginButton.setOnClickListener(v -> loginActivityIntegration
            .login(this, usernameView.getText().toString(), passwordView.getText().toString()));
    }

    public void displayError(String error) {
        errorView.setVisibility(View.VISIBLE);
        errorView.setText(error);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.login, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();
        if (id == R.id.action_settings) {
            return true;
        }
        return super.onOptionsItemSelected(item);
    }
}
