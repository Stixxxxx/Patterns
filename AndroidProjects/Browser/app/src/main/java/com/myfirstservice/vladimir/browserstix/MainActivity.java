package com.myfirstservice.vladimir.browserstix;

import android.content.Intent;
import android.net.Uri;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.text.Editable;
import android.text.Selection;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;

public class MainActivity extends AppCompatActivity {

    EditText editText;
    String address;
    String addressRemProbel;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);


        editText = (EditText) findViewById(R.id.editText2);
        int position = editText.length();
        Editable etext = editText.getText();
        Selection.setSelection(etext, position);


        (findViewById(R.id.btnWeb)).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                    address = editText.getText().toString();
                    addressRemProbel = address.replace(" ", "");
                    if (address.contains("http://")|| address.contains("http:\\\\")) {

                        startActivity(new Intent(Intent.ACTION_VIEW, Uri.parse(addressRemProbel)));
                    }


            }
        });
    }
}