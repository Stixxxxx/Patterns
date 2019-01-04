package com.lesson1.vladimir.myapplication;

import java.io.File;
import java.util.ArrayList;
import java.util.List;

import android.Manifest;
import android.app.Activity;
import android.app.AlertDialog;
import android.app.ListActivity;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.DialogInterface.OnClickListener;
import android.content.pm.PackageManager;
import android.net.Uri;
import android.os.Bundle;
import android.support.v4.app.ActivityCompat;
import android.support.v4.content.FileProvider;
import android.view.View;
import android.webkit.MimeTypeMap;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

public class MainActivity extends ListActivity {
    private List<String> directoryEntries = new ArrayList<String>();
    private File currentDirectory = new File("/");
    private static long back_pressed;

    static int permissionStorage;
    private static final int REQUEST_EXTERNAL_STORAGE = 1;
    private static String[] PERMISSIONS_STORAGE = {
            Manifest.permission.READ_EXTERNAL_STORAGE,
            Manifest.permission.WRITE_EXTERNAL_STORAGE
    };

    //when application started
    @Override
    public void onCreate(Bundle icicle) {
        super.onCreate(icicle);
        //set main layout
        setContentView(R.layout.activity_main);
        //browse to root directory
        browseTo(new File("/"));


        verifyStoragePermissions(this);

    }

    //browse to parent directory
    private void upOneLevel() {
        if (this.currentDirectory.getParent() != null) {
            this.browseTo(this.currentDirectory.getParentFile());
        }
    }

    //browse to file or directory
    private void browseTo(final File aDirectory) {


        //if we want to browse directory
        if (aDirectory.isDirectory()) {
            //fill list with files from this directory
            this.currentDirectory = aDirectory;
            File[] i = aDirectory.listFiles();
            fill(aDirectory.listFiles());

            //set titleManager text
            TextView titleManager = (TextView) findViewById(R.id.titleManager);
            titleManager.setText(aDirectory.getAbsolutePath());
        } else {
            //if we want to open file, show this dialog:
            //listener when YES button clicked
            OnClickListener okButtonListener = new OnClickListener() {
                public void onClick(DialogInterface arg0, int arg1) {
                    //intent to navigate file

                    String r = "file://" + aDirectory.getAbsolutePath();
                    try {
                        String extension = MimeTypeMap.getFileExtensionFromUrl(r);
                        String mType = MimeTypeMap.getSingleton().getMimeTypeFromExtension(extension.toLowerCase());
                        if (mType != null) {

                            Intent i = new Intent(android.content.Intent.ACTION_VIEW, Uri.parse("content://" + aDirectory.getAbsolutePath()));
                            //Uri photoURI = Uri.fromFile(createImageFile());
                            //FileProvider.getUriForFile(context, context.getApplicationContext().getPackageName() + ".provider", aDirectory);


                            i.setDataAndType(FileProvider.getUriForFile(MainActivity.this, MainActivity.this.getApplicationContext().getPackageName() + ".provider", aDirectory), mType);

                            //start this activity
                            startActivity(i);
                        }

                    else{

                        Toast.makeText(getApplication(), "Невозможно открыть файл", Toast.LENGTH_SHORT).show();

                    }
                } catch (Exception e) {

                        e.getStackTrace();
                        e.getMessage();
                }

                }
            };
            //listener when NO button clicked
            OnClickListener cancelButtonListener = new OnClickListener() {
                public void onClick(DialogInterface arg0, int arg1) {
                    //do nothing
                    //or add something you want
                }
            };

            //create dialog
            new AlertDialog.Builder(this)
                    .setTitle("Подтверждение") //title
                    .setMessage("Хотите открыть файл " + aDirectory.getName() + "?") //message
                    .setPositiveButton("Да", okButtonListener) //positive button
                    .setNegativeButton("Нет", cancelButtonListener) //negative button
                    .show(); //show dialog
        }
    }


    //fill list
    private void fill(File[] files) {
        //clear list
        this.directoryEntries.clear();

        String i = this.currentDirectory.getParent();

        if (this.currentDirectory.getParent() != null)
            this.directoryEntries.add("..");
        try {
            //add every file into list
            for (File file : files) {
                this.directoryEntries.add(file.getAbsolutePath());
            }
        } catch (Exception e) {

            String d = e.getMessage();
        }
        //create array adapter to show everything
        ArrayAdapter<String> directoryList = new ArrayAdapter<String>(this, R.layout.row, this.directoryEntries);
        this.setListAdapter(directoryList);


    }

    //when you clicked onto item
    @Override
    protected void onListItemClick(ListView l, View v, int position, long id) {
        //get selected file name
        int selectionRowID = position;
        String selectedFileString = this.directoryEntries.get(selectionRowID);

        //if we select ".." then go upper
        if (selectedFileString.equals("..")) {
            this.upOneLevel();
        } else {
            //browse to clicked file or directory using browseTo()
            File clickedFile = null;
            clickedFile = new File(selectedFileString);
            if (clickedFile != null)
                this.browseTo(clickedFile);
        }
    }

    @Override
    public void onBackPressed() {
        if (back_pressed + 2000 > System.currentTimeMillis())
            super.onBackPressed();
        else
            Toast.makeText(getBaseContext(), "Press once again to exit!",
                    Toast.LENGTH_SHORT).show();
        back_pressed = System.currentTimeMillis();
    }

    public static void verifyStoragePermissions(Activity activity) {
        // Check if we have write permission
        permissionStorage = ActivityCompat.checkSelfPermission(activity, Manifest.permission.WRITE_EXTERNAL_STORAGE);

        if (permissionStorage != PackageManager.PERMISSION_GRANTED) {
            // We don't have permission so prompt the user
            ActivityCompat.requestPermissions(
                    activity,
                    PERMISSIONS_STORAGE,
                    REQUEST_EXTERNAL_STORAGE
            );
        }
    }

}