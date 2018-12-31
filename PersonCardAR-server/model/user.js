// Librerias y dependencias
let mongoose = require('mongoose');

// Declaración del esquema
let UserSchema = new mongoose.Schema(
    {
        username:      { type: String, required: true, unique: true },  // Campo obligatório para insertar
        password:      { type: String, required: true },                // Campo obligatório para insertar

        name:          { type: String },
	    country:       { type: String },
        company:       { type: String },
        position:      { type: String },
        mail:          { type: String }
    }
);

// Exporta el modelo a la Base de Datos
module.exports = mongoose.model('User', UserSchema);
