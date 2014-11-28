package de.madesign.androidloginkata.app;

import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import com.google.inject.Inject;
import de.madesign.androidloginkata.app.model.PersonalizedSlogan;
import roboguice.activity.RoboFragmentActivity;
import roboguice.inject.ContentView;
import roboguice.inject.InjectView;

@ContentView(R.layout.activity_spruch)
public class SpruchActivity extends RoboFragmentActivity implements View.OnClickListener{

    @InjectView(R.id.tvGreetings)
    private TextView greetings;

    @InjectView(R.id.tvSloganOfTheDay)
    private TextView sloganOfTheDay;

    @InjectView(R.id.bLogout)
    private Button logout;

    @Inject
    private Interactions interactions;

    @Override
    protected void onCreate(final Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        logout.setOnClickListener(this);
    }

    @Override
    protected void onResume() {
        super.onResume();

        PersonalizedSlogan personalizedSlogan = getIntent().getParcelableExtra("personalizedSlogan");
        getIntent().removeExtra("personalizedSlogan");

        greetings.setText("Hallo " + personalizedSlogan.getName());
        sloganOfTheDay.setText(personalizedSlogan.getSlogan());
    }

    /**
     * Called when a view has been clicked.
     *
     * @param v The view that was clicked.
     */
    @Override
    public void onClick(final View v) {
        interactions.logout();
    }
}
