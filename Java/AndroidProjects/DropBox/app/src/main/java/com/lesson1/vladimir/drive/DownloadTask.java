package com.lesson1.vladimir.drive;

import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Environment;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.LinearLayout;
import android.widget.ProgressBar;
import android.widget.Toast;

import com.dropbox.core.DbxException;
import com.dropbox.core.v2.DbxClientV2;
import com.dropbox.core.v2.files.Metadata;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.OutputStream;

/**
 * Created by Vladimir on 29.04.2017.
 */

public class DownloadTask extends AsyncTask<Void, Void, Void> {

    private DbxClientV2 dbxClient;
    private String pathFileDRBX;
    private String nameFileDRBX;
    private Context context;
    private Exception mException;
    private String message;
    private File file;
    ProgressDialog progressDialog;


    DownloadTask(DbxClientV2 dbxClient, String pathFileDRBX, String nameFileDRBX, Context context, ProgressDialog progressDialog) {

        this.dbxClient = dbxClient;
        this.pathFileDRBX = pathFileDRBX;
        this.nameFileDRBX = nameFileDRBX;
        this.context = context;
        this.progressDialog = progressDialog;

    }


    @Override
    protected void onPreExecute() {
        super.onPreExecute();

        progressDialog.setMessage("Downloading ...");
        progressDialog.setCancelable(false);
        progressDialog.setMax(100);
        progressDialog
                .setProgressStyle(ProgressDialog.STYLE_SPINNER);

        progressDialog.show();



    }

    @Override
    protected Void doInBackground(Void... params) {

        File path = Environment.getExternalStoragePublicDirectory(
                Environment.DIRECTORY_DOWNLOADS);
        file = new File(path, nameFileDRBX);

        String fileStr = file.toString();
        if (fileStr.contains(".")) {
            try {
                if (!path.exists()) {

                    if (!path.mkdirs()) {

                        mException = new RuntimeException("Unable to create directory: " + path);
                    }
                } else if (!path.isDirectory()) {

                    mException = new IllegalStateException("Download path is not a directory: " + path);
                    return null;
                }


                try (OutputStream outputStream = new FileOutputStream(file)) {
                    dbxClient.files().download(pathFileDRBX)
                            .download(outputStream);

                    message = "Фаил "+nameFileDRBX + " успешно загружен";


                    return null;
                }




            } catch (IOException e) {
                e.printStackTrace();
            } catch (DbxException e) {
                e.printStackTrace();
            }

        }
        else{

            message = "Невозможно скачать папку";
        }


        return null;
    }

    @Override
    protected void onPostExecute(Void aVoid) {
        super.onPostExecute(aVoid);





        progressDialog.hide();




        Toast.makeText(context, message, Toast.LENGTH_LONG).show();

        Intent intent = new Intent(Intent.ACTION_MEDIA_SCANNER_SCAN_FILE);
        intent.setData(Uri.fromFile(file));
        context.sendBroadcast(intent);

    }
}