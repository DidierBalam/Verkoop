$(document).ready(function () {
    Boton();

});

function Boton() {
    let lFormularioValido = false;

    $("#btnIniciarSesion").click(function (e) {

        e.preventDefault();

        lFormularioValido = ValidarInicioSesion();
        if (lFormularioValido === true) {

            IniciarSesion($("#cCorreo").val(), $("#cContrasenia").val());
            
        }
        else {
            Swal.fire("Alerta", "Verifique el formulario", "warning")
        }

    });

}

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
                required: function () { showValidate(cCorreo) }},
            cContrasenia: {
                required: function () { showValidate(cContrasenia) } }
        }
    });

    return oFormulario.valid();
}

function showValidate(input) {
    var thisAlert = $(input).parent();

    $(thisAlert).addClass('alert-validate');
}

function hideValidate(input) {
    var thisAlert = $(input).parent();

    $(thisAlert).removeClass('alert-validate');
}