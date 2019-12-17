//INPUT PAÍS.
$(document).on('change', '#cPais', function () {

    let iIdPais = $(this, Selection).val();

    if (iIdPais != "") ObtenerEstadosPorPais(iIdPais);
});

//INPUT ESTADO.
$(document).on('change', '#cEstado', function () {

    let iIdEstado = $(this, Selection).val();

    if (iIdEstado != "") ObtenerMunicipiosPorEstado(iIdEstado);
});

//INPUT MUNICIPIO.
$(document).on('change', '#cMunicipio', function () {

    let iIdEstado = $(this, Selection).val();

    if (iIdEstado != "") $("#cContinuar").prop('disabled', false);
    else $("#cContinuar").prop('disabled', true);
});

//BOTÓN REGISTRAR DEL APARTADO CONTRASEÑA.
$(document).on('click', '#btnRegistar', function () {


    lFormularioValido = ValidarContrasenia();

    if (lFormularioValido === true) {

        let objDatosUsuario = ObtenerValoresDelUsuario();

        if (objDatosUsuario.cContrasenia == $("#confirmar").val()) {

            RegistrarUsuario(objDatosUsuario);

        }
        else {

            let objRespuesta={

                _bEstadoOperacion : false,
                _cMensaje : "¡Las contraseñas no coinciden!"
            };

            llamarSwetalert(objRespuesta);
        }

    }
    
});

//LINK YA TENGO CUENTA
$(document).on('click', '#linkInicioSesion', function () {

    window.location.href = "../Sesion/Index";
});

//BOTÓN SIGUIENTE
$(document).on('click', '#cContinuar', function () {

    lFormularioValido = ValidarDatosPersonales();

    if (lFormularioValido === true) {

        $("#cRegistro").css('display', 'none');
        $("#cContrasenia").css('display', 'inline');

    }
});

//BOTÓN ATRÁS.
$(document).on('click', '#cAtras', function () {

    $("#cRegistro").css('display', 'inline');
    $("#cContrasenia").css('display', 'none');

});



/**
 * MÉTODO PARA OBTENER LOS DATOS DEL USUARIO A REGISTRAR.
 * */
function ObtenerValoresDelUsuario() {

    var objUsuario = {

        cApellidoMaterno: $("#cApellidoMaterno").val(),
        cApellidoPaterno: $("#cApellidoPaterno").val(),
        cCorreo: $("#cCorreo").val(),
        cContrasenia: $("#cPassword").val(),
        cDireccion: $("#cDireccion").val(),
        cNombre: $("#cNombre").val(),
        iIdMunicipio: $("#cMunicipio", Selection).val()

    };

    return objUsuario;

}

$('#cNombre').on('input', function (e) {
    if (!/^[ a-záéíóúüñ-]*$/i.test(this.value)) {
        this.value = this.value.replace(/[^ a-záéíóúüñ-]+/ig, "");
    }
});

$('#cApellidoPaterno').on('input', function (e) {
    if (!/^[ a-záéíóúüñ-]*$/i.test(this.value)) {
        this.value = this.value.replace(/[^ a-záéíóúüñ-]+/ig, "");
    }
    
});

$('#cApellidoMaterno').on('input', function (e) {
    if (!/^[ a-záéíóúüñ-]*$/i.test(this.value)) {
        this.value = this.value.replace(/[^ a-záéíóúüñ-]+/ig, "");
    }
});



/**Función para validar los campos de iniciar sesión */
function ValidarDatosPersonales() {
    let oFormulario = $("#DatosPersonales");
    oFormulario.validate({
        errorClass: "is-invalid",
        validClass: "is-valid",

        rules: {
            Nombre: { required: true },
            ApellidoP: { required: true, },
            Correo: { required: true },
            Direccion: { required: true },
        },
        messages: {
            Nombre: {
                required: function () { showValidate(cCorreo) }
            },
            ApellidoP: {
                required: function () { showValidate(cContrasenia) }
            },
            Correo: {
                required: function () { showValidate(cContrasenia) }
            },
            Direccion: {
                required: function () { showValidate(cContrasenia) }
            }
        }
    });

    return oFormulario.valid();
}


/**Función para validar los campos de iniciar sesión */
function ValidarContrasenia() {

    let oFormulario = $("#tapContrasenia");

    oFormulario.validate({
       
        errorClass: "is-invalid",
        validClass: "is-valid",

        rules: {
            contrasenia: { required: true },
            confirmar: { required: true },
           
        },
        messages: {
            contrasenia: {
                required: function () { showValidate(cCorreo) }
            },
            confirmar: {
                required: function () { showValidate(cContrasenia) }
            }
        }
    });

    return oFormulario.valid();
}

/**Mensaje de validacion de formato de correo electrónico correcto */
jQuery.extend(jQuery.validator.messages, {

    email: "Formato de correo electrónico incorrecto."

});

/**
 * Función para mandar una alerta de la validación
 * @param {any} input Recibe el imput
 */
function showValidate(input) {
    var thisAlert = $(input).parent();

    $(thisAlert).addClass('alert-validate');
}

/**
 * Función para mandar una alerta de la validación
 * @param {any} input Recibe el imput
 */
function hideValidate(input) {
    var thisAlert = $(input).parent();

    $(thisAlert).removeClass('alert-validate');
}