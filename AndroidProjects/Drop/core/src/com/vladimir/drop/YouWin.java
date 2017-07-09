package com.vladimir.drop;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.Screen;
import com.badlogic.gdx.audio.Music;
import com.badlogic.gdx.graphics.GL20;
import com.badlogic.gdx.graphics.OrthographicCamera;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;
import com.badlogic.gdx.math.Rectangle;


public class YouWin implements Screen {

    final Drop game;
    OrthographicCamera camera;
    SpriteBatch batch;
    Texture youWinScreen;
    Rectangle youWinRectangle;
    Music ulybkaMusic;

    public YouWin(Drop game) {
        this.game = game;
        camera = new OrthographicCamera();
        camera.setToOrtho(false, 800, 480);


        youWinScreen =  new Texture("youwin.png");
        youWinRectangle = new Rectangle();
        youWinRectangle.x = 0;
        youWinRectangle.y = 0;
        youWinRectangle.width = 800;
        youWinRectangle.height = 480;


        ulybkaMusic =   Gdx.audio.newMusic(Gdx.files.internal("mamontenok.mp3"));
        ulybkaMusic.setLooping(true);

    }

    @Override
    public void show() {

        ulybkaMusic.play();

    }

    @Override
    public void render(float delta) {

        Gdx.app.log("MyTag", "1");
        Gdx.gl.glClearColor(0, 0, 2f, 1);
        Gdx.app.log("MyTag", "2");
        Gdx.gl.glClear(GL20.GL_COLOR_BUFFER_BIT);
        Gdx.app.log("MyTag", "3");

        camera.update();
        Gdx.app.log("MyTag", "4");
        game.batch.setProjectionMatrix(camera.combined);
        Gdx.app.log("MyTag", "5");
        game.batch.begin();
        Gdx.app.log("MyTag", "6");
        game.batch.draw(youWinScreen, youWinRectangle.x, youWinRectangle.y);

        game.batch.end();

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

        ulybkaMusic.dispose();
        youWinScreen.dispose();

    }
}
