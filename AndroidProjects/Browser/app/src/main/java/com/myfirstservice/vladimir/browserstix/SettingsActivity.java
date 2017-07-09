package com.myfirstservice.vladimir.browserstix;

import android.content.Context;
import android.content.SharedPreferences;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.CheckBox;
import android.widget.CompoundButton;
import android.widget.Toast;

public class SettingsActivity extends AppCompatActivity {

    CheckBox checkBox;
    SharedPreferences sPref;
    final String SAVED_TEXT = "valueCheckBox";
    public static final String APP_PREFERENCES = "settings";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_settings);


        sPref = getSharedPreferences(APP_PREFERENCES, Context.MODE_PRIVATE);
        checkBox = (CheckBox) findViewById(R.id.checkBox);
        String valueCheckBox = sPref.getString(SAVED_TEXT, "");
        switch (valueCheckBox) {
            case "true":
                checkBox.setChecked(true);
                break;
            case "false":
                checkBox.setChecked(false);
                break;
        }

        checkBox.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {

                                                @Override
                                                public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {


                                                    if (isChecked) {
                                                        SharedPreferences.Editor ed = sPref.edit();
                                                        ed.putString(SAVED_TEXT, "true");
                                                        ed.apply();

                                                    } else {
                                                        SharedPreferences.Editor ed2 = sPref.edit();
                                                        ed2.putString(SAVED_TEXT, "false");
                                                        ed2.apply();

                                                    }

                                                    String valueCheckBox = sPref.getString(SAVED_TEXT, "");
                                                    Toast.makeText(getApplication(), "Значение чекбокса сохранено в системе как " + valueCheckBox, Toast.LENGTH_SHORT).show();
                                                }
                                            }
        );


    }


    @Override
    protected void onPause() {
        super.onPause();

        if (checkBox.isChecked()) {
            SharedPreferences.Editor ed = sPref.edit();
            ed.putString(SAVED_TEXT, "true");
            ed.apply();

        } else {
            SharedPreferences.Editor ed2 = sPref.edit();
            ed2.putString(SAVED_TEXT, "false");
            ed2.apply();

        }


    }
}
