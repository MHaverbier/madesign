package de.madesign.androidloginkata.app.model;

import android.os.Parcel;
import android.os.Parcelable;

import java.io.Serializable;

public class PersonalizedSlogan implements Parcelable {
    private String name;
    private String slogan;

    public PersonalizedSlogan(String name, String slogan) {
        this.name = name;
        this.slogan = slogan;
    }

    public String getName() {
        return name;
    }

    public String getSlogan() {
        return slogan;
    }

    /**
     * Describe the kinds of special objects contained in this Parcelable's
     * marshalled representation.
     *
     * @return a bitmask indicating the set of special object types marshalled
     * by the Parcelable.
     */
    @Override
    public int describeContents() {
        return 0;
    }

    /**
     * Flatten this object in to a Parcel.
     *
     * @param dest  The Parcel in which the object should be written.
     * @param flags Additional flags about how the object should be written.
     *              May be 0 or {@link #PARCELABLE_WRITE_RETURN_VALUE}.
     */
    @Override
    public void writeToParcel(final Parcel dest, final int flags) {
        dest.writeStringArray(new String[] {name, slogan});
    }
}
