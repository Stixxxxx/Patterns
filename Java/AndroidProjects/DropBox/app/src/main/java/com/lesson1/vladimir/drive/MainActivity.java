package com.lesson1.vladimir.drive;

import android.Manifest;
import android.app.Activity;

import android.app.ProgressDialog;
import android.content.DialogInterface;
import android.content.Intent;

import android.content.pm.PackageManager;
import android.net.Uri;
import android.os.Handler;
import android.support.design.widget.FloatingActionButton;
import android.support.v4.app.ActivityCompat;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;

import android.util.Log;
import android.view.ContextMenu;
import android.view.LayoutInflater;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;

import android.widget.Button;

import android.widget.LinearLayout;
import android.widget.ListView;
import android.widget.SimpleAdapter;
import android.widget.TextView;
import android.widget.Toast;

import com.dropbox.core.DbxException;
import com.dropbox.core.v2.files.ListFolderResult;
import com.dropbox.core.v2.files.Metadata;

import com.dropbox.core.v2.users.FullAccount;
import com.google.android.gms.ads.AdRequest;
import com.google.android.gms.ads.AdView;
import com.google.android.gms.ads.MobileAds;


import java.io.File;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;


public class MainActivity extends AppCompatActivity {


    private static final String TAG = "MainActivity";

    private AdView mAdView;

    Button btnAuth;
    Button btnUp;
    Button btnLike;

    Intent intent;
    TextView tvToken;
    TextView tvFileFullPathFile;
    ListView lvFiles;
    int imgFolder;
    int imgFile;
    int imgImage;
    int imgVideo;
    int imgDoc;
    int imgPdf;
    String fileFullPathFile = "";
    LinearLayout llLLFL;

    ArrayList<String> filesArray;
    ArrayList<String> filesArrayName;

    final int REQUEST_CODE_TOKEN = 99;
    final int IMAGE_REQUEST_CODE = 1;

    final int MENU_DELETE = 1;
    final int MENU_DOWNLOAD = 2;

    final String ATTRIBUTE_NAME_TEXT = "text";
    final String ATTRIBUTE_NAME_IMAGE = "image";

    ProgressDialog progressDialog;

    private static String ACCESS_TOKEN;

    static int permissionStorage;
    private static final int REQUEST_EXTERNAL_STORAGE = 1;
    private static String[] PERMISSIONS_STORAGE = {
            Manifest.permission.READ_EXTERNAL_STORAGE,
            Manifest.permission.WRITE_EXTERNAL_STORAGE
    };

    boolean doubleBackToExitPressedOnce = false;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main_new);


        progressDialog = new ProgressDialog(this);

        llLLFL = (LinearLayout) findViewById(R.id.llLLFL);

        btnAuth = (Button) findViewById(R.id.btnAuth);
        btnAuth.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                intent = new Intent(getApplicationContext(), Auth.class);
                startActivityForResult(intent, REQUEST_CODE_TOKEN);


            }
        });



        btnUp = (Button) findViewById(R.id.btnUp);
        btnUp = (Button) findViewById(R.id.btnUp);
        btnUp.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                if (fileFullPathFile.contains("/")) {
                    String temp = fileFullPathFile.substring(0, fileFullPathFile.lastIndexOf("/"));
                    fileFullPathFile = temp;
                    getFiles();

                }


            }
        });


        btnLike = (Button) findViewById(R.id.btnEvaluate);
        btnLike.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                try {
                    Intent intent = new Intent(Intent.ACTION_VIEW);
                    intent.setData(Uri.parse("market://details?id=com.lesson1.vladimir.drive"));
                    startActivity(intent);

                } catch (Exception e) {
                    e.getMessage();
                    e.getMessage();
                    e.printStackTrace();
                    Toast.makeText(getApplicationContext(), "У вас не установлены гугл сервисы", Toast.LENGTH_LONG).show();
                }

            }
        });


        filesArray = new ArrayList<String>();
        filesArrayName = new ArrayList<String>();
        tvToken = (TextView) findViewById(R.id.tvToken);
        tvFileFullPathFile = (TextView) findViewById(R.id.tvFullPathFile);
        lvFiles = (ListView) findViewById(R.id.lvFiles);
        imgFolder = R.drawable.folder_adop;
        imgFile = R.drawable.file;
        imgImage = R.drawable.image;
        imgVideo = R.drawable.video;
        imgDoc = R.drawable.doc;
        imgPdf = R.drawable.pdf;


        FloatingActionButton fab = (FloatingActionButton) findViewById(R.id.fab);
        fab.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                upload(view);
            }
        });

        FloatingActionButton fabVideo = (FloatingActionButton) findViewById(R.id.fabVideo);
        fabVideo.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                upload(view);
            }
        });


        verifyStoragePermissions(this);

        registerForContextMenu(lvFiles);


        MobileAds.initialize(getApplicationContext(), "ca-app-pub-5213968678812167~1343922336");

        AdView mAdView = (AdView) findViewById(R.id.adView);
        AdRequest adRequest = new AdRequest.Builder()
//                .addTestDevice("5BE9972EE4DADEB838DBFA71526664BD")
                .build();
        mAdView.loadAd(adRequest);


    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        if (resultCode != RESULT_OK || data == null) return;

        switch (requestCode) {
            case REQUEST_CODE_TOKEN:
                ACCESS_TOKEN = data.getStringExtra("token");
                tvToken.setText("Подождите немного");
                getUserAccount();
                getFiles();
                break;
        }

        if (requestCode == IMAGE_REQUEST_CODE) {
            // Make sure the request was successful
            permissionStorage = ActivityCompat.checkSelfPermission(MainActivity.this, Manifest.permission.WRITE_EXTERNAL_STORAGE);
            if (permissionStorage == PackageManager.PERMISSION_GRANTED) {


                if (resultCode == RESULT_OK) {

                    //Image URI received
                    File file = new File(URI_to_Path.getPath(getApplication(), data.getData()));
                    if (file != null) {
                        //Initialize UploadTask
                        new UploadTask(DropboxClient.getClient(ACCESS_TOKEN), file, getApplicationContext(), fileFullPathFile).execute();
                        getFiles();
                    }


                }
            } else {
                Toast.makeText(MainActivity.this, "Вы не разрешили доступ к фаилам", Toast.LENGTH_LONG).show();

            }
        }


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

    private void upload(View view) {

        switch (view.getId()) {

            case R.id.fab:

                if (ACCESS_TOKEN == null) return;
                //Select image to upload
                Intent intent = new Intent();
                intent.setType("image/*");
                intent.setAction(Intent.ACTION_GET_CONTENT);
                intent.putExtra(Intent.EXTRA_LOCAL_ONLY, true);
                startActivityForResult(Intent.createChooser(intent, "Upload to Dropbox"), IMAGE_REQUEST_CODE);

                break;

            case R.id.fabVideo:

                if (ACCESS_TOKEN == null) return;
                //Select image to upload
                Intent intentVideo = new Intent();
                intentVideo.setType("video/*");
                intentVideo.setAction(Intent.ACTION_GET_CONTENT);
                intentVideo.putExtra(Intent.EXTRA_LOCAL_ONLY, true);
                startActivityForResult(Intent.createChooser(intentVideo, "Upload to Dropbox"), IMAGE_REQUEST_CODE);

                break;

        }


    }


    protected void getUserAccount() {
        if (ACCESS_TOKEN == null) return;
        new UserAccountTask(DropboxClient.getClient(ACCESS_TOKEN), new UserAccountTask.TaskDelegate() {
            @Override
            public void onAccountReceived(FullAccount account) {
                //Print account's info

                tvToken.setText(account.getName().getDisplayName() + " \n" + account.getEmail());
                Log.d("User", account.getEmail());
                Log.d("User", account.getName().getDisplayName());
                Log.d("User", account.getAccountType().name());

            }

            @Override
            public void onError(Exception error) {
                Log.d("User", "Error receiving account details.");
            }
        }).execute();


    }


    protected void getFiles() {
        if (ACCESS_TOKEN == null) return;

        new FileTask(DropboxClient.getClient(ACCESS_TOKEN), new FileTask.TaskDelegate() {


            @Override
            public void onFilesReceived(ListFolderResult result) {

                filesArray.clear();
                filesArrayName.clear();
                while (true) {
                    for (Metadata metadata : result.getEntries()) {
                        filesArray.add(metadata.getPathLower());
                        filesArrayName.add(metadata.getName());

                    }

//                    adapter = new ArrayAdapter<String>(getApplicationContext(), R.layout.item_listview, R.id.tv_item, filesArrayName);
//                    lvFiles.setAdapter(adapter);

                    ArrayList<Map<String, Object>> data = new ArrayList<Map<String, Object>>(
                            filesArrayName.size());
                    Map<String, Object> m;
                    for (int i = 0; i < filesArrayName.size(); i++) {
                        m = new HashMap<String, Object>();
                        m.put(ATTRIBUTE_NAME_TEXT, filesArrayName.get(i));

                        m.put(ATTRIBUTE_NAME_IMAGE, imgFolder);

                        if (filesArrayName.get(i).contains(".")) {
                            m.put(ATTRIBUTE_NAME_IMAGE, imgFile);
                        }

                        if (filesArrayName.get(i).contains(".jpg") || filesArrayName.get(i).contains(".png")) {
                            m.put(ATTRIBUTE_NAME_IMAGE, imgImage);
                        }


                        if (filesArrayName.get(i).contains(".mpg") || filesArrayName.get(i).contains(".avi") || filesArrayName.get(i).contains(".mp4")) {
                            m.put(ATTRIBUTE_NAME_IMAGE, imgVideo);

                        }

                        if (filesArrayName.get(i).contains(".doc") || filesArrayName.get(i).contains(".docx") || filesArrayName.get(i).contains(".txt")) {
                            m.put(ATTRIBUTE_NAME_IMAGE, imgDoc);

                        }

                        if (filesArrayName.get(i).contains(".pdf")) {
                            m.put(ATTRIBUTE_NAME_IMAGE, imgPdf);

                        }

                        data.add(m);
                    }

                    // массив имен атрибутов, из которых будут читаться данные
                    String[] from = {ATTRIBUTE_NAME_TEXT, ATTRIBUTE_NAME_IMAGE};
                    // массив ID View-компонентов, в которые будут вставлять данные
                    int[] to = {R.id.tv_item, R.id.ivFolder};

                    // создаем адаптер
                    SimpleAdapter sAdapter = new SimpleAdapter(getApplicationContext(), data, R.layout.item_listview,
                            from, to);

                    // определяем список и присваиваем ему адаптер

                    lvFiles.setAdapter(sAdapter);


                    lvFiles.setOnItemClickListener(new AdapterView.OnItemClickListener() {
                        public void onItemClick(AdapterView<?> parent, View view,
                                                int position, long id) {

                            filesArray.get(position);

                            fileFullPathFile = filesArray.get(position);

                            getFiles();

                            String a = String.valueOf(position);

                        }
                    });

                    if (!result.getHasMore()) {
                        break;
                    }

                }

//                MainActivity.llLLFL.setVisibility(LinearLayout.VISIBLE);

                LayoutInflater inflater = getLayoutInflater();
                View llLLFL = inflater.inflate(R.layout.activity_main_new, null);
                LinearLayout llLLFLinflate = (LinearLayout) findViewById(R.id.llLLFL);
                llLLFLinflate.setVisibility(LinearLayout.VISIBLE);

                tvFileFullPathFile.setText(fileFullPathFile);
            }

            @Override
            public void onError(Exception error) {
                Log.d("User", "Error receiving account details.");
            }
        }, fileFullPathFile).execute();

    }

    @Override
    public void onCreateContextMenu(ContextMenu menu, View v,
                                    ContextMenu.ContextMenuInfo menuInfo) {
        // TODO Auto-generated method stub

        switch (v.getId()) {
            case R.id.lvFiles:
                menu.add(0, MENU_DOWNLOAD, 0, "download");
                menu.add(0, MENU_DELETE, 0, "delete");
                break;

        }
    }

    @Override
    public boolean onContextItemSelected(MenuItem item) {
        AdapterView.AdapterContextMenuInfo info = (AdapterView.AdapterContextMenuInfo) item.getMenuInfo();

        long idItemListView = info.id;
        int sdf = info.position;
        int afadf = item.getItemId();
        int asdf = afadf;

        switch (item.getItemId()) {
            case MENU_DOWNLOAD:

                download(info.position);

                return true;

            case MENU_DELETE:

                delete(info.position);

                return true;


        }


        return super.onContextItemSelected(item);
    }


    private void download(int position) {

        String pathFileDRBX = filesArray.get(position);
        String nameFileDRBX = filesArrayName.get(position);

        DownloadTask downloadTask = new DownloadTask(DropboxClient.getClient(ACCESS_TOKEN), pathFileDRBX, nameFileDRBX, getApplicationContext(), progressDialog);
        downloadTask.execute();
        getFiles();


    }

    private void delete(int position) {

        final String path = filesArray.get(position);

        AlertDialog.Builder builder = new AlertDialog.Builder(MainActivity.this);
        builder.setTitle("Внимание!")
                .setMessage("Подтвердите удаление")
                .setIcon(R.drawable.ic_action_content_new)
                .setCancelable(true)
                .setPositiveButton("Удалить", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {

                        DeleteTask deleteTask = new DeleteTask(DropboxClient.getClient(ACCESS_TOKEN), path, getApplicationContext());
                        deleteTask.execute();
                        getFiles();


                    }
                })
                .setNegativeButton("Отмена",
                        new DialogInterface.OnClickListener() {
                            public void onClick(DialogInterface dialog, int id) {
                                dialog.cancel();
                            }
                        });

        AlertDialog alert = builder.create();
        alert.show();

    }


    @Override
    public void onBackPressed() {
        if (doubleBackToExitPressedOnce) {
            super.onBackPressed();
            return;
        }

        this.doubleBackToExitPressedOnce = true;
        Toast.makeText(this, "Для выхода нажимите кнопку НАЗАД еще раз", Toast.LENGTH_SHORT).show();

        new Handler().postDelayed(new Runnable() {

            @Override
            public void run() {
                doubleBackToExitPressedOnce = false;
            }
        }, 2000);
    }

}





