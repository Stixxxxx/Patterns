package com.lesson1.vladimir.p1391_googlemaps;

import android.Manifest;
import android.annotation.TargetApi;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.os.Build;
import android.support.annotation.NonNull;
import android.support.v4.app.ActivityCompat;
import android.support.v4.app.FragmentActivity;
import android.os.Bundle;
import android.support.v4.content.ContextCompat;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import com.google.android.gms.maps.CameraUpdate;
import com.google.android.gms.maps.CameraUpdateFactory;
import com.google.android.gms.maps.GoogleMap;
import com.google.android.gms.maps.OnMapReadyCallback;
import com.google.android.gms.maps.SupportMapFragment;
import com.google.android.gms.maps.model.CameraPosition;
import com.google.android.gms.maps.model.LatLng;
import com.google.android.gms.maps.model.MarkerOptions;

public class MapsActivity extends FragmentActivity implements OnMapReadyCallback, View.OnClickListener, LocationListener {

    private static final String TAG = MapsActivity.class.getName();
    TextView tvInfo;

    GoogleMap mMap;
    Button btnMode;
    Button btnCompass;
    Button btnLocation;
    Button btnCamera;
    private static final int PERMISSION_REQUEST = 1;
    private static final int PERMISSION_REQUEST2 = 2;
    private static final int REQ_PERMISSION = 3;

    double latitudeCamera;
    double longtitudeCamera;

    double latitudeCameraClick;
    double longtitudeCameraClick;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_maps);


        btnMode = (Button) findViewById(R.id.btnMode);
        btnMode.setOnClickListener(this);

        btnCompass = (Button) findViewById(R.id.btnCompass);
        btnCompass.setOnClickListener(this);

        btnCamera = (Button) findViewById(R.id.btnCamera);
        btnCamera.setOnClickListener(this);

        tvInfo = (TextView) findViewById(R.id.tvInfo);

        btnLocation = (Button) findViewById(R.id.btnLocation);
        btnLocation.setOnClickListener(this);

        // Obtain the SupportMapFragment and get notified when the map is ready to be used.
        SupportMapFragment mapFragment = (SupportMapFragment) getSupportFragmentManager()
                .findFragmentById(R.id.map);
        mapFragment.getMapAsync(this);
    }


    /**
     * Manipulates the map once available.
     * This callback is triggered when the map is ready to be used.
     * This is where we can add markers or lines, add listeners or move the camera. In this case,
     * we just add a marker near Sydney, Australia.
     * If Google Play services is not installed on the device, the user will be prompted to install
     * it inside the SupportMapFragment. This method will only be triggered once the user has
     * installed Google Play services and returned to the app.
     */
    @Override
    public void onMapReady(GoogleMap googleMap) {
        mMap = googleMap;
        mMap.getUiSettings().setCompassEnabled(true);
        mMap.getUiSettings().setZoomControlsEnabled(true);
        mMap.getUiSettings().setTiltGesturesEnabled(true);


        if (checkPermission())
            mMap.setMyLocationEnabled(true);
        else askPermission();


        LatLng sydney = new LatLng(-34, 151);

        mMap.addMarker(new MarkerOptions().position(sydney).title("Marker in Sydney"));
        mMap.moveCamera(CameraUpdateFactory.newLatLng(sydney));


        mMap.setOnMapClickListener(new GoogleMap.OnMapClickListener() {

            @Override
            public void onMapClick(LatLng latLng) {
                Log.d(TAG, "onMapClick: " + latLng.latitude + "," + latLng.longitude);
                tvInfo.setText("LNG: " + latLng.latitude + "," + latLng.longitude);


                latitudeCameraClick = latLng.latitude;
                longtitudeCameraClick = latLng.longitude;

                LatLng coordClick = new LatLng(latitudeCameraClick, longtitudeCameraClick);
                mMap.moveCamera(CameraUpdateFactory.newLatLng(coordClick));


            }
        });

        mMap.setOnMapLongClickListener(new GoogleMap.OnMapLongClickListener() {

            @Override
            public void onMapLongClick(LatLng latLng) {
                Log.d(TAG, "onMapLongClick: " + latLng.latitude + "," + latLng.longitude);
            }
        });

        mMap.setOnCameraChangeListener(new GoogleMap.OnCameraChangeListener() {

            @Override
            public void onCameraChange(CameraPosition camera) {
                Log.d(TAG, "onCameraChange: " + camera.target.latitude + "," + camera.target.longitude);

                latitudeCamera = camera.target.latitude;
                longtitudeCamera = camera.target.longitude;
            }
        });


    }

    private boolean checkPermission() {
        Log.d(TAG, "checkPermission()");
        // Ask for permission if it wasn't granted yet
        return (ContextCompat.checkSelfPermission(this, Manifest.permission.ACCESS_FINE_LOCATION)
                == PackageManager.PERMISSION_GRANTED);


    }

    // Asks for permission
    private void askPermission() {
        Log.d(TAG, "askPermission()");
        ActivityCompat.requestPermissions(this,
                new String[]{Manifest.permission.ACCESS_FINE_LOCATION},
                REQ_PERMISSION
        );
    }

    @Override
    public void onRequestPermissionsResult(int requestCode, @NonNull String[] permissions, @NonNull int[] grantResults) {
        Log.d(TAG, "onRequestPermissionsResult()");
        super.onRequestPermissionsResult(requestCode, permissions, grantResults);
        switch (requestCode) {
            case REQ_PERMISSION: {
                if (grantResults.length > 0
                        && grantResults[0] == PackageManager.PERMISSION_GRANTED) {
                    // Permission granted
                    if (checkPermission())
                        mMap.setMyLocationEnabled(true);

                } else {
                    // Permission denied

                }
                break;
            }
        }

    }


    public void onClick(View view) {

        switch (view.getId()) {

            case R.id.btnMode:

                if (mMap.getMapType() == GoogleMap.MAP_TYPE_NORMAL) {
                    mMap.setMapType(GoogleMap.MAP_TYPE_SATELLITE);
                    Toast.makeText(this, "снимки со спутника", Toast.LENGTH_LONG).show();
                    return;
                }
                if (mMap.getMapType() == GoogleMap.MAP_TYPE_SATELLITE) {
                    mMap.setMapType(GoogleMap.MAP_TYPE_TERRAIN);
                    Toast.makeText(this, "карта рельефа местности", Toast.LENGTH_LONG).show();
                    return;

                }
                if (mMap.getMapType() == GoogleMap.MAP_TYPE_TERRAIN) {
                    mMap.setMapType(GoogleMap.MAP_TYPE_HYBRID);
                    Toast.makeText(this, "снимки со спутника + инфа о улицах и транспорте", Toast.LENGTH_LONG).show();
                    return;
                }

                if (mMap.getMapType() == GoogleMap.MAP_TYPE_HYBRID) {
                    mMap.setMapType(GoogleMap.MAP_TYPE_NORMAL);
                    Toast.makeText(this, "обычный режим", Toast.LENGTH_LONG).show();
                    return;
                }

                break;


            case R.id.btnCompass:


                if (mMap.getUiSettings().isCompassEnabled()) {
                    mMap.getUiSettings().setCompassEnabled(false);
                    Toast.makeText(this, "компас отключен", Toast.LENGTH_LONG).show();
                    return;
                }

                if (!mMap.getUiSettings().isCompassEnabled()) {
                    mMap.getUiSettings().setCompassEnabled(true);
                    Toast.makeText(this, "компас включен", Toast.LENGTH_LONG).show();
                    return;
                }

                break;

            case R.id.btnLocation:


                startActivity(new Intent(
                        android.provider.Settings.ACTION_LOCATION_SOURCE_SETTINGS));

                break;

            case R.id.btnCamera:

                CameraPosition cameraPosition = new CameraPosition.Builder()
                        .target(new LatLng(latitudeCamera, longtitudeCamera))
                        .zoom(45)
                        .bearing(45)
                        .tilt(45)
                        .build();
                CameraUpdate cameraUpdate = CameraUpdateFactory.newCameraPosition(cameraPosition);
                mMap.animateCamera(cameraUpdate);

                break;


        }
    }


    @Override
    public void onLocationChanged(Location location) {

        if (location != null) {
            Log.d(TAG, "Широта=" + location.getLatitude());
            Log.d(TAG, "Долгота=" + location.getLongitude());
        }


    }

    @Override
    public void onStatusChanged(String s, int i, Bundle bundle) {

    }

    @Override
    public void onProviderEnabled(String s) {

    }

    @Override
    public void onProviderDisabled(String s) {

    }
}
