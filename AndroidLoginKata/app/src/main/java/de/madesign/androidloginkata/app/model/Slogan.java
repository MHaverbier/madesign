package de.madesign.androidloginkata.app.model;

public class Slogan {
    private boolean isXRated;
    private String text;

    public Slogan(String text, boolean isXRated) {
        this.text = text;
        this.isXRated = isXRated;
    }

    public String getText() {
        return text;
    }

    public boolean isXRated() {
        return isXRated;
    }
}
