// Librerias y dependencias
const mongoose = require('mongoose');
const nodemailer = require('nodemailer');

mongoose.connect('mongodb://localhost/PersonCard',{ useNewUrlParser: true }, function (err) {
    if(err) throw err;
    console.log('Connected to MongoDB');
});

// Declaración del esquema
let User = require('../model/user');

/***************************** FUNCIONES SOBRE USUARIOS *****************************/

// Devuelve una lista con todos los usuarios
exports.selectAllUsers = function (req, res) {
    User.find({}, { __v: false }).exec( function (err, users) {
        if (err) {
            console.log(err);
            return res.status(202).send({'result': 'ERROR'});  // Devuelve un JSON
        } else {
            return res.status(200).send(users);                // Devuelve un JSON
        }
    });
};

// Devuelve el usuario buscado
exports.selectUser = function (req, res) {
    User.findOne({ username: req.params.name }, { __v: false }).exec( function (err, user) {
        if(err){
            console.log(err);
            return res.status(202).send({'result': 'ERROR'});  // Devuelve un JSON
        }else{
            return res.status(200).send(user);                 // Devuelve un JSON
        }
    });
};

// Devuelve la imagen a partir del nombre del usuario
exports.selectImage = function (req, res) {
    User.find({}, { __v: false }).exec( function (err, users) {
        if (err) {
            console.log(err);
            return res.status(202).send({'result': 'ERROR'});  // Devuelve un JSON
        } else {
            for(let i = 0; i < users.length; i++){
                if(users[i].username === req.params.name){
                    return res.status(201).sendFile(__dirname + '/images/' + i + '.jpg');   // Devuelve una imagen
                }
            }
            return res.status(202).send({'result': 'Not Found'});	//Devuelve un JSON
        }
    });
};

// Devuelve el usuario en la posicion requerida
exports.selectID = function (req, res) {
    User.find({}, { __v : false }).exec( function (err, users) {
	    if(err){
	        console.log(err);
	        return res.status(202).send({'result': 'ERROR'});	//Devuelve un JSON
	    } else {
	        return res.status(200).send(users[req.params.id]);	//Devuelve un JSON
	    }
    });
};

// Inserta un nuevo usuario (username único)
exports.insertUser = function (req, res) {
    User(req.body).save(function (err) {
        if(err){
            console.log(err);
            return res.status(202).send({'result': 'ERROR'});     // Devuelve un JSON
        }else{
            let text = 'Please, download your Image Target to be readable for the rest of all users.\n\nmyhouselan.ddns.net:3000/images/' + req.body.username;
            // Mail options
            let transporter = nodemailer.createTransport( {service: 'Gmail', auth: {user: 'eetac.ea@gmail.com', pass: 'eetac123'}} );
            // Definimos el email
            let mailOptions = {
            	from: 'eetac.ea@gmail.com',
                to: req.body.mail,
                subject: 'Welcome to PersonCardAR!',
                text: text
            };
            // Send the mail
      	    transporter.sendMail(mailOptions, function (error, info) {
         	if (error) {
            	     console.log(error);
                     res.status(500).send(error.message);
                }
                return res.status(201).send({'result': 'INSERTADO'});
	    });
	}
    });
};

// Actualiza la información de un usuario
exports.updateUser = function (req, res) {
    User.update({ username: req.params.name }, req.body, function(err) {
        if (err) {
            console.log(err);
            return res.status(202).send({'result': 'ERROR'});       // Devuelve un JSON
        }else{
            return res.status(200).send({'result': 'ACTUALIZADO'}); // Devuelve un JSON
        }
    });
};

// Elimina de la Base de Datos el usuario buscado
exports.deleteUser = function (req, res) {
    User.remove({ username: req.params.name }, function(err) {
        if(err){
            console.log(err);
            return res.status(202).send({'result': 'ERROR'});     // Devuelve un JSON
        }else{
            return res.status(200).send({'result': 'ELIMINADO'}); // Devuelve un JSON
        }
    });
};
