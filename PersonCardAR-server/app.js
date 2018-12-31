const express = require('express');
const path = require('path');
const cookieParser = require('cookie-parser');
const logger = require('morgan');

// Funciones de la Base de Datos (MongoDB)
let database = require('./database/mongoDB');

const app = express();

app.use(logger('dev'));
app.use(express.json());
app.use(express.urlencoded({ extended: false }));
app.use(cookieParser());

// Cors
app.use(function(req, res, next) {
    res.header("Access-Control-Allow-Origin", "*");
    res.header("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
    next();
});

app.get('/', database.selectAllUsers);
app.get('/user/:name', database.selectUser);
app.get('/images/:name', database.selectImage);
app.get('/id/:id', database.selectID);
app.post('/', database.insertUser);
app.put('/user/:name', database.updateUser);
app.delete('/user/:name', database.deleteUser);

module.exports = app;
