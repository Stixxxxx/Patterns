package com.myfirstservice.vladimir.browserstix;

import android.app.ActionBar;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.net.Uri;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.view.ActionMode;
import android.text.Editable;
import android.text.Selection;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.MotionEvent;
import android.view.View;
import android.webkit.WebSettings;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.EditText;
import android.widget.Toast;

public class BrowserActivity extends AppCompatActivity {

    EditText editText;
    String address;
    String addressRemProbel;
    WebView webView;

    SharedPreferences sPref;
    public static final String APP_PREFERENCES = "settings";
    final String SAVED_TEXT = "valueCheckBox";
    final String SAVED_URL = "valueUrl";
    String valueCheckBox = "Пусто";
    String webUrl;

    Boolean FirstLaunch = false;


    @Override
    protected void onCreate(Bundle savedInstanceState) {


        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_browser);



        webView = (WebView) findViewById(R.id.webView);

//        webView.setWebViewClient(new WebViewClient());
        webView.getSettings().setJavaScriptEnabled(true);


        webView.getSettings().setUseWideViewPort(true);
        webView.getSettings().setLoadWithOverviewMode(true);


        webView.setWebViewClient(new WebViewClient() {
            @Override
            public void onPageFinished(WebView view, String url) {

                editText.setText(webUrl = webView.getUrl());
                int position = editText.length();
                Editable etext = editText.getText();
                Selection.setSelection(etext, position);
            }
        });



//        webView.getSettings().setSupportZoom(true);

//        webView.getSettings().setBuiltInZoomControls(false);
//        webView.getSettings().setDisplayZoomControls(true);


        Uri data = getIntent().getData();
        webUrl = data.toString();
        webView.loadUrl(webUrl);

        webUrl = webView.getUrl();


        editText = (EditText) findViewById(R.id.editText2);
        editText.setText(webUrl = webView.getUrl());
        int position = editText.length();
        Editable etext = editText.getText();
        Selection.setSelection(etext, position);


        (findViewById(R.id.btnWeb)).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                address = editText.getText().toString();
                addressRemProbel = address.replace(" ", "");
                webView.loadUrl(addressRemProbel);

                editText.setText(webUrl = webView.getUrl());
                int position = editText.length();
                Editable etext = editText.getText();
                Selection.setSelection(etext, position);


//                Toast.makeText(getApplication(), valueCheckBox, Toast.LENGTH_SHORT).show();

            }
        });


    }


    @Override
    protected void onStart() {
        super.onStart();


        sPref = getSharedPreferences(APP_PREFERENCES, Context.MODE_PRIVATE);
        if (sPref.contains(SAVED_TEXT)) {
            valueCheckBox = sPref.getString(SAVED_TEXT, "");
            if (valueCheckBox.equals("true")) {
                webView.getSettings().setBuiltInZoomControls(true);
                webView.getSettings().setDisplayZoomControls(false);
                Toast.makeText(getApplication(), "Маcштабирование включено", Toast.LENGTH_SHORT).show();
            } if (valueCheckBox.equals("false")){
                webView.getSettings().setBuiltInZoomControls(false);
                webView.getSettings().setDisplayZoomControls(false);
                Toast.makeText(getApplication(), "Маcштабирование выключено", Toast.LENGTH_SHORT).show();
            }


        }

    }


    protected void onRestoreInstanceState(Bundle savedInstanceState) {
        super.onRestoreInstanceState(savedInstanceState);

        webUrl = savedInstanceState.getString("url");
        Log.d("Log Tag", "onRestoreInstanceState");
    }


    @Override
    protected void onResume() {
        super.onResume();
        webView.loadUrl(webUrl);

    }

    protected void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putString("url", webUrl);

        Log.d("Log Tag", "onSaveInstanceState");
    }


    @Override
    protected void onDestroy() {
        super.onDestroy();

        SharedPreferences.Editor ed = sPref.edit();
        ed.putString(SAVED_URL, webUrl);
        ed.apply();
    }


    @Override
    public void onBackPressed() {
        if (webView.canGoBack()) {
            webView.goBack();
        } else {
            super.onBackPressed();
        }
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // TODO Auto-generated method stub
        // добавляем пункты меню
        menu.add(0, 1, 0, "Настройки");

        return super.onCreateOptionsMenu(menu);
    }


    // обработка нажатий
    @Override
    public boolean onOptionsItemSelected(MenuItem item) {

        Intent intent = new Intent(this, SettingsActivity.class);
        startActivity(intent);

        return super.onOptionsItemSelected(item);
    }





}