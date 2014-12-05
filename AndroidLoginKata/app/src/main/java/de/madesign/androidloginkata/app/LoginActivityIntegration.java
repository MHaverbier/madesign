package de.madesign.androidloginkata.app;

import com.google.inject.Inject;
import de.madesign.androidloginkata.app.Adapter.SpruchActivityAdapter;
import de.madesign.androidloginkata.app.domain.Login;
import roboguice.inject.ContextSingleton;
import rx.functions.Action1;

@ContextSingleton
public class LoginActivityIntegration {
    @Inject
    private SpruchActivityAdapter spruchActivityAdapter;

    public void login(String name, String password, Action1<String> loginFailed) {
        Login login = new Login();
        login.login(name, password, spruchActivityAdapter::show, () -> loginFailed.call("Du kommst hier nicht rein!"));
    }
}
