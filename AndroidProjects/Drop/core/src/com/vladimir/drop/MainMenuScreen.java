package com.vladimir.drop;


import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.Screen;
import com.badlogic.gdx.graphics.Color;
import com.badlogic.gdx.graphics.GL20;
import com.badlogic.gdx.graphics.OrthographicCamera;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.math.Rectangle;
import com.badlogic.gdx.utils.TimeUtils;

import sun.rmi.runtime.Log;


/**
 * Created by Vladimir on 18.03.2017.
 */

public class MainMenuScreen implements Screen {

    final Drop game;
    OrthographicCamera camera;
    Texture backGround;
    Rectangle backgrondRectangle;

    public MainMenuScreen(final Drop gam) {
        game = gam;
        camera = new OrthographicCamera();
        camera.setToOrtho(false, 800, 480);

        backGround = new Texture("start.png");

        backgrondRectangle = new Rectangle();
        backgrondRectangle.x = 0;
        backgrondRectangle.y = 0;
        backgrondRectangle.width = 800;
        backgrondRectangle.height = 480;

    }

    @Override
    public void show() {

    }

    @Override
    public void render(float delta) {

        Gdx.gl.glClearColor(0, 0, 2f, 1);
        Gdx.gl.glClear(GL20.GL_COLOR_BUFFER_BIT);

        camera.update();

        game.batch.setProjectionMatrix(camera.combined);
        game.batch.begin();
        game.batch.draw(backGround, backgrondRectangle.x, backgrondRectangle.y);

        game.font.setColor(Color.GREEN);
        game.font.draw(game.batch, "Cautch all drops", 50, 100);
        game.font.draw(game.batch, "Touch The screen to start!", 50, 50);
        game.batch.end();

        if (Gdx.input.isTouched()) {
            game.setScreen(new GameScreen(game));
            dispose();

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


    }
}
