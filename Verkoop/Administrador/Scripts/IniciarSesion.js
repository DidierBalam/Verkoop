
$(document).ready(function () {
    Boton();

});

/**Función para iniciar sesión al hacer click en el botón */
function Boton() {
    let lFormularioValido = false;

    $("#btnIniciarSesion").click(function (e) {

        e.preventDefault();

        lFormularioValido = ValidarInicioSesion();
        if (lFormularioValido === true) {

            IniciarSesion($("#cCorreo").val(), $("#cContrasenia").val());

        }
       
    });

}

/**Función para validar los campos de iniciar sesión */
function ValidarInicioSesion() {
    let oFormulario = $("#FormInicioSesion");
    oFormulario.validate({
        errorClass: "is-invalid",
        validClass: "is-valid",

        rules: {
            cCorreo: { required: true },
            cContrasenia: { required: true }
        },
        messages: {
            cCorreo: {
                required: function () { showValidate(cCorreo) }
            },
            cContrasenia: {
                required: function () { showValidate(cContrasenia) }
            }
        }
    });

    return oFormulario.valid();
}

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
/**Mensaje de validacion de formato de correo electrónico correcto */
jQuery.extend(jQuery.validator.messages, {

    email: "Formato de correo lectrónico incorrecto."
    
});