package com.lesson1.vladimir.drive;

import android.content.Context;
import android.os.AsyncTask;
import android.widget.Toast;

import com.dropbox.core.DbxException;
import com.dropbox.core.v1.DbxClientV1;
import com.dropbox.core.v2.DbxClientV2;
import com.dropbox.core.v2.files.Metadata;
import com.dropbox.core.v2.users.FullAccount;

import java.net.URL;

public class DeleteTask extends AsyncTask<Void, Void, Void> {

    private DbxClientV2 dbxClient;
    private String path;
    private Context context;

    DeleteTask(DbxClientV2 dbxClient, String path, Context context){

        this.dbxClient = dbxClient;
        this.path = path;
        this.context = context;

    }


    @Override
    protected Void doInBackground(Void... params) {

        try {
            Metadata metadata = dbxClient.files().delete(path);
        } catch (DbxException e) {
            e.printStackTrace();
        }
        catch (Exception e){
            e.getMessage();

        }

        return null;
    }

    @Override
    protected void onPostExecute(Void aVoid) {
        super.onPostExecute(aVoid);

        Toast.makeText(context, "Удаление выполнено", Toast.LENGTH_LONG).show();

    }
}