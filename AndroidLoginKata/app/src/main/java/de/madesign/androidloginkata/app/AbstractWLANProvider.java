package de.madesign.androidloginkata.app;

import ma.bindings.events.Event;

public abstract class AbstractWLANProvider {
    public Event<Boolean> WLANActive = Event.create();

    public abstract void start();
    public abstract void stop();
}
