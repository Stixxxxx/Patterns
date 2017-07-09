package com.vladimir.drop;

import com.badlogic.gdx.ApplicationAdapter;
import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.Input;
import com.badlogic.gdx.Screen;
import com.badlogic.gdx.audio.Music;
import com.badlogic.gdx.audio.Sound;
import com.badlogic.gdx.graphics.Color;
import com.badlogic.gdx.graphics.GL20;
import com.badlogic.gdx.graphics.OrthographicCamera;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;
import com.badlogic.gdx.math.MathUtils;
import com.badlogic.gdx.math.Vector3;
import com.badlogic.gdx.utils.Array;
import com.badlogic.gdx.utils.Logger;
import com.badlogic.gdx.utils.TimeUtils;

import com.badlogic.gdx.math.Rectangle;

import java.util.Iterator;

import sun.rmi.runtime.Log;

public class GameScreen implements Screen {

    final Drop game;
    OrthographicCamera camera;
    SpriteBatch batch;
    Texture dropImage;
    Texture bucketImage;
    Texture background;
    Sound dropSound;
    Sound thanderSound;
    Sound mimoSound;
    Music rainMusic;
    Music oblakaMusic;
    Rectangle bucket;
    Rectangle backgrondRectangle;
    Rectangle blackRectangle;
    Vector3 touchPos;
    Array<Rectangle> raindrops;
    long lastDropTime;
    public long timeNowAndThander;
    int catchDropCount;
    int dropsCatchered;
    int dropsCatNotchered;

    public GameScreen(final Drop gam) {

        this.game = gam;

        catchDropCount = 0;
        dropsCatNotchered =0;

        camera = new OrthographicCamera();
        camera.setToOrtho(false, 800, 480);

        batch = new SpriteBatch();

        touchPos = new Vector3();

        dropImage = new Texture("droplet.png");
        bucketImage = new Texture("bucket.png");
        background = new Texture("background.png");

        dropSound = Gdx.audio.newSound(Gdx.files.internal("waterdrop.wav"));
        mimoSound = Gdx.audio.newSound(Gdx.files.internal("mimo.wav"));
        thanderSound = Gdx.audio.newSound(Gdx.files.internal("thander.wav"));
        rainMusic = Gdx.audio.newMusic(Gdx.files.internal("undertreeinrain.mp3"));
        oblakaMusic = Gdx.audio.newMusic(Gdx.files.internal("oblaka.mp3"));

        timeNowAndThander = TimeUtils.millis();

        rainMusic.setLooping(true);


        oblakaMusic.setLooping(true);
        oblakaMusic.setVolume(0.4f);


        backgrondRectangle = new Rectangle();
        backgrondRectangle.x = 0;
        backgrondRectangle.y = 0;
        backgrondRectangle.width = 800;
        backgrondRectangle.height = 480;


        bucket = new Rectangle();
        bucket.x = 800 / 2 - 64 / 2;
        bucket.y = 20;
        bucket.width = 64;
        bucket.height = 48;


        raindrops = new Array<Rectangle>();
        spawnRaindrop();

    }


    private void spawnRaindrop() {

        Rectangle raindrop = new Rectangle();
        raindrop.x = MathUtils.random(0, 800 - 64);
        raindrop.y = 480;
        raindrop.width = 64;
        raindrop.height = 64;
        raindrops.add(raindrop);
        lastDropTime = TimeUtils.millis();


    }

    @Override
    public void render(float delta) {

        Gdx.gl.glClearColor(0, 0, 2f, 1);
        Gdx.gl.glClear(GL20.GL_COLOR_BUFFER_BIT);


        if (TimeUtils.millis() - timeNowAndThander > 25000) {
            thanderSound.play();
            timeNowAndThander = TimeUtils.millis();
        }

        camera.update();

        game.batch.setProjectionMatrix(camera.combined);
        game.batch.begin();


        game.batch.draw(background, backgrondRectangle.x, backgrondRectangle.y);


        game.batch.draw(bucketImage, bucket.x, bucket.y);
        for (Rectangle raindrop : raindrops) {
            game.batch.draw(dropImage, raindrop.x, raindrop.y);
        }

        game.font.setColor(Color.GREEN);
        game.font.draw(game.batch, "Cought drops: " + dropsCatchered, 40, 310);
        game.font.setColor(Color.RED);
        game.font.draw(game.batch, "Not cought drops: " + dropsCatNotchered, 40, 260);

        game.batch.end();

        if (dropsCatNotchered  == 3) {
            game.setScreen(new GameOver(game));
            dispose();

        }

            if (dropsCatchered  == 30) {
                game.setScreen(new YouWin(game));
                dispose();

            }
            Gdx.app.log("MyTag", "my informative message");




        if (Gdx.input.isTouched()) {
            touchPos.set(Gdx.input.getX(), Gdx.input.getY(), 0);
            camera.unproject(touchPos);
            bucket.x = (int) (touchPos.x - 64 / 2);
        }

        if (Gdx.input.isKeyPressed(Input.Keys.LEFT)) bucket.x -= 500 * Gdx.graphics.getDeltaTime();
        if (Gdx.input.isKeyPressed(Input.Keys.RIGHT)) bucket.x += 500 * Gdx.graphics.getDeltaTime();

        if (bucket.x < 0) bucket.x = 0;
        if (bucket.x > 800 - 64) bucket.x = 800 - 64;


        if (TimeUtils.millis() - lastDropTime > 3000 - (catchDropCount * 60)) spawnRaindrop();

        Iterator<Rectangle> iter = raindrops.iterator();
        while (iter.hasNext()) {
            Rectangle raindrop = iter.next();
            raindrop.y -= (100 + (catchDropCount * 10)) * (Gdx.graphics.getDeltaTime());
            if (raindrop.y + 64 < 0) {
                iter.remove();
                mimoSound.play();
                dropsCatNotchered++;
            }

            if (raindrop.overlaps(bucket)) {


                dropsCatchered++;
                dropSound.play();
                iter.remove();
                catchDropCount++;
            }

        }


    }

    @Override
    public void resize(int width, int height) {

    }

    @Override
    public void pause() {


    }

    @Override
    public void resume() {

    }

    @Override
    public void hide() {

    }

    @Override
    public void dispose() {
        dropImage.dispose();
        bucketImage.dispose();
        dropSound.dispose();
        rainMusic.dispose();
        batch.dispose();
        mimoSound.dispose();
        oblakaMusic.dispose();

    }

    @Override
    public void show() {

        rainMusic.play();
        oblakaMusic.play();
    }
}
