package de.madesign.androidloginkata.app;

public class SloganCollection {
    public String selectSlogan(boolean istVolljaehrig) {
        String result = null;
        if (istVolljaehrig) {
            result = "volljaehriger Spruch";
        }else {
            result = "minderjaehriger Spruch";
        }
        return result;
    }
}
