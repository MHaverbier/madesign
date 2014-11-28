package de.madesign.androidloginkata.app.model;

import android.os.Parcel;
import android.os.Parcelable;

import java.io.Serializable;

public class PersonalizedSlogan implements Parcelable {
    private String name;
    private String slogan;

    public PersonalizedSlogan(User user, String slogan) {
        this.name = user.getName();
        this.slogan = slogan;
    }

    private PersonalizedSlogan(Parcel in) {
        name = in.readString();
        slogan = in.readString();
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
        dest.writeString(name);
        dest.writeString(slogan);
    }

    public static final Parcelable.Creator<PersonalizedSlogan> CREATOR
        = new Parcelable.Creator<PersonalizedSlogan>() {
        public PersonalizedSlogan createFromParcel(final Parcel in) {
            return new PersonalizedSlogan(in);
        }

        public PersonalizedSlogan[] newArray(final int size) {
            return new PersonalizedSlogan[size];
        }
    };
}
