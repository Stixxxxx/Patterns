package com.lesson1.vladimir.drive;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.Button;
import android.widget.LinearLayout;

import java.util.zip.Inflater;

public class Auth extends AppCompatActivity {

    WebView webView;
    String token;
    final int REQUEST_CODE_TOKEN = 99;



    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_auth);


        webView = (WebView) findViewById(R.id.webView);
        webView.getSettings().setJavaScriptEnabled(true);
        webView.loadUrl("http://www.dropbox.com/1/oauth2/authorize?response_type=token&client_id=swrita0hnhirvym&redirect_uri=http://localhost");
        webView.setWebViewClient(new MyWebViewClient());

//        llLLFL.setVisibility(LinearLayout.VISIBLE);





    }

    private class MyWebViewClient extends WebViewClient {
        @Override
        public boolean shouldOverrideUrlLoading(WebView view, String url) {
            view.loadUrl(url);
            return true;
        }


        @Override
        public void onPageFinished(WebView view, String url) {
            super.onPageFinished(view, url);
            String urlCancelLoad = url;

            //            http://localhost/#access_token=S15kjJEg4LAAAAAAAAB1RMlaQyL4WgIvxRv9m9Dxq9SZ2KvFuW5HDAlIW8jc2bME&token_type=bearer&uid=206810328&account_id=dbid%3AAAAUGyylz86BVfqsnIjwI4AxtF0sycvrS00

            if (urlCancelLoad.contains("access_token=")) {
                String[] arrayUrl1 = urlCancelLoad.split("=");
                String[] arrayUrl2 = arrayUrl1[1].split("&");
                token = arrayUrl2[0];

                Intent intent = new Intent();
                intent.putExtra("token", token);
                setResult(RESULT_OK, intent);
                finish();



            }


        }
    }


}
