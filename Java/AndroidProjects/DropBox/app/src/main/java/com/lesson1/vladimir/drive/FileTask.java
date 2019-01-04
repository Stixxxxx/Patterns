package com.lesson1.vladimir.drive;

import android.os.AsyncTask;

import com.dropbox.core.DbxException;
import com.dropbox.core.v2.DbxClientV2;
import com.dropbox.core.v2.files.ListFolderResult;
import com.dropbox.core.v2.users.FullAccount;

/**
 * Created by Vladimir on 22.04.2017.
 */

public class FileTask extends AsyncTask<Void, Void, ListFolderResult> {

    private DbxClientV2 dbxClient;
    private FileTask.TaskDelegate delegate;
    private Exception error;
    private String filePath = "";

    public interface TaskDelegate {

        void onFilesReceived(ListFolderResult result);
        void onError(Exception error);
    }

    FileTask(DbxClientV2 dbxClient, FileTask.TaskDelegate delegate, String filePath){
        this.dbxClient =dbxClient;
        this.delegate = delegate;
        this.filePath = filePath;
    }

    @Override
    protected ListFolderResult doInBackground(Void... params) {
        try {
            //get the users FullAccount
            return dbxClient.files().listFolder("" + filePath);
        } catch (DbxException e) {
            e.printStackTrace();
            error = e;
        }
        return null;
    }

    @Override
    protected void onPostExecute(ListFolderResult result) {
        super.onPostExecute(result);

        if (result != null && error == null){
            //User Account received successfully
            delegate.onFilesReceived(result);
        }
        else {
            // Something went wrong
            delegate.onError(error);
        }
    }
}
